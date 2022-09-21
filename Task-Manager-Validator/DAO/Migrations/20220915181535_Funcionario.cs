using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAO.Migrations
{
    public partial class Funcionario : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "FUNCIONARIOS",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Sobrenome",
                table: "FUNCIONARIOS",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIOS_CPF",
                table: "FUNCIONARIOS",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIOS_RG",
                table: "FUNCIONARIOS",
                column: "RG",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FUNCIONARIOS_CPF",
                table: "FUNCIONARIOS");

            migrationBuilder.DropIndex(
                name: "IX_FUNCIONARIOS_RG",
                table: "FUNCIONARIOS");

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

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "FUNCIONARIOS",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30);

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
