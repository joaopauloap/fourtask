using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoFourTask.Migrations
{
    public partial class correcaoColunasNulas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_AspNetUsers_UsuarioId",
                table: "Tarefas");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Tarefas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_AspNetUsers_UsuarioId",
                table: "Tarefas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_AspNetUsers_UsuarioId",
                table: "Tarefas");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Tarefas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_AspNetUsers_UsuarioId",
                table: "Tarefas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
