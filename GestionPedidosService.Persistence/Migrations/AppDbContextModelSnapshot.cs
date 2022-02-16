﻿// <auto-generated />
using System;
using GestionPedidosService.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GestionPedidosService.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GestionPedidosService.Domain.Entities.FeatureGarment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<int>("GarmentId")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("TypeFeature")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GarmentId");

                    b.ToTable("FeatureGarments");
                });

            modelBuilder.Entity("GestionPedidosService.Domain.Entities.Garment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AtelierId")
                        .HasColumnType("int");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<string>("CodeGarment")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<decimal>("FirstRangePrice")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<string>("NameGarment")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("SecondRangePrice")
                        .HasColumnType("decimal(10,2)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Garments");
                });

            modelBuilder.Entity("GestionPedidosService.Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AtelierId")
                        .HasColumnType("int");

                    b.Property<string>("CodeOrder")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime");

                    b.Property<byte>("OrderStatus")
                        .HasColumnType("tinyint");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int?>("UserAtelierId")
                        .HasColumnType("int");

                    b.Property<int>("UserClientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("GestionPedidosService.Domain.Entities.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<int>("GarmentId")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset(7)");

                    b.Property<byte>("OrderDetailStatus")
                        .HasColumnType("tinyint");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<byte>("Quantity")
                        .HasColumnType("tinyint");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GarmentId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("GestionPedidosService.Domain.Entities.PatternDimension", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PatternGarmentId")
                        .HasColumnType("int");

                    b.Property<string>("Units")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(6,2)");

                    b.HasKey("Id");

                    b.HasIndex("PatternGarmentId");

                    b.ToTable("PatternDimensions");
                });

            modelBuilder.Entity("GestionPedidosService.Domain.Entities.PatternGarment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GarmentId")
                        .HasColumnType("int");

                    b.Property<string>("ImagePattern")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ScaledStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("TypePattern")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("GarmentId");

                    b.ToTable("PatternGarments");
                });

            modelBuilder.Entity("GestionPedidosService.Domain.Entities.FeatureGarment", b =>
                {
                    b.HasOne("GestionPedidosService.Domain.Entities.Garment", "Garment")
                        .WithMany("FeatureGarments")
                        .HasForeignKey("GarmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Garment");
                });

            modelBuilder.Entity("GestionPedidosService.Domain.Entities.OrderDetail", b =>
                {
                    b.HasOne("GestionPedidosService.Domain.Entities.Garment", "Garment")
                        .WithMany("OrderDetails")
                        .HasForeignKey("GarmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionPedidosService.Domain.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Garment");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("GestionPedidosService.Domain.Entities.PatternDimension", b =>
                {
                    b.HasOne("GestionPedidosService.Domain.Entities.PatternGarment", "PatternGarment")
                        .WithMany("PatternDimensions")
                        .HasForeignKey("PatternGarmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PatternGarment");
                });

            modelBuilder.Entity("GestionPedidosService.Domain.Entities.PatternGarment", b =>
                {
                    b.HasOne("GestionPedidosService.Domain.Entities.Garment", "Garment")
                        .WithMany("PatternGarments")
                        .HasForeignKey("GarmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Garment");
                });

            modelBuilder.Entity("GestionPedidosService.Domain.Entities.Garment", b =>
                {
                    b.Navigation("FeatureGarments");

                    b.Navigation("OrderDetails");

                    b.Navigation("PatternGarments");
                });

            modelBuilder.Entity("GestionPedidosService.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("GestionPedidosService.Domain.Entities.PatternGarment", b =>
                {
                    b.Navigation("PatternDimensions");
                });
#pragma warning restore 612, 618
        }
    }
}
