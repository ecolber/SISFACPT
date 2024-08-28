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
    public class RptFacturaRepositorio : IRptFacturaRepositorio
    {
        private readonly AppDbContext _db;

        public RptFacturaRepositorio(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<RptFacturaDTO> ObtenerReporteFacturas()
        {
            var reporte = from factura in _db.Facturas
                          join cliente in _db.Clientes on factura.ClienteId equals cliente.Id
                          join detalle in _db.FacturaDetalles on factura.Id equals detalle.FacturaId
                          join producto in _db.Productos on detalle.ProductoId equals producto.Id
                          group new { factura, detalle, cliente, producto } by new
                          {
                              cliente.Codigo,
                              cliente.Nombre,
                              cliente.Apellido,
                              producto.SKU,
                              producto.Descripcion,
                              Mes = factura.Fecha.Month,
                              Anio = factura.Fecha.Year
                          } into g
                          select new RptFacturaDTO
                          {
                              CodigoCliente = g.Key.Codigo,
                              Nombre = g.Key.Nombre + ' ' + g.Key.Apellido,                              
                              Mes = g.Key.Mes,
                              Anio = g.Key.Anio,
                              TotalDolar = g.Sum(x => x.factura.MonedaId == 1 ? (x.detalle.PrecioUnitario * x.detalle.Cantidad) : 0) * 1.15m,
                              TotalCordobas = g.Sum(x => x.factura.MonedaId == 2 ? x.detalle.PrecioUnitario * x.detalle.Cantidad : 0) * 1.15m,
                              SKU = g.Key.SKU,
                              Producto = g.Key.Descripcion
                          };

            return reporte.ToList();
        } 

    }
}
