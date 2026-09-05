using Microsoft.EntityFrameworkCore;
using MedOne.Web.Models;

namespace MedOne.Web;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Clinica> Clinicas => Set<Clinica>();
    public DbSet<Especialidad> Especialidades => Set<Especialidad>();
    public DbSet<Profesional> Profesionales => Set<Profesional>();
    public DbSet<Paciente> Pacientes => Set<Paciente>();
    public DbSet<Cita> Citas => Set<Cita>();
    public DbSet<Ficha> Fichas => Set<Ficha>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Datos iniciales: las tres especialidades del MVP
        modelBuilder.Entity<Especialidad>().HasData(
            new Especialidad { Id = 1, Nombre = "Medicina General" },
            new Especialidad { Id = 2, Nombre = "Salud Mental" },
            new Especialidad { Id = 3, Nombre = "Kinesiología" }
        );
    }
}