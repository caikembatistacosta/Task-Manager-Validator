using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class _76 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ENDERECO_CLIENTE",
                table: "CLIENTES");

            migrationBuilder.DropForeignKey(
                name: "FK_FUNCIONARIOS_ENDERECOS_EnderecoId",
                table: "FUNCIONARIOS");

            migrationBuilder.DropIndex(
                name: "IX_CLIENTES_EnderecoID",
                table: "CLIENTES");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTES_EnderecoID",
                table: "CLIENTES",
                column: "EnderecoID");

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENTES_ENDERECOS_EnderecoID",
                table: "CLIENTES",
                column: "EnderecoID",
                principalTable: "ENDERECOS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FUNCIONARIOS_ENDERECOS",
                table: "FUNCIONARIOS",
                column: "EnderecoId",
                principalTable: "ENDERECOS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLIENTES_ENDERECOS_EnderecoID",
                table: "CLIENTES");

            migrationBuilder.DropForeignKey(
                name: "FK_FUNCIONARIOS_ENDERECOS",
                table: "FUNCIONARIOS");

            migrationBuilder.DropIndex(
                name: "IX_CLIENTES_EnderecoID",
                table: "CLIENTES");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTES_EnderecoID",
                table: "CLIENTES",
                column: "EnderecoID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ENDERECO_CLIENTE",
                table: "CLIENTES",
                column: "EnderecoID",
                principalTable: "ENDERECOS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FUNCIONARIOS_ENDERECOS_EnderecoId",
                table: "FUNCIONARIOS",
                column: "EnderecoId",
                principalTable: "ENDERECOS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
