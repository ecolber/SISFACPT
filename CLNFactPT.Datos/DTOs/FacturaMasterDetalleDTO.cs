using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLNFactPT.Datos.DTOs
{
    public class FacturaMasterDetalleDTO
    {
        public int FacturaId {  get; set; }
        public string NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public int MonedaId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }

        // Detalles de la Factura
        public List<FacturaDetalleDTO> FacturaDetalles { get; set; }
    }
}
