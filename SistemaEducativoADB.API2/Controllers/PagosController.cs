using Microsoft.AspNetCore.Mvc;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IPagosService _service;

        public PagosController(IPagosService service)
        {
            _service = service;
        }

        // ------------------ Helpers ------------------
        private static object MapPago(Pago p) => new
        {
            idPago = p.IdPago,
            idEstudiante = p.IdEstudiante,
            monto = p.Monto,
            fecha = p.Fecha,
            estado = p.Estado,
            metodoPago = p.MetodoPago,
            estudiante = p.Estudiante == null ? null : new
            {
                idEstudiante = p.Estudiante.IdEstudiante,
                carnet = p.Estudiante.Carnet,
                telefono = p.Estudiante.Telefono

            }
        };

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pagos = await _service.GetAllPagos();
            return Ok(pagos.Select(MapPago));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pago = await _service.GetPagoById(id);
            if (pago == null) return NotFound();
            return Ok(MapPago(pago));
        }

        [HttpGet("PorEstudiante/{id_estudiante}")]
        public async Task<IActionResult> GetByEstudiante(int id_estudiante)
        {
            var pagos = await _service.GetPagosPorEstudiante(id_estudiante);
            return Ok(pagos.Select(MapPago));
        }

        [HttpGet("PorEstado/{estado}")]
        public async Task<IActionResult> GetByEstado(string estado)
        {
            var pagos = await _service.GetPagosPorEstado(estado);
            return Ok(pagos.Select(MapPago));
        }

        [HttpGet("PorFechas")]
        public async Task<IActionResult> GetByRangoFechas([FromQuery] DateTime desde, [FromQuery] DateTime hasta)
        {
            if (desde == default || hasta == default) return BadRequest("Debe especificar 'desde' y 'hasta'.");
            if (hasta < desde) return BadRequest("'hasta' no puede ser menor que 'desde'.");

            var pagos = await _service.GetPagosPorRangoFechas(desde, hasta);
            return Ok(pagos.Select(MapPago));
        }

        [HttpPost]
        public async Task<IActionResult> CrearPago([FromBody] PagosCreateDto dto)
        {
            if (dto == null) return BadRequest("Datos del pago son obligatorios.");
            if (dto.id_estudiante <= 0) return BadRequest("id_estudiante inválido.");
            if (dto.monto <= 0) return BadRequest("monto debe ser mayor que 0.");
            if (string.IsNullOrWhiteSpace(dto.metodo_pago)) return BadRequest("metodo_pago es obligatorio.");

            var pago = new Pago
            {
                IdEstudiante = dto.id_estudiante,
                Monto = dto.monto,
                Fecha = dto.fecha == default ? DateTime.UtcNow : dto.fecha,
                Estado = string.IsNullOrWhiteSpace(dto.estado) ? "Pendiente" : dto.estado.Trim(),
                MetodoPago = dto.metodo_pago.Trim()
            };

            await _service.AddPago(pago);
            var creado = await _service.GetPagoById(pago.IdPago);
            return CreatedAtAction(nameof(GetById), new { id = creado!.IdPago }, MapPago(creado));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PagoPutBody body)
        {
            if (body == null) return BadRequest("Datos inválidos.");
            if (id != body.idPago) return BadRequest("El id de la ruta no coincide con el del cuerpo.");
            if (body.monto <= 0) return BadRequest("Monto debe ser mayor que 0.");
            if (body.idEstudiante <= 0) return BadRequest("idEstudiante inválido.");

            var entity = await _service.GetPagoById(id);
            if (entity == null) return NotFound();

            // Mapear del body plano a la entidad
            entity.IdEstudiante = body.idEstudiante;
            entity.Monto = body.monto;
            entity.Fecha = body.fecha;
            entity.Estado = (body.estado ?? "").Trim();
            entity.MetodoPago = (body.metodoPago ?? "").Trim();

            await _service.UpdatePago(entity);
            return NoContent();
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] CambiarEstadoDto body)
        {
            if (body == null || string.IsNullOrWhiteSpace(body.estado))
                return BadRequest("Debe indicar el nuevo estado.");

            var ok = await _service.CambiarEstadoPago(id, body.estado);
            if (!ok) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeletePago(id);
            return NoContent();
        }

        public class CambiarEstadoDto
        {
            public string estado { get; set; } = "";
        }

        public class PagoPutBody
        {
            public int idPago { get; set; }
            public int idEstudiante { get; set; }
            public decimal monto { get; set; }
            public DateTime fecha { get; set; }
            public string estado { get; set; } = "";
            public string metodoPago { get; set; } = "";
            public EstudianteMini? estudiante { get; set; }
        }

        public class EstudianteMini
        {
            public int idEstudiante { get; set; }
            public string carnet { get; set; } = "";
            public string telefono { get; set; } = "";
        }
    }
}
