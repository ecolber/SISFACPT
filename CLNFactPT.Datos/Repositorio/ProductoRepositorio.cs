using CLNFactPT.Datos.Data;
using CLNFactPT.Datos.DTOs;
using CLNFactPT.Datos.Repositorio.IRepositorio;
using CLNFactPT.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CLNFactPT.Datos.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly AppDbContext _db;

        public ProductoRepositorio(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Producto producto)
        {
            var productoDb = _db.Productos.FirstOrDefault(x => x.Id == producto.Id);

            if (productoDb != null)
            {
                producto.Id = productoDb.Id;
                productoDb.Descripcion = producto.Descripcion;
                productoDb.SKU = producto.SKU;   
                productoDb.PrecioDolar = producto.PrecioDolar;
                productoDb.Estado = producto.Estado;
                _db.SaveChanges();
            }
        }

        public async Task<List<Producto>> ObtenerPorDescripcion(string descripcion)
        {
            return await _db.Set<Producto>().Where(p => p.Descripcion.Contains(descripcion)).ToListAsync();
        }

        public async Task<List<ProductoCnPrecioCordobaDTO>> ObtenerProdActivos()
        {
            var productos = await _db.Set<Producto>().Where(p => p.Estado == true).ToListAsync();

            // Obtener la tasa de cambio del día actual
            var tasaCambio = await _db.Set<TasaCambio>().Where(tc => tc.Fecha.Date == DateTime.Today).FirstOrDefaultAsync(); // Implementa este método

            if (tasaCambio == null)
            {
                throw new InvalidOperationException("No se encontró la tasa de cambio para el día de hoy.");
            }

            // Calcular el precio en córdobas para cada producto
            var productosActivos = productos.Select(p => new ProductoCnPrecioCordobaDTO
            {
                Descripcion = p.Descripcion,
                SKU = p.SKU,
                PrecioDolar = p.PrecioDolar,
                Estado = p.Estado,
                PrecioCordobas = p.PrecioDolar * tasaCambio.TasaDeCambio
            }).ToList();

            return productosActivos;
        }

        public async Task<Producto> ObtenerPorSKU(string sku)
        {
            return await _db.Set<Producto>().FirstOrDefaultAsync(p => p.SKU == sku);
        }
    }
}
