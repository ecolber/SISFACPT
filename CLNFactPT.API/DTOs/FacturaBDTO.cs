using CLNFactPT.Dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CLNFactPT.API.DTOs
{
    public class FacturaBDTO
    {
        public int Id { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public int MonedaId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }

    }
}
