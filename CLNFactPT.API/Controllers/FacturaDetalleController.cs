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
    public class FacturaDetalleController : ControllerBase
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public FacturaDetalleController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var lista = await _unidadTrabajo.FacturaDetalle.ObtenerTodos();
            return StatusCode(StatusCodes.Status200OK, new { value = lista });
        }

      

        [HttpPost]
        [Route("Agregar")]
        public async Task<ActionResult> AgregarDetalle([FromBody] FacturaDetalleAgregarDTO facturaDetalleDto)
        {
            var facturaDetalle = new FacturaDetalle
            {
                FacturaId = facturaDetalleDto.FacturaId,
                ProductoId = facturaDetalleDto.ProductoId,
                Cantidad = facturaDetalleDto.Cantidad
            };
            _unidadTrabajo.FacturaDetalle.AgregarDetalle(facturaDetalle);
            await _unidadTrabajo.Guardar();
            return Ok(new { success = true, message = "Detalle de factura guardado" });
        }

        [HttpPost]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar(FacturaDetalleActualizarDTO facturaDetalleDTO)
        {
            
            var facturaDetalle = new FacturaDetalle
            {
                Id = facturaDetalleDTO.Id,
                ProductoId = facturaDetalleDTO.ProductoId,
                Cantidad = facturaDetalleDTO.Cantidad
            };

            _unidadTrabajo.FacturaDetalle.Actualizar(facturaDetalle);
            await _unidadTrabajo.Guardar();
            return Ok(new { success = true, message = "Detalle de factura actualizado" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var facturaDetalleDb = await _unidadTrabajo.FacturaDetalle.Obtener(id);

            if (facturaDetalleDb == null)
            {
                return BadRequest("El Id de factura detalle no coincide.");
            }

            _unidadTrabajo.FacturaDetalle.Eliminar(facturaDetalleDb);
            await _unidadTrabajo.Guardar();

            _unidadTrabajo.FacturaDetalle.ActualizarTotalFactura(facturaDetalleDb.FacturaId);
            return Ok(new { success = true, message = "detalle de factura eliminada" });
        }
    }

}
