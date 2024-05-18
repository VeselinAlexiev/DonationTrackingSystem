using DonationTrackingSystem.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationTrackingSystem.Data.Seeding
{
    public static class DataGenerator
    {
        public static IEnumerable<IdentityUser> SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();
            IEnumerable<IdentityUser> users = new List<IdentityUser>()
        {
            new IdentityUser()
            {
                Id = "00c8a77c-98d2-4864-88fd-b37f659e7133",
                UserName = "campaignCreator@mail.com",
                NormalizedUserName = "CAMPAIGNCREATOR@MAIL.COM",
                Email = "campaignCreator@mail.com",
                NormalizedEmail = "CAMPAIGNCREATOR@MAIL.COM"
            },
            new IdentityUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "campaignCreator2@mail.com",
                NormalizedUserName = "CAMPAIGNCREATOR2@MAIL.COM",
                Email = "campaignCreator2@mail.com",
                NormalizedEmail = "CAMPAIGNCREATOR2@MAIL.COM"
            },
            new IdentityUser()
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

        public static IEnumerable<CampaignCreator> SeedCampaignCreator()
        {
            IEnumerable<CampaignCreator> campaignCreators = new List<CampaignCreator>()
        {
            new CampaignCreator()
            {
                Id = Guid.Parse("3ed381b8-6973-4b97-94ab-988ab41458a9"),
                UserId = "00c8a77c-98d2-4864-88fd-b37f659e7133",
            },
            new CampaignCreator()
            {
                Id = Guid.Parse("de2758f2-1c58-490c-8445-835b9dc348b0"),
                UserId = "dea12856-c198-4129-b3f3-b893d8395082",
            }
        };
            return campaignCreators;
        }

        public static IEnumerable<Campaign> SeedCampaign()
        {
            IEnumerable<Campaign> campaigns = new List<Campaign>()
        {
            new Campaign()
            {
                Id = 1,
                CampaignCreatorId = Guid.Parse("3ed381b8-6973-4b97-94ab-988ab41458a9"),
                Name = "SoftUni Buditel",
                Description = "High school. Give us money because why not.",
                GoalAmount = 69000,
            },
            new Campaign()
            {
                Id = 2,
                CampaignCreatorId = Guid.Parse("de2758f2-1c58-490c-8445-835b9dc348b0"),
                Name = "Gerb-SDS",
                Description = "Political party.",
                GoalAmount = 100000,
            }
        };
            return campaigns;
        }

        public static IEnumerable<Donation> SeedDonations()
        {
            IEnumerable<Donation> donations = new List<Donation>()
        {
            new Donation()
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
