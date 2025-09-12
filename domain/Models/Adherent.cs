using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace domain.Models
{
    [Table("Adherents")]
    public class Adherent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdherentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Prenom { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual ICollection<DemandeAdhesion> DemandesAdhesion { get; set; }

        [JsonIgnore]
        public virtual ICollection<Reclamation> Reclamations { get; set; }

        [JsonIgnore]
        public virtual ICollection<Cotisation> Cotisations { get; set; }

        [JsonIgnore]
        public virtual ICollection<Paiement> Paiements { get; set; }

        public Adherent()
        {
            DemandesAdhesion = new HashSet<DemandeAdhesion>();
            Reclamations = new HashSet<Reclamation>();
            Cotisations = new HashSet<Cotisation>();
            Paiements = new HashSet<Paiement>();
        }
    }
}