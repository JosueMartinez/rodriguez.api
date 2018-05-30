namespace rodriguez.api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    
    public partial class HistorialBono
    {
        public int Id { get; set; }

        public int BonoId { get; set; }

        public int EstadoBonoId { get; set; }

        public DateTime FechaEntradaEstado { get; set; }

        public DateTime? FechaSalidaEstado { get; set; }

        public virtual Bono Bono { get; set; }

        public virtual EstadoBono Estadobono { get; set; }
    }
}
