// API2/Controllers/MatriculasController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEducativoADB.API2.Data;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatriculasController : ControllerBase
    {
        private readonly IMatriculaService _service;   // para tus GET/CRUD existentes
        private readonly DBContext _db;                // para GuardarSeleccion

        // 👇 ÚNICO CONSTRUCTOR (evita el error de "Multiple constructors…")
        public MatriculasController(IMatriculaService service, DBContext db)
        {
            _service = service;
            _db = db;
        }

        // ---------- Endpoints existentes que usan _service ----------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var matriculas = await _service.GetAllMatriculas();
            return Ok(matriculas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var matricula = await _service.GetMatriculaById(id);
            if (matricula == null) return NotFound();
            return Ok(matricula);
        }

        [HttpGet("Estudiante/{id_estudiante}")]
        public async Task<IActionResult> GetByEstudiante(int id_estudiante)
        {
            var matriculas = await _service.GetByEstudiante(id_estudiante);
            return Ok(matriculas);
        }

        [HttpGet("Periodo/{id_periodo}")]
        public async Task<IActionResult> GetByPeriodo(int id_periodo)
        {
            var matriculas = await _service.GetByPeriodo(id_periodo);
            return Ok(matriculas);
        }

        [HttpPost]
        public async Task<IActionResult> CrearMatricula([FromBody] MatriculaCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Los datos de la matrícula son obligatorios.");

            var exists = await _service.ExistsForEstudiantePeriodo(dto.id_estudiante, dto.id_periodo);
            if (exists)
                return Conflict("El estudiante ya tiene una matrícula en este período.");

            var matricula = new Matricula
            {
                id_estudiante = dto.id_estudiante,
                id_periodo = dto.id_periodo,
                estado = string.IsNullOrWhiteSpace(dto.estado) ? "Pendiente" : dto.estado
            };

            await _service.AddMatricula(matricula);
            return CreatedAtAction(nameof(GetById), new { id = matricula.id_matricula }, matricula);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Matricula matricula)
        {
            if (id != matricula.id_matricula)
                return BadRequest("El id no coincide con la matrícula.");

            await _service.UpdateMatricula(matricula);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteMatricula(id);
            return NoContent();
        }

        // ---------- NUEVO: Guardar selección de grupos ----------
        public class SeleccionDto
        {
            public List<int> grupos { get; set; } = new();
        }

        [HttpPost("usuario/{idUsuario:int}/seleccion")]
        public async Task<IActionResult> GuardarSeleccion(int idUsuario, [FromBody] SeleccionDto body)
        {
            if (body == null) return BadRequest("Cuerpo vacío.");

            // 1) Buscar estudiante por usuario
            var est = await _db.Estudiantes
                .FirstOrDefaultAsync(e => e.IdUsuario == idUsuario);
            if (est == null) return NotFound("Estudiante no encontrado.");

            // 2) Tomar período activo (ajusta si usas otro criterio)
            var periodo = await _db.Periodo_Lectivo
                .OrderByDescending(p => p.anio)
                .ThenByDescending(p => p.cuatrimestre)
                .FirstOrDefaultAsync();
            if (periodo == null) return BadRequest("No hay período activo.");

            // 3) Obtener o crear matrícula del estudiante en el período
            var mat = await _db.Matriculas
                .FirstOrDefaultAsync(m => m.id_estudiante == est.IdEstudiante &&
                                          m.id_periodo == periodo.id_periodo);

            if (mat == null)
            {
                mat = new Matricula
                {
                    id_estudiante = est.IdEstudiante,
                    id_periodo = periodo.id_periodo,
                    estado = "Pendiente"
                };
                _db.Matriculas.Add(mat);
                await _db.SaveChangesAsync(); // para obtener id_matricula
            }

            // 4) Limpiar selección previa y registrar la nueva
            var prev = await _db.Detalle_Matriculas
                                .Where(x => x.IdMatricula == mat.id_matricula)
                                .ToListAsync();
            _db.Detalle_Matriculas.RemoveRange(prev);

            foreach (var idGrupo in body.grupos.Distinct())
            {
                _db.Detalle_Matriculas.Add(new Detalle_Matricula
                {
                    IdMatricula = mat.id_matricula, // FK real (DETALLE_MATRICULA.id_matricula)
                    IdGrupo = idGrupo,          // FK real (DETALLE_MATRICULA.id_grupo)
                    Nota = null,             // usa 0m si tu columna NO acepta NULL
                    Condicion = ""
                });
            }

            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}


