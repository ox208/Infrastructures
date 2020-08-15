﻿// <auto-generated />
using Aiursoft.Probe.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Aiursoft.Probe.Migrations
{
    [DbContext(typeof(ProbeDbContext))]
    [Migration("20200501065254_AddHardwareId")]
    partial class AddHardwareId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Aiursoft.Probe.SDK.Models.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContextId")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<string>("HardwareId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ContextId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Aiursoft.Probe.SDK.Models.Folder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContextId")
                        .HasColumnType("int");

                    b.Property<string>("FolderName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContextId");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("Aiursoft.Probe.SDK.Models.ProbeApp", b =>
                {
                    b.Property<string>("AppId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AppId");

                    b.ToTable("Apps");
                });

            modelBuilder.Entity("Aiursoft.Probe.SDK.Models.Site", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("FolderId")
                        .HasColumnType("int");

                    b.Property<bool>("OpenToDownload")
                        .HasColumnType("bit");

                    b.Property<bool>("OpenToUpload")
                        .HasColumnType("bit");

                    b.Property<string>("SiteName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.HasIndex("FolderId");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("Aiursoft.Probe.SDK.Models.File", b =>
                {
                    b.HasOne("Aiursoft.Probe.SDK.Models.Folder", "Context")
                        .WithMany("Files")
                        .HasForeignKey("ContextId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aiursoft.Probe.SDK.Models.Folder", b =>
                {
                    b.HasOne("Aiursoft.Probe.SDK.Models.Folder", "Context")
                        .WithMany("SubFolders")
                        .HasForeignKey("ContextId");
                });

            modelBuilder.Entity("Aiursoft.Probe.SDK.Models.Site", b =>
                {
                    b.HasOne("Aiursoft.Probe.SDK.Models.ProbeApp", "Context")
                        .WithMany("Sites")
                        .HasForeignKey("AppId");

                    b.HasOne("Aiursoft.Probe.SDK.Models.Folder", "Root")
                        .WithMany()
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}