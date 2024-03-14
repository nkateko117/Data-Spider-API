using System;
using System.ComponentModel.DataAnnotations;
using Data_Spider_API.DataModels;
using Data_Spider_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data_Spider_API.DBContext
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Enable sensitive data logging
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<AnalysisTool> AnalysisTools { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<FundamentalEvent> FundamentalEvents { get; set; }
        public DbSet<MarketType> MarketTypes { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<TradingAccount> TradingAccounts { get; set; }
        public DbSet<TradingInstrument> TradingInstruments { get; set; }
        public DbSet<UserAnalysis> UserAnalysis { get; set; }
        public DbSet<UserTimeZone> UserTimeZone { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Demo Broker
            modelBuilder.Entity<Broker>()
                .HasData(
                new
                {
                    BrokerID = 999,
                    BrokerName = "Generic Broker"
                }
                );


            modelBuilder.Entity<MarketType>()
                .HasData(
                new
                {
                    MarketTypeID = 1,
                    MarketTypeName = "Index"
                }
                );

            modelBuilder.Entity<MarketType>()
                .HasData(
                new
                {
                    MarketTypeID = 2,
                    MarketTypeName = "Forex"
                }
                );

            modelBuilder.Entity<MarketType>()
                .HasData(
                new
                {
                    MarketTypeID = 3,
                    MarketTypeName = "Stock"
                }
                );

            modelBuilder.Entity<MarketType>()
                .HasData(
                new
                {
                    MarketTypeID = 4,
                    MarketTypeName = "Crypto"
                }
                );

            modelBuilder.Entity<MarketType>()
                .HasData(
                new
                {
                    MarketTypeID = 5,
                    MarketTypeName = "EFT"
                }
                );

            modelBuilder.Entity<MarketType>()
                .HasData(
                new
                {
                    MarketTypeID = 6,
                    MarketTypeName = "Future"
                }
                );

            modelBuilder.Entity<MarketType>()
                .HasData(
                new
                {
                    MarketTypeID = 7,
                    MarketTypeName = "Bond"
                }
                );
        }
    }
}
