using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class _77 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FUNCIONARIOS_ENDERECOS",
                table: "FUNCIONARIOS");

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "FUNCIONARIOS",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20);

            migrationBuilder.AddForeignKey(
                name: "FK_FUNCIONARIOS_ENDERECOS_EnderecoId",
                table: "FUNCIONARIOS",
                column: "EnderecoId",
                principalTable: "ENDERECOS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FUNCIONARIOS_ENDERECOS_EnderecoId",
                table: "FUNCIONARIOS");

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "FUNCIONARIOS",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_FUNCIONARIOS_ENDERECOS",
                table: "FUNCIONARIOS",
                column: "EnderecoId",
                principalTable: "ENDERECOS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
