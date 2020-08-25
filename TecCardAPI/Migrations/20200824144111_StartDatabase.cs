using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace TecCardAPI.Migrations
{
    public partial class StartDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    RM = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Senha = table.Column<string>(maxLength: 18, nullable: false),
                    Foto = table.Column<string>(type: "longtext", nullable: true),
                    QrCode = table.Column<string>(type: "longtext", nullable: false),
                    CursoCodigo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.RM);
                    table.ForeignKey(
                        name: "FK_Aluno_Curso_CursoCodigo",
                        column: x => x.CursoCodigo,
                        principalTable: "Curso",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Acesso",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 8, 24, 11, 41, 11, 110, DateTimeKind.Local).AddTicks(7588)),
                    Status = table.Column<string>(maxLength: 45, nullable: false),
                    Resultado = table.Column<string>(maxLength: 45, nullable: false),
                    AlunoRM = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acesso", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Acesso_Aluno_AlunoRM",
                        column: x => x.AlunoRM,
                        principalTable: "Aluno",
                        principalColumn: "RM",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(maxLength: 45, nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 8, 24, 11, 41, 11, 116, DateTimeKind.Local).AddTicks(5782)),
                    DataFim = table.Column<DateTime>(nullable: false),
                    AlunoRM = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Status_Aluno_AlunoRM",
                        column: x => x.AlunoRM,
                        principalTable: "Aluno",
                        principalColumn: "RM",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acesso_AlunoRM",
                table: "Acesso",
                column: "AlunoRM");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_CursoCodigo",
                table: "Aluno",
                column: "CursoCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Status_AlunoRM",
                table: "Status",
                column: "AlunoRM");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acesso");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Curso");
        }
    }
}
