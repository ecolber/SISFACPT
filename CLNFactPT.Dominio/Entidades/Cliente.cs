using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLNFactPT.Dominio.Entidades
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Codigo es requerido")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Apellido es requerido")]
        public string Apellido { get; set; }

    }
}
