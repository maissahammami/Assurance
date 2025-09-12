using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace domain.Models
{
    [Table("Cotisations")]
    public class Cotisation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CotisationId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrimeBrute { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PartEmployeur { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PartSalarie { get; set; }

        [Required]
        public DateTime Periode { get; set; }

        // Foreign keys
        [Required]
        public int AdherentId { get; set; }

        public int? DemandeAdhesionId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual Adherent Adherent { get; set; }

        [JsonIgnore]
        public virtual DemandeAdhesion DemandeAdhesion { get; set; }

        [JsonIgnore]
        public virtual Facture Facture { get; set; }

        public Cotisation()
        {
            Periode = DateTime.Now;
        }
    }
}