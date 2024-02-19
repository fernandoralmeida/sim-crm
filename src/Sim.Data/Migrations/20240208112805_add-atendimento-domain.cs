using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sim.Data.Migrations
{
    public partial class addatendimentodomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DominioId",
                table: "Atendimento",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_DominioId",
                table: "Atendimento",
                column: "DominioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimento_Secretaria_DominioId",
                table: "Atendimento",
                column: "DominioId",
                principalTable: "Secretaria",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atendimento_Secretaria_DominioId",
                table: "Atendimento");

            migrationBuilder.DropIndex(
                name: "IX_Atendimento_DominioId",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "DominioId",
                table: "Atendimento");
        }
    }
}
