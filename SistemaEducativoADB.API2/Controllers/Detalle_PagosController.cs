using Microsoft.AspNetCore.Mvc;
using SistemaEducativoADB.API.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Detalle_PagosController : ControllerBase
    {
        private readonly IDetalle_PagosService _service;

        public Detalle_PagosController(IDetalle_PagosService service)
        {
            _service = service;
        }

        // GET: api/Detalle_Pagos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllDetallePagos();
            var result = items.Select(d => new {
                d.IdDetallePago,
                d.IdPago,
                d.IdMatricula,
                pago = d.Pago == null ? null : new
                {
                    d.Pago.IdPago,
                    d.Pago.Monto,
                    d.Pago.Estado
                },
                matricula = d.Matricula == null ? null : new
                {
                    d.Matricula.id_matricula,
                    d.Matricula.estado
                }
            });
            return Ok(result);
        }

        // GET: api/Detalle_Pagos
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var d = await _service.GetDetallePagoById(id);
            if (d == null) return NotFound();

            var result = new
            {
                idDetallePago = d.IdDetallePago,
                idPago = d.IdPago,
                idMatricula = d.IdMatricula,
                pago = d.Pago == null ? null : new
                {
                    idPago = d.Pago.IdPago,
                    idEstudiante = d.Pago.IdEstudiante,
                    monto = d.Pago.Monto,
                    fecha = d.Pago.Fecha,
                    estado = d.Pago.Estado,
                    metodoPago = d.Pago.MetodoPago
                },
                matricula = d.Matricula == null ? null : new
                {
                    id_matricula = d.Matricula.id_matricula,
                    id_estudiante = d.Matricula.id_estudiante,
                    id_periodo = d.Matricula.id_periodo,
                    estado = d.Matricula.estado
                }
            };

            return Ok(result);
        }

        // GET: api/Detalle_Pagos/PorPago
        [HttpGet("PorPago/{id_pago}")]
        public async Task<IActionResult> GetByPago(int id_pago)
        {
            var items = await _service.GetDetallePagosByPago(id_pago);

            var result = items.Select(d => new
            {
                idDetallePago = d.IdDetallePago,
                idPago = d.IdPago,
                idMatricula = d.IdMatricula,
                pago = d.Pago == null ? null : new
                {
                    idPago = d.Pago.IdPago,
                    idEstudiante = d.Pago.IdEstudiante,
                    monto = d.Pago.Monto,
                    fecha = d.Pago.Fecha,
                    estado = d.Pago.Estado,
                    metodoPago = d.Pago.MetodoPago
                },
                matricula = d.Matricula == null ? null : new
                {
                    id_matricula = d.Matricula.id_matricula,
                    id_estudiante = d.Matricula.id_estudiante,
                    id_periodo = d.Matricula.id_periodo,
                    estado = d.Matricula.estado
                }
            });

            return Ok(result);
        }

        // GET: api/Detalle_Pagos/PorMatricula
        [HttpGet("PorMatricula/{id_matricula}")]
        public async Task<IActionResult> GetByMatricula(int id_matricula)
        {
            var items = await _service.GetDetallePagosByMatricula(id_matricula);
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Detalle_PagosCreateDto dto)
        {
            if (dto == null) return BadRequest("Datos inválidos.");
            if (dto.id_pago <= 0 || dto.id_matricula <= 0)
                return BadRequest("id_pago e id_matricula deben ser > 0.");

            if (await _service.ExistsAsync(dto.id_pago, dto.id_matricula))
                return Conflict("Ya existe un detalle para ese pago y matrícula.");

            var entity = new DetallePago
            {
                IdPago = dto.id_pago,
                IdMatricula = dto.id_matricula
            };

            await _service.AddDetallePago(entity);

            var creado = await _service.GetDetallePagoById(entity.IdDetallePago);
            if (creado == null) return Problem("No fue posible recuperar el detalle creado.");

            var result = new
            {
                idDetallePago = creado.IdDetallePago,
                idPago = creado.IdPago,
                idMatricula = creado.IdMatricula,
                pago = creado.Pago == null ? null : new
                {
                    idPago = creado.Pago.IdPago,
                    monto = creado.Pago.Monto,
                    estado = creado.Pago.Estado
                },
                matricula = creado.Matricula == null ? null : new
                {
                    idMatricula = creado.Matricula.id_matricula,
                    estado = creado.Matricula.estado
                }
            };

            return CreatedAtAction(nameof(GetById), new { id = creado.IdDetallePago }, result);
        }

        // PUT: api/Detalle_Pagos
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DetallePago detalle)
        {
            if (detalle == null) return BadRequest("Datos inválidos.");
            if (id != detalle.IdDetallePago) return BadRequest("El id de la ruta no coincide con el del cuerpo.");
            if (detalle.IdPago <= 0 || detalle.IdMatricula <= 0)
                return BadRequest("IdPago e IdMatricula deben ser > 0.");

            var actual = await _service.GetDetallePagoById(id);
            if (actual == null) return NotFound();

            if ((actual.IdPago != detalle.IdPago || actual.IdMatricula != detalle.IdMatricula) &&
                await _service.ExistsAsync(detalle.IdPago, detalle.IdMatricula))
            {
                return Conflict("Ya existe un detalle para ese pago y matrícula.");
            }

            await _service.UpdateDetallePago(detalle);
            return NoContent();
        }

        // DELETE: api/Detalle_Pagos
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteDetallePago(id);
            return NoContent();
        }
    }
}
