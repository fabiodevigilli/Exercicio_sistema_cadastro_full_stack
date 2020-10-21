using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstOne.Cadastros.Infra.Data.Migrations
{
    public partial class initialsecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Usuario",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PessoaId",
                table: "Usuario",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Usuario",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Pessoa",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PessoaId",
                table: "Usuario",
                column: "PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Pessoa_PessoaId",
                table: "Usuario",
                column: "PessoaId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Pessoa_PessoaId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_PessoaId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "PessoaId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Pessoa");
        }
    }
}
