using CLNFactPT.Datos.Data;
using CLNFactPT.Datos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLNFactPT.Datos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly AppDbContext _db;

        public IClienteRepositorio Cliente { get; private set; }
        public ITasaCambioRepositorio TasaCambio { get; private set; }
        public IProductoRepositorio Producto { get; private set; }
        public IFacturaRepositorio Factura { get; private set; }
        public IFacturaDetalleRepositorio FacturaDetalle { get; private set; }
        public IRptFacturaRepositorio RptFacturaRepositorio { get; private set; }
        public UnidadTrabajo(AppDbContext db)
        {
            _db = db;
            Cliente = new ClienteRepositorio(_db);
            TasaCambio = new TasaCambioRepositorio(_db);
            Producto = new ProductoRepositorio(_db);
            Factura = new FacturaRepositorio(_db);
            FacturaDetalle = new FacturaDetalleRepositorio(_db);
            RptFacturaRepositorio = new RptFacturaRepositorio(_db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
