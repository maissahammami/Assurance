using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace domain.Models
{
    [Table("DemandesAdhesion")]
    public class DemandeAdhesion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DemandeAdhesionId { get; set; }

        [Required]
        public int Statut { get; set; } // Référence à StatutAdhesion enum

        [Required]
        public DateTime DateDemande { get; set; }

        // Foreign keys
        [Required]
        public int AdherentId { get; set; }

        [Required]
        public int PlanSanteId { get; set; }

        public int? AgentId { get; set; }

        public int? CotisationId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual Adherent Adherent { get; set; }

        [JsonIgnore]
        public virtual PlanSante PlanSante { get; set; }

        [JsonIgnore]
        public virtual Agent Agent { get; set; }

        [JsonIgnore]
        public virtual Cotisation Cotisation { get; set; }

        public DemandeAdhesion()
        {
            DateDemande = DateTime.Now;
        }
    }
}