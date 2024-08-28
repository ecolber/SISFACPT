using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLNFactPT.Dominio.Entidades
{
    public class TasaCambio
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Fecha es requerida")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Tasa de cambio es requerida")]
        public decimal TasaDeCambio { get; set; }


    }
}
