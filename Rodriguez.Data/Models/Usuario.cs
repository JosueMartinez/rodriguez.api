using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rodriguez.Data.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string NombreCompleto { get; set; }

        [Required]
        [StringLength(20)]
        [Index(IsUnique = true)]
        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Contrasena", ErrorMessage = "Las contrasenas deben ser iguales")]
        public string ConfirmarContrasena { get; set; }

        public bool Activo { get; set; }

        public int RolId { get; set; }

        public virtual Rol rol { get; set; }
    }
}