using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLNFactPT.Datos.DTOs
{
    public class ProductoCnPrecioCordobaDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string SKU { get; set; }
        public decimal PrecioDolar { get; set; }

        public decimal PrecioCordobas { get; set; }
        public bool Estado { get; set; }
        
    }
}
