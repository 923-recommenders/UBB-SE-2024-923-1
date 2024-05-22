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
    [Migration("20240522114935_AddSongAndImagePathToSong")]
    partial class AddSongAndImagePathToSong
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

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.ExcludedCountry", b =>
                {
                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("SongId")
                        .HasColumnType("int");

                    b.HasKey("CountryId", "SongId");

                    b.ToTable("ExcludedCountries");
                });

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.MostPlayedArtistInformation", b =>
                {
                    b.Property<int>("Artist_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Artist_Id"));

                    b.Property<int>("Start_Listen_Events")
                        .HasColumnType("int");

                    b.HasKey("Artist_Id");

                    b.ToTable("MostPlayedArtistInformation");
                });

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.PlaylistSongItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PlaylistId")
                        .HasColumnType("int");

                    b.Property<int>("SongId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlaylistId");

                    b.HasIndex("SongId");

                    b.ToTable("PlaylistSongItems");
                });

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.Song", b =>
                {
                    b.Property<int>("SongId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SongId"));

                    b.Property<string>("ArtistName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsExplicit")
                        .HasColumnType("bit");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SongPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subgenre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SongId");

                    b.ToTable("Songs");
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

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.Sound", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoundFilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sounds");
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

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.UserDemographicsDetails", b =>
                {
                    b.Property<int>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("User_Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date_Of_fBirth")
                        .HasColumnType("datetime2");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Premium_User")
                        .HasColumnType("bit");

                    b.Property<string>("Race")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("User_Id");

                    b.ToTable("UserDemographicsDetails");
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

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.UserRoles", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UBB_SE_2024_923_1.Models.PlaylistSongItem", b =>
                {
                    b.HasOne("UBB_SE_2024_923_1.Models.Playlist", "Playlist")
                        .WithMany()
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UBB_SE_2024_923_1.Models.Song", "Song")
                        .WithMany()
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("Song");
                });
#pragma warning restore 612, 618
        }
    }
}
