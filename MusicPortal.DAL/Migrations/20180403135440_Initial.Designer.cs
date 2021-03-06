﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MusicPortal.DAL.EF;
using System;

namespace MusicPortal.DAL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20180403135440_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MusicPortal.DAL.Entities.Album", b =>
                {
                    b.Property<string>("AlbumId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArtistId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PictureURL")
                        .IsRequired();

                    b.HasKey("AlbumId");

                    b.HasIndex("ArtistId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("MusicPortal.DAL.Entities.Artist", b =>
                {
                    b.Property<string>("ArtistId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PictureURL");

                    b.HasKey("ArtistId");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("MusicPortal.DAL.Entities.Song", b =>
                {
                    b.Property<string>("SondId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlbumId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("SongURL")
                        .IsRequired();

                    b.HasKey("SondId");

                    b.HasIndex("AlbumId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("MusicPortal.DAL.Entities.Album", b =>
                {
                    b.HasOne("MusicPortal.DAL.Entities.Artist", "Artist")
                        .WithMany("Albums")
                        .HasForeignKey("ArtistId");
                });

            modelBuilder.Entity("MusicPortal.DAL.Entities.Song", b =>
                {
                    b.HasOne("MusicPortal.DAL.Entities.Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId");
                });
#pragma warning restore 612, 618
        }
    }
}
