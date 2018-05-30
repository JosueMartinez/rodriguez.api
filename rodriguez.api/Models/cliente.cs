namespace rodriguez.api.Models
{
    using DevelopersDo.DataAnnotations;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    public partial class Cliente : IUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            Bonos = new HashSet<Bono>();
            Listas = new HashSet<ListaCompra>();
            this.Id = Guid.NewGuid().ToString();
        }
        
        [NotMapped]
        public virtual string Id { get; set; }
        [NotMapped]
        public string UserName
        {
            get { return Usuario; }
            set { Usuario = value; }
        }

        public int ClienteId { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(20)]
        public string Usuario { get; set; }

        [Required]
        [StringLength(200)]
        public string NombreCompleto { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string nombre { get; set; }

        //[Required]
        //[StringLength(100)]
        //public string apellidos { get; set; }

        [Required]
        [StringLength(11)]
        [Cedula]
        public string Cedula { get; set; }

        [StringLength(10)]
        public string Celular { get; set; }

        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [NotMapped]
        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped] // Does not effect with your database
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bono> Bonos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListaCompra> Listas { get; set; }
    }
}
