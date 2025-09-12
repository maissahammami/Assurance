using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace domain.Models
{
    [Table("Reclamations")]
    public class Reclamation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReclamationId { get; set; }

        [Required]
        public int Type { get; set; } // Référence à TypeReclamation enum

        [Required]
        public int Statut { get; set; } // Référence à StatutReclamation enum

        [Required]
        public DateTime DateCreation { get; set; }

        // Foreign keys
        [Required]
        public int AdherentId { get; set; }

        public int? AgentId { get; set; }

        public int? DemandeVisiteControleId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual Adherent Adherent { get; set; }

        [JsonIgnore]
        public virtual Agent Agent { get; set; }

        [JsonIgnore]
        public virtual DemandeContreVisite DemandeContreVisite { get; set; }

        public Reclamation()
        {
            DateCreation = DateTime.Now;
        }
    }
}