using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class _75 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FUNCIONARIOS_ENDERECOS",
                table: "FUNCIONARIOS");

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
