﻿// <auto-generated />
using System;
using ExampleApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExampleApi.Migrations
{
    [DbContext(typeof(AppDbContexts))]
    [Migration("20241203000550_ChangeColumnNameInVideoGameDescriptionToDescription")]
    partial class ChangeColumnNameInVideoGameDescriptionToDescription
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Provider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Providers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContactName = "Saul",
                            Name = "Perrors el loco",
                            Phone = "1234"
                        },
                        new
                        {
                            Id = 2,
                            ContactName = "Mario",
                            Name = "Magico Mundo",
                            Phone = "23425"
                        },
                        new
                        {
                            Id = 3,
                            ContactName = "Mario",
                            Name = "Carpintero",
                            Phone = "23425"
                        });
                });

            modelBuilder.Entity("Models.VideoGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Developer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Platform")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Publisher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("VideoGames");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Developer = "Kachimoto",
                            Platform = "Nintendo",
                            Publisher = "Hirul",
                            Title = "Zelda"
                        },
                        new
                        {
                            Id = 2,
                            Developer = "Bowseer",
                            Platform = "Nintendo",
                            Publisher = "Velozitrx",
                            Title = "Mario Kart"
                        },
                        new
                        {
                            Id = 3,
                            Developer = "Alkadeishon",
                            Platform = "Nintendo",
                            Publisher = "Rapliz",
                            Title = "Bomber Man"
                        });
                });

            modelBuilder.Entity("Models.VideoGameDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RealiseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("VideoGameId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VideoGameId")
                        .IsUnique();

                    b.ToTable("VideoGameDetails");
                });

            modelBuilder.Entity("Models.VideoGameDetails", b =>
                {
                    b.HasOne("Models.VideoGame", null)
                        .WithOne("VideoGameDetails")
                        .HasForeignKey("Models.VideoGameDetails", "VideoGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.VideoGame", b =>
                {
                    b.Navigation("VideoGameDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
