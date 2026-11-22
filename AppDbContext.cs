using Microsoft.EntityFrameworkCore;
using MedOne.Web.Models;
using MedOne.Web.Seguridad;

namespace MedOne.Web;

public class AppDbContext : DbContext
{
    private readonly ClinicaActual? _clinicaActual;

    public AppDbContext(DbContextOptions<AppDbContext> options, ClinicaActual? clinicaActual = null)
        : base(options)
    {
        _clinicaActual = clinicaActual;
    }

    // Clínica del usuario conectado (null si no tiene)
    public Guid? ClinicaIdActual => _clinicaActual?.Id;

    public DbSet<Clinica> Clinicas => Set<Clinica>();
    public DbSet<Especialidad> Especialidades => Set<Especialidad>();
    public DbSet<Profesional> Profesionales => Set<Profesional>();
    public DbSet<Paciente> Pacientes => Set<Paciente>();
    public DbSet<Cita> Citas => Set<Cita>();
    public DbSet<Ficha> Fichas => Set<Ficha>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Datos iniciales: las tres especialidades del MVP
        modelBuilder.Entity<Especialidad>().HasData(
            new Especialidad { Id = 1, Nombre = "Medicina General" },
            new Especialidad { Id = 2, Nombre = "Salud Mental" },
            new Especialidad { Id = 3, Nombre = "Kinesiología" }
        );

        modelBuilder.Entity<Usuario>(e =>
        {
            e.HasIndex(u => u.Auth0Id).IsUnique(); // una cuenta de Auth0 = un solo usuario
            e.Property(u => u.Rol).HasConversion<string>(); // guarda el rol como texto legible
        });

        // Filtro automático: cada clínica ve solo sus propios datos
        modelBuilder.Entity<Profesional>().HasQueryFilter(e => ClinicaIdActual != null && e.ClinicaId == ClinicaIdActual);
        modelBuilder.Entity<Paciente>().HasQueryFilter(e => ClinicaIdActual != null && e.ClinicaId == ClinicaIdActual);
        modelBuilder.Entity<Cita>().HasQueryFilter(e => ClinicaIdActual != null && e.ClinicaId == ClinicaIdActual);
        modelBuilder.Entity<Ficha>().HasQueryFilter(e => ClinicaIdActual != null && e.ClinicaId == ClinicaIdActual);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        AsignarClinica();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        AsignarClinica();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    // Al crear registros nuevos, les pone la clínica del usuario automáticamente
    private void AsignarClinica()
    {
        foreach (var entrada in ChangeTracker.Entries<IPerteneceAClinica>())
        {
            if (entrada.State == EntityState.Added && entrada.Entity.ClinicaId == Guid.Empty)
            {
                entrada.Entity.ClinicaId = ClinicaIdActual
                    ?? throw new InvalidOperationException("No hay una clínica activa para guardar este registro.");
            }
        }
    }
}