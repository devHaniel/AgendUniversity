using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asignatura_Periodo_PeriodoId",
                table: "Asignatura");

            migrationBuilder.DropForeignKey(
                name: "FK_Periodo_Usuarios_UsuarioId",
                table: "Periodo");

            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Asignatura_AsignaturaId",
                table: "Tareas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Periodo",
                table: "Periodo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Asignatura",
                table: "Asignatura");

            migrationBuilder.RenameTable(
                name: "Periodo",
                newName: "Periodos");

            migrationBuilder.RenameTable(
                name: "Asignatura",
                newName: "Asignaturas");

            migrationBuilder.RenameIndex(
                name: "IX_Periodo_UsuarioId",
                table: "Periodos",
                newName: "IX_Periodos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Asignatura_PeriodoId",
                table: "Asignaturas",
                newName: "IX_Asignaturas_PeriodoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Periodos",
                table: "Periodos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asignaturas",
                table: "Asignaturas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Asignaturas_Periodos_PeriodoId",
                table: "Asignaturas",
                column: "PeriodoId",
                principalTable: "Periodos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Periodos_Usuarios_UsuarioId",
                table: "Periodos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Asignaturas_AsignaturaId",
                table: "Tareas",
                column: "AsignaturaId",
                principalTable: "Asignaturas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asignaturas_Periodos_PeriodoId",
                table: "Asignaturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Periodos_Usuarios_UsuarioId",
                table: "Periodos");

            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Asignaturas_AsignaturaId",
                table: "Tareas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Periodos",
                table: "Periodos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Asignaturas",
                table: "Asignaturas");

            migrationBuilder.RenameTable(
                name: "Periodos",
                newName: "Periodo");

            migrationBuilder.RenameTable(
                name: "Asignaturas",
                newName: "Asignatura");

            migrationBuilder.RenameIndex(
                name: "IX_Periodos_UsuarioId",
                table: "Periodo",
                newName: "IX_Periodo_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Asignaturas_PeriodoId",
                table: "Asignatura",
                newName: "IX_Asignatura_PeriodoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Periodo",
                table: "Periodo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asignatura",
                table: "Asignatura",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Asignatura_Periodo_PeriodoId",
                table: "Asignatura",
                column: "PeriodoId",
                principalTable: "Periodo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Periodo_Usuarios_UsuarioId",
                table: "Periodo",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Asignatura_AsignaturaId",
                table: "Tareas",
                column: "AsignaturaId",
                principalTable: "Asignatura",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
