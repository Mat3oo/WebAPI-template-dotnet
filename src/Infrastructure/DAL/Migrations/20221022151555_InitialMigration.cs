using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.DAL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToDoItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", maxLength: 50, nullable: false),
                    ProjectId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDoItems_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_ProjectId",
                table: "ToDoItems",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoItems");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
