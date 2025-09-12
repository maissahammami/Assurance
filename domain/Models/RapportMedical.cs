using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace domain.Models
{
    [Table("RapportsMedical")]
    public class RapportMedical
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RapportMedicalId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Conclusion { get; set; }

        [Required]
        public DateTime DateCreation { get; set; }

        // Foreign keys
        [Required]
        public int DemandeVisiteControleId { get; set; }

        [Required]
        public int ControleurMedicalId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual DemandeContreVisite DemandeContreVisite { get; set; }

        [JsonIgnore]
        public virtual MedecinControle MedecinControle { get; set; }

        public RapportMedical()
        {
            DateCreation = DateTime.Now;
        }
    }
}