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
    public class ClienteController : ControllerBase
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public ClienteController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var lista = await _unidadTrabajo.Cliente.ObtenerTodos();
            return StatusCode(StatusCodes.Status200OK, new { value = lista });
        }

        [HttpGet("BuscarPorCodigo/{codigo}")]
        public async Task<IActionResult> ObtenerPorCodigo(string codigo)
        {
            var cliente = await _unidadTrabajo.Cliente.ObtenerPorCodigo(codigo);

            if (cliente == null)
            {
                return NotFound(new { success = false, message = "Cliente no encontrado" });
            }

            return Ok(new { success = true, cliente });
        }

        [HttpGet("BuscarPorNombre/{nombre}")]
        public async Task<IActionResult> ObtenerPorNombre(string nombre)
        {
            var cliente = await _unidadTrabajo.Cliente.ObtenerPorNombre(nombre);

            if (cliente == null)
            {
                return NotFound(new { success = false, message = "Cliente no encontrado" });
            }

            return Ok(new { success = true, cliente });
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<ActionResult> Agregar([FromBody] ClienteDTO clienteDto)
        {
            var cliente = new Cliente
            {
                Codigo = clienteDto.Codigo,
                Nombre = clienteDto.Nombre,
                Apellido = clienteDto.Apellido
            };
            await _unidadTrabajo.Cliente.Agregar(cliente);
            await _unidadTrabajo.Guardar();
            return Ok(new { success = true, message = "Cliente guardado" });
        }

        [HttpPost]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar(Cliente cliente)
        {

            _unidadTrabajo.Cliente.Actualizar(cliente);
            await _unidadTrabajo.Guardar();
            return Ok(new { success = true, message = "Cliente actualizado" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var clienteDb = await _unidadTrabajo.Cliente.Obtener(id);

            if (clienteDb == null)
            {
                return BadRequest("El ID del cliente no coincide.");
            }

            _unidadTrabajo.Cliente.Eliminar(clienteDb);
            await _unidadTrabajo.Guardar();
            return Ok(new { success = true, message = "Cliente eliminado" });
        }
    }

}
