﻿// <auto-generated />
using Cuidados.Caninos.Marcos.Montiel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Cuidados.Caninos.Marcos.Montiel.Migrations
{
    [DbContext(typeof(CCContext))]
    partial class CCContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("Cuidados.Caninos.Marcos.Montiel.Models.ComCatEscolaridad", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("ComCatEscolaridad");
                });

            modelBuilder.Entity("Cuidados.Caninos.Marcos.Montiel.Models.ComCatSexo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("ComCatSexo");
                });

            modelBuilder.Entity("Cuidados.Caninos.Marcos.Montiel.Models.ComPersona", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AMaterno")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("APaterno")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Curp")
                        .IsRequired()
                        .HasMaxLength(18);

                    b.Property<int>("FKComCatEscolaridad");

                    b.Property<int>("FKComCatSexo");

                    b.Property<DateTime>("FechaNac");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("FKComCatEscolaridad");

                    b.HasIndex("FKComCatSexo");

                    b.ToTable("ComPersona");
                });

            modelBuilder.Entity("Cuidados.Caninos.Marcos.Montiel.Models.ComPersona", b =>
                {
                    b.HasOne("Cuidados.Caninos.Marcos.Montiel.Models.ComCatEscolaridad", "ComCatEscolaridad")
                        .WithMany()
                        .HasForeignKey("FKComCatEscolaridad")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Cuidados.Caninos.Marcos.Montiel.Models.ComCatSexo", "ComCatSexo")
                        .WithMany()
                        .HasForeignKey("FKComCatSexo")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
