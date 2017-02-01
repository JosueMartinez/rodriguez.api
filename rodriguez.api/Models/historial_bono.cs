namespace rodriguez.api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smrodriguez.historial_bono")]
    public partial class historial_bono
    {
        public int id { get; set; }

        public int bono { get; set; }

        public int estado { get; set; }

        public DateTime fecha_entrada_estado { get; set; }

        public DateTime? fecha_salida_estado { get; set; }

        public virtual bono bono1 { get; set; }

        public virtual estado_bono estado_bono { get; set; }
    }
}
