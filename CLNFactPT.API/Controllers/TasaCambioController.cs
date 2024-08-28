using CLNFactPT.API.DTOs;
using CLNFactPT.Datos.Data;
using CLNFactPT.Datos.Repositorio.IRepositorio;
using CLNFactPT.Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace CLNFactPT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasaCambioController : ControllerBase
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public TasaCambioController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var lista = await _unidadTrabajo.TasaCambio.ObtenerTodos();
            return StatusCode(StatusCodes.Status200OK, new { value = lista });
        }

        [HttpGet("BuscarPorFecha/{fecha}")]
        public async Task<IActionResult> ObtenerPorFecha(string fecha)
        {
            var tasaCambio = await _unidadTrabajo.TasaCambio.ObtenerPorFecha(fecha);

            if (tasaCambio == null)
            {
                return NotFound(new { success = false, message = "tasa de cambio no encontrado" });
            }

            return Ok(new { success = true, tasaCambio });
        }

        [HttpGet("BuscarPorMes/{mes}")]
        public async Task<IActionResult> ObtenerPorMes(int mes)
        {
            var tasaCambio = await _unidadTrabajo.TasaCambio.ObtenerPorMes(mes);

            if (tasaCambio == null)
            {
                return NotFound(new { success = false, message = "Tasa de cambio no encontrado" });
            }

            return Ok(new { success = true, tasaCambio });
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<ActionResult> Agregar([FromBody] TasaCambioDTO tasaCambioDto)
        {
            //Validar que no se repita tasa de cambio para una misma fecha
            var existeTasaCambio = await _unidadTrabajo.TasaCambio.ObtenerPorFecha(tasaCambioDto.Fecha.ToString());

            if (existeTasaCambio != null)
            {
                return BadRequest(new { success = false, message = "Ya existe una tasa de cambio para esta fecha." });
            }

            var tasaCambio = new TasaCambio
            {
                Fecha = tasaCambioDto.Fecha,
                TasaDeCambio = tasaCambioDto.TasaDeCambio
            };
            await _unidadTrabajo.TasaCambio.Agregar(tasaCambio);
            await _unidadTrabajo.Guardar();
            return Ok(new { success = true, message = "Tasa de cambio guardada" });
        }

        [HttpPost]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar(TasaCambio tasaCambio)
        {
            //validar que no  repita tasa de cambio para misma fecha
            var existeTasaCambio = await _unidadTrabajo.TasaCambio.ObtenerPorFecha(tasaCambio.Fecha.ToString());

            if (existeTasaCambio != null && existeTasaCambio.Id != tasaCambio.Id)//para evitar que me salga error cuando estoy actualizando el original
            {
                return BadRequest(new { success = false, message = "Ya existe una tasa de cambio para esta fecha." });
            }
            _unidadTrabajo.TasaCambio.Actualizar(tasaCambio);
            await _unidadTrabajo.Guardar();
            return Ok(new { success = true, message = "Tasa de cambio actualizada" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var tasaCambioDb = await _unidadTrabajo.TasaCambio.Obtener(id);

            if (tasaCambioDb == null)
            {
                return BadRequest("El Id de tasa de cambio no coincide.");
            }

            _unidadTrabajo.TasaCambio.Eliminar(tasaCambioDb);
            await _unidadTrabajo.Guardar();
            return Ok(new { success = true, message = "Tasa de cambio eliminada" });
        }
    }

}
