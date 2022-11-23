using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.DAL.Migrations.SqliteAppDb
{
    /// <inheritdoc />
    public partial class FluentValidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_Projects_ProjectId",
                table: "ToDoItems");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCompleted",
                table: "ToDoItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_Projects_ProjectId",
                table: "ToDoItems",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_Projects_ProjectId",
                table: "ToDoItems");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCompleted",
                table: "ToDoItems",
                type: "INTEGER",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_Projects_ProjectId",
                table: "ToDoItems",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
