﻿// <auto-generated />
using System;
using GestioneSagre.Utility.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestioneSagre.Utility.DataAccessLayer.Migrations
{
    [DbContext(typeof(UtilityDbContext))]
    [Migration("20230916220420_AddCurrencyTable")]
    partial class AddCurrencyTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GestioneSagre.Utility.DataAccessLayer.Entities.ClienteTipo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Valore")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("TipoCliente", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("b8fbba5b-8395-42be-bafb-c1bdffc141c0"),
                            Valore = "CLIENTE"
                        },
                        new
                        {
                            Id = new Guid("21a3647c-ebf6-4b3e-afd5-054ee7e53343"),
                            Valore = "STAFF"
                        });
                });

            modelBuilder.Entity("GestioneSagre.Utility.DataAccessLayer.Entities.PagamentoTipo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Valore")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("TipoPagamento", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("f88348a3-6cbb-4108-a79e-e38bc07824d2"),
                            Valore = "CONTANTI"
                        });
                });

            modelBuilder.Entity("GestioneSagre.Utility.DataAccessLayer.Entities.ScontrinoStato", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Valore")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("StatoScontrino", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("ba8d1d47-2980-4081-adfa-59a8a283c168"),
                            Valore = "APERTO"
                        },
                        new
                        {
                            Id = new Guid("d3bca587-1d4b-478f-a1cf-77283cacad54"),
                            Valore = "IN ELABORAZIONE"
                        },
                        new
                        {
                            Id = new Guid("ada6fe1c-1cff-41fa-88d5-adde721ccb6e"),
                            Valore = "DA INCASSARE"
                        },
                        new
                        {
                            Id = new Guid("7a449f0c-773a-4e1b-b195-48765c93b64a"),
                            Valore = "PAGATO"
                        },
                        new
                        {
                            Id = new Guid("cd7435a1-8352-451e-9640-4b55bcec0d3f"),
                            Valore = "CHIUSO"
                        },
                        new
                        {
                            Id = new Guid("3cc4bff5-0e3a-48e5-bc0a-a6518b4f9227"),
                            Valore = "ANNULLATO"
                        });
                });

            modelBuilder.Entity("GestioneSagre.Utility.DataAccessLayer.Entities.ScontrinoTipo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Valore")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("TipoScontrino", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("c1b77737-f730-4220-a556-92ca67203779"),
                            Valore = "PAGAMENTO"
                        },
                        new
                        {
                            Id = new Guid("4fd0dba1-d49e-4d33-8ce2-fc35bb607141"),
                            Valore = "OMAGGIO"
                        });
                });

            modelBuilder.Entity("GestioneSagre.Utility.DataAccessLayer.Entities.Valuta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Simbolo")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Valore")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("Id");

                    b.ToTable("Currency", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("39b42616-38bf-4ca8-ac47-e7ab5415247e"),
                            Descrizione = "Euro",
                            Simbolo = "€",
                            Valore = "EUR"
                        },
                        new
                        {
                            Id = new Guid("4cb5ba1f-0a26-4904-9335-a4e7a6df957b"),
                            Descrizione = "Dollaro USA",
                            Simbolo = "$",
                            Valore = "USD"
                        },
                        new
                        {
                            Id = new Guid("1062f8a7-07ae-4ff3-9122-e1061344e968"),
                            Descrizione = "Sterlina UK",
                            Simbolo = "£",
                            Valore = "GBP"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
