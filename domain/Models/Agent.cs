using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace domain.Models
{
    [Table("Agents")]
    public class Agent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AgentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual ICollection<DemandeAdhesion> DemandesAdhesion { get; set; }

        [JsonIgnore]
        public virtual ICollection<Reclamation> Reclamations { get; set; }

        public Agent()
        {
            DemandesAdhesion = new HashSet<DemandeAdhesion>();
            Reclamations = new HashSet<Reclamation>();
        }
    }
}