using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class implementarrecordatoriosact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Recordatorios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recordatorios_UsuarioId",
                table: "Recordatorios",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recordatorios_Usuarios_UsuarioId",
                table: "Recordatorios",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recordatorios_Usuarios_UsuarioId",
                table: "Recordatorios");

            migrationBuilder.DropIndex(
                name: "IX_Recordatorios_UsuarioId",
                table: "Recordatorios");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Recordatorios");
        }
    }
}
