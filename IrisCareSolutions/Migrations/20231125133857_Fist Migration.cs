using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IrisCareSolutions.Migrations
{
    /// <inheritdoc />
    public partial class FistMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultadoData",
                table: "Tbl_Exame");

            migrationBuilder.AddColumn<string>(
                name: "ResultadoPath",
                table: "Tbl_Exame",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultadoPath",
                table: "Tbl_Exame");

            migrationBuilder.AddColumn<byte[]>(
                name: "ResultadoData",
                table: "Tbl_Exame",
                type: "varbinary(max)",
                unicode: false,
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
