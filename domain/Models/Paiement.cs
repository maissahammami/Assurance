using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace domain.Models
{
    [Table("Paiements")]
    public class Paiement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaiementId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Montant { get; set; }

        [Required]
        public int Methode { get; set; } // Référence à MethodePaiement enum

        [Required]
        public DateTime DatePaiement { get; set; }

        // Foreign keys
        [Required]
        public int FactureId { get; set; }

        [Required]
        public int AdherentId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual Facture Facture { get; set; }

        [JsonIgnore]
        public virtual Adherent Adherent { get; set; }

        public Paiement()
        {
            DatePaiement = DateTime.Now;
        }
    }
}