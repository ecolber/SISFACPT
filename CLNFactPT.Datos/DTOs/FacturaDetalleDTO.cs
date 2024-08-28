using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLNFactPT.Datos.DTOs
{
    public class FacturaDetalleDTO
    {
        public int FacturaDetalleId { get; set; }
        public int FacturaId {  get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario   { get; set; }
    }
}
