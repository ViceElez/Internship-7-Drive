using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drive.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriveComments_Users_UserId",
                table: "DriveComments");

            migrationBuilder.DropForeignKey(
                name: "FK_DriveComments_driveFiles_FileId",
                table: "DriveComments");

            migrationBuilder.DropForeignKey(
                name: "FK_DriveFileUser_Users_UserId",
                table: "DriveFileUser");

            migrationBuilder.DropForeignKey(
                name: "FK_DriveFileUser_driveFiles_DriveFileId",
                table: "DriveFileUser");

            migrationBuilder.DropForeignKey(
                name: "FK_DriveFolderUser_Users_UserId",
                table: "DriveFolderUser");

            migrationBuilder.DropForeignKey(
                name: "FK_DriveFolderUser_driveFolders_DriveFolderId",
                table: "DriveFolderUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriveComments",
                table: "DriveComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriveFolderUser",
                table: "DriveFolderUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriveFileUser",
                table: "DriveFileUser");

            migrationBuilder.RenameTable(
                name: "DriveComments",
                newName: "driveComments");

            migrationBuilder.RenameTable(
                name: "DriveFolderUser",
                newName: "driveFolderUsers");

            migrationBuilder.RenameTable(
                name: "DriveFileUser",
                newName: "driveFileUsers");

            migrationBuilder.RenameIndex(
                name: "IX_DriveComments_UserId",
                table: "driveComments",
                newName: "IX_driveComments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DriveComments_FileId",
                table: "driveComments",
                newName: "IX_driveComments_FileId");

            migrationBuilder.RenameIndex(
                name: "IX_DriveFolderUser_UserId",
                table: "driveFolderUsers",
                newName: "IX_driveFolderUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DriveFolderUser_DriveFolderId",
                table: "driveFolderUsers",
                newName: "IX_driveFolderUsers_DriveFolderId");

            migrationBuilder.RenameIndex(
                name: "IX_DriveFileUser_UserId",
                table: "driveFileUsers",
                newName: "IX_driveFileUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DriveFileUser_DriveFileId",
                table: "driveFileUsers",
                newName: "IX_driveFileUsers_DriveFileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_driveComments",
                table: "driveComments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_driveFolderUsers",
                table: "driveFolderUsers",
                columns: new[] { "DriveFolderId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_driveFileUsers",
                table: "driveFileUsers",
                columns: new[] { "DriveFileId", "UserId" });

            migrationBuilder.UpdateData(
                table: "driveComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 1, 16, 30, 23, 472, DateTimeKind.Utc).AddTicks(4679));

            migrationBuilder.UpdateData(
                table: "driveComments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 1, 16, 30, 23, 472, DateTimeKind.Utc).AddTicks(4683));

            migrationBuilder.UpdateData(
                table: "driveComments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 1, 16, 30, 23, 472, DateTimeKind.Utc).AddTicks(4684));

            migrationBuilder.UpdateData(
                table: "driveComments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 1, 16, 30, 23, 472, DateTimeKind.Utc).AddTicks(4685));

            migrationBuilder.UpdateData(
                table: "driveComments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 1, 16, 30, 23, 472, DateTimeKind.Utc).AddTicks(4686));

            migrationBuilder.UpdateData(
                table: "driveFolders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 1, 17, 30, 23, 472, DateTimeKind.Utc).AddTicks(4575));

            migrationBuilder.UpdateData(
                table: "driveFolders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 1, 17, 30, 23, 472, DateTimeKind.Utc).AddTicks(4627));

            migrationBuilder.UpdateData(
                table: "driveFolders",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 1, 17, 30, 23, 472, DateTimeKind.Utc).AddTicks(4631));

            migrationBuilder.UpdateData(
                table: "driveFolders",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 1, 17, 30, 23, 472, DateTimeKind.Utc).AddTicks(4633));

            migrationBuilder.AddForeignKey(
                name: "FK_driveComments_Users_UserId",
                table: "driveComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_driveComments_driveFiles_FileId",
                table: "driveComments",
                column: "FileId",
                principalTable: "driveFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_driveFileUsers_Users_UserId",
                table: "driveFileUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_driveFileUsers_driveFiles_DriveFileId",
                table: "driveFileUsers",
                column: "DriveFileId",
                principalTable: "driveFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_driveFolderUsers_Users_UserId",
                table: "driveFolderUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_driveFolderUsers_driveFolders_DriveFolderId",
                table: "driveFolderUsers",
                column: "DriveFolderId",
                principalTable: "driveFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_driveComments_Users_UserId",
                table: "driveComments");

            migrationBuilder.DropForeignKey(
                name: "FK_driveComments_driveFiles_FileId",
                table: "driveComments");

            migrationBuilder.DropForeignKey(
                name: "FK_driveFileUsers_Users_UserId",
                table: "driveFileUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_driveFileUsers_driveFiles_DriveFileId",
                table: "driveFileUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_driveFolderUsers_Users_UserId",
                table: "driveFolderUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_driveFolderUsers_driveFolders_DriveFolderId",
                table: "driveFolderUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_driveComments",
                table: "driveComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_driveFolderUsers",
                table: "driveFolderUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_driveFileUsers",
                table: "driveFileUsers");

            migrationBuilder.RenameTable(
                name: "driveComments",
                newName: "DriveComments");

            migrationBuilder.RenameTable(
                name: "driveFolderUsers",
                newName: "DriveFolderUser");

            migrationBuilder.RenameTable(
                name: "driveFileUsers",
                newName: "DriveFileUser");

            migrationBuilder.RenameIndex(
                name: "IX_driveComments_UserId",
                table: "DriveComments",
                newName: "IX_DriveComments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_driveComments_FileId",
                table: "DriveComments",
                newName: "IX_DriveComments_FileId");

            migrationBuilder.RenameIndex(
                name: "IX_driveFolderUsers_UserId",
                table: "DriveFolderUser",
                newName: "IX_DriveFolderUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_driveFolderUsers_DriveFolderId",
                table: "DriveFolderUser",
                newName: "IX_DriveFolderUser_DriveFolderId");

            migrationBuilder.RenameIndex(
                name: "IX_driveFileUsers_UserId",
                table: "DriveFileUser",
                newName: "IX_DriveFileUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_driveFileUsers_DriveFileId",
                table: "DriveFileUser",
                newName: "IX_DriveFileUser_DriveFileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriveComments",
                table: "DriveComments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriveFolderUser",
                table: "DriveFolderUser",
                columns: new[] { "DriveFolderId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriveFileUser",
                table: "DriveFileUser",
                columns: new[] { "DriveFileId", "UserId" });

            migrationBuilder.UpdateData(
                table: "DriveComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 31, 19, 58, 7, 874, DateTimeKind.Utc).AddTicks(7207));

            migrationBuilder.UpdateData(
                table: "DriveComments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 31, 19, 58, 7, 874, DateTimeKind.Utc).AddTicks(7209));

            migrationBuilder.UpdateData(
                table: "DriveComments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 31, 19, 58, 7, 874, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "DriveComments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 31, 19, 58, 7, 874, DateTimeKind.Utc).AddTicks(7211));

            migrationBuilder.UpdateData(
                table: "DriveComments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 31, 19, 58, 7, 874, DateTimeKind.Utc).AddTicks(7213));

            migrationBuilder.UpdateData(
                table: "driveFolders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 31, 20, 58, 7, 874, DateTimeKind.Utc).AddTicks(7106));

            migrationBuilder.UpdateData(
                table: "driveFolders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 31, 20, 58, 7, 874, DateTimeKind.Utc).AddTicks(7153));

            migrationBuilder.UpdateData(
                table: "driveFolders",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 31, 20, 58, 7, 874, DateTimeKind.Utc).AddTicks(7156));

            migrationBuilder.UpdateData(
                table: "driveFolders",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 31, 20, 58, 7, 874, DateTimeKind.Utc).AddTicks(7158));

            migrationBuilder.AddForeignKey(
                name: "FK_DriveComments_Users_UserId",
                table: "DriveComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriveComments_driveFiles_FileId",
                table: "DriveComments",
                column: "FileId",
                principalTable: "driveFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriveFileUser_Users_UserId",
                table: "DriveFileUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriveFileUser_driveFiles_DriveFileId",
                table: "DriveFileUser",
                column: "DriveFileId",
                principalTable: "driveFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriveFolderUser_Users_UserId",
                table: "DriveFolderUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriveFolderUser_driveFolders_DriveFolderId",
                table: "DriveFolderUser",
                column: "DriveFolderId",
                principalTable: "driveFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
