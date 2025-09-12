using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace domain.Models
{
    [Table("DemandeContreVisite")]
    public class DemandeContreVisite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DemandeVisiteControleId { get; set; }

        [Required]
        [StringLength(500)]
        public string Motif { get; set; }

        [Required]
        public int Statut { get; set; } // Référence à StatutVisiteControle enum

        [Required]
        public DateTime DateDemande { get; set; }

        // Foreign keys
        [Required]
        public int ControleurMedicalId { get; set; }

        public int? RapportMedicalId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual MedecinControle MedecinControle { get; set; }

        [JsonIgnore]
        public virtual RapportMedical RapportMedical { get; set; }

        [JsonIgnore]
        public virtual ICollection<Reclamation> Reclamations { get; set; }

        public DemandeContreVisite()
        {
            DateDemande = DateTime.Now;
            Reclamations = new HashSet<Reclamation>();
        }
    }
}