using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Drive.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
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
                    ParentFolderId = table.Column<int>(type: "integer", nullable: false)
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
                    FolderId = table.Column<int>(type: "integer", nullable: false)
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
