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
                .HasKey(a => new {a.Carne});
            modelBuilder.Entity<Carrera>()
                .ToTable("CarrerasTecnicas")
                .HasKey(ct => new {ct.CarreraId});
            modelBuilder.Entity<Instructor>()
                .ToTable("Instructores")
                .HasKey(i => new {i.InstructorId});
            modelBuilder.Entity<Horario>()
                .ToTable("Horarios")
                .HasKey(h => new {h.HorarioId});
            modelBuilder.Entity<Salon>()
                .ToTable("Salones")
                .HasKey(s => new {s.SalonId});
            modelBuilder.Entity<Clase>()
                .ToTable("Clases")
                .HasKey(c => new {c.ClaseId});
            modelBuilder.Entity<Clase>()
                .HasOne<Carrera>(c => c.Carrera)
                .WithMany(cc => cc.Clases)
                .HasForeignKey(c => c.CarreraId);
            modelBuilder.Entity<Clase>()
                .HasOne<Horario>(h => h.Horario)
                .WithMany(c => c.Clases)
                .HasForeignKey(c => c.HorarioId);
            modelBuilder.Entity<Clase>()
                .HasOne<Instructor>(i => i.Instructor)
                .WithMany(c => c.Clases)
                .HasForeignKey(c => c.InstructorId);
            modelBuilder.Entity<Clase>()
                .HasOne<Salon>(s => s.Salon)
                .WithMany(c => c.Clases)
                .HasForeignKey(c => c.SalonId);
            modelBuilder.Entity<AsignacionAlumno>()
                .ToTable("AsignacionAlumnos")
                .HasKey(aa => aa.AsignacionId);
            modelBuilder.Entity<AsignacionAlumno>()
                .HasOne<Alumno>(aa => aa.Alumno)
                .WithMany(a => a.Asignaciones)
                .HasForeignKey(a => a.Carne);
            modelBuilder.Entity<Seminario>()
                .ToTable("Seminarios")
                .HasKey(se => new {se.SeminarioId});
            modelBuilder.Entity<Seminario>()
                .HasOne<Modulo>(m => m.Modulo)
                .WithMany(s => s.Seminario)
                .HasForeignKey(s => s.ModuloId);
            modelBuilder.Entity<Modulo>()
                .ToTable("Modulos")
                .HasKey(m => new {m.ModuloId});
            modelBuilder.Entity<Modulo>()
                .HasOne<Carrera>(m => m.Carrera)
                .WithMany(ct => ct.Modulos)
                .HasForeignKey(ct => ct.CarreraId);
            modelBuilder.Entity<DetalleActividad>()
                .ToTable("DetalleActividades")
                .HasKey(d => d.DetalleActividadId);
            modelBuilder.Entity<DetalleActividad>()
                .HasOne<Seminario>(s => s.Seminario)
                .WithMany(dt => dt.DetalleActividades)
                .HasForeignKey(dt => dt.SeminarioId);
            modelBuilder.Entity<DetalleNota>()
                .ToTable("DetalleNotas")
                .HasKey(dn => dn.DetalleNotaId);
            modelBuilder.Entity<DetalleNota>()
                .HasOne<DetalleActividad>(da => da.DetalleActividad)
                .WithMany(dt => dt.DetalleNota)
                .HasForeignKey(dt => dt.DetalleActividadId);
            modelBuilder.Entity<DetalleNota>()
                .HasOne<Alumno>(a => a.Alumno)
                .WithMany(dt => dt.DetalleNotas)
                .HasForeignKey(dt => dt.Carne);
        }
    }
}