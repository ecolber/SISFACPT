using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLNFactPT.Datos.DTOs
{
    public class RptFacturaDTO
    {
        public string CodigoCliente { get; set; }
        public string Nombre { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public decimal TotalDolar { get; set; }
        public decimal TotalCordobas { get; set; }
        public string Producto { get; set; }
        public string SKU { get; set; }

    }
}
