using System.Security.Claims;
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
    public DbSet<FichaVersion> FichaVersiones => Set<FichaVersion>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Invitacion> Invitaciones => Set<Invitacion>();
    public DbSet<EventoAuditoria> EventosAuditoria => Set<EventoAuditoria>();
    public DbSet<SolicitudContacto> SolicitudesContacto => Set<SolicitudContacto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Especialidades disponibles (los Id 1 a 3 se mantienen para no afectar datos existentes)
        modelBuilder.Entity<Especialidad>().HasData(
            new Especialidad { Id = 1, Nombre = "Médico" },
            new Especialidad { Id = 2, Nombre = "Psicólogo" },
            new Especialidad { Id = 3, Nombre = "Kinesiólogo" },
            new Especialidad { Id = 4, Nombre = "Fonoaudiólogo" },
            new Especialidad { Id = 5, Nombre = "Terapeuta Ocupacional" },
            new Especialidad { Id = 6, Nombre = "Neuropsicólogo" }
        );

        modelBuilder.Entity<Usuario>(e =>
        {
            e.HasIndex(u => u.Auth0Id).IsUnique(); // una cuenta de Auth0 = un solo usuario
            e.Property(u => u.Rol).HasConversion<string>(); // guarda el rol como texto legible
        });

        modelBuilder.Entity<Invitacion>(e =>
        {
            e.HasIndex(i => i.Email); // búsqueda rápida al iniciar sesión
            e.Property(i => i.Rol).HasConversion<string>();
        });

        modelBuilder.Entity<EventoAuditoria>(e =>
        {
            e.HasIndex(a => new { a.ClinicaId, a.Fecha });
            e.HasIndex(a => a.PacienteId);
        });

        modelBuilder.Entity<Ficha>(e =>
        {
            e.HasIndex(f => f.CitaId).IsUnique(); // una ficha por cita

            // Si se intenta borrar una cita, paciente o profesional con fichas, la base lo impide
            e.HasOne(f => f.Cita).WithMany().HasForeignKey(f => f.CitaId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(f => f.Paciente).WithMany().HasForeignKey(f => f.PacienteId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(f => f.Profesional).WithMany().HasForeignKey(f => f.ProfesionalId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<FichaVersion>(e =>
        {
            e.HasIndex(v => v.FichaId);
        });

        // Mensajes del formulario de contacto de la portada
        modelBuilder.Entity<SolicitudContacto>(e =>
        {
            e.Property(s => s.Nombre).HasMaxLength(120);
            e.Property(s => s.Email).HasMaxLength(160);
            e.Property(s => s.Telefono).HasMaxLength(40);
            e.Property(s => s.TipoCentro).HasMaxLength(40);
            e.Property(s => s.Mensaje).HasMaxLength(2000);
            e.HasIndex(s => s.Fecha);
        });

        // Filtro automático: cada clínica ve solo sus propios datos
        modelBuilder.Entity<Profesional>().HasQueryFilter(e => ClinicaIdActual != null && e.ClinicaId == ClinicaIdActual);
        modelBuilder.Entity<Paciente>().HasQueryFilter(e => ClinicaIdActual != null && e.ClinicaId == ClinicaIdActual);
        modelBuilder.Entity<Cita>().HasQueryFilter(e => ClinicaIdActual != null && e.ClinicaId == ClinicaIdActual);
        modelBuilder.Entity<Ficha>().HasQueryFilter(e => ClinicaIdActual != null && e.ClinicaId == ClinicaIdActual);
        modelBuilder.Entity<FichaVersion>().HasQueryFilter(e => ClinicaIdActual != null && e.ClinicaId == ClinicaIdActual);
        modelBuilder.Entity<Invitacion>().HasQueryFilter(e => ClinicaIdActual != null && e.ClinicaId == ClinicaIdActual);
        modelBuilder.Entity<EventoAuditoria>().HasQueryFilter(e => ClinicaIdActual != null && e.ClinicaId == ClinicaIdActual);
    }

    // Agrega un evento de auditoría (se guarda junto con el resto de los cambios)
    public void RegistrarAuditoria(ClaimsPrincipal actor, string tipo, string detalle,
        Guid? pacienteId = null, Guid? fichaId = null)
    {
        EventosAuditoria.Add(new EventoAuditoria
        {
            UsuarioId = Guid.TryParse(actor.FindFirst("usuario_id")?.Value, out var id) ? id : null,
            UsuarioNombre = actor.Identity?.Name ?? string.Empty,
            Tipo = tipo,
            Detalle = detalle,
            PacienteId = pacienteId,
            FichaId = fichaId
        });
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ValidarAntesDeGuardar();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        ValidarAntesDeGuardar();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void ValidarAntesDeGuardar()
    {
        // La auditoría solo se agrega, nunca se modifica ni se borra
        foreach (var entrada in ChangeTracker.Entries<EventoAuditoria>())
        {
            if (entrada.State is EntityState.Modified or EntityState.Deleted)
                throw new InvalidOperationException("El registro de auditoría no se puede modificar ni eliminar.");
        }

        // Las versiones anteriores de fichas tampoco
        foreach (var entrada in ChangeTracker.Entries<FichaVersion>())
        {
            if (entrada.State is EntityState.Modified or EntityState.Deleted)
                throw new InvalidOperationException("El historial de fichas no se puede modificar ni eliminar.");
        }

        // Las fichas clínicas no se eliminan
        foreach (var entrada in ChangeTracker.Entries<Ficha>())
        {
            if (entrada.State == EntityState.Deleted)
                throw new InvalidOperationException("Las fichas clínicas no se pueden eliminar.");
        }

        // Al crear registros nuevos, les pone la clínica del usuario automáticamente
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
