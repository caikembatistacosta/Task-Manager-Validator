using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAO.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DEMANDAS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nome = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DescricaoCurta = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DescricaoDetalhada = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    StatusDaDemanda = table.Column<int>(type: "int", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEMANDAS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ESTADOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UF = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
                    NomeEstado = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTADOS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FUNCIONARIOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Senha = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Rg = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: false),
                    Cpf = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Genero = table.Column<int>(type: "int", nullable: false),
                    NivelDeAcesso = table.Column<int>(type: "int", nullable: false),
                    IsAtivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FUNCIONARIOS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ENDERECOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cep = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Numero = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    Rua = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    Bairro = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Complemento = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PontoReferencia = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENDERECOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ENDERECOS_ESTADOS_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "ESTADOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CLIENTES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CPF = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Genero = table.Column<int>(type: "int", nullable: false),
                    EstaAtivo = table.Column<bool>(type: "bit", nullable: false),
                    EnderecoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ENDERECO_CLIENTE",
                        column: x => x.EnderecoID,
                        principalTable: "ENDERECOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTES_EnderecoID",
                table: "CLIENTES",
                column: "EnderecoID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ENDERECOS_EstadoId",
                table: "ENDERECOS",
                column: "EstadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLIENTES");

            migrationBuilder.DropTable(
                name: "DEMANDAS");

            migrationBuilder.DropTable(
                name: "FUNCIONARIOS");

            migrationBuilder.DropTable(
                name: "ENDERECOS");

            migrationBuilder.DropTable(
                name: "ESTADOS");
        }
    }
}
