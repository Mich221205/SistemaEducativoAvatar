using Microsoft.EntityFrameworkCore;
using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        // DbSets (Tablas)
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Detalle_Matricula> Detalle_Matriculas { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<Correquisito> Correquisitos { get; set; }
        public DbSet<Cita_Matricula> Cita_Matriculas { get; set; }
        public DbSet<Bitacora> Bitacoras { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de ROLES
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("ROLES");

                entity.HasKey(r => r.IdRol);

                entity.Property(r => r.IdRol)
                      .HasColumnName("id_rol");

                entity.Property(r => r.NombreRol)
                      .HasColumnName("nombre_rol")
                      .HasMaxLength(100)
                      .IsRequired();
            });

            // Configuración de ESTUDIANTES
            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.ToTable("ESTUDIANTES");

                entity.HasKey(e => e.IdEstudiante);

                entity.Property(e => e.IdEstudiante)
                      .HasColumnName("id_estudiante");

                entity.Property(e => e.IdUsuario)
                      .HasColumnName("id_usuario");

                entity.Property(e => e.Carnet)
                      .HasColumnName("carnet")
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.Telefono)
                      .HasColumnName("telefono")
                      .HasMaxLength(20);

                entity.Property(e => e.Direccion)
                      .HasColumnName("direccion")
                      .HasMaxLength(200);

                entity.Property(e => e.IdCarrera)
                      .HasColumnName("id_carrera");

                // Relación 1:1 con Usuario
                entity.HasOne(e => e.Usuario)
                      .WithOne(u => u.Estudiante)
                      .HasForeignKey<Estudiante>(e => e.IdUsuario)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de PROFESORES
            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.ToTable("PROFESORES");

                entity.HasKey(p => p.IdProfesor);

                entity.Property(p => p.IdProfesor)
                      .HasColumnName("id_profesor");

                entity.Property(p => p.IdUsuario)
                      .HasColumnName("id_usuario");

                entity.Property(p => p.Cedula)
                      .HasColumnName("cedula")
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(p => p.Telefono)
                      .HasColumnName("telefono")
                      .HasMaxLength(20);

                entity.Property(p => p.CorreoPersonal)
                      .HasColumnName("correo_personal")
                      .HasMaxLength(100);

                entity.HasOne(p => p.Usuario)
                      .WithOne(u => u.Profesor)
                      .HasForeignKey<Profesor>(p => p.IdUsuario)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            // Configuración de MATERIAS
            modelBuilder.Entity<Materia>(entity =>
            {
                entity.ToTable("MATERIAS");

                entity.HasKey(m => m.IdMateria);

                entity.Property(m => m.IdMateria)
                      .HasColumnName("id_materia");

                entity.Property(m => m.Codigo)
                      .HasColumnName("codigo")
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(m => m.Nombre)
                      .HasColumnName("nombre")
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(m => m.Creditos)
                      .HasColumnName("creditos")
                      .IsRequired();

                entity.Property(m => m.IdPlan)
                      .HasColumnName("id_plan");

                // Relación con PLAN_ESTUDIO (si hay entidad)
                // Si tienes un modelo PlanEstudio, activa esto:
                // entity.HasOne(m => m.PlanEstudio)
                //       .WithMany(p => p.Materias)
                //       .HasForeignKey(m => m.IdPlan)
                //       .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de CARRERAS
            modelBuilder.Entity<Carrera>(entity =>
            {
                entity.ToTable("CARRERAS");

                entity.HasKey(c => c.IdCarrera);

                entity.Property(c => c.IdCarrera)
                      .HasColumnName("id_carrera");

                entity.Property(c => c.NombreCarrera)
                      .HasColumnName("nombre_carrera")
                      .HasMaxLength(100)
                      .IsRequired();
            });

            // Configuración de USUARIOS
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("USUARIOS");
                entity.HasKey(u => u.IdUsuario);

                entity.Property(u => u.IdUsuario).HasColumnName("id_usuario");
                entity.Property(u => u.nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
                entity.Property(u => u.email).HasColumnName("email").HasMaxLength(150).IsRequired();
                entity.Property(u => u.contrasena).HasColumnName("contrasena").HasMaxLength(200).IsRequired();
                entity.Property(u => u.Estado).HasColumnName("estado").HasDefaultValue(true);
                entity.Property(u => u.FechaCreacion).HasColumnName("fecha_creacion").HasDefaultValueSql("GETDATE()");
                entity.Property(u => u.IdRol).HasColumnName("id_rol");

                // Relación Usuario - Rol
                entity.HasOne(u => u.Rol)
                      .WithMany(r => r.Usuarios)
                      .HasForeignKey(u => u.IdRol)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            //correquisitos
            modelBuilder.Entity<Correquisito>(entity =>
            {
                entity.ToTable("CORREQUISITOS");
                entity.HasKey(c => new { c.IdMateria, c.IdCorrequisito });

                entity.Property(c => c.IdMateria).HasColumnName("id_materia");
                entity.Property(c => c.IdCorrequisito).HasColumnName("id_correquisito");

                entity.HasOne(c => c.Materia)
                      .WithMany()
                      .HasForeignKey(c => c.IdMateria)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.CorrequisitoMateria)
                      .WithMany()
                      .HasForeignKey(c => c.IdCorrequisito)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            //detalle matricula
            modelBuilder.Entity<Detalle_Matricula>(entity =>
            {
                entity.ToTable("DETALLES_MATRICULA");
                entity.HasKey(d => d.IdDetalle);
                entity.Property(d => d.IdDetalle).HasColumnName("id_detalle");
                entity.Property(d => d.IdMatricula).HasColumnName("id_matricula");
                entity.Property(d => d.IdGrupo).HasColumnName("id_grupo");
                entity.Property(d => d.Nota).HasColumnName("nota");
                entity.Property(d => d.Condicion).HasColumnName("condicion").HasMaxLength(50);

                //entity.HasOne(d => d.m)
                //      .WithMany()
                //      .HasForeignKey(d => d.IdMatricula)
                //      .OnDelete(DeleteBehavior.Restrict);

                //entity.HasOne(d => d.Grupo)
                //      .WithMany()
                //      .HasForeignKey(d => d.IdGrupo)
                //      .OnDelete(DeleteBehavior.Restrict);
            });
            //bitacora
            modelBuilder.Entity<Bitacora>(entity =>
            {
                entity.ToTable("BITACORAS");
                entity.HasKey(b => b.IdLog);
                entity.Property(b => b.IdLog).HasColumnName("id_log");
                entity.Property(b => b.IdUsuario).HasColumnName("id_usuario");
                entity.Property(b => b.Accion).HasColumnName("accion").IsRequired();
                entity.Property(b => b.FechaHora).HasColumnName("fecha_hora").IsRequired();
                entity.Property(b => b.Ip).HasColumnName("ip");

                entity.HasOne(b => b.Usuario)
                      .WithMany()
                      .HasForeignKey(b => b.IdUsuario)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            //asistencia
            modelBuilder.Entity<Asistencia>(entity =>
            {
                entity.ToTable("ASISTENCIAS");
                entity.HasKey(a => a.IdAsistencia);
                entity.Property(a => a.IdAsistencia).HasColumnName("id_asistencia");
                entity.Property(a => a.IdGrupo).HasColumnName("id_grupo");
                entity.Property(a => a.IdProfesor).HasColumnName("id_profesor");
                entity.Property(a => a.IdEstudiante).HasColumnName("id_estudiante");
                entity.Property(a => a.eAsistencia).HasColumnName("e_asistencia");
                entity.Property(a => a.Fecha).HasColumnName("fecha");

                //entity.HasOne(a => a.Grupo)
                //      .WithMany()
                //      .HasForeignKey(a => a.IdGrupo)
                //      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Profesor)
                      .WithMany()
                      .HasForeignKey(a => a.IdProfesor)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Estudiante)
                      .WithMany()
                      .HasForeignKey(a => a.IdEstudiante)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            //cita matricula
            modelBuilder.Entity<Cita_Matricula>(entity =>
            {
                entity.ToTable("CITAS_MATRICULA");
                entity.HasKey(c => c.IdCita);
                entity.Property(c => c.IdCita).HasColumnName("id_cita");
                entity.Property(c => c.IdEstudiante).HasColumnName("id_estudiante");
                entity.Property(c => c.IdPeriodo).HasColumnName("id_periodo");
                entity.Property(c => c.FechaHora).HasColumnName("fecha_hora").IsRequired();

                entity.HasOne(c => c.Estudiante)
                      .WithMany()
                      .HasForeignKey(c => c.IdEstudiante)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
