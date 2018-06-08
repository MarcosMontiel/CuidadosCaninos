using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Cuidados.Caninos.Marcos.Montiel.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComCatEscolaridad",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComCatEscolaridad", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ComCatSexo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComCatSexo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ComPersona",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AMaterno = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    APaterno = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Curp = table.Column<string>(type: "TEXT", maxLength: 18, nullable: false),
                    FKComCatEscolaridad = table.Column<int>(type: "INTEGER", nullable: false),
                    FKComCatSexo = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaNac = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComPersona", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ComPersona_ComCatEscolaridad_FKComCatEscolaridad",
                        column: x => x.FKComCatEscolaridad,
                        principalTable: "ComCatEscolaridad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComPersona_ComCatSexo_FKComCatSexo",
                        column: x => x.FKComCatSexo,
                        principalTable: "ComCatSexo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComPersona_FKComCatEscolaridad",
                table: "ComPersona",
                column: "FKComCatEscolaridad");

            migrationBuilder.CreateIndex(
                name: "IX_ComPersona_FKComCatSexo",
                table: "ComPersona",
                column: "FKComCatSexo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComPersona");

            migrationBuilder.DropTable(
                name: "ComCatEscolaridad");

            migrationBuilder.DropTable(
                name: "ComCatSexo");
        }
    }
}
