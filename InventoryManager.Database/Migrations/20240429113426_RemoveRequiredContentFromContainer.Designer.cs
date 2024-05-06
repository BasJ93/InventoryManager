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
    [Migration("20240429113426_RemoveRequiredContentFromContainer")]
    partial class RemoveRequiredContentFromContainer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("InventoryManager.Domain.CaseContainerPosition", b =>
                {
                    b.Property<Guid>("CaseId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("ContainerId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<int>("PositionX")
                        .HasColumnType("INT");

                    b.Property<int>("PositionY")
                        .HasColumnType("INT");

                    b.HasKey("CaseId", "ContainerId")
                        .HasName("PK_CaseContainerPosition");

                    b.HasIndex("ContainerId")
                        .IsUnique();

                    b.ToTable("CaseContainerPosition", (string)null);
                });

            modelBuilder.Entity("InventoryManager.Domain.Container", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid?>("ContentId")
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

                    b.Property<string>("Standard")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<int>("Type")
                        .HasColumnType("INT");

                    b.HasKey("Id")
                        .HasName("PK_Content");

                    b.ToTable("Content", (string)null);
                });

            modelBuilder.Entity("InventoryManager.Domain.StorageCase", b =>
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
                        .HasName("PK_StorageCase");

                    b.ToTable("StorageCase", (string)null);
                });

            modelBuilder.Entity("InventoryManager.Domain.CaseContainerPosition", b =>
                {
                    b.HasOne("InventoryManager.Domain.StorageCase", "Case")
                        .WithMany("Containers")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_StorageCase_CaseContainerPosition");

                    b.HasOne("InventoryManager.Domain.Container", "Container")
                        .WithOne("Position")
                        .HasForeignKey("InventoryManager.Domain.CaseContainerPosition", "ContainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Container_CaseContainerPosition");

                    b.Navigation("Case");

                    b.Navigation("Container");
                });

            modelBuilder.Entity("InventoryManager.Domain.Container", b =>
                {
                    b.HasOne("InventoryManager.Domain.Content", "Content")
                        .WithMany("Containers")
                        .HasForeignKey("ContentId")
                        .HasConstraintName("FK_Container_Content");

                    b.Navigation("Content");
                });

            modelBuilder.Entity("InventoryManager.Domain.Container", b =>
                {
                    b.Navigation("Position");
                });

            modelBuilder.Entity("InventoryManager.Domain.Content", b =>
                {
                    b.Navigation("Containers");
                });

            modelBuilder.Entity("InventoryManager.Domain.StorageCase", b =>
                {
                    b.Navigation("Containers");
                });
#pragma warning restore 612, 618
        }
    }
}
