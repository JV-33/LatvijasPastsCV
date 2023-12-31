﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using LatvijasPastsCV.DBData;

#nullable disable

namespace LatvijasPastsCV.Migrations
{
    [DbContext(typeof(CVDbContext))]
    partial class CVDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("Pasts.Models.Adrese", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Iela")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int>("Indekss")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Numurs")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Pilseta")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Valsts")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Adreses");
                });

            modelBuilder.Entity("Pasts.Models.CV", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AdreseID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PamatdatiID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("AdreseID");

                    b.HasIndex("PamatdatiID");

                    b.ToTable("CV");
                });

            modelBuilder.Entity("Pasts.Models.DarbaPieredze", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Amats")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int?>("CVID")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("DarbaStazs")
                        .HasColumnType("TEXT");

                    b.Property<string>("IenemamaisAmats")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nosaukums")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int>("SlodzesApmers")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Stazs")
                        .HasColumnType("TEXT");

                    b.Property<string>("Vieta")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("CVID");

                    b.ToTable("DarbaPieredze");
                });

            modelBuilder.Entity("Pasts.Models.Izglitiba", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CVID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Fakultate")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Iestade")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("IzglitibasLimenis")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("MacibasLaiks")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nosaukums")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Statuss")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("StudijuVirziens")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("CVID");

                    b.ToTable("Izglitiba");
                });

            modelBuilder.Entity("Pasts.Models.Pamatdati", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EPasts")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Talrunis")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Uzvards")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Vards")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Pamatdati");
                });

            modelBuilder.Entity("Pasts.Models.Prasmes", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Apraksts")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int?>("CVID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Veids")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("CVID");

                    b.ToTable("Prasmes");
                });

            modelBuilder.Entity("Pasts.Models.CV", b =>
                {
                    b.HasOne("Pasts.Models.Adrese", "Adrese")
                        .WithMany()
                        .HasForeignKey("AdreseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pasts.Models.Pamatdati", "Pamatdati")
                        .WithMany()
                        .HasForeignKey("PamatdatiID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adrese");

                    b.Navigation("Pamatdati");
                });

            modelBuilder.Entity("Pasts.Models.DarbaPieredze", b =>
                {
                    b.HasOne("Pasts.Models.CV", null)
                        .WithMany("DarbaPieredzes")
                        .HasForeignKey("CVID");
                });

            modelBuilder.Entity("Pasts.Models.Izglitiba", b =>
                {
                    b.HasOne("Pasts.Models.CV", null)
                        .WithMany("Izglitiba")
                        .HasForeignKey("CVID");
                });

            modelBuilder.Entity("Pasts.Models.Prasmes", b =>
                {
                    b.HasOne("Pasts.Models.CV", null)
                        .WithMany("Prasmes")
                        .HasForeignKey("CVID");
                });

            modelBuilder.Entity("Pasts.Models.CV", b =>
                {
                    b.Navigation("DarbaPieredzes");

                    b.Navigation("Izglitiba");

                    b.Navigation("Prasmes");
                });
#pragma warning restore 612, 618
        }
    }
}
