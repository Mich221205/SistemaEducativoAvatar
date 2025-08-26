using Microsoft.AspNetCore.Mvc;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Periodo_LectivoController : ControllerBase
    {
        private readonly IPeriodo_LectivoService _service;

        public Periodo_LectivoController(IPeriodo_LectivoService service)
        {
            _service = service;
        }

        // GET: api/Periodo_Lectivo
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var periodos = await _service.GetAllPeriodos();
            return Ok(periodos);
        }

        // GET: api/Periodo_Lectivo
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var periodo = await _service.GetPeriodoById(id);
            if (periodo == null) return NotFound();
            return Ok(periodo);
        }

        // GET: api/Periodo_Lectivo/PorAnio
        [HttpGet("PorAnio/{anio:int}")]
        public async Task<IActionResult> GetByAnio(int anio)
        {
            var periodos = await _service.GetPeriodosPorAnio(anio);
            return Ok(periodos);
        }

        // GET: api/Periodo_Lectivo/Vigente
        [HttpGet("Vigente")]
        public async Task<IActionResult> GetVigente([FromQuery] DateTime? fecha = null)
        {
            var f = fecha ?? DateTime.Today;
            var vigente = await _service.GetPeriodoVigente(f);
            if (vigente == null) return NotFound();
            return Ok(vigente);
        }

        // POST: api/Periodo_Lectivo
        [HttpPost]
        public async Task<IActionResult> CrearPeriodo([FromBody] Periodo_LectivoCreateDto dto)
        {
            if (dto == null) return BadRequest("Datos inválidos.");
            if (string.IsNullOrWhiteSpace(dto.cuatrimestre))
                return BadRequest("El cuatrimestre es obligatorio.");
            if (dto.fecha_fin < dto.fecha_inicio)
                return BadRequest("La fecha_fin no puede ser menor que fecha_inicio.");

            var entity = new Periodo_Lectivo
            {
                anio = dto.anio,
                cuatrimestre = dto.cuatrimestre.Trim(),
                fecha_inicio = dto.fecha_inicio,
                fecha_fin = dto.fecha_fin
            };

            await _service.AddPeriodo(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.id_periodo }, entity);
        }

        // PUT: api/Periodo_Lectivo
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Periodo_Lectivo periodo)
        {
            if (periodo == null) return BadRequest("Datos inválidos.");
            if (id != periodo.id_periodo) return BadRequest("El id de la ruta no coincide.");
            if (string.IsNullOrWhiteSpace(periodo.cuatrimestre))
                return BadRequest("El cuatrimestre es obligatorio.");
            if (periodo.fecha_fin < periodo.fecha_inicio)
                return BadRequest("La fecha_fin no puede ser menor que fecha_inicio.");

            await _service.UpdatePeriodo(periodo);
            return NoContent();
        }

        // DELETE: api/Periodo_Lectivo
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeletePeriodo(id);
            return NoContent();
        }
    }
}
