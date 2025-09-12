using Microsoft.EntityFrameworkCore;
using domain.Models;
using System;

namespace domain.Data
{
    public class AssuranceDbContext : DbContext
    {
        public AssuranceDbContext(DbContextOptions<AssuranceDbContext> options) : base(options)
        {
        }

        public DbSet<Adherent> Adherents { get; set; }
        public DbSet<PlanSante> PlansSante { get; set; }
        public DbSet<DemandeAdhesion> DemandesAdhesion { get; set; }
        public DbSet<DemandeContreVisite> DemandesContreVisite { get; set; }
        public DbSet<MedecinControle> MedecinsControle { get; set; }
        public DbSet<RapportMedical> RapportsMedical { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Cotisation> Cotisations { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<Paiement> Paiements { get; set; }
        public DbSet<Reclamation> Reclamations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration des relations et contraintes

            // Adherent
            modelBuilder.Entity<Adherent>()
                .HasMany(a => a.DemandesAdhesion)
                .WithOne(d => d.Adherent)
                .HasForeignKey(d => d.AdherentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Adherent>()
                .HasMany(a => a.Reclamations)
                .WithOne(r => r.Adherent)
                .HasForeignKey(r => r.AdherentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Adherent>()
                .HasMany(a => a.Cotisations)
                .WithOne(c => c.Adherent)
                .HasForeignKey(c => c.AdherentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Adherent>()
                .HasMany(a => a.Paiements)
                .WithOne(p => p.Adherent)
                .HasForeignKey(p => p.AdherentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Agent
            modelBuilder.Entity<Agent>()
                .HasMany(a => a.DemandesAdhesion)
                .WithOne(d => d.Agent)
                .HasForeignKey(d => d.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Agent>()
                .HasMany(a => a.Reclamations)
                .WithOne(r => r.Agent)
                .HasForeignKey(r => r.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            // PlanSante
            modelBuilder.Entity<PlanSante>()
                .HasMany(p => p.DemandesAdhesion)
                .WithOne(d => d.PlanSante)
                .HasForeignKey(d => d.PlanSanteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cotisation
            modelBuilder.Entity<Cotisation>()
                .HasOne(c => c.DemandeAdhesion)
                .WithMany()
                .HasForeignKey(c => c.DemandeAdhesionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cotisation>()
                .HasOne(c => c.Facture)
                .WithOne(f => f.Cotisation)
                .HasForeignKey<Facture>(f => f.CotisationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Facture - Paiements
            modelBuilder.Entity<Facture>()
                .HasMany(f => f.Paiements)
                .WithOne(p => p.Facture)
                .HasForeignKey(p => p.FactureId)
                .OnDelete(DeleteBehavior.NoAction); // ← IMPORTANT
            //.OnDelete(DeleteBehavior.Restrict); // ← Ou DeleteBehavior.NoAction
            //.OnDelete(DeleteBehavior.Cascade);

            // DemandeAdhesion - Cotisation
            modelBuilder.Entity<DemandeAdhesion>()
                .HasOne(d => d.Cotisation)
                .WithOne(c => c.DemandeAdhesion)
                .HasForeignKey<Cotisation>(c => c.DemandeAdhesionId)
                .OnDelete(DeleteBehavior.Restrict);

            // MedecinControle
            modelBuilder.Entity<MedecinControle>()
                .HasMany(m => m.DemandeContreVisites)
                .WithOne(d => d.MedecinControle)
                .HasForeignKey(d => d.ControleurMedicalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedecinControle>()
                .HasMany(m => m.RapportsMedical)
                .WithOne(r => r.MedecinControle)
                .HasForeignKey(r => r.ControleurMedicalId)
                .OnDelete(DeleteBehavior.Restrict);

            // DemandeContreVisite
            modelBuilder.Entity<DemandeContreVisite>()
                .HasOne(d => d.RapportMedical)
                .WithOne(r => r.DemandeContreVisite)
                .HasForeignKey<RapportMedical>(r => r.DemandeVisiteControleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DemandeContreVisite>()
                .HasMany(d => d.Reclamations)
                .WithOne(r => r.DemandeContreVisite)
                .HasForeignKey(r => r.DemandeVisiteControleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Reclamation
            modelBuilder.Entity<Reclamation>()
                .HasOne(r => r.DemandeContreVisite)
                .WithMany(d => d.Reclamations)
                .HasForeignKey(r => r.DemandeVisiteControleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration des tailles de colonnes
            modelBuilder.Entity<Adherent>()
                .Property(a => a.Prenom)
                .HasMaxLength(100);

            modelBuilder.Entity<Adherent>()
                .Property(a => a.Nom)
                .HasMaxLength(100);

            modelBuilder.Entity<Agent>()
                .Property(a => a.Nom)
                .HasMaxLength(100);

            modelBuilder.Entity<PlanSante>()
                .Property(p => p.Libelle)
                .HasMaxLength(200);

            modelBuilder.Entity<DemandeContreVisite>()
                .Property(d => d.Motif)
                .HasMaxLength(500);

            modelBuilder.Entity<MedecinControle>()
                .Property(m => m.Nom)
                .HasMaxLength(100);

            modelBuilder.Entity<RapportMedical>()
                .Property(r => r.Conclusion)
                .HasMaxLength(1000);

            modelBuilder.Entity<Facture>()
                .Property(f => f.Numero)
                .HasMaxLength(50);

            // Configuration des types décimaux
            modelBuilder.Entity<Cotisation>()
                .Property(c => c.PrimeBrute)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Cotisation>()
                .Property(c => c.PartEmployeur)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Cotisation>()
                .Property(c => c.PartSalarie)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Facture>()
                .Property(f => f.Montant)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Paiement>()
                .Property(p => p.Montant)
                .HasColumnType("decimal(18,2)");
        }
    }
}