using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CryptoMaket.EFMarket_DAL.Models.DB
{
    public partial class CryptoMarketContext : DbContext
    {
        public virtual DbSet<CryptoCoinsHistory> CryptoCoinsHistory { get; set; }
        public virtual DbSet<CrytpoCoins> CrytpoCoins { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRefreshToken> UserRefreshToken { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }


        public CryptoMarketContext(DbContextOptions<CryptoMarketContext> options)
         : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CryptoCoinsHistory>(entity =>
            {
                entity.Property(e => e.Change1h)
                    .HasColumnName("Change_1H")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.Change1w)
                    .HasColumnName("Change_1W")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.Change24h)
                    .HasColumnName("Change_24H")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PriceBtc)
                    .HasColumnName("Price_BTC")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.PriceUsd)
                    .HasColumnName("Price_USD")
                    .HasColumnType("decimal(19, 6)");

                entity.Property(e => e.Symbol)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedTime)
                    .HasColumnName("Updated_Time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Volume24Usd)
                    .HasColumnName("Volume_24_USD")
                    .HasColumnType("decimal(19, 6)");

                entity.HasOne(d => d.Coin)
                    .WithMany(p => p.CryptoCoinsHistory)
                    .HasForeignKey(d => d.CoinId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CryptoCoinValues_CryptoCoins");
            });

            modelBuilder.Entity<CrytpoCoins>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRefreshToken>(entity =>
            {
                entity.Property(e => e.RefreshToken)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRefreshToken)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRefreshToken_User");
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_User");
            });
        }
    }
}
