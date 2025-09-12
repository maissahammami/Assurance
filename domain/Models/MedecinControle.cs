using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace domain.Models
{
    [Table("MedecinControle")]
    public class MedecinControle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ControleurMedicalId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual ICollection<DemandeContreVisite> DemandeContreVisites { get; set; }

        [JsonIgnore]
        public virtual ICollection<RapportMedical> RapportsMedical { get; set; }

        public MedecinControle()
        {
            DemandeContreVisites = new HashSet<DemandeContreVisite>();
            RapportsMedical = new HashSet<RapportMedical>();
        }
    }
}