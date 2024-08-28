using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLNFactPT.Datos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable
    {
        IClienteRepositorio Cliente { get; }
        ITasaCambioRepositorio TasaCambio { get; }
        IProductoRepositorio Producto { get; }
        IFacturaRepositorio Factura { get; }
        IFacturaDetalleRepositorio FacturaDetalle { get; }

        IRptFacturaRepositorio RptFacturaRepositorio { get; }
        Task Guardar();
    }
}
