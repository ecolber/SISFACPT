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
    public class FacturaRepositorio : Repositorio<Factura>, IFacturaRepositorio
    {
        private readonly AppDbContext _db;

        public FacturaRepositorio(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Factura factura)
        {
            var facturaDb = _db.Facturas.FirstOrDefault(x => x.Id == factura.Id);

            //la moneda, el subtotal, el iva y el total no se actualizaran  desde el master
            if (facturaDb != null)
            {
                facturaDb.Id = factura.Id;
                facturaDb.NumeroFactura = factura.NumeroFactura;
                facturaDb.Fecha = factura.Fecha;
                facturaDb.ClienteId = factura.ClienteId;
                facturaDb.MonedaId = factura.MonedaId;
                facturaDb.Subtotal = factura.Subtotal;
                facturaDb.IVA = factura.IVA;
                facturaDb.Total = factura.Total;
                _db.SaveChanges();
            }
        }

        public async Task<List<Factura>> ObtenerPorCliente(int clienteId)
        {
            return await _db.Set<Factura>().Where(f => f.ClienteId == clienteId).ToListAsync();
        }

        public async Task<Factura> ObtenerPorNumFactura(string factura)
        {
            return await _db.Set<Factura>().FirstOrDefaultAsync(f => f.NumeroFactura == factura);
        }

        public async Task<List<FacturaMasterDetalleDTO>> ObtenerFacturasConDetalle()
        {
            var facturas = await _db.Facturas
                                         .Include(fm => fm.FacturaDetalles)
                                         .Select(fm => new FacturaMasterDetalleDTO
                                         {
                                             FacturaId = fm.Id,
                                             NumeroFactura = fm.NumeroFactura,
                                             Fecha = fm.Fecha,
                                             MonedaId = fm.MonedaId,
                                             Subtotal = fm.Subtotal,
                                             IVA = fm.IVA,
                                             Total = fm.Total,
                                             FacturaDetalles = fm.FacturaDetalles.Select(fd => new FacturaDetalleDTO
                                             {
                                                 FacturaDetalleId = fd.Id,
                                                 ProductoId = fd.ProductoId,
                                                 Cantidad = fd.Cantidad,
                                                 PrecioUnitario = fd.PrecioUnitario
                                             }).ToList()
                                         })
                                         .ToListAsync();

            return facturas;
        }

    }
}
