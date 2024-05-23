﻿// <auto-generated />
using System;
using InventoryManager.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryManager.Database.Migrations
{
    [DbContext(typeof(InventoryManagerContext))]
    [Migration("20240523110844_renameStorageCaseToStorageLocation")]
    partial class renameStorageCaseToStorageLocation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("InventoryManager.Domain.Container", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid?>("ContentId")
                        .IsRequired()
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<int>("Size")
                        .HasColumnType("INT");

                    b.HasKey("Id")
                        .HasName("PK_Container");

                    b.HasIndex("ContentId");

                    b.ToTable("Container", (string)null);
                });

            modelBuilder.Entity("InventoryManager.Domain.Content", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<string>("Length")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<Guid>("StandardId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<int>("Type")
                        .HasColumnType("INT");

                    b.HasKey("Id")
                        .HasName("PK_Content");

                    b.HasIndex("StandardId");

                    b.ToTable("Content", (string)null);
                });

            modelBuilder.Entity("InventoryManager.Domain.Standard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("AlternativeNames")
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<string>("Description")
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<string>("Path")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NVARCHAR(MAX)")
                        .HasDefaultValue("");

                    b.HasKey("Id")
                        .HasName("PK_Standard");

                    b.ToTable("Standard", (string)null);
                });

            modelBuilder.Entity("InventoryManager.Domain.StorageLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<int>("SizeX")
                        .HasColumnType("INT");

                    b.Property<int>("SizeY")
                        .HasColumnType("INT");

                    b.HasKey("Id")
                        .HasName("PK_StorageLocation");

                    b.ToTable("StorageLocation", (string)null);
                });

            modelBuilder.Entity("InventoryManager.Domain.StorageLocationContainerPosition", b =>
                {
                    b.Property<Guid>("StorageLocationId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("ContainerId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<int>("PositionX")
                        .HasColumnType("INT");

                    b.Property<int>("PositionY")
                        .HasColumnType("INT");

                    b.HasKey("StorageLocationId", "ContainerId")
                        .HasName("PK_StorageLocationContainerPosition");

                    b.HasIndex("ContainerId")
                        .IsUnique();

                    b.ToTable("StorageLocationContainerPosition", (string)null);
                });

            modelBuilder.Entity("InventoryManager.Domain.Container", b =>
                {
                    b.HasOne("InventoryManager.Domain.Content", "Content")
                        .WithMany("Containers")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Container_Content");

                    b.Navigation("Content");
                });

            modelBuilder.Entity("InventoryManager.Domain.Content", b =>
                {
                    b.HasOne("InventoryManager.Domain.Standard", "Standard")
                        .WithMany("Contents")
                        .HasForeignKey("StandardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Content_Standard");

                    b.Navigation("Standard");
                });

            modelBuilder.Entity("InventoryManager.Domain.StorageLocationContainerPosition", b =>
                {
                    b.HasOne("InventoryManager.Domain.Container", "Container")
                        .WithOne("Position")
                        .HasForeignKey("InventoryManager.Domain.StorageLocationContainerPosition", "ContainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Container_StorageLocationContainerPosition");

                    b.HasOne("InventoryManager.Domain.StorageLocation", "Location")
                        .WithMany("Containers")
                        .HasForeignKey("StorageLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_StorageLocation_StorageLocationContainerPosition");

                    b.Navigation("Container");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("InventoryManager.Domain.Container", b =>
                {
                    b.Navigation("Position");
                });

            modelBuilder.Entity("InventoryManager.Domain.Content", b =>
                {
                    b.Navigation("Containers");
                });

            modelBuilder.Entity("InventoryManager.Domain.Standard", b =>
                {
                    b.Navigation("Contents");
                });

            modelBuilder.Entity("InventoryManager.Domain.StorageLocation", b =>
                {
                    b.Navigation("Containers");
                });
#pragma warning restore 612, 618
        }
    }
}
