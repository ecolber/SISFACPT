using CLNFactPT.Dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CLNFactPT.API.DTOs
{
    public class FacturaDetalleAgregarDTO
    {
        public int FacturaId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}
