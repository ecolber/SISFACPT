using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLNFactPT.Dominio.Entidades
{
    public class Factura
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numero de factura es requerida")]
        [MaxLength(20)]
        public string NumeroFactura { get; set; }

        [Required(ErrorMessage = "Fecha es requerida")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Cliente es requerido")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Moneda es requerida")]
        public int MonedaId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }

        public ICollection<FacturaDetalle> FacturaDetalles { get; set; }
    }
}
