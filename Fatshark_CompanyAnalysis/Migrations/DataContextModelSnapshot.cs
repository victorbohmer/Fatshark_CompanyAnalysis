﻿// <auto-generated />
using Fatshark_CompanyAnalysis.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fatshark_CompanyAnalysis.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Fatshark_CompanyAnalysis.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("CompanyName")
                        .HasColumnType("TEXT");

                    b.Property<int>("CompanySetId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("County")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone2")
                        .HasColumnType("TEXT");

                    b.Property<string>("Postal")
                        .HasColumnType("TEXT");

                    b.Property<string>("Web")
                        .HasColumnType("TEXT");

                    b.HasKey("CompanyId");

                    b.HasIndex("CompanySetId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Fatshark_CompanyAnalysis.Models.CompanySet", b =>
                {
                    b.Property<int>("CompanySetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("CompanySetId");

                    b.ToTable("CompanySets");
                });

            modelBuilder.Entity("Fatshark_CompanyAnalysis.Models.PostcodeInfo", b =>
                {
                    b.Property<int>("PostcodeInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("country")
                        .HasColumnType("TEXT");

                    b.Property<int>("eastings")
                        .HasColumnType("INTEGER");

                    b.Property<int>("northings")
                        .HasColumnType("INTEGER");

                    b.Property<string>("postcode")
                        .HasColumnType("TEXT");

                    b.HasKey("PostcodeInfoId");

                    b.ToTable("PostcodeInfos");
                });

            modelBuilder.Entity("Fatshark_CompanyAnalysis.Models.Company", b =>
                {
                    b.HasOne("Fatshark_CompanyAnalysis.Models.CompanySet", null)
                        .WithMany("Companies")
                        .HasForeignKey("CompanySetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fatshark_CompanyAnalysis.Models.CompanySet", b =>
                {
                    b.Navigation("Companies");
                });
#pragma warning restore 612, 618
        }
    }
}
