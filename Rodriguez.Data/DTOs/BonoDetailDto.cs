using System;

namespace Rodriguez.Data.DTOs
{
    public class BonoDetailDto
    {
        public int Id { get; set; }
        public string Destinatario { get; set; }
        public string Remitente { get; set; }
        public string CedulaDestino { get; set; }
        public DateTime FechaCompra { get; set; }
        public double Monto { get; set; }
        public string SimboloMonedaOriginal { get; set; }
        public string EstadoBono { get; set; }
        public double Tasa { get; set; }
    }
}