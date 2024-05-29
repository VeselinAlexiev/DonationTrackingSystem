using DonationTrackingSystem.Data;
using DonationTrackingSystem.Data.Entities;
using DonationTrackingSystem.Data.Models.Campaigns;
using DonationTrackingSystem.Data.Models.Donations;
using DonationTrackingSystem.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        [Authorize]
        public IActionResult Mine()
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var allCampaigns = new AllViewModel()
            {
                TotalCampaigns = this.data.Campaigns
                .Where(c => c.CampaignCreatorId.ToString() == currentUserId.ToString()).Count(),
                Campaigns = (IEnumerable<CampaignAllViewModel>)this.data.Campaigns
                    .Where(c => c.CampaignCreatorId.ToString() == currentUserId.ToString())
                    .Select(c => new CampaignAllViewModel()
                    {
                        Id = c.Id,
                        CampaignName = c.Name,
                        GoalPercentage = (double)c.TotalAmountDonated / (double)c.GoalAmount * 100,
                    })
            };

            return View(allCampaigns);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.data.CampaignCreators.Any(cc => cc.UserId == this.User.Id()))
            {
                return RedirectToAction(nameof(CampaignCreatorsController.Become), "CampaignCreators");
            }
            return View(new CampaignFormModel());
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(CampaignFormModel model)
        {
            if (!this.data.CampaignCreators.Any(cc => cc.UserId == this.User.Id()))
            {
                return RedirectToAction(nameof(CampaignCreatorsController.Become), "CampaignCreators");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var campaignCreatorsId = this.data.CampaignCreators
                .First(cc => cc.UserId == this.User.Id())
                .Id;

            var campaign = new Campaign
            {
                Name = model.Name,
                Description = model.Description,
                GoalAmount = model.GoalAmount,
                CampaignCreatorId = campaignCreatorsId
            };

            this.data.Campaigns.Add(campaign);
            this.data.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = campaign.Id });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var campaign = this.data.Campaigns.Find(id);

            if (campaign is null)
            {
                return BadRequest();
            }

            var campaignCreator = this.data.CampaignCreators.FirstOrDefault(cc => cc.Id == campaign.CampaignCreatorId);

            if (campaignCreator?.UserId != this.User.Id())
            {
                return Unauthorized();
            }

            var campaignModel = new CampaignFormModel()
            {
                Name = campaign.Name,
                Description = campaign.Description,
                GoalAmount = campaign.GoalAmount,
            };

            return View(campaignModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, CampaignFormModel model)
        {
            var campaign = this.data.Campaigns.Find(id);
            if (campaign is null)
            {
                return this.View();
            }

            var campaignCreator = this.data.CampaignCreators.FirstOrDefault(cc => cc.Id == campaign.CampaignCreatorId);

            if (campaignCreator?.UserId != this.User.Id())
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            campaign.Name = model.Name;
            campaign.Description = model.Description;
            campaign.GoalAmount = model.GoalAmount;

            this.data.SaveChanges();

            return RedirectToAction(nameof(Details), new {id = campaign.Id});
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var campaign = this.data.Campaigns.Find(id);

            if (campaign is null)
            {
                return BadRequest();
            }

            var campaignCreator = this.data.CampaignCreators.FirstOrDefault(cc => cc.Id == campaign.CampaignCreatorId);

            if (campaignCreator?.UserId != this.User.Id())
            {
                return Unauthorized();
            }

            var model = new CampaignAllViewModel()
            {
                CampaignName = campaign.Name,
                GoalPercentage = (double)campaign.TotalAmountDonated / (double)campaign.GoalAmount * 100,
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(CampaignAllViewModel model)
        {
            var campaign = this.data.Campaigns.Find(model.Id);

            if (campaign is null) 
            {
                return BadRequest();
            }

            var campaignCreator = this.data.CampaignCreators.FirstOrDefault(cc => cc.Id == campaign.CampaignCreatorId);

            if (campaignCreator?.UserId != this.User.Id())
            {
                return Unauthorized();
            }

            this.data.Campaigns.Remove(campaign);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }
    }
}
