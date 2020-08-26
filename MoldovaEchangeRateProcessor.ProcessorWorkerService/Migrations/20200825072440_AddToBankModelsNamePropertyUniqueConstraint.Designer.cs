﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoldovaEchangeRateProcessor.ProcessorWorkerService.Data;

namespace MoldovaEchangeRateProcessor.ProcessorWorkerService.Migrations
{
    [DbContext(typeof(SqlServerDbContext))]
    [Migration("20200825072440_AddToBankModelsNamePropertyUniqueConstraint")]
    partial class AddToBankModelsNamePropertyUniqueConstraint
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MoldovaExchangeRateProcessor.WebParser.Models.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Bank");
                });

            modelBuilder.Entity("MoldovaExchangeRateProcessor.WebParser.Models.ExchangeRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BankId")
                        .HasColumnType("int");

                    b.Property<double>("BuyRate")
                        .HasColumnType("float");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("SellRate")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("MoldovaExchangeRateProcessor.WebParser.Models.ExchangeRate", b =>
                {
                    b.HasOne("MoldovaExchangeRateProcessor.WebParser.Models.Bank", "Bank")
                        .WithMany("ExchangeRates")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
