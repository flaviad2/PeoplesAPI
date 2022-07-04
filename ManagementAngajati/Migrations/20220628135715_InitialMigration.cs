﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementAngajati.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Angajati",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNasterii = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experienta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Angajati", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Posturi",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Functie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetaliuFunctie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Departament = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posturi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Concedii",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AngajatID = table.Column<long>(type: "bigint", nullable: false),
                    DataIncepere = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataTerminare = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concedii", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Concedii_Angajati_AngajatID",
                        column: x => x.AngajatID,
                        principalTable: "Angajati",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AngajatPost",
                columns: table => new
                {
                    AngajatiID = table.Column<long>(type: "bigint", nullable: false),
                    PosturiID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AngajatPost", x => new { x.AngajatiID, x.PosturiID });
                    table.ForeignKey(
                        name: "FK_AngajatPost_Angajati_AngajatiID",
                        column: x => x.AngajatiID,
                        principalTable: "Angajati",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AngajatPost_Posturi_PosturiID",
                        column: x => x.PosturiID,
                        principalTable: "Posturi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IstoricuriAngajati",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AngajatID = table.Column<long>(type: "bigint", nullable: false),
                    PostID = table.Column<long>(type: "bigint", nullable: false),
                    DataAngajare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salariu = table.Column<int>(type: "int", nullable: false),
                    DataReziliere = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IstoricuriAngajati", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IstoricuriAngajati_Angajati_AngajatID",
                        column: x => x.AngajatID,
                        principalTable: "Angajati",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IstoricuriAngajati_Posturi_PostID",
                        column: x => x.PostID,
                        principalTable: "Posturi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AngajatPost_PosturiID",
                table: "AngajatPost",
                column: "PosturiID");

            migrationBuilder.CreateIndex(
                name: "IX_Concedii_AngajatID",
                table: "Concedii",
                column: "AngajatID");

            migrationBuilder.CreateIndex(
                name: "IX_IstoricuriAngajati_AngajatID",
                table: "IstoricuriAngajati",
                column: "AngajatID");

            migrationBuilder.CreateIndex(
                name: "IX_IstoricuriAngajati_PostID",
                table: "IstoricuriAngajati",
                column: "PostID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AngajatPost");

            migrationBuilder.DropTable(
                name: "Concedii");

            migrationBuilder.DropTable(
                name: "IstoricuriAngajati");

            migrationBuilder.DropTable(
                name: "Angajati");

            migrationBuilder.DropTable(
                name: "Posturi");
        }
    }
}
