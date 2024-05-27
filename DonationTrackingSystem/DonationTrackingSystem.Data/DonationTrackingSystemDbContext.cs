using DonationTrackingSystem.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Emit;
using DonationTrackingSystem.Data.Seeding;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Linq;


namespace DonationTrackingSystem.Data
{
    public class DonationTrackingSystemDbContext : IdentityDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DonationTrackingSystemDbContext(DbContextOptions<DonationTrackingSystemDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
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

            builder.Entity<Campaign>()
                .HasOne(u => u.CampaignCreator)
                .WithMany(c => c.ManagedCampaigns)
                .HasForeignKey(cc => cc.CampaignCreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            SeedData(builder);
        }

        private static void SeedData(ModelBuilder builder)
        {
            // Seed Users
            builder.Entity<IdentityUser>()
                .HasData(DataGenerator.SeedUsers());

            // Seed Campaign Creators
            var campaignCreators = DataGenerator.SeedCampaignCreators();
            builder.Entity<CampaignCreator>()
                .HasData(campaignCreators);

            // Seed Campaigns
            var campaigns = DataGenerator.SeedCampaigns();
            builder.Entity<Campaign>()
                .HasData(campaigns);

            // Seed Donations
            builder.Entity<Donation>()
                .HasData(DataGenerator.SeedDonations());
        }


        public override int SaveChanges()
        {
            UpdateCampaignTotalAmountDonated();
/*            SetDonatorIdForNewDonations();
*/            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateCampaignTotalAmountDonated();
            return await base.SaveChangesAsync(cancellationToken);
        }

        /*private void SetDonatorIdForNewDonations()
        {
            var newDonations = ChangeTracker.Entries<Donation>()
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity)
                .ToList();

            foreach (var donation in newDonations)
            {
                // Set the DonatorId property to the authenticated user's ID
                donation.DonatorId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        }
*/
        private void UpdateCampaignTotalAmountDonated()
        {
            var newDonations = ChangeTracker.Entries<Donation>()
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity)
                .ToList();

            foreach (var donation in newDonations)
            {
                var campaign = Campaigns.FirstOrDefault(c => c.Id == donation.CampaignId);
                if (campaign != null)
                {
                    campaign.TotalAmountDonated += donation.Amount;
                }
            }
        }
    }
}