﻿// <auto-generated />
using HienTangToc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HienTangToc.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240821164015_HienTangToc")]
    partial class HienTangToc
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HienTangToc.Models.HTvsSalonModel", b =>
                {
                    b.Property<int>("idNH")
                        .HasColumnType("int");

                    b.Property<int>("idSalon")
                        .HasColumnType("int");

                    b.Property<int>("HTvsSalonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HTvsSalonId"));

                    b.HasKey("idNH", "idSalon");

                    b.HasIndex("idSalon");

                    b.ToTable("HTvsSalon");
                });

            modelBuilder.Entity("HienTangToc.Models.MTvsSalonModel", b =>
                {
                    b.Property<int>("idNM")
                        .HasColumnType("int");

                    b.Property<int>("idSalon")
                        .HasColumnType("int");

                    b.Property<int>("MTvsSalonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MTvsSalonId"));

                    b.HasKey("idNM", "idSalon");

                    b.HasIndex("idSalon");

                    b.ToTable("MTvsSalon");
                });

            modelBuilder.Entity("HienTangToc.Models.NguoiHienModel", b =>
                {
                    b.Property<int>("idNH")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idNH"));

                    b.Property<string>("DiaChiNH")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailNH")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTenNH")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDTNH")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idNH");

                    b.ToTable("NguoiHien");
                });

            modelBuilder.Entity("HienTangToc.Models.NguoiMuonModel", b =>
                {
                    b.Property<int>("idNM")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idNM"));

                    b.Property<string>("DiaChiNM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailNM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTenNM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDTNM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idNM");

                    b.ToTable("NguoiMuon");
                });

            modelBuilder.Entity("HienTangToc.Models.SalonTocModel", b =>
                {
                    b.Property<int>("idSalon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idSalon"));

                    b.Property<string>("DiaChiSalon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDTSalon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenSalon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idSalon");

                    b.ToTable("SalonToc");
                });

            modelBuilder.Entity("HienTangToc.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HienTangToc.Models.HTvsSalonModel", b =>
                {
                    b.HasOne("HienTangToc.Models.NguoiHienModel", "NguoiHienModel")
                        .WithMany("HTvsSalonModels")
                        .HasForeignKey("idNH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HienTangToc.Models.SalonTocModel", "SalonTocModel")
                        .WithMany("HTvsSalonModels")
                        .HasForeignKey("idSalon")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiHienModel");

                    b.Navigation("SalonTocModel");
                });

            modelBuilder.Entity("HienTangToc.Models.MTvsSalonModel", b =>
                {
                    b.HasOne("HienTangToc.Models.NguoiMuonModel", "NguoiMuonModel")
                        .WithMany("MTvsSalonModels")
                        .HasForeignKey("idNM")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HienTangToc.Models.SalonTocModel", "SalonTocModel")
                        .WithMany("MTvsSalonModels")
                        .HasForeignKey("idSalon")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiMuonModel");

                    b.Navigation("SalonTocModel");
                });

            modelBuilder.Entity("HienTangToc.Models.NguoiHienModel", b =>
                {
                    b.Navigation("HTvsSalonModels");
                });

            modelBuilder.Entity("HienTangToc.Models.NguoiMuonModel", b =>
                {
                    b.Navigation("MTvsSalonModels");
                });

            modelBuilder.Entity("HienTangToc.Models.SalonTocModel", b =>
                {
                    b.Navigation("HTvsSalonModels");

                    b.Navigation("MTvsSalonModels");
                });
#pragma warning restore 612, 618
        }
    }
}
