﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using demo_tt.Database;

#nullable disable

namespace DemoAPI.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20241020082242_FKTimeToArea")]
    partial class FKTimeToArea
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("demo_tt.Entities.AffectedAreas", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AreaID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("TimeConstraint")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UrgencyLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AffectedAreas");
                });

            modelBuilder.Entity("demo_tt.Entities.Assignments", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AreaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("TruckID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AreaID");

                    b.HasIndex("TruckID");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("demo_tt.Entities.MasterResource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MasterResources");
                });

            modelBuilder.Entity("demo_tt.Entities.ResourceAffectedAreas", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AreaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("ResourceID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AreaID");

                    b.HasIndex("ResourceID");

                    b.ToTable("ResourceAffectedAreas");
                });

            modelBuilder.Entity("demo_tt.Entities.ResourceTrucks", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AvailableQuantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("ResourceID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TruckID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ResourceID");

                    b.HasIndex("TruckID");

                    b.ToTable("ResourceTrucks");
                });

            modelBuilder.Entity("demo_tt.Entities.TravelTimeToAreas", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AreaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("TravelTime")
                        .HasColumnType("int");

                    b.Property<Guid>("TruckID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AreaID");

                    b.HasIndex("TruckID");

                    b.ToTable("TravelTimeToAreas");
                });

            modelBuilder.Entity("demo_tt.Entities.Trucks", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("TruckID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Trucks");
                });

            modelBuilder.Entity("demo_tt.Entities.Assignments", b =>
                {
                    b.HasOne("demo_tt.Entities.AffectedAreas", "AffectedAreas")
                        .WithMany("Assignments")
                        .HasForeignKey("AreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("demo_tt.Entities.Trucks", "Trucks")
                        .WithMany("Assignments")
                        .HasForeignKey("TruckID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AffectedAreas");

                    b.Navigation("Trucks");
                });

            modelBuilder.Entity("demo_tt.Entities.ResourceAffectedAreas", b =>
                {
                    b.HasOne("demo_tt.Entities.AffectedAreas", "AffectedAreas")
                        .WithMany("ResourceAffectedAreas")
                        .HasForeignKey("AreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("demo_tt.Entities.MasterResource", "MasterResource")
                        .WithMany()
                        .HasForeignKey("ResourceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AffectedAreas");

                    b.Navigation("MasterResource");
                });

            modelBuilder.Entity("demo_tt.Entities.ResourceTrucks", b =>
                {
                    b.HasOne("demo_tt.Entities.MasterResource", "MasterResource")
                        .WithMany()
                        .HasForeignKey("ResourceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("demo_tt.Entities.Trucks", "Trucks")
                        .WithMany("ResourceTrucks")
                        .HasForeignKey("TruckID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MasterResource");

                    b.Navigation("Trucks");
                });

            modelBuilder.Entity("demo_tt.Entities.TravelTimeToAreas", b =>
                {
                    b.HasOne("demo_tt.Entities.AffectedAreas", "AffectedAreas")
                        .WithMany("TravelTimeToAreas")
                        .HasForeignKey("AreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("demo_tt.Entities.Trucks", "Trucks")
                        .WithMany("TravelTimeToAreas")
                        .HasForeignKey("TruckID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AffectedAreas");

                    b.Navigation("Trucks");
                });

            modelBuilder.Entity("demo_tt.Entities.AffectedAreas", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("ResourceAffectedAreas");

                    b.Navigation("TravelTimeToAreas");
                });

            modelBuilder.Entity("demo_tt.Entities.Trucks", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("ResourceTrucks");

                    b.Navigation("TravelTimeToAreas");
                });
#pragma warning restore 612, 618
        }
    }
}
