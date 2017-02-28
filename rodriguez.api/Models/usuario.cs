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

        [Required]
        [StringLength(40)]
        public string nombres { get; set; }

        [Required]
        [StringLength(80)]
        public string apellidos { get; set; }

        [Required]
        [StringLength(20)]
        public string nombreUsuario { get; set; }

        [Required]
        [StringLength(20)]
        public string contrasena { get; set; }

        public bool activo { get; set; }

        public int rolId { get; set; }

        public virtual rol rol { get; set; }
    }
}
