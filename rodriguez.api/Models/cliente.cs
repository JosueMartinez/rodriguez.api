namespace rodriguez.api.Models
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [Table("smrodriguez.cliente")]
    public partial class cliente : IUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cliente()
        {
            bonoes = new HashSet<bono>();
            listacompras = new HashSet<listacompra>();
            this.Id = Guid.NewGuid().ToString();
        }
        
        [NotMapped]
        public virtual string Id { get; set; }
        [NotMapped]
        public string UserName
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public int id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(20)]
        public string usuario { get; set; }

        [Required]
        [StringLength(200)]
        public string nombreCompleto { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string nombre { get; set; }

        //[Required]
        //[StringLength(100)]
        //public string apellidos { get; set; }

        [Required]
        [StringLength(11)]
        public string cedula { get; set; }

        [StringLength(10)]
        public string celular { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [Required]
        [NotMapped]
        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped] // Does not effect with your database
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bono> bonoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<listacompra> listacompras { get; set; }
    }
}
