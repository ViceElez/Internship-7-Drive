﻿// <auto-generated />
using System;
using Drive.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Drive.Data.Migrations
{
    [DbContext(typeof(DriveDbContext))]
    [Migration("20250101163023_AddedNewDbSet")]
    partial class AddedNewDbSet
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Drive.Data.Entities.Models.Comments.DriveComments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FileId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("UserId");

                    b.ToTable("driveComments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Ovo izgleda super! Pregledajmo i završimo.",
                            CreatedAt = new DateTime(2025, 1, 1, 16, 30, 23, 472, DateTimeKind.Utc).AddTicks(4679),
                            FileId = 1,
                            UserId = 2
                        },
                        new
                        {
                            Id = 2,
                            Content = "Izvrstan napredak na izvještaju.",
                            CreatedAt = new DateTime(2025, 1, 1, 16, 30, 23, 472, DateTimeKind.Utc).AddTicks(4683),
                            FileId = 2,
                            UserId = 3
                        },
                        new
                        {
                            Id = 3,
                            Content = "Hvala na dijeljenju dnevničkih zapisa.",
                            CreatedAt = new DateTime(2025, 1, 1, 16, 30, 23, 472, DateTimeKind.Utc).AddTicks(4684),
                            FileId = 3,
                            UserId = 4
                        },
                        new
                        {
                            Id = 4,
                            Content = "Prekrasne fotografije! Moram ih provjeriti.",
                            CreatedAt = new DateTime(2025, 1, 1, 16, 30, 23, 472, DateTimeKind.Utc).AddTicks(4685),
                            FileId = 4,
                            UserId = 1
                        },
                        new
                        {
                            Id = 5,
                            Content = "Ove upute će mi biti vrlo korisne.",
                            CreatedAt = new DateTime(2025, 1, 1, 16, 30, 23, 472, DateTimeKind.Utc).AddTicks(4686),
                            FileId = 5,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Files.DriveFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FileUserId")
                        .HasColumnType("integer");

                    b.Property<int?>("FolderId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastChanges")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FileUserId");

                    b.HasIndex("FolderId");

                    b.ToTable("driveFiles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FileUserId = 1,
                            FolderId = 2,
                            LastChanges = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "ProjektniPrijedlog.pdf",
                            Text = "Projektni prijedlog koji detaljno opisuje ciljeve i plan."
                        },
                        new
                        {
                            Id = 2,
                            FileUserId = 2,
                            FolderId = 1,
                            LastChanges = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "MjesečniIzvještaj.docx",
                            Text = "Mjesečni financijski izvještaj koji sažima ključne metrike."
                        },
                        new
                        {
                            Id = 3,
                            FileUserId = 3,
                            FolderId = 3,
                            LastChanges = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "OsobniDnevnik.txt",
                            Text = "Dnevnički zapisi koji bilježe osobne refleksije."
                        },
                        new
                        {
                            Id = 4,
                            FileUserId = 4,
                            FolderId = 4,
                            LastChanges = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "ZbirkaFotografija.zip",
                            Text = "Kolekcija fotografija s odmora."
                        },
                        new
                        {
                            Id = 5,
                            FileUserId = 5,
                            FolderId = 4,
                            LastChanges = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Upute.txt",
                            Text = "Korak-po-korak upute za postavljanje sustava."
                        });
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Folders.DriveFolder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FolderUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ParentFolderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FolderUserId");

                    b.HasIndex("ParentFolderId");

                    b.ToTable("driveFolders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 1, 1, 17, 30, 23, 472, DateTimeKind.Local).AddTicks(4575),
                            FolderUserId = 1,
                            Name = "Dokumenti"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 1, 1, 17, 30, 23, 472, DateTimeKind.Local).AddTicks(4627),
                            FolderUserId = 2,
                            Name = "Rad",
                            ParentFolderId = 1
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2025, 1, 1, 17, 30, 23, 472, DateTimeKind.Local).AddTicks(4631),
                            FolderUserId = 3,
                            Name = "Osobno",
                            ParentFolderId = 1
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2025, 1, 1, 17, 30, 23, 472, DateTimeKind.Local).AddTicks(4633),
                            FolderUserId = 4,
                            Name = "PraznaMapa"
                        });
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Users.DriveFileUser", b =>
                {
                    b.Property<int>("DriveFileId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("DriveFileId", "UserId");

                    b.HasIndex("DriveFileId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("driveFileUsers");

                    b.HasData(
                        new
                        {
                            DriveFileId = 1,
                            UserId = 2
                        },
                        new
                        {
                            DriveFileId = 2,
                            UserId = 3
                        },
                        new
                        {
                            DriveFileId = 3,
                            UserId = 4
                        },
                        new
                        {
                            DriveFileId = 4,
                            UserId = 1
                        },
                        new
                        {
                            DriveFileId = 5,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Users.DriveFolderUser", b =>
                {
                    b.Property<int>("DriveFolderId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("DriveFolderId", "UserId");

                    b.HasIndex("DriveFolderId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("driveFolderUsers");

                    b.HasData(
                        new
                        {
                            DriveFolderId = 1,
                            UserId = 2
                        },
                        new
                        {
                            DriveFolderId = 2,
                            UserId = 3
                        },
                        new
                        {
                            DriveFolderId = 2,
                            UserId = 4
                        },
                        new
                        {
                            DriveFolderId = 3,
                            UserId = 4
                        });
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "alice@example.com",
                            Password = "password123"
                        },
                        new
                        {
                            Id = 2,
                            Email = "bob@example.com",
                            Password = "password456"
                        },
                        new
                        {
                            Id = 3,
                            Email = "charlie@example.com",
                            Password = "password789"
                        },
                        new
                        {
                            Id = 4,
                            Email = "david@example.com",
                            Password = "password101"
                        },
                        new
                        {
                            Id = 5,
                            Email = "emma@example.com",
                            Password = "password112"
                        });
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Comments.DriveComments", b =>
                {
                    b.HasOne("Drive.Data.Entities.Models.Files.DriveFile", "File")
                        .WithMany("DriveComments")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Drive.Data.Entities.Models.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Files.DriveFile", b =>
                {
                    b.HasOne("Drive.Data.Entities.Models.Users.User", "FileUser")
                        .WithMany("Files")
                        .HasForeignKey("FileUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Drive.Data.Entities.Models.Folders.DriveFolder", "Folder")
                        .WithMany("Files")
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("FileUser");

                    b.Navigation("Folder");
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Folders.DriveFolder", b =>
                {
                    b.HasOne("Drive.Data.Entities.Models.Users.User", "FolderUser")
                        .WithMany("Folders")
                        .HasForeignKey("FolderUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Drive.Data.Entities.Models.Folders.DriveFolder", "ParentFolder")
                        .WithMany("SubFolders")
                        .HasForeignKey("ParentFolderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("FolderUser");

                    b.Navigation("ParentFolder");
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Users.DriveFileUser", b =>
                {
                    b.HasOne("Drive.Data.Entities.Models.Files.DriveFile", "DriveFile")
                        .WithMany("SharedUsers")
                        .HasForeignKey("DriveFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Drive.Data.Entities.Models.Users.User", "User")
                        .WithMany("SharedFiles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DriveFile");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Users.DriveFolderUser", b =>
                {
                    b.HasOne("Drive.Data.Entities.Models.Folders.DriveFolder", "DriveFolder")
                        .WithMany("SharedUsers")
                        .HasForeignKey("DriveFolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Drive.Data.Entities.Models.Users.User", "User")
                        .WithMany("SharedFolders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DriveFolder");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Files.DriveFile", b =>
                {
                    b.Navigation("DriveComments");

                    b.Navigation("SharedUsers");
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Folders.DriveFolder", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("SharedUsers");

                    b.Navigation("SubFolders");
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Users.User", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("Folders");

                    b.Navigation("SharedFiles");

                    b.Navigation("SharedFolders");
                });
#pragma warning restore 612, 618
        }
    }
}
