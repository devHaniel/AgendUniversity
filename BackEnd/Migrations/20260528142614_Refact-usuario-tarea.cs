using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Refactusuariotarea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Usuarios_UsuarioId",
                table: "Tareas");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Tareas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Periodo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Periodo_UsuarioId",
                table: "Periodo",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Periodo_Usuarios_UsuarioId",
                table: "Periodo",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Usuarios_UsuarioId",
                table: "Tareas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Periodo_Usuarios_UsuarioId",
                table: "Periodo");

            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Usuarios_UsuarioId",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Periodo_UsuarioId",
                table: "Periodo");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Periodo");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Tareas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Usuarios_UsuarioId",
                table: "Tareas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
