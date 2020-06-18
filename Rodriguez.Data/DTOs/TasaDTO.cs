using System;

namespace Rodriguez.Data.DTOs
{
    public class TasaDto
    {
        public double Valor { get; set; }
        public DateTime Fecha { get; set; }
        public string Simbolo { get; set; }
        public string Moneda { get; set; }
    }
}