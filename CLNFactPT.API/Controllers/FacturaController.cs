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
    public class FacturaController : ControllerBase
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public FacturaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var lista = await _unidadTrabajo.Factura.ObtenerTodos();
            return StatusCode(StatusCodes.Status200OK, new { value = lista });
        }

      

        [HttpGet("BuscarPorCliente/{clienteId}")]
        public async Task<IActionResult> ObtenerPorCliente(int clienteId)
        {
            var facturas = await _unidadTrabajo.Factura.ObtenerPorCliente(clienteId);

            if (facturas == null)
            {
                return BadRequest(new { success = false, message = "El cliente no tiene facturas" });
            }

            return Ok(new { success = true, facturas });
        }

        [HttpGet("BuscarPorFactura/{numFactura}")]
        public async Task<IActionResult> ObtenerPorFactura(string numFactura)
        {
            var factura = await _unidadTrabajo.Factura.ObtenerPorNumFactura(numFactura);

            if (factura == null)
            {
                return BadRequest(new { success = false, message = "Factura no encontrada" });
            }

            return Ok(new { success = true, factura });
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<ActionResult> Agregar([FromBody] FacturaAgregarDTO facturaDto)
        {
            //Validar que no se repita numero de factura
            var existeFactura = await _unidadTrabajo.Factura.ObtenerPorNumFactura(facturaDto.NumeroFactura);

            if (existeFactura != null)
            {
                return BadRequest(new { success = false, message = "Ya existe una factura con este numero." });
            }

            var factura = new Factura
            {
                NumeroFactura = facturaDto.NumeroFactura,
                Fecha = facturaDto.Fecha,
                ClienteId = facturaDto.ClienteId,
                MonedaId = facturaDto.MonedaId,
                Subtotal = 0,
                IVA = 0,
                Total = 0
            };
            await _unidadTrabajo.Factura.Agregar(factura);
            await _unidadTrabajo.Guardar();
            return Ok(new { success = true, message = "Factura guardada" });
        }

        [HttpPost]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar(FacturaActualizarDTO facturaDTO)
        {
            //validar que no  repita el numero de factura
            var existeFactura = await _unidadTrabajo.Factura.ObtenerPorNumFactura(facturaDTO.NumeroFactura);

            if (existeFactura != null && existeFactura.Id != facturaDTO.Id)//para evitar que me salga error cuando estoy actualizando el original
            {
                return BadRequest(new { success = false, message = "Ya existe una factura con este numero." });
            }

            var factura = new Factura
            {
                NumeroFactura = facturaDTO.NumeroFactura,
                Fecha = facturaDTO.Fecha,
                ClienteId = facturaDTO.ClienteId
            };

            _unidadTrabajo.Factura.Actualizar(factura);
            await _unidadTrabajo.Guardar();
            return Ok(new { success = true, message = "Factura actualizada" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var facturaDb = await _unidadTrabajo.Factura.Obtener(id);

            if (facturaDb == null)
            {
                return BadRequest("El Id de factura no coincide.");
            }

            _unidadTrabajo.Factura.Eliminar(facturaDb);
            await _unidadTrabajo.Guardar();
            return Ok(new { success = true, message = "factura eliminada" });
        }

        [HttpGet]
        [Route("FacturasDetalle")]
        public async Task<IActionResult> ObtenerFacturaConDetalle()
        {
            var facturas = await _unidadTrabajo.Factura.ObtenerFacturasConDetalle();

            return Ok(facturas);
        }
    }

}
