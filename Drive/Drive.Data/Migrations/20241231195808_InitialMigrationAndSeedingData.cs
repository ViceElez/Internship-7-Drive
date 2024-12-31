using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Drive.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationAndSeedingData : Migration
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "driveFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "DriveFolderUser",
                columns: table => new
                {
                    DriveFolderId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriveFolderUser", x => new { x.DriveFolderId, x.UserId });
                    table.ForeignKey(
                        name: "FK_DriveFolderUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriveFolderUser_driveFolders_DriveFolderId",
                        column: x => x.DriveFolderId,
                        principalTable: "driveFolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriveComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FileId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriveComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriveComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriveComments_driveFiles_FileId",
                        column: x => x.FileId,
                        principalTable: "driveFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriveFileUser",
                columns: table => new
                {
                    DriveFileId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriveFileUser", x => new { x.DriveFileId, x.UserId });
                    table.ForeignKey(
                        name: "FK_DriveFileUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriveFileUser_driveFiles_DriveFileId",
                        column: x => x.DriveFileId,
                        principalTable: "driveFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password" },
                values: new object[,]
                {
                    { 1, "alice@example.com", "password123" },
                    { 2, "bob@example.com", "password456" },
                    { 3, "charlie@example.com", "password789" },
                    { 4, "david@example.com", "password101" },
                    { 5, "emma@example.com", "password112" }
                });

            migrationBuilder.InsertData(
                table: "driveFolders",
                columns: new[] { "Id", "CreatedAt", "FolderUserId", "Name", "ParentFolderId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 31, 20, 58, 7, 874, DateTimeKind.Utc).AddTicks(7106), 1, "Dokumenti", null },
                    { 4, new DateTime(2024, 12, 31, 20, 58, 7, 874, DateTimeKind.Utc).AddTicks(7158), 4, "PraznaMapa", null }
                });

            migrationBuilder.InsertData(
                table: "DriveFolderUser",
                columns: new[] { "DriveFolderId", "UserId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "driveFiles",
                columns: new[] { "Id", "FileUserId", "FolderId", "LastChanges", "Name", "Text" },
                values: new object[,]
                {
                    { 2, 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MjesečniIzvještaj.docx", "Mjesečni financijski izvještaj koji sažima ključne metrike." },
                    { 4, 4, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ZbirkaFotografija.zip", "Kolekcija fotografija s odmora." },
                    { 5, 5, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Upute.txt", "Korak-po-korak upute za postavljanje sustava." }
                });

            migrationBuilder.InsertData(
                table: "driveFolders",
                columns: new[] { "Id", "CreatedAt", "FolderUserId", "Name", "ParentFolderId" },
                values: new object[,]
                {
                    { 2, new DateTime(2024, 12, 31, 20, 58, 7, 874, DateTimeKind.Utc).AddTicks(7153), 2, "Rad", 1 },
                    { 3, new DateTime(2024, 12, 31, 20, 58, 7, 874, DateTimeKind.Utc).AddTicks(7156), 3, "Osobno", 1 }
                });

            migrationBuilder.InsertData(
                table: "DriveComments",
                columns: new[] { "Id", "Content", "CreatedAt", "FileId", "UserId" },
                values: new object[,]
                {
                    { 2, "Izvrstan napredak na izvještaju.", new DateTime(2024, 12, 31, 19, 58, 7, 874, DateTimeKind.Utc).AddTicks(7209), 2, 3 },
                    { 4, "Prekrasne fotografije! Moram ih provjeriti.", new DateTime(2024, 12, 31, 19, 58, 7, 874, DateTimeKind.Utc).AddTicks(7211), 4, 1 },
                    { 5, "Ove upute će mi biti vrlo korisne.", new DateTime(2024, 12, 31, 19, 58, 7, 874, DateTimeKind.Utc).AddTicks(7213), 5, 2 }
                });

            migrationBuilder.InsertData(
                table: "DriveFileUser",
                columns: new[] { "DriveFileId", "UserId" },
                values: new object[,]
                {
                    { 2, 3 },
                    { 4, 1 },
                    { 5, 2 }
                });

            migrationBuilder.InsertData(
                table: "DriveFolderUser",
                columns: new[] { "DriveFolderId", "UserId" },
                values: new object[,]
                {
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "driveFiles",
                columns: new[] { "Id", "FileUserId", "FolderId", "LastChanges", "Name", "Text" },
                values: new object[,]
                {
                    { 1, 1, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ProjektniPrijedlog.pdf", "Projektni prijedlog koji detaljno opisuje ciljeve i plan." },
                    { 3, 3, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OsobniDnevnik.txt", "Dnevnički zapisi koji bilježe osobne refleksije." }
                });

            migrationBuilder.InsertData(
                table: "DriveComments",
                columns: new[] { "Id", "Content", "CreatedAt", "FileId", "UserId" },
                values: new object[,]
                {
                    { 1, "Ovo izgleda super! Pregledajmo i završimo.", new DateTime(2024, 12, 31, 19, 58, 7, 874, DateTimeKind.Utc).AddTicks(7207), 1, 2 },
                    { 3, "Hvala na dijeljenju dnevničkih zapisa.", new DateTime(2024, 12, 31, 19, 58, 7, 874, DateTimeKind.Utc).AddTicks(7210), 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "DriveFileUser",
                columns: new[] { "DriveFileId", "UserId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 3, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriveComments_FileId",
                table: "DriveComments",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_DriveComments_UserId",
                table: "DriveComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_driveFiles_FileUserId",
                table: "driveFiles",
                column: "FileUserId");

            migrationBuilder.CreateIndex(
                name: "IX_driveFiles_FolderId",
                table: "driveFiles",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_DriveFileUser_DriveFileId",
                table: "DriveFileUser",
                column: "DriveFileId");

            migrationBuilder.CreateIndex(
                name: "IX_DriveFileUser_UserId",
                table: "DriveFileUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_driveFolders_FolderUserId",
                table: "driveFolders",
                column: "FolderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_driveFolders_ParentFolderId",
                table: "driveFolders",
                column: "ParentFolderId");

            migrationBuilder.CreateIndex(
                name: "IX_DriveFolderUser_DriveFolderId",
                table: "DriveFolderUser",
                column: "DriveFolderId");

            migrationBuilder.CreateIndex(
                name: "IX_DriveFolderUser_UserId",
                table: "DriveFolderUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriveComments");

            migrationBuilder.DropTable(
                name: "DriveFileUser");

            migrationBuilder.DropTable(
                name: "DriveFolderUser");

            migrationBuilder.DropTable(
                name: "driveFiles");

            migrationBuilder.DropTable(
                name: "driveFolders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
