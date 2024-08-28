using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLNFactPT.Dominio.Entidades
{
    public class FacturaDetalle
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Factura es requerido")]
        public int FacturaId { get; set; }

        [ForeignKey("FacturaId")]
        public Factura Factura { get; set; }

        [Required(ErrorMessage = "Producto es requerido")]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }

        [Required(ErrorMessage = "Cantidad es requerida")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Precio es requerido")]
        public decimal PrecioUnitario { get; set; }
    }
}
