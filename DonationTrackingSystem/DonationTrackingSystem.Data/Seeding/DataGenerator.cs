using DonationTrackingSystem.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace DonationTrackingSystem.Data.Seeding
{
    public static class DataGenerator
    {
        public static IEnumerable<IdentityUser> SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();
            var users = new List<IdentityUser>
    {
        new IdentityUser
        {
            Id = "3ed381b8-6973-4b97-94ab-988ab41458a9",
            UserName = "campaignCreator@mail.com",
            NormalizedUserName = "CAMPAIGNCREATOR@MAIL.COM",
            Email = "campaignCreator@mail.com",
            NormalizedEmail = "CAMPAIGNCREATOR@MAIL.COM"
        },
        new IdentityUser
        {
            Id = "de2758f2-1c58-490c-8445-835b9dc348b0",
            UserName = "campaignCreator2@mail.com",
            NormalizedUserName = "CAMPAIGNCREATOR2@MAIL.COM",
            Email = "campaignCreator2@mail.com",
            NormalizedEmail = "CAMPAIGNCREATOR2@MAIL.COM"
        },
        new IdentityUser
        {
            Id = "5d6ca01f-2556-4861-99f3-dbceb4e0bb99",
            UserName = "guest@mail.com",
            NormalizedUserName = "GUEST@MAIL.COM",
            Email = "guest@mail.com",
            NormalizedEmail = "GUEST@MAIL.COM"
        }
    };

            foreach (var user in users)
            {
                user.PasswordHash = hasher.HashPassword(user, "123456");
            }

            return users;
        }

        public static IEnumerable<CampaignCreator> SeedCampaignCreators()
        {
            var campaignCreators = new List<CampaignCreator>
    {
        new CampaignCreator
        {
            Id = Guid.Parse("3ed381b8-6973-4b97-94ab-988ab41458a9"),
            Username = "Chicho Krasi",
            UserId = "3ed381b8-6973-4b97-94ab-988ab41458a9"
        },
        new CampaignCreator
        {
            Id = Guid.Parse("de2758f2-1c58-490c-8445-835b9dc348b0"),
            Username = "Boyko Borissov",
            UserId = "de2758f2-1c58-490c-8445-835b9dc348b0"
        }
    };
            return campaignCreators;
        }


        public static IEnumerable<Campaign> SeedCampaigns()
        {
            var donations = SeedDonations();

            var campaigns = new List<Campaign>
    {
        new Campaign
        {
            Id = 1,
            CampaignCreatorId = Guid.Parse("3ed381b8-6973-4b97-94ab-988ab41458a9"),
            Name = "SoftUni Buditel",
            Description = "High school. Give us money because why not.",
            GoalAmount = 69000,
            TotalAmountDonated = donations.Where(d => d.CampaignId == 1).Sum(d => d.Amount)
        },
        new Campaign
        {
            Id = 2,
            CampaignCreatorId = Guid.Parse("de2758f2-1c58-490c-8445-835b9dc348b0"),
            Name = "Gerb-SDS",
            Description = "Political party.",
            GoalAmount = 100000,
            TotalAmountDonated = donations.Where(d => d.CampaignId == 2).Sum(d => d.Amount)
        }
    };
            return campaigns;
        }

        public static IEnumerable<Donation> SeedDonations()
        {
            var donations = new List<Donation>
    {
        new Donation
        {
            Id = 1,
            DonatorId = "5d6ca01f-2556-4861-99f3-dbceb4e0bb99",
            Amount = 300,
            CampaignId = 2
        }
    };
            return donations;
        }
    }
}
