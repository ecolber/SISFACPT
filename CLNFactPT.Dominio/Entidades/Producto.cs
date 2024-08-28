using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLNFactPT.Dominio.Entidades
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Descripcion es requerido")]
        [MaxLength(200)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "SKU es requerido")]
        [MaxLength(15)]
        public string SKU { get; set; }

        [Required(ErrorMessage = "Precio (en dolares) es requerido")]
        public decimal PrecioDolar { get; set; }

        [Required(ErrorMessage = "Estado es requerido")]
        public bool Estado { get; set; }

    }
}
