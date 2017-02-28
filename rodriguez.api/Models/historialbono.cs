namespace rodriguez.api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smrodriguez.historialbono")]
    public partial class historialbono
    {
        public int id { get; set; }

        public int bonoId { get; set; }

        public int estadoBonoId { get; set; }

        public DateTime fechaEntradaEstado { get; set; }

        public DateTime? fechaSalidaEstado { get; set; }

        public virtual bono bono { get; set; }

        public virtual estadobono estadobono { get; set; }
    }
}
