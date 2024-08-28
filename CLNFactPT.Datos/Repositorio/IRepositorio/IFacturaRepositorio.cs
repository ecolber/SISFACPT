using CLNFactPT.Datos.DTOs;
using CLNFactPT.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CLNFactPT.Datos.Repositorio.IRepositorio
{
    public interface IFacturaRepositorio : IRepositorio<Factura>
    {
        void Actualizar(Factura factura);
        Task<List<Factura>> ObtenerPorCliente(int clienteId);
        Task<Factura> ObtenerPorNumFactura(string factura);
        Task<List<FacturaMasterDetalleDTO>> ObtenerFacturasConDetalle();
    }
}
