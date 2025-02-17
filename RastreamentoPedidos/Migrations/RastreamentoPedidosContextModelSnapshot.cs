﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RastreamentoPedidos.Data;

#nullable disable

namespace RastreamentoPedidos.Migrations
{
    [DbContext(typeof(RastreamentoPedidosContext))]
    partial class RastreamentoPedidosContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RastreamentoPedidos.Model.Cliente", b =>
                {
                    b.Property<int>("id_cliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("id_cliente"));

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("email");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("nome");

                    b.Property<string>("telefone")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("telefone");

                    b.HasKey("id_cliente");

                    b.ToTable("clientes", (string)null);
                });

            modelBuilder.Entity("RastreamentoPedidos.Model.Encomenda", b =>
                {
                    b.Property<int>("id_encomenda")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("id_encomenda"));

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("data_encomenda")
                        .HasColumnType("timestamp")
                        .HasColumnName("data_pedido");

                    b.Property<string>("descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar")
                        .HasColumnName("descricao");

                    b.Property<int>("id_cliente")
                        .HasColumnType("integer")
                        .HasColumnName("id_cliente");

                    b.HasKey("id_encomenda");

                    b.HasIndex("id_cliente");

                    b.ToTable("encomendas", (string)null);
                });

            modelBuilder.Entity("RastreamentoPedidos.Model.StatusEntrega", b =>
                {
                    b.Property<int>("id_status_entrega")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_encomenda");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("id_status_entrega"));

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp")
                        .HasColumnName("data_atualizacao");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("status");

                    b.HasKey("id_status_entrega");

                    b.ToTable("status_entrega", (string)null);
                });

            modelBuilder.Entity("RastreamentoPedidos.Model.Encomenda", b =>
                {
                    b.HasOne("RastreamentoPedidos.Model.Cliente", "cliente")
                        .WithMany("encomendas")
                        .HasForeignKey("id_cliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cliente");
                });

            modelBuilder.Entity("RastreamentoPedidos.Model.StatusEntrega", b =>
                {
                    b.HasOne("RastreamentoPedidos.Model.Encomenda", "encomenda")
                        .WithMany("statusEntregas")
                        .HasForeignKey("id_status_entrega")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("encomenda");
                });

            modelBuilder.Entity("RastreamentoPedidos.Model.Cliente", b =>
                {
                    b.Navigation("encomendas");
                });

            modelBuilder.Entity("RastreamentoPedidos.Model.Encomenda", b =>
                {
                    b.Navigation("statusEntregas");
                });
#pragma warning restore 612, 618
        }
    }
}
