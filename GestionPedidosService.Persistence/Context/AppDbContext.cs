﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GestionPedidosService.Domain.Base;
using GestionPedidosService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionPedidosService.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ModelConfig(builder);
            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ProcessAuditing();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ModelConfig(ModelBuilder builder)
        {
            builder.Entity<PatternDimension>(eb =>
            {
                eb.HasKey(e => e.Id);
                eb.Property(e => e.Label).HasColumnType("nvarchar(50)").IsRequired();
                eb.Property(e => e.Value).HasColumnType("decimal(6,2)").IsRequired();
                eb.Property(e => e.Units).HasColumnType("nvarchar(5)").IsRequired();
                eb.HasOne(e => e.PatternGarment)
                    .WithMany(e => e.PatternDimensions);
            });

            builder.Entity<PatternGarment>(eb =>
            {
                eb.HasKey(e => e.Id);
                eb.Property(e => e.TypePattern).HasColumnType("nvarchar(20)").IsRequired();
                eb.Property(e => e.ImagePattern).HasColumnType("nvarchar(max)").IsRequired();
                eb.Property(e => e.ScaledStatus).HasColumnType("nvarchar(20)").IsRequired();
                eb.HasOne(e => e.Garment)
                    .WithMany(e => e.PatternGarments);
            });

            builder.Entity<FeatureGarment>(eb =>
            {
                eb.HasKey(e => e.Id);
                eb.Property(e => e.Value).HasColumnType("nvarchar(max)").IsRequired();
                eb.Property(e => e.TypeFeature).HasColumnType("nvarchar(20)").IsRequired();
                eb.HasOne(e => e.Garment)
                    .WithMany(e => e.FeatureGarments);
            });

            builder.Entity<Garment>(eb =>
            {
                eb.HasKey(e => e.Id);
                eb.Property(e => e.CodeGarment).HasColumnType("nvarchar(100)").IsRequired();
                eb.Property(e => e.NameGarment).HasColumnType("nvarchar(100)").IsRequired();
                eb.Property(e => e.FirstRangePrice).HasColumnType("decimal(10,2)").IsRequired();
                eb.Property(e => e.SecondRangePrice).HasColumnType("decimal(10,2)").IsRequired();
                eb.Property(e => e.Available).HasColumnType("tinyint").IsRequired();
                eb.Property(e => e.AtelierId).IsRequired();
            });

            builder.Entity<Order>(eb =>
            {
                eb.HasKey(e => e.Id);
                eb.Property(e => e.CodeOrder).HasColumnType("nvarchar(20)").IsRequired();
                eb.Property(e => e.OrderStatus).IsRequired();
                eb.Property(e => e.OrderDate).IsRequired();
                eb.Property(e => e.AtelierId).IsRequired();
                eb.Property(e => e.UserAtelierId).IsRequired();
                eb.Property(e => e.UserAtelierId).IsRequired();
                eb.HasMany(e => e.Garments)
                    .WithMany(e => e.Orders)
                    .UsingEntity<OrderDetail>(
                        j => j.HasOne(e => e.Garment)
                            .WithMany(e => e.OrderDetails)
                            .HasForeignKey(e => e.GarmentId),
                        j => j.HasOne(e => e.Order)
                            .WithMany(e => e.OrderDetails)
                            .HasForeignKey(e => e.OrderId),
                        j =>
                        {
                            j.HasKey(e => e.Id);
                            j.Property(e => e.Color).HasColumnType("nvarchar(7)").IsRequired();
                        }
                    );
            });
        }

        private void ProcessAuditing()
        {
            foreach (var item in ChangeTracker.Entries().Where(e => 
                e.State == EntityState.Added && 
                e.Entity is Auditable))
            {
                var entity = item.Entity as Auditable;
                entity.CreatedBy = "system";
                entity.CreatedDate = DateTimeOffset.Now;
            }

            foreach (var item in ChangeTracker.Entries().Where(e =>
                e.State == EntityState.Modified &&
                e.Entity is Auditable))
            {
                var entity = item.Entity as Auditable;
                entity.ModifiedBy = "system";
                entity.ModifiedDate = DateTimeOffset.Now;
                item.Property(nameof(entity.CreatedDate)).IsModified = false;
                item.Property(nameof(entity.CreatedBy)).IsModified = false;
            }
        }
    }
}