﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UBB_SE_2024_923_1.Data;

#nullable disable

namespace UBB_SE_2024_923_1.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240511131055_TrendsSongFeatures")]
    partial class TrendsSongFeatures
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.AdDistributionData", b =>
                {
                    b.Property<int>("SongId")
                        .HasColumnType("int");

                    b.Property<int>("AdCampaign")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("SongId", "AdCampaign");

                    b.ToTable("AdDistributionData");
                });

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.ArtistDetails", b =>
                {
                    b.Property<int>("ArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArtistId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArtistId");

                    b.ToTable("ArtistDetails");
                });

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.SongDataBaseModel", b =>
                {
                    b.Property<int>("SongId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SongId"));

                    b.Property<string>("Album")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subgenre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SongId");

                    b.ToTable("SongDataBaseModel");
                });

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.SongFeatures", b =>
                {
                    b.Property<int>("SongId")
                        .HasColumnType("int");

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.HasKey("SongId", "ArtistId");

                    b.ToTable("SongFeatures");
                });

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.SongRecommendationDetails", b =>
                {
                    b.Property<int>("SongId")
                        .HasColumnType("int");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<int>("Dislikes")
                        .HasColumnType("int");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<int>("MinutesListened")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfPlays")
                        .HasColumnType("int");

                    b.HasKey("SongId", "Month", "Year");

                    b.ToTable("SongRecommendationDetails");
                });

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.Trends", b =>
                {
                    b.Property<int>("SongId")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SongId", "Genre", "Language", "Country");

                    b.ToTable("Trends");
                });

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.UserPlaybackBehaviour", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("SongId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventType")
                        .HasColumnType("int");

                    b.HasKey("UserId", "SongId", "Timestamp");

                    b.ToTable("UserPlaybackBehaviour");
                });
#pragma warning restore 612, 618
        }
    }
}