using ApiControlDeColegio.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiControlDeColegio.DbContexts
{
    public class DbContextApi : DbContext
    {
        public DbSet<Clase> Clases {get; set;}
        public DbSet<Alumno> Alumnos {get; set;}
        public DbSet<Carrera> Carreras {get; set;}
        public DbSet<Horario> Horarios {get; set;}
        public DbSet<Salon> Salones {get; set;}
        public DbSet<Seminario> Seminarios {get; set;}
        public DbSet<Instructor> Instructores {get; set;}
        public DbSet<Modulo> Modulos {get; set;}
        public DbSet<DetalleActividad> DetallesActividad {get; set;}
        public DbSet<DetalleNota> DetalleNotas {get; set;}
        public DbSet<AsignacionAlumno> AsignacionAlumnos {get; set;} 
        public DbContextApi(DbContextOptions<DbContextApi> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Alumno>()
                .ToTable("Alumnos")
                .HasKey(a => a.Carne);
            modelBuilder.Entity<Carrera>()
                .ToTable("CarrerasTecnicas")
                .HasKey(a => new {a.CarreraId});
            modelBuilder.Entity<Instructor>()
                .ToTable("Instructores")
                .HasKey(a => new {a.InstructorId});
            modelBuilder.Entity<Horario>()
                .ToTable("Horarios")
                .HasKey(a => new {a.HorarioId});
            modelBuilder.Entity<Salon>()
                .ToTable("Salones")
                .HasKey(a => new {a.SalonId});
            modelBuilder.Entity<Seminario>()
                .ToTable("Seminarios")
                .HasKey(a => a.SeminarioId);
            modelBuilder.Entity<Modulo>()
                .ToTable("Modulos")
                .HasKey(a => a.ModuloId);
            modelBuilder.Entity<Clase>()
                .ToTable("Clases")
                .HasKey(c => c.ClaseId);
            modelBuilder.Entity<AsignacionAlumno>()
                .ToTable("AsignacionAlumnos")
                .HasKey(aa => aa.AsignacionId);
            modelBuilder.Entity<AsignacionAlumno>()
                .HasOne<Alumno>(x => x.Alumno)
                .WithMany(x => x.Asignaciones)
                .HasForeignKey(x => x.Carne);
            modelBuilder.Entity<DetalleActividad>()
                .ToTable("DetalleActividades")
                .HasKey(a => a.DetalleActividadId);
            modelBuilder.Entity<DetalleNota>()
                .ToTable("DetalleNotas")
                .HasKey(a => a.DetalleNotaId);
        }
    }
}