using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace domain.Models
{
    [Table("Factures")]
    public class Facture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FactureId { get; set; }

        [Required]
        [StringLength(50)]
        public string Numero { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Montant { get; set; }

        [Required]
        public int Statut { get; set; } // Référence à StatutFacture enum

        [Required]
        public DateTime DateEmission { get; set; }

        // Foreign keys
        [Required]
        public int CotisationId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual Cotisation Cotisation { get; set; }

        [JsonIgnore]
        public virtual ICollection<Paiement> Paiements { get; set; }

        public Facture()
        {
            DateEmission = DateTime.Now;
            Paiements = new HashSet<Paiement>();
        }
    }
}