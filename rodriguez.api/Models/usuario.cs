namespace rodriguez.api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    
    public partial class Usuario
    {
        public int Id { get; set; }

        //[required]
        //[stringlength(40)]
        //public string nombres { get; set; }

        //[required]
        //[stringlength(80)]
        //public string apellidos { get; set; }

        [Required]
        [StringLength(200)]
        public string NombreCompleto { get; set; }

        [Required]
        [StringLength(20)]
        [Index(IsUnique = true)]
        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(20,MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Contrasena", ErrorMessage = "Las contrasenas deben ser iguales")]
        public string ConfirmarContrasena { get; set; }

        public bool Activo { get; set; }

        public int RolId { get; set; }

        public virtual Rol Rol { get; set; }
    }
}
