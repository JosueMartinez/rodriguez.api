namespace rodriguez.api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smrodriguez.usuario")]
    public partial class usuario
    {
        public int id { get; set; }

        [StringLength(40)]
        public string nombres { get; set; }

        [StringLength(80)]
        public string apellidos { get; set; }

        [StringLength(20)]
        public string nombre_usuario { get; set; }

        [StringLength(20)]
        public string contrasena { get; set; }

        public bool? activo { get; set; }

        public int? rol { get; set; }

        public virtual rol rol1 { get; set; }
    }
}
