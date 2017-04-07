using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace rodriguez.api.Models
{
    [Table("smrodriguez.tasamoneda")]
    public partial class tasamoneda
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int monedaId { get; set; }

        [Required]
        public double valor { get; set; }

        [Required]
        public DateTime fecha { get; set; }

        [JsonIgnore]
        public virtual moneda moneda { get; set; }
    }
}