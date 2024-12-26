using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Drive.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatingMigrationAndSeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "driveFolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FolderUserId = table.Column<int>(type: "integer", nullable: false),
                    ParentFolderId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_driveFolders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_driveFolders_Users_FolderUserId",
                        column: x => x.FolderUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_driveFolders_driveFolders_ParentFolderId",
                        column: x => x.ParentFolderId,
                        principalTable: "driveFolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "driveFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    FileUserId = table.Column<int>(type: "integer", nullable: false),
                    LastChanges = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FolderId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_driveFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_driveFiles_Users_FileUserId",
                        column: x => x.FileUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_driveFiles_driveFolders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "driveFolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password" },
                values: new object[,]
                {
                    { 1, "ivana@vip.com", "password123" },
                    { 2, "josip@yahoo.pro", "password456" },
                    { 3, "mario@abc.com", "password789" },
                    { 4, "luka@gmail.com", "password000" },
                    { 5, "ana@domain.com", "password111" },
                    { 6, "nikola@company.com", "password222" }
                });

            migrationBuilder.InsertData(
              table: "driveFiles",
              columns: new[] { "Id", "FileUserId", "FolderId", "LastChanges", "Name", "Text" },
              values: new object[,]
              {
                { 1, 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Resume.pdf", "Resume content" },
                { 2, 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ProjectPlan.docx", "Project Plan content" },
                { 3, 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Budget.xlsx", "Budget content" },
                { 4, 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hobbies.txt", "Hobbies content" },
                { 5, 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ToDoList.docx", "ToDo list content" },
                { 6, 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Notes.txt", "Notes content" },
                { 7, 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Presentation.pptx", "Presentation content" },
                { 8, 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Invoice.pdf", "Invoice content" },
                { 9, 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MeetingNotes.doc", "Meeting notes content" },
                { 10, 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ProductDesign.png", "Product design content" },
                { 11, 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CodeSnippet.cs", "Code snippet content" },
                { 12, 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SetupGuide.md", "Setup guide content" }
              });

            migrationBuilder.InsertData(
                 table: "driveFolders",
                 columns: new[] { "Id", "CreatedAt", "FolderUserId", "Name", "ParentFolderId" },
                 values: new object[,]
                 {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Work", null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Personal", null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Projects", 1 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Finance", 1 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Designs", null },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Development", null }
                 });

            migrationBuilder.CreateIndex(
                name: "IX_driveFiles_FileUserId",
                table: "driveFiles",
                column: "FileUserId");

            migrationBuilder.CreateIndex(
                name: "IX_driveFiles_FolderId",
                table: "driveFiles",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_driveFolders_FolderUserId",
                table: "driveFolders",
                column: "FolderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_driveFolders_ParentFolderId",
                table: "driveFolders",
                column: "ParentFolderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "driveFiles");

            migrationBuilder.DropTable(
                name: "driveFolders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
