using DonationTrackingSystem.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Emit;
using DonationTrackingSystem.Data.Seeding;

namespace DonationTrackingSystem.Data
{
    public class DonationTrackingSystemDbContext : IdentityDbContext
    {
        public DonationTrackingSystemDbContext(DbContextOptions<DonationTrackingSystemDbContext> options)
        : base(options)
        {
        }

        public DbSet<Campaign> Campaigns { get; set; } = null!;
        public DbSet<CampaignCreator> CampaignCreators { get; set; } = null!;
        public DbSet<Donation> Donations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Donation>()
                .HasOne(d => d.Campaign)
                .WithMany(c => c.Donations)
                .HasForeignKey(d => d.CampaignId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CampaignCreator>()
                .HasOne(cc => cc.User)
                .WithMany()
                .HasForeignKey(cc => cc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            SeedData(builder);
        }
        private void SeedData(ModelBuilder builder)
        {
            builder.Entity<IdentityUser>()
                .HasData(DataGenerator.SeedUsers());

            builder.Entity<CampaignCreator>()
                .HasData(DataGenerator.SeedCampaignCreator());

            builder.Entity<Campaign>()
                .HasData(DataGenerator.SeedCampaign());

            builder.Entity<Donation>()
                .HasData(DataGenerator.SeedDonations());
        }
    }
}