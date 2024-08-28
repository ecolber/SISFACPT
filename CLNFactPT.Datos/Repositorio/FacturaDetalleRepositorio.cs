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
    public class FacturaDetalleRepositorio : Repositorio<FacturaDetalle>, IFacturaDetalleRepositorio
    {
        private readonly AppDbContext _db;

        public FacturaDetalleRepositorio(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(FacturaDetalle facturaDetalle)
        {
            var facturaDetalleDb = _db.FacturaDetalles.FirstOrDefault(x => x.Id == facturaDetalle.Id);
            decimal tasaCambio = 1; //si no es dolar lo multiplico por uno para no alterar monto

            //obtengo la factura
            var facturaId = _db.FacturaDetalles.FirstOrDefault(x => x.Id == facturaDetalle.Id).FacturaId;
            //obtengo el tipo de moneda de la factura
            var monedaId = _db.Facturas.FirstOrDefault(x => x.Id == facturaId).MonedaId;

            if (monedaId == 2) //si es cordoba
            {
                //aplico la tasa de cambio porque el producto esta guardado en dolares
                //obtengo la fecha de la factura
                var fechaFac = _db.Facturas.FirstOrDefault(x => x.Id == facturaId).Fecha;

                //obtengo la tasa de cambio de esa fecha
                tasaCambio = Convert.ToDecimal(_db.TasaCambios.FirstOrDefault(x => x.Fecha == fechaFac).TasaDeCambio);

            }
            //busco precio del producto
            var precioProd = _db.Productos.FirstOrDefault(x => x.Id == facturaDetalle.ProductoId).PrecioDolar;


            if (facturaDetalleDb != null)
            {
                facturaDetalleDb.Id = facturaDetalle.Id;
                facturaDetalleDb.ProductoId = facturaDetalle.ProductoId;
                facturaDetalleDb.Cantidad = facturaDetalle.Cantidad;
                if (monedaId == 1)//es dolar
                    facturaDetalleDb.PrecioUnitario = precioProd;
                else //es cordoba, aplico tasa
                facturaDetalleDb.PrecioUnitario = precioProd * tasaCambio;

                _db.SaveChanges();

                //Actualizar los totales en la tabla master
                ActualizarTotalFactura(facturaId);
            }
        }

        public void AgregarDetalle(FacturaDetalle facturaDetalle)
        {
            decimal tasaCambio = 1; //si no es dolar lo multiplico por uno para no alterar monto
            //obtengo el tipo de moneda de la factura
            var monedaId = _db.Facturas.FirstOrDefault(x => x.Id == facturaDetalle.FacturaId).MonedaId;

            if (monedaId == 2) //si es cordoba
            {
                //aplico la tasa de cambio porque el producto esta guardado en dolares
                //obtengo la fecha de la factura
                var fechaFac = _db.Facturas.FirstOrDefault(x => x.Id == facturaDetalle.FacturaId).Fecha;

                //obtengo la tasa de cambio de esa fecha
                tasaCambio = Convert.ToDecimal(_db.TasaCambios.FirstOrDefault(x => x.Fecha == fechaFac).TasaDeCambio);

            }
            //busco precio del producto
            var precioProd = _db.Productos.FirstOrDefault(x => x.Id == facturaDetalle.ProductoId).PrecioDolar;


            var detalle = new FacturaDetalle
            {
                FacturaId = facturaDetalle.FacturaId,
                ProductoId = facturaDetalle.ProductoId,
                Cantidad = facturaDetalle.Cantidad,
                PrecioUnitario = (monedaId == 1) ? precioProd : precioProd * tasaCambio
            };
           _db.FacturaDetalles.Add(detalle);
            _db.SaveChanges();
            //Actualizar los totales en la tabla master
            ActualizarTotalFactura(facturaDetalle.FacturaId);
            
        }

        public void ActualizarTotalFactura(int facturaId)
        {
            var factura = _db.Facturas.FirstOrDefault(f => f.Id == facturaId);

            if (factura != null)
            {
                //Calculamos el 
                var subtotal = _db.FacturaDetalles
                         .Where(fd => fd.FacturaId == facturaId)
                         .Sum(fd => fd.Cantidad * fd.PrecioUnitario);

                // Aplicamos el IVA
                var iva = subtotal * 0.15m;

                // Actualizamos los campos Subtotal, IVA y Total
                factura.Subtotal = subtotal;
                factura.IVA = iva;
                factura.Total = subtotal + iva;

                _db.SaveChanges();
            }
        }

    }
}
