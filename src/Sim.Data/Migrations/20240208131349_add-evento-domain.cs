using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sim.Data.Migrations
{
    public partial class addeventodomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DominioId",
                table: "Evento",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evento_DominioId",
                table: "Evento",
                column: "DominioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_Secretaria_DominioId",
                table: "Evento",
                column: "DominioId",
                principalTable: "Secretaria",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_Secretaria_DominioId",
                table: "Evento");

            migrationBuilder.DropIndex(
                name: "IX_Evento_DominioId",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "DominioId",
                table: "Evento");
        }
    }
}
