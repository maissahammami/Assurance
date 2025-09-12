using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace domain.Models
{
    [Table("PlansSante")]
    public class PlanSante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlanSanteId { get; set; }

        [Required]
        [StringLength(200)]
        public string Libelle { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual ICollection<DemandeAdhesion> DemandesAdhesion { get; set; }

        public PlanSante()
        {
            DemandesAdhesion = new HashSet<DemandeAdhesion>();
        }
    }
}