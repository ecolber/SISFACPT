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
    public class ProductoController : ControllerBase
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public ProductoController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var lista = await _unidadTrabajo.Producto.ObtenerTodos();
            return StatusCode(StatusCodes.Status200OK, new { value = lista });
        }

        [HttpGet]
        [Route("ProductosActivos")]
        public async Task<IActionResult> ObtenerProdActivos()
        {
            var lista = await _unidadTrabajo.Producto.ObtenerProdActivos();
            return StatusCode(StatusCodes.Status200OK, new { value = lista });
        }

        [HttpGet("BuscarPorDescripcion/{descripcion}")]
        public async Task<IActionResult> ObtenerPorDescripcion(string descripcion)
        {
            var producto = await _unidadTrabajo.Producto.ObtenerPorDescripcion(descripcion);

            if (producto == null)
            {
                return BadRequest(new { success = false, message = "Producto no encontrado" });
            }

            return Ok(new { success = true, producto });
        }

        [HttpGet("BuscarPorSKU/{sku}")]
        public async Task<IActionResult> ObtenerPorSKU(string sku)
        {
            var producto = await _unidadTrabajo.Producto.ObtenerPorSKU(sku);

            if (producto == null)
            {
                return BadRequest(new { success = false, message = "Producto no encontrado" });
            }

            return Ok(new { success = true, producto });
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<ActionResult> Agregar([FromBody] ProductoDTO productoDto)
        {
            //Validar que no se repita SKU
            var existeProducto = await _unidadTrabajo.Producto.ObtenerPorSKU(productoDto.SKU);

            if (existeProducto != null)
            {
                return BadRequest(new { success = false, message = "Ya existe un producto con este SKU." });
            }

            var producto = new Producto
            {
                Descripcion = productoDto.Descripcion,
                SKU = productoDto.SKU,
                PrecioDolar = productoDto.PrecioDolar,
                Estado = productoDto.Estado
            };
            await _unidadTrabajo.Producto.Agregar(producto);
            await _unidadTrabajo.Guardar();
            return Ok(new { success = true, message = "Producto guardado" });
        }

        [HttpPost]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar(Producto producto)
        {
            //validar que no  repita el SKU
            var existeProducto = await _unidadTrabajo.Producto.ObtenerPorSKU(producto.SKU);

            if (existeProducto != null && existeProducto.Id != producto.Id)//para evitar que me salga error cuando estoy actualizando el original
            {
                return BadRequest(new { success = false, message = "Ya existe un producto con este SKU." });
            }
            _unidadTrabajo.Producto.Actualizar(producto);
            await _unidadTrabajo.Guardar();
            return Ok(new { success = true, message = "Producto actualizado" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var productoDb = await _unidadTrabajo.Producto.Obtener(id);

            if (productoDb == null)
            {
                return BadRequest("El Id de producto no coincide.");
            }

            _unidadTrabajo.Producto.Eliminar(productoDb);
            await _unidadTrabajo.Guardar();
            return Ok(new { success = true, message = "Producto eliminado" });
        }
    }

}
