using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionAssociatifERP.Migrations
{
    /// <inheritdoc />
    public partial class EnableCascadeDelete_information_financiere : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "enfant",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    civilite = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    date_naissance = table.Column<DateOnly>(type: "date", nullable: true),
                    presence_fratrie = table.Column<bool>(type: "bit", nullable: true),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    telephone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    ville_naissance = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__enfant__3213E83F21B6F534", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "personne_autorisee",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    telephone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__personne__3213E83FBB21BAC4", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "responsable",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    civilite = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    nom_naissance = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    adresse = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    code_postal = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    ville = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    telephone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    telephone2 = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    numero_alloc = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__responsa__3213E83F3AA359CD", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "donnee_supplementaire",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    enfant_id = table.Column<int>(type: "int", nullable: false),
                    parametre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    valeur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    commentaire = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__donnee_s__3213E83FCB0923C0", x => x.id);
                    table.ForeignKey(
                        name: "FK__donnee_su__enfan__6E01572D",
                        column: x => x.enfant_id,
                        principalTable: "enfant",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "personne_autorisee_enfant",
                columns: table => new
                {
                    personne_autorisee_id = table.Column<int>(type: "int", nullable: false),
                    enfant_id = table.Column<int>(type: "int", nullable: false),
                    affiliation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    contact_urgence = table.Column<bool>(type: "bit", nullable: true),
                    commentaire = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__personne__D627FA0E735E9FB4", x => new { x.personne_autorisee_id, x.enfant_id });
                    table.ForeignKey(
                        name: "FK__personne___enfan__72C60C4A",
                        column: x => x.enfant_id,
                        principalTable: "enfant",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__personne___perso__71D1E811",
                        column: x => x.personne_autorisee_id,
                        principalTable: "personne_autorisee",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "information_financiere",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    responsable_id = table.Column<int>(type: "int", nullable: false),
                    quotient_familiale = table.Column<int>(type: "int", nullable: true),
                    revenu_mensuel = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    revenu_annuel = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    modele = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    date_debut = table.Column<DateOnly>(type: "date", nullable: true),
                    date_fin = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__informat__3213E83F2EC9DD4D", x => x.id);
                    table.ForeignKey(
                        name: "FK__informati__respo__6EF57B66",
                        column: x => x.responsable_id,
                        principalTable: "responsable",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "responsable_enfant",
                columns: table => new
                {
                    responsable_id = table.Column<int>(type: "int", nullable: false),
                    enfant_id = table.Column<int>(type: "int", nullable: false),
                    affiliation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__responsa__CB2A4AEE6C215CCB", x => new { x.responsable_id, x.enfant_id });
                    table.ForeignKey(
                        name: "FK__responsab__enfan__70DDC3D8",
                        column: x => x.enfant_id,
                        principalTable: "enfant",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__responsab__respo__6FE99F9F",
                        column: x => x.responsable_id,
                        principalTable: "responsable",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "situation_personnelle",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    responsable_id = table.Column<int>(type: "int", nullable: false),
                    situation_familiale = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    secteur = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    zone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    regime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__situatio__3213E83F1F894EA8", x => x.id);
                    table.ForeignKey(
                        name: "FK__situation__respo__73BA3083",
                        column: x => x.responsable_id,
                        principalTable: "responsable",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_donnee_supplementaire_enfant_id",
                table: "donnee_supplementaire",
                column: "enfant_id");

            migrationBuilder.CreateIndex(
                name: "UQ__informat__02BC536E4507DFCE",
                table: "information_financiere",
                column: "responsable_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_personne_autorisee_enfant_enfant_id",
                table: "personne_autorisee_enfant",
                column: "enfant_id");

            migrationBuilder.CreateIndex(
                name: "IX_responsable_enfant_enfant_id",
                table: "responsable_enfant",
                column: "enfant_id");

            migrationBuilder.CreateIndex(
                name: "UQ__situatio__02BC536E52B651A0",
                table: "situation_personnelle",
                column: "responsable_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "donnee_supplementaire");

            migrationBuilder.DropTable(
                name: "information_financiere");

            migrationBuilder.DropTable(
                name: "personne_autorisee_enfant");

            migrationBuilder.DropTable(
                name: "responsable_enfant");

            migrationBuilder.DropTable(
                name: "situation_personnelle");

            migrationBuilder.DropTable(
                name: "personne_autorisee");

            migrationBuilder.DropTable(
                name: "enfant");

            migrationBuilder.DropTable(
                name: "responsable");
        }
    }
}
