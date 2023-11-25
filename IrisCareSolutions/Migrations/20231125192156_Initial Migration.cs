using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IrisCareSolutions.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Responsavels",
                columns: table => new
                {
                    ResponsavelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cpf = table.Column<int>(type: "int", unicode: false, nullable: false),
                    Nome = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Telefone = table.Column<decimal>(type: "decimal(18,2)", unicode: false, nullable: false),
                    Parentesco = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsavels", x => x.ResponsavelId);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Tutelado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Dt_Nascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ds_Modalidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Tutelado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Lembrete",
                columns: table => new
                {
                    LembreteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Validade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Lembrete", x => x.LembreteId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Exame",
                columns: table => new
                {
                    ExameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TuteladoId = table.Column<int>(type: "int", nullable: false),
                    ResultadoPath = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    ResultadoFileName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Exame", x => x.ExameId);
                    table.ForeignKey(
                        name: "FK_Tbl_Exame_Tb_Tutelado_TuteladoId",
                        column: x => x.TuteladoId,
                        principalTable: "Tb_Tutelado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TuteladosLembretes",
                columns: table => new
                {
                    TuteladoId = table.Column<int>(type: "int", nullable: false),
                    LembreteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TuteladosLembretes", x => new { x.TuteladoId, x.LembreteId });
                    table.ForeignKey(
                        name: "FK_TuteladosLembretes_Tb_Tutelado_TuteladoId",
                        column: x => x.TuteladoId,
                        principalTable: "Tb_Tutelado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TuteladosLembretes_Tbl_Lembrete_LembreteId",
                        column: x => x.LembreteId,
                        principalTable: "Tbl_Lembrete",
                        principalColumn: "LembreteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Exame_TuteladoId",
                table: "Tbl_Exame",
                column: "TuteladoId");

            migrationBuilder.CreateIndex(
                name: "IX_TuteladosLembretes_LembreteId",
                table: "TuteladosLembretes",
                column: "LembreteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Responsavels");

            migrationBuilder.DropTable(
                name: "Tbl_Exame");

            migrationBuilder.DropTable(
                name: "TuteladosLembretes");

            migrationBuilder.DropTable(
                name: "Tb_Tutelado");

            migrationBuilder.DropTable(
                name: "Tbl_Lembrete");
        }
    }
}
