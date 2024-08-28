using CLNFactPT.API.DTOs;
using CLNFactPT.Datos.Data;
using CLNFactPT.Datos.Repositorio;
using CLNFactPT.Datos.Repositorio.IRepositorio;
using CLNFactPT.Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace CLNFactPT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RptFacturaController : ControllerBase
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public RptFacturaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var lista =  _unidadTrabajo.RptFacturaRepositorio.ObtenerReporteFacturas();
            return Ok(new { value = lista });
        }

        [HttpGet("BuscarPorAnio/{anio}")]
        public async Task<IActionResult> ObtenerPorAnio(int anio)
        {
            var reporte = _unidadTrabajo.RptFacturaRepositorio.ObtenerReporteFacturas()
                                         .Where(f => f.Anio == anio)
                                         .ToList();

            if (reporte.IsNullOrEmpty())
            {
                return BadRequest(new { success = false, message = "No hay datos" });
            }

            return Ok(new { success = true, reporte });
        }

        [HttpGet("BuscarPorMes/{mes}")]
        public async Task<IActionResult> ObtenerPorMes(int mes)
        {
            var reporte = _unidadTrabajo.RptFacturaRepositorio.ObtenerReporteFacturas()
                                         .Where(f => f.Mes == mes)
                                         .ToList();

            if (reporte.IsNullOrEmpty())
            {
                return BadRequest(new { success = false, message = "No hay datos" });
            }

            return Ok(new { success = true, reporte });
        }

    }

}
