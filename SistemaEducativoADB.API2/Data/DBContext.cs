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
        public DbSet<DetallePago> DetallePagos { get; set; }
        public DbSet<Periodo_Lectivo> Periodo_Lectivo { get; set; }
        public DbSet<Plan_Estudio> Plan_Estudio { get; set; }
        public DbSet<Requisito> Requisitos { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Pago> Pagos { get; set; }



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

                
                entity.HasOne(m => m.PlanEstudio)
                      .WithMany(p => p.Materias)
                      .HasForeignKey(m => m.IdPlan)
                      .OnDelete(DeleteBehavior.Restrict);
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
            
            entity.ToTable("DETALLE_MATRICULA");

            entity.HasKey(d => d.IdDetalle);


                entity.Property(d => d.IdDetalle).HasColumnName("id_detalle");
                entity.Property(d => d.IdMatricula).HasColumnName("id_matricula");
                entity.Property(d => d.IdGrupo).HasColumnName("id_grupo");
                entity.Property(d => d.Nota).HasColumnName("nota");
                entity.Property(d => d.Condicion).HasColumnName("condicion").HasMaxLength(50);

// Relación con Matricula
            entity.HasOne(d => d.Matricula)
                  .WithMany(m => m.Detalles) // usamos la colección de navegación en Matricula
                  .HasForeignKey(d => d.IdMatricula)
                  .OnDelete(DeleteBehavior.Restrict);

            // Relación con Grupo
            entity.HasOne(d => d.Grupo)
                  .WithMany(g => g.Detalles) // usamos la colección de navegación en Grupo
                  .HasForeignKey(d => d.IdGrupo)
                  .OnDelete(DeleteBehavior.Restrict);

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
                entity.ToTable("ASISTENCIA");

                entity.HasKey(a => a.IdAsistencia);

                entity.Property(a => a.IdAsistencia).HasColumnName("id_Asistencia");
                entity.Property(a => a.IdGrupo).HasColumnName("id_grupo");
                entity.Property(a => a.IdProfesor).HasColumnName("id_profesor");
                entity.Property(a => a.IdEstudiante).HasColumnName("id_estudiante");
                entity.Property(a => a.asistencia).HasColumnName("Asistencia");
                entity.Property(a => a.Fecha).HasColumnName("Fecha");

                entity.HasOne(a => a.Grupo)
                      .WithMany()
                      .HasForeignKey(a => a.IdGrupo);

                entity.HasOne(a => a.Profesor)
                      .WithMany()
                      .HasForeignKey(a => a.IdProfesor);

                entity.HasOne(a => a.Estudiante)
                      .WithMany()
                      .HasForeignKey(a => a.IdEstudiante);
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

            //DETALLE_PAGOS
            modelBuilder.Entity<DetallePago>(entity =>
            {
                entity.ToTable("DETALLE_PAGO");

                entity.HasKey(d => d.IdDetallePago);

                entity.Property(d => d.IdDetallePago)
                      .HasColumnName("id_detalle_pago");

                entity.Property(d => d.IdPago)
                      .HasColumnName("id_pago")
                      .IsRequired();

                entity.Property(d => d.IdMatricula)
                      .HasColumnName("id_matricula")
                      .IsRequired();

                entity.HasIndex(d => new { d.IdPago, d.IdMatricula })
                      .IsUnique();

                entity.HasOne(d => d.Pago)
                      .WithMany()
                      .HasForeignKey(d => d.IdPago)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Matricula)
                      .WithMany()
                      .HasForeignKey(d => d.IdMatricula)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            //PERIODO_LECTIVO
            modelBuilder.Entity<Periodo_Lectivo>(entity =>
            {
                entity.ToTable("PERIODO_LECTIVO");

                entity.HasKey(p => p.id_periodo);

                entity.Property(p => p.id_periodo)
                      .HasColumnName("id_periodo");

                entity.Property(p => p.anio)
                      .HasColumnName("anio");

                entity.Property(p => p.cuatrimestre)
                      .HasColumnName("cuatrimestre")
                      .HasMaxLength(10)
                      .IsRequired();

                entity.Property(p => p.fecha_inicio)
                      .HasColumnName("fecha_inicio")
                      .HasColumnType("date");

                entity.Property(p => p.fecha_fin)
                      .HasColumnName("fecha_fin")
                      .HasColumnType("date");
            });

            //PLAN_ESTUDIO
            modelBuilder.Entity<Plan_Estudio>(entity =>
            {
                entity.ToTable("PLAN_ESTUDIO");

                entity.HasKey(p => p.id_plan);

                entity.Property(p => p.id_plan)
                      .HasColumnName("id_plan");

                entity.Property(p => p.id_carrera)
                      .HasColumnName("id_carrera");

                entity.Property(p => p.anio_inicio)
                      .HasColumnName("anio_inicio");

                entity.HasOne(p => p.Carrera)
                      .WithMany()
                      .HasForeignKey(p => p.id_carrera)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            //REQUISITOS
            modelBuilder.Entity<Requisito>(entity =>
            {
                entity.ToTable("REQUISITOS");

                entity.HasKey(r => new { r.IdMateria, r.IdRequisito });

                entity.Property(r => r.IdMateria)
                      .HasColumnName("id_materia");

                entity.Property(r => r.IdRequisito)
                      .HasColumnName("id_requisito");

                entity.HasOne(r => r.Materia)
                      .WithMany()
                      .HasForeignKey(r => r.IdMateria)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.MateriaRequisito)
                      .WithMany()
                      .HasForeignKey(r => r.IdRequisito)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            //GRUPOS
            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.ToTable("GRUPOS");
                entity.HasKey(g => g.IdGrupo);

                entity.Property(g => g.IdGrupo).HasColumnName("id_grupo");
                entity.Property(g => g.IdMateria).HasColumnName("id_materia");
                entity.Property(g => g.IdProfesor).HasColumnName("id_profesor");
                entity.Property(g => g.GrupoNumero).HasColumnName("grupo_numero").HasMaxLength(10);
                entity.Property(g => g.Aula).HasColumnName("aula").HasMaxLength(50);
                entity.Property(g => g.CupoMax).HasColumnName("cupo_max");

                entity.HasOne<Materia>()
                .WithMany()
                .HasForeignKey(g => g.IdMateria)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Profesor>()
                      .WithMany()
                      .HasForeignKey(g => g.IdProfesor)
                      .OnDelete(DeleteBehavior.Restrict);

            });

            //HORARIOS
            modelBuilder.Entity<Horario>(entity =>
            {
                entity.ToTable("HORARIOS");
                entity.HasKey(h => h.IdHorario);

                entity.Property(h => h.IdHorario).HasColumnName("id_horario");
                entity.Property(h => h.IdGrupo).HasColumnName("id_grupo");
                entity.Property(h => h.DiaSemana).HasColumnName("dia_semana").HasMaxLength(20);
                entity.Property(h => h.HoraInicio).HasColumnName("hora_inicio").HasColumnType("time(0)");
                entity.Property(h => h.HoraFin).HasColumnName("hora_fin").HasColumnType("time(0)");

                // FK hacia Grupo
                entity.HasOne(h => h.Grupo)
                      .WithMany()
                      .HasForeignKey(h => h.IdGrupo)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            //MATRICULAS
            modelBuilder.Entity<Matricula>(entity =>
            {
                entity.ToTable("MATRICULAS");
                entity.HasKey(e => e.id_matricula);
                entity.Property(e => e.id_matricula).HasColumnName("id_matricula");
                entity.Property(e => e.id_estudiante).HasColumnName("id_estudiante");
                entity.Property(e => e.id_periodo).HasColumnName("id_periodo");
                entity.Property(e => e.estado).HasColumnName("estado").HasMaxLength(30);

                entity.HasIndex(e => new { e.id_estudiante, e.id_periodo })
                .IsUnique()
                .HasDatabaseName("UX_Matriculas_EstudiantePeriodo");

                // Relación con ESTUDIANTE
                entity.HasOne<Estudiante>()
                .WithMany()
                .HasForeignKey(e => e.id_estudiante)
                .HasConstraintName("FK_MATRICULAS_ESTUDIANTES")
                .OnDelete(DeleteBehavior.Restrict);
            });

            //PAGOS
            modelBuilder.Entity<Pago>(entity =>
            {
                entity.ToTable("PAGOS");
                entity.HasKey(p => p.IdPago);

                entity.Property(p => p.IdPago).HasColumnName("id_pago");
                entity.Property(p => p.IdEstudiante).HasColumnName("id_estudiante");
                entity.Property(p => p.Monto).HasColumnName("monto").HasColumnType("decimal(18,2)"); // o "money"
                entity.Property(p => p.Fecha).HasColumnName("fecha");
                entity.Property(p => p.Estado).HasColumnName("estado").HasMaxLength(30);
                entity.Property(p => p.MetodoPago).HasColumnName("metodo_pago").HasMaxLength(30);

                // FK opcional hacia Estudiante
                entity.HasOne(p => p.Estudiante)
                      .WithMany()
                      .HasForeignKey(p => p.IdEstudiante)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
