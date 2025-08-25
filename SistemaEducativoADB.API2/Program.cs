using Microsoft.EntityFrameworkCore;
using SistemaEducativoADB.API2.Data;
using SistemaEducativoADB.API2.Repositories.Implementatios;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services;
using SistemaEducativoADB.API2.Services.Implementations;
using SistemaEducativoADB.API2.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IEstudianteRepository, EstudianteRepository>();
builder.Services.AddScoped<ICarreraRepository, CarreraRepository>();
builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();
builder.Services.AddScoped<IProfesorRepository, ProfesorRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAsistenciaRepository, AsistenciaRepository>();
builder.Services.AddScoped<IBitacoraRepository, BitacoraRepository>();
builder.Services.AddScoped<ICita_MatriculaRepository, Cita_MatriculaRepository>();
builder.Services.AddScoped<ICorrequisitoRepository, CorrequisitoRepository>();
builder.Services.AddScoped<IDetalle_MatriculaRepository, Detalle_MatriculaRepository>();
builder.Services.AddScoped<IDetalle_PagosRepository, Detalle_PagosRepository>();
builder.Services.AddScoped<IPeriodo_LectivoRepository, Periodo_LectivoRepository>();
builder.Services.AddScoped<IPlan_EstudioRepository, Plan_EstudioRepository>();
builder.Services.AddScoped<IRequisitosRepository, RequisitosRepository>();
builder.Services.AddScoped<IRolesRepository, RolesRepository>();
builder.Services.AddScoped<IGruposRepository, GruposRepository>();
builder.Services.AddScoped<IHorariosRepository, HorariosRepository>();
builder.Services.AddScoped<IMatriculaRepository, MatriculaRepository>();
builder.Services.AddScoped<IPagosRepository, PagosRepository>();

// Services
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<ICarreraService, CarreraService>();
builder.Services.AddScoped<IMateriaService, MateriaService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAsistenciaService, AsistenciaService>();
builder.Services.AddScoped<IBitacoraService, BitacoraService>();
builder.Services.AddScoped<ICita_MatriculaService, Cita_MatriculaService>();
builder.Services.AddScoped<ICorrequisitoService, CorrequisitoService>();
builder.Services.AddScoped<IDetalle_MatriculaService, Detalle_MatriculaService>();
builder.Services.AddScoped<IDetalle_PagosService, Detalle_PagosService>();
builder.Services.AddScoped<IPeriodo_LectivoService, Periodo_LectivoService>();
builder.Services.AddScoped<IPlan_EstudioService, Plan_EstudioService>();
builder.Services.AddScoped<IRequisitosService, RequisitosService>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<IGruposService, GruposService>();
builder.Services.AddScoped<IHorariosService, HorariosService>();
builder.Services.AddScoped<IMatriculaService, MatriculaService>();
builder.Services.AddScoped<IPagosService, PagosService>();

// Configuración CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
