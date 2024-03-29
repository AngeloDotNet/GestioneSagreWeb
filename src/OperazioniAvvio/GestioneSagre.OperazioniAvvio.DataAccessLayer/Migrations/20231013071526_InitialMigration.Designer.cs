﻿// <auto-generated />
using System;
using GestioneSagre.OperazioniAvvio.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestioneSagre.OperazioniAvvio.DataAccessLayer.Migrations
{
    [DbContext(typeof(OperazioniAvvioDbContext))]
    [Migration("20231013071526_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities.Festa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataFine")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataInizio")
                        .HasColumnType("date");

                    b.Property<string>("StatusFesta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Feste", (string)null);
                });

            modelBuilder.Entity("GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities.Impostazione", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("GestioneCategorie")
                        .HasColumnType("bit");

                    b.Property<bool>("GestioneMenu")
                        .HasColumnType("bit");

                    b.Property<Guid>("IdFesta")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("StampaCarta")
                        .HasColumnType("bit");

                    b.Property<bool>("StampaRicevuta")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IdFesta");

                    b.ToTable("Impostazioni", (string)null);
                });

            modelBuilder.Entity("GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities.Intestazione", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Edizione")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("IdFesta")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Luogo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Titolo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("IdFesta");

                    b.ToTable("Intestazioni", (string)null);
                });

            modelBuilder.Entity("GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities.Impostazione", b =>
                {
                    b.HasOne("GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities.Festa", "Festa")
                        .WithMany("Impostazioni")
                        .HasForeignKey("IdFesta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Festa");
                });

            modelBuilder.Entity("GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities.Intestazione", b =>
                {
                    b.HasOne("GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities.Festa", "Festa")
                        .WithMany("Intestazioni")
                        .HasForeignKey("IdFesta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Festa");
                });

            modelBuilder.Entity("GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities.Festa", b =>
                {
                    b.Navigation("Impostazioni");

                    b.Navigation("Intestazioni");
                });
#pragma warning restore 612, 618
        }
    }
}
