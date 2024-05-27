using DonationTrackingSystem.Common;
using DonationTrackingSystem.Data;
using DonationTrackingSystem.Data.Models.Campaigns;
using DonationTrackingSystem.Data.Models.Donations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DonationTrackingSystem.Controllers
{
    public class CampaignsController : Controller
    {
        private readonly DonationTrackingSystemDbContext data;

        public CampaignsController(DonationTrackingSystemDbContext data)
            => this.data = data;

        public IActionResult All()
        {
            AllViewModel campaigns = new AllViewModel()
            {
                TotalCampaigns = this.data.Campaigns.Count(),
                TotalDonations = this.data.Donations.Count(),
                Campaigns = this.data.Campaigns
                    .Select(c => new CampaignAllViewModel()
                    {
                        Id = c.Id,
                        CampaignName = c.Name,
                        GoalPercentage = (double)c.TotalAmountDonated / (double)c.GoalAmount * 100,
                    })
                    .ToList()
            };

            return View(campaigns);
        }

        public IActionResult Details(int id)
        {
            var campaign = this.data.Campaigns
                            .Include(c => c.Donations)
                            .ThenInclude(d => d.Donator)
                            .FirstOrDefault(c => c.Id == id);

            if (campaign is null)
            {
                return BadRequest();
            }

            CampaignDetailsViewModel campaignModel = new CampaignDetailsViewModel()
            {
                Id = campaign.Id,
                CampaignName = campaign.Name,
                CampaignDescription = campaign.Description,
                GoalAmount = campaign.GoalAmount,
                TotalDonatedAmount = campaign.TotalAmountDonated,
                Donations = campaign.Donations.Select(d => new DonationViewModel
                {
                    Id = d.Id,
                    DonatorId = d.DonatorId,
                    DonatorName = d.Donator.Email,
                    Amount = d.Amount
                }).ToList()
            };

            return View(campaignModel);
        }
    }
}
