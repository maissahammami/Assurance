using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace data.Migrations
{
    public partial class Assurance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adherents",
                columns: table => new
                {
                    AdherentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prenom = table.Column<string>(maxLength: 100, nullable: false),
                    Nom = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adherents", x => x.AdherentId);
                });

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    AgentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.AgentId);
                });

            migrationBuilder.CreateTable(
                name: "MedecinControle",
                columns: table => new
                {
                    ControleurMedicalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedecinControle", x => x.ControleurMedicalId);
                });

            migrationBuilder.CreateTable(
                name: "PlansSante",
                columns: table => new
                {
                    PlanSanteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlansSante", x => x.PlanSanteId);
                });

            migrationBuilder.CreateTable(
                name: "DemandeContreVisite",
                columns: table => new
                {
                    DemandeVisiteControleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Motif = table.Column<string>(maxLength: 500, nullable: false),
                    Statut = table.Column<int>(nullable: false),
                    DateDemande = table.Column<DateTime>(nullable: false),
                    ControleurMedicalId = table.Column<int>(nullable: false),
                    RapportMedicalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandeContreVisite", x => x.DemandeVisiteControleId);
                    table.ForeignKey(
                        name: "FK_DemandeContreVisite_MedecinControle_ControleurMedicalId",
                        column: x => x.ControleurMedicalId,
                        principalTable: "MedecinControle",
                        principalColumn: "ControleurMedicalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DemandesAdhesion",
                columns: table => new
                {
                    DemandeAdhesionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Statut = table.Column<int>(nullable: false),
                    DateDemande = table.Column<DateTime>(nullable: false),
                    AdherentId = table.Column<int>(nullable: false),
                    PlanSanteId = table.Column<int>(nullable: false),
                    AgentId = table.Column<int>(nullable: true),
                    CotisationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandesAdhesion", x => x.DemandeAdhesionId);
                    table.ForeignKey(
                        name: "FK_DemandesAdhesion_Adherents_AdherentId",
                        column: x => x.AdherentId,
                        principalTable: "Adherents",
                        principalColumn: "AdherentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DemandesAdhesion_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DemandesAdhesion_PlansSante_PlanSanteId",
                        column: x => x.PlanSanteId,
                        principalTable: "PlansSante",
                        principalColumn: "PlanSanteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RapportsMedical",
                columns: table => new
                {
                    RapportMedicalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Conclusion = table.Column<string>(maxLength: 1000, nullable: false),
                    DateCreation = table.Column<DateTime>(nullable: false),
                    DemandeVisiteControleId = table.Column<int>(nullable: false),
                    ControleurMedicalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RapportsMedical", x => x.RapportMedicalId);
                    table.ForeignKey(
                        name: "FK_RapportsMedical_MedecinControle_ControleurMedicalId",
                        column: x => x.ControleurMedicalId,
                        principalTable: "MedecinControle",
                        principalColumn: "ControleurMedicalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RapportsMedical_DemandeContreVisite_DemandeVisiteControleId",
                        column: x => x.DemandeVisiteControleId,
                        principalTable: "DemandeContreVisite",
                        principalColumn: "DemandeVisiteControleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reclamations",
                columns: table => new
                {
                    ReclamationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    Statut = table.Column<int>(nullable: false),
                    DateCreation = table.Column<DateTime>(nullable: false),
                    AdherentId = table.Column<int>(nullable: false),
                    AgentId = table.Column<int>(nullable: true),
                    DemandeVisiteControleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reclamations", x => x.ReclamationId);
                    table.ForeignKey(
                        name: "FK_Reclamations_Adherents_AdherentId",
                        column: x => x.AdherentId,
                        principalTable: "Adherents",
                        principalColumn: "AdherentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reclamations_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reclamations_DemandeContreVisite_DemandeVisiteControleId",
                        column: x => x.DemandeVisiteControleId,
                        principalTable: "DemandeContreVisite",
                        principalColumn: "DemandeVisiteControleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cotisations",
                columns: table => new
                {
                    CotisationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimeBrute = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartEmployeur = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartSalarie = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Periode = table.Column<DateTime>(nullable: false),
                    AdherentId = table.Column<int>(nullable: false),
                    DemandeAdhesionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotisations", x => x.CotisationId);
                    table.ForeignKey(
                        name: "FK_Cotisations_Adherents_AdherentId",
                        column: x => x.AdherentId,
                        principalTable: "Adherents",
                        principalColumn: "AdherentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cotisations_DemandesAdhesion_DemandeAdhesionId",
                        column: x => x.DemandeAdhesionId,
                        principalTable: "DemandesAdhesion",
                        principalColumn: "DemandeAdhesionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    FactureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(maxLength: 50, nullable: false),
                    Montant = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Statut = table.Column<int>(nullable: false),
                    DateEmission = table.Column<DateTime>(nullable: false),
                    CotisationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.FactureId);
                    table.ForeignKey(
                        name: "FK_Factures_Cotisations_CotisationId",
                        column: x => x.CotisationId,
                        principalTable: "Cotisations",
                        principalColumn: "CotisationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paiements",
                columns: table => new
                {
                    PaiementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Montant = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Methode = table.Column<int>(nullable: false),
                    DatePaiement = table.Column<DateTime>(nullable: false),
                    FactureId = table.Column<int>(nullable: false),
                    AdherentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paiements", x => x.PaiementId);
                    table.ForeignKey(
                        name: "FK_Paiements_Adherents_AdherentId",
                        column: x => x.AdherentId,
                        principalTable: "Adherents",
                        principalColumn: "AdherentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paiements_Factures_FactureId",
                        column: x => x.FactureId,
                        principalTable: "Factures",
                        principalColumn: "FactureId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cotisations_AdherentId",
                table: "Cotisations",
                column: "AdherentId");

            migrationBuilder.CreateIndex(
                name: "IX_Cotisations_DemandeAdhesionId",
                table: "Cotisations",
                column: "DemandeAdhesionId",
                unique: true,
                filter: "[DemandeAdhesionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DemandeContreVisite_ControleurMedicalId",
                table: "DemandeContreVisite",
                column: "ControleurMedicalId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandesAdhesion_AdherentId",
                table: "DemandesAdhesion",
                column: "AdherentId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandesAdhesion_AgentId",
                table: "DemandesAdhesion",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandesAdhesion_PlanSanteId",
                table: "DemandesAdhesion",
                column: "PlanSanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_CotisationId",
                table: "Factures",
                column: "CotisationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paiements_AdherentId",
                table: "Paiements",
                column: "AdherentId");

            migrationBuilder.CreateIndex(
                name: "IX_Paiements_FactureId",
                table: "Paiements",
                column: "FactureId");

            migrationBuilder.CreateIndex(
                name: "IX_RapportsMedical_ControleurMedicalId",
                table: "RapportsMedical",
                column: "ControleurMedicalId");

            migrationBuilder.CreateIndex(
                name: "IX_RapportsMedical_DemandeVisiteControleId",
                table: "RapportsMedical",
                column: "DemandeVisiteControleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reclamations_AdherentId",
                table: "Reclamations",
                column: "AdherentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reclamations_AgentId",
                table: "Reclamations",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reclamations_DemandeVisiteControleId",
                table: "Reclamations",
                column: "DemandeVisiteControleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paiements");

            migrationBuilder.DropTable(
                name: "RapportsMedical");

            migrationBuilder.DropTable(
                name: "Reclamations");

            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "DemandeContreVisite");

            migrationBuilder.DropTable(
                name: "Cotisations");

            migrationBuilder.DropTable(
                name: "MedecinControle");

            migrationBuilder.DropTable(
                name: "DemandesAdhesion");

            migrationBuilder.DropTable(
                name: "Adherents");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "PlansSante");
        }
    }
}
