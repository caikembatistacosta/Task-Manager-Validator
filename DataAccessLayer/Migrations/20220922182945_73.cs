using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class _73 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "FUNCIONARIOS");

            migrationBuilder.RenameColumn(
                name: "Rg",
                table: "FUNCIONARIOS",
                newName: "RG");

            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "FUNCIONARIOS",
                newName: "CPF");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "FUNCIONARIOS",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ENDERECOS",
                newName: "ID");

            migrationBuilder.AddColumn<string>(
                name: "Sobrenome",
                table: "FUNCIONARIOS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sobrenome",
                table: "FUNCIONARIOS");

            migrationBuilder.RenameColumn(
                name: "RG",
                table: "FUNCIONARIOS",
                newName: "Rg");

            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "FUNCIONARIOS",
                newName: "Cpf");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "FUNCIONARIOS",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ENDERECOS",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "FUNCIONARIOS",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
