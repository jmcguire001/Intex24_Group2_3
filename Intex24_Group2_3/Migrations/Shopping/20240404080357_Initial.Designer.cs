﻿// <auto-generated />
using System;
using Intex24_Group2_3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Intex24_Group2_3.Migrations.Shopping
{
    [DbContext(typeof(ShoppingContext))]
    [Migration("20240404080357_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("Intex24_Group2_3.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProgramName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectImpact")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ProjectInstallation")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectPhase")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectType")
                        .HasColumnType("TEXT");

                    b.HasKey("ProjectId");

                    b.ToTable("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
