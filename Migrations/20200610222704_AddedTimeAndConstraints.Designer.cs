﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheTowerAPI.Models;

namespace TheTowerAPI.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20200610222704_AddedTimeAndConstraints")]
    partial class AddedTimeAndConstraints
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TheTowerAPI.Models.Level", b =>
                {
                    b.Property<string>("LevelName")
                        .HasColumnType("varchar(30) CHARACTER SET utf8mb4")
                        .HasMaxLength(30);

                    b.HasKey("LevelName");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("TheTowerAPI.Models.Record", b =>
                {
                    b.Property<string>("Nickname")
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.Property<string>("LevelName")
                        .HasColumnType("varchar(30) CHARACTER SET utf8mb4")
                        .HasMaxLength(30);

                    b.Property<long>("Time")
                        .HasColumnType("bigint");

                    b.HasKey("Nickname", "LevelName");

                    b.HasIndex("LevelName");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("TheTowerAPI.Models.User", b =>
                {
                    b.Property<string>("Nickname")
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Nickname");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasDiscriminator<bool>("IsAdmin").HasValue(false);
                });

            modelBuilder.Entity("TheTowerAPI.Models.Admin", b =>
                {
                    b.HasBaseType("TheTowerAPI.Models.User");

                    b.HasDiscriminator().HasValue(true);
                });

            modelBuilder.Entity("TheTowerAPI.Models.Record", b =>
                {
                    b.HasOne("TheTowerAPI.Models.Level", "Level")
                        .WithMany("Records")
                        .HasForeignKey("LevelName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheTowerAPI.Models.User", "User")
                        .WithMany("Records")
                        .HasForeignKey("Nickname")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}