﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PingTracker.Data;

#nullable disable

namespace PingTracker.Migrations
{
    [DbContext(typeof(PingTrackerContext))]
    partial class PingTrackerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PingTracker.Models.PingResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<long>("RTT")
                        .HasColumnType("bigint");

                    b.Property<int>("WebsiteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PingResults");
                });

            modelBuilder.Entity("PingTracker.Models.Website", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("AveragePing")
                        .HasColumnType("decimal(5,4)");

                    b.Property<int?>("PingResultId")
                        .HasColumnType("int");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("PingResultId");

                    b.ToTable("Websites");
                });

            modelBuilder.Entity("PingTracker.Models.Website", b =>
                {
                    b.HasOne("PingTracker.Models.PingResult", null)
                        .WithMany("Website")
                        .HasForeignKey("PingResultId");
                });

            modelBuilder.Entity("PingTracker.Models.PingResult", b =>
                {
                    b.Navigation("Website");
                });
#pragma warning restore 612, 618
        }
    }
}
