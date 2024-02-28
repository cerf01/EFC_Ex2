﻿// <auto-generated />
using System;
using EFC_Ex2.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFC_Ex2.DAL.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240302132410_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EFC_Ex2.DAL.Moduls.Matches", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfMatch")
                        .HasColumnType("datetime2");

                    b.Property<int>("HittedGoalsByTeam1")
                        .HasColumnType("int");

                    b.Property<int>("HittedGoalsByTeam2")
                        .HasColumnType("int");

                    b.Property<string>("Winner")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("EFC_Ex2.DAL.Moduls.SoccerTeamComposition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("SoccerTeamCmp");
                });

            modelBuilder.Entity("EFC_Ex2.DAL.Moduls.SoccerTeams", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DefCount")
                        .HasColumnType("int");

                    b.Property<int>("DrawCount")
                        .HasColumnType("int");

                    b.Property<int?>("HittedGoals")
                        .HasColumnType("int");

                    b.Property<int?>("MatchesId")
                        .HasColumnType("int");

                    b.Property<int?>("MissedGoals")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WinCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MatchesId");

                    b.ToTable("SoccerTeams");
                });

            modelBuilder.Entity("EFC_Ex2.DAL.Moduls.SoccerTeamComposition", b =>
                {
                    b.HasOne("EFC_Ex2.DAL.Moduls.SoccerTeams", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("EFC_Ex2.DAL.Moduls.SoccerTeams", b =>
                {
                    b.HasOne("EFC_Ex2.DAL.Moduls.Matches", null)
                        .WithMany("Teams")
                        .HasForeignKey("MatchesId");
                });

            modelBuilder.Entity("EFC_Ex2.DAL.Moduls.Matches", b =>
                {
                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
