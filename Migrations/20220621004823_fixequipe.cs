using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoFourTask.Migrations
{
    public partial class fixequipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Equipes",
                newName: "EquipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EquipeId",
                table: "Equipes",
                newName: "Id");
        }
    }
}
