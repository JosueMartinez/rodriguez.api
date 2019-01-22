using System;

namespace Rodriguez.Data.Models
{
    public class HistorialBono
    {
        public int Id { get; set; }

        public int BonoId { get; set; }
        public Bono Bono { get; set; }

        public int EstadoBonoId { get; set; }
        public EstadoBono EstadoBono { get; set; }

        public DateTime FechaEntradaEstado { get; set; }

        public DateTime? FechaSalidaEstado { get; set; }
    }
}