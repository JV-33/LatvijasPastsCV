using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LatvijasPastsCV.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adreses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Valsts = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Indekss = table.Column<int>(type: "INTEGER", nullable: false),
                    Pilseta = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Iela = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Numurs = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adreses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pamatdati",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Vards = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Uzvards = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Talrunis = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    EPasts = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pamatdati", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CV",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PamatdatiID = table.Column<int>(type: "INTEGER", nullable: false),
                    AdreseID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CV", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CV_Adreses_AdreseID",
                        column: x => x.AdreseID,
                        principalTable: "Adreses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CV_Pamatdati_PamatdatiID",
                        column: x => x.PamatdatiID,
                        principalTable: "Pamatdati",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DarbaPieredze",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Vieta = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Nosaukums = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    IenemamaisAmats = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    SlodzesApmers = table.Column<int>(type: "INTEGER", nullable: false),
                    DarbaStazs = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Stazs = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Amats = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CVID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DarbaPieredze", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DarbaPieredze_CV_CVID",
                        column: x => x.CVID,
                        principalTable: "CV",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Izglitiba",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nosaukums = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Fakultate = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    StudijuVirziens = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    IzglitibasLimenis = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Statuss = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Iestade = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    MacibasLaiks = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CVID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izglitiba", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Izglitiba_CV_CVID",
                        column: x => x.CVID,
                        principalTable: "CV",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Prasmes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Apraksts = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Veids = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CVID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prasmes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Prasmes_CV_CVID",
                        column: x => x.CVID,
                        principalTable: "CV",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CV_AdreseID",
                table: "CV",
                column: "AdreseID");

            migrationBuilder.CreateIndex(
                name: "IX_CV_PamatdatiID",
                table: "CV",
                column: "PamatdatiID");

            migrationBuilder.CreateIndex(
                name: "IX_DarbaPieredze_CVID",
                table: "DarbaPieredze",
                column: "CVID");

            migrationBuilder.CreateIndex(
                name: "IX_Izglitiba_CVID",
                table: "Izglitiba",
                column: "CVID");

            migrationBuilder.CreateIndex(
                name: "IX_Prasmes_CVID",
                table: "Prasmes",
                column: "CVID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DarbaPieredze");

            migrationBuilder.DropTable(
                name: "Izglitiba");

            migrationBuilder.DropTable(
                name: "Prasmes");

            migrationBuilder.DropTable(
                name: "CV");

            migrationBuilder.DropTable(
                name: "Adreses");

            migrationBuilder.DropTable(
                name: "Pamatdati");
        }
    }
}