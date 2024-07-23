using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLager.Migrations
{
    /// <inheritdoc />
    public partial class M : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "anställda",
                columns: table => new
                {
                    anställdID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lösenord = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anställda", x => x.anställdID);
                });

            migrationBuilder.CreateTable(
                name: "DateRange",
                columns: table => new
                {
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "företagkunder",
                columns: table => new
                {
                    företagkundID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kreditGräns = table.Column<float>(type: "real", nullable: false),
                    rabatt = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_företagkunder", x => x.företagkundID);
                });

            migrationBuilder.CreateTable(
                name: "kunder",
                columns: table => new
                {
                    kundID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kreditGräns = table.Column<float>(type: "real", nullable: false),
                    rabatt = table.Column<float>(type: "real", nullable: false),
                    faktureringsAddress = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kunder", x => x.kundID);
                });

            migrationBuilder.CreateTable(
                name: "lektioner",
                columns: table => new
                {
                    skolaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Typ = table.Column<int>(type: "int", nullable: false),
                    LärareID = table.Column<int>(type: "int", nullable: false),
                    dagar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tillgänglig = table.Column<bool>(type: "bit", nullable: false),
                    pris = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lektioner", x => x.skolaID);
                });

            migrationBuilder.CreateTable(
                name: "logialer",
                columns: table => new
                {
                    logiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriserPerVecka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sönfre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fresön = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pris = table.Column<int>(type: "int", nullable: false),
                    tillgänglig = table.Column<bool>(type: "bit", nullable: false),
                    storlek = table.Column<int>(type: "int", nullable: false),
                    Typ = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logialer", x => x.logiID);
                });

            migrationBuilder.CreateTable(
                name: "utrustningar",
                columns: table => new
                {
                    utrustningID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Typ = table.Column<int>(type: "int", nullable: false),
                    AlpintPaket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NilaPulka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkoterLynx50 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hjälm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkorSnowbord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Snowbord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaketSnowbord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LängdPaket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LängdStavar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LängdPjäxor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LängdSkidor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlpintStavar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlpintPjäxor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlpintSkidor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    benämning = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utrustningar", x => x.utrustningID);
                });

            migrationBuilder.CreateTable(
                name: "konferensBokningar",
                columns: table => new
                {
                    KonferensBokningID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtlämningsDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ÅterlämningsDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FöretagKundNrföretagkundID = table.Column<int>(type: "int", nullable: false),
                    Aktiv = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_konferensBokningar", x => x.KonferensBokningID);
                    table.ForeignKey(
                        name: "FK_konferensBokningar_företagkunder_FöretagKundNrföretagkundID",
                        column: x => x.FöretagKundNrföretagkundID,
                        principalTable: "företagkunder",
                        principalColumn: "företagkundID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bokningar",
                columns: table => new
                {
                    BokningID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtlämningsDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ÅterlämningsDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPris = table.Column<int>(type: "int", nullable: false),
                    KundNrkundID = table.Column<int>(type: "int", nullable: false),
                    Aktiv = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bokningar", x => x.BokningID);
                    table.ForeignKey(
                        name: "FK_bokningar_kunder_KundNrkundID",
                        column: x => x.KundNrkundID,
                        principalTable: "kunder",
                        principalColumn: "kundID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "skidshopbokningar",
                columns: table => new
                {
                    SkidshopBokningID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sluttid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KundNrkundID = table.Column<int>(type: "int", nullable: false),
                    Aktiv = table.Column<bool>(type: "bit", nullable: false),
                    TotalPris = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skidshopbokningar", x => x.SkidshopBokningID);
                    table.ForeignKey(
                        name: "FK_skidshopbokningar_kunder_KundNrkundID",
                        column: x => x.KundNrkundID,
                        principalTable: "kunder",
                        principalColumn: "kundID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "konferenslokaler",
                columns: table => new
                {
                    konferensID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Typ = table.Column<int>(type: "int", nullable: false),
                    storlek = table.Column<int>(type: "int", nullable: false),
                    beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tillgänglig = table.Column<bool>(type: "bit", nullable: false),
                    PrisPerVeckaLokal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrisPerDygnLokal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrisPerTimLokal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KonferensBokningViewKonferensBokningID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_konferenslokaler", x => x.konferensID);
                    table.ForeignKey(
                        name: "FK_konferenslokaler_konferensBokningar_KonferensBokningViewKonferensBokningID",
                        column: x => x.KonferensBokningViewKonferensBokningID,
                        principalTable: "konferensBokningar",
                        principalColumn: "KonferensBokningID");
                });

            migrationBuilder.CreateTable(
                name: "bokningsRad",
                columns: table => new
                {
                    BokningRadLogialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BokningID = table.Column<int>(type: "int", nullable: false),
                    LogialID = table.Column<int>(type: "int", nullable: false),
                    startTid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    slutTid = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bokningsRad", x => x.BokningRadLogialID);
                    table.ForeignKey(
                        name: "FK_bokningsRad_bokningar_BokningID",
                        column: x => x.BokningID,
                        principalTable: "bokningar",
                        principalColumn: "BokningID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bokningsRad_logialer_LogialID",
                        column: x => x.LogialID,
                        principalTable: "logialer",
                        principalColumn: "logiID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BokningsRadSkidskola",
                columns: table => new
                {
                    SkidskolaRadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BokningID = table.Column<int>(type: "int", nullable: false),
                    SKidskolaID = table.Column<int>(type: "int", nullable: false),
                    BokningMottagareViewBokningID = table.Column<int>(type: "int", nullable: false),
                    startTid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    slutTid = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BokningsRadSkidskola", x => x.SkidskolaRadID);
                    table.ForeignKey(
                        name: "FK_BokningsRadSkidskola_bokningar_BokningMottagareViewBokningID",
                        column: x => x.BokningMottagareViewBokningID,
                        principalTable: "bokningar",
                        principalColumn: "BokningID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BokningsRadSkidskola_lektioner_SKidskolaID",
                        column: x => x.SKidskolaID,
                        principalTable: "lektioner",
                        principalColumn: "skolaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BokningsRadUtrustning",
                columns: table => new
                {
                    UtrustningsRadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BokningID = table.Column<int>(type: "int", nullable: false),
                    UtrustningID = table.Column<int>(type: "int", nullable: false),
                    BokningMottagareViewBokningID = table.Column<int>(type: "int", nullable: false),
                    startTid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    slutTid = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BokningsRadUtrustning", x => x.UtrustningsRadID);
                    table.ForeignKey(
                        name: "FK_BokningsRadUtrustning_bokningar_BokningMottagareViewBokningID",
                        column: x => x.BokningMottagareViewBokningID,
                        principalTable: "bokningar",
                        principalColumn: "BokningID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BokningsRadUtrustning_utrustningar_UtrustningID",
                        column: x => x.UtrustningID,
                        principalTable: "utrustningar",
                        principalColumn: "utrustningID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkidshopBokningsRadSkidskola",
                columns: table => new
                {
                    RadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SKidskolaID = table.Column<int>(type: "int", nullable: false),
                    SkidshopBokningID = table.Column<int>(type: "int", nullable: false),
                    BokningSkidshopViewSkidshopBokningID = table.Column<int>(type: "int", nullable: false),
                    startTid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    slutTid = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkidshopBokningsRadSkidskola", x => x.RadID);
                    table.ForeignKey(
                        name: "FK_SkidshopBokningsRadSkidskola_lektioner_SKidskolaID",
                        column: x => x.SKidskolaID,
                        principalTable: "lektioner",
                        principalColumn: "skolaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkidshopBokningsRadSkidskola_skidshopbokningar_BokningSkidshopViewSkidshopBokningID",
                        column: x => x.BokningSkidshopViewSkidshopBokningID,
                        principalTable: "skidshopbokningar",
                        principalColumn: "SkidshopBokningID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkidshopBokningsRadUtrustning",
                columns: table => new
                {
                    RadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtrusningsID = table.Column<int>(type: "int", nullable: false),
                    SkidshopBokningID = table.Column<int>(type: "int", nullable: false),
                    BokningSkidshopViewSkidshopBokningID = table.Column<int>(type: "int", nullable: false),
                    utrustningID = table.Column<int>(type: "int", nullable: false),
                    startTid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    slutTid = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkidshopBokningsRadUtrustning", x => x.RadID);
                    table.ForeignKey(
                        name: "FK_SkidshopBokningsRadUtrustning_skidshopbokningar_BokningSkidshopViewSkidshopBokningID",
                        column: x => x.BokningSkidshopViewSkidshopBokningID,
                        principalTable: "skidshopbokningar",
                        principalColumn: "SkidshopBokningID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkidshopBokningsRadUtrustning_utrustningar_utrustningID",
                        column: x => x.utrustningID,
                        principalTable: "utrustningar",
                        principalColumn: "utrustningID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KonferensBokningsRad",
                columns: table => new
                {
                    KonferensBokningRadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KonferensBokningID = table.Column<int>(type: "int", nullable: false),
                    KonferenslokalID = table.Column<int>(type: "int", nullable: false),
                    startTid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    slutTid = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KonferensBokningsRad", x => x.KonferensBokningRadID);
                    table.ForeignKey(
                        name: "FK_KonferensBokningsRad_konferensBokningar_KonferensBokningID",
                        column: x => x.KonferensBokningID,
                        principalTable: "konferensBokningar",
                        principalColumn: "KonferensBokningID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KonferensBokningsRad_konferenslokaler_KonferenslokalID",
                        column: x => x.KonferenslokalID,
                        principalTable: "konferenslokaler",
                        principalColumn: "konferensID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bokningar_KundNrkundID",
                table: "bokningar",
                column: "KundNrkundID");

            migrationBuilder.CreateIndex(
                name: "IX_bokningsRad_BokningID",
                table: "bokningsRad",
                column: "BokningID");

            migrationBuilder.CreateIndex(
                name: "IX_bokningsRad_LogialID",
                table: "bokningsRad",
                column: "LogialID");

            migrationBuilder.CreateIndex(
                name: "IX_BokningsRadSkidskola_BokningMottagareViewBokningID",
                table: "BokningsRadSkidskola",
                column: "BokningMottagareViewBokningID");

            migrationBuilder.CreateIndex(
                name: "IX_BokningsRadSkidskola_SKidskolaID",
                table: "BokningsRadSkidskola",
                column: "SKidskolaID");

            migrationBuilder.CreateIndex(
                name: "IX_BokningsRadUtrustning_BokningMottagareViewBokningID",
                table: "BokningsRadUtrustning",
                column: "BokningMottagareViewBokningID");

            migrationBuilder.CreateIndex(
                name: "IX_BokningsRadUtrustning_UtrustningID",
                table: "BokningsRadUtrustning",
                column: "UtrustningID");

            migrationBuilder.CreateIndex(
                name: "IX_konferensBokningar_FöretagKundNrföretagkundID",
                table: "konferensBokningar",
                column: "FöretagKundNrföretagkundID");

            migrationBuilder.CreateIndex(
                name: "IX_KonferensBokningsRad_KonferensBokningID",
                table: "KonferensBokningsRad",
                column: "KonferensBokningID");

            migrationBuilder.CreateIndex(
                name: "IX_KonferensBokningsRad_KonferenslokalID",
                table: "KonferensBokningsRad",
                column: "KonferenslokalID");

            migrationBuilder.CreateIndex(
                name: "IX_konferenslokaler_KonferensBokningViewKonferensBokningID",
                table: "konferenslokaler",
                column: "KonferensBokningViewKonferensBokningID");

            migrationBuilder.CreateIndex(
                name: "IX_skidshopbokningar_KundNrkundID",
                table: "skidshopbokningar",
                column: "KundNrkundID");

            migrationBuilder.CreateIndex(
                name: "IX_SkidshopBokningsRadSkidskola_BokningSkidshopViewSkidshopBokningID",
                table: "SkidshopBokningsRadSkidskola",
                column: "BokningSkidshopViewSkidshopBokningID");

            migrationBuilder.CreateIndex(
                name: "IX_SkidshopBokningsRadSkidskola_SKidskolaID",
                table: "SkidshopBokningsRadSkidskola",
                column: "SKidskolaID");

            migrationBuilder.CreateIndex(
                name: "IX_SkidshopBokningsRadUtrustning_BokningSkidshopViewSkidshopBokningID",
                table: "SkidshopBokningsRadUtrustning",
                column: "BokningSkidshopViewSkidshopBokningID");

            migrationBuilder.CreateIndex(
                name: "IX_SkidshopBokningsRadUtrustning_utrustningID",
                table: "SkidshopBokningsRadUtrustning",
                column: "utrustningID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "anställda");

            migrationBuilder.DropTable(
                name: "bokningsRad");

            migrationBuilder.DropTable(
                name: "BokningsRadSkidskola");

            migrationBuilder.DropTable(
                name: "BokningsRadUtrustning");

            migrationBuilder.DropTable(
                name: "DateRange");

            migrationBuilder.DropTable(
                name: "KonferensBokningsRad");

            migrationBuilder.DropTable(
                name: "SkidshopBokningsRadSkidskola");

            migrationBuilder.DropTable(
                name: "SkidshopBokningsRadUtrustning");

            migrationBuilder.DropTable(
                name: "logialer");

            migrationBuilder.DropTable(
                name: "bokningar");

            migrationBuilder.DropTable(
                name: "konferenslokaler");

            migrationBuilder.DropTable(
                name: "lektioner");

            migrationBuilder.DropTable(
                name: "skidshopbokningar");

            migrationBuilder.DropTable(
                name: "utrustningar");

            migrationBuilder.DropTable(
                name: "konferensBokningar");

            migrationBuilder.DropTable(
                name: "kunder");

            migrationBuilder.DropTable(
                name: "företagkunder");
        }
    }
}
