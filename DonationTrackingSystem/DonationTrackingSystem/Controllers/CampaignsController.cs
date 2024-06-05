using DonationTrackingSystem.Data;
using DonationTrackingSystem.Data.Entities;
using DonationTrackingSystem.Data.Models.Campaigns;
using DonationTrackingSystem.Data.Models.Donations;
using DonationTrackingSystem.Infrastructure;
using DonationTrackingSystem.Web.ViewModel;
using DonationTrackingSystem.Web.ViewModel.Campaigns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using System.IO;

namespace DonationTrackingSystem.Controllers
{
    public class CampaignsController : Controller
    {
        private readonly DonationTrackingSystemDbContext data;

        public CampaignsController(DonationTrackingSystemDbContext data)
            => this.data = data;

        public IActionResult All([FromQuery] AllCampaignsQueryModel query)
        {
            var campaignsQuery = this.data.Campaigns.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                campaignsQuery = campaignsQuery.Where(c =>
                    c.Name.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    c.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            campaignsQuery = query.Sorting switch
            {
                CampaignSorting.Name => campaignsQuery.OrderBy(c => c.Name),
                CampaignSorting.TotalAmountDonated => campaignsQuery.OrderBy(c => c.TotalAmountDonated).ThenByDescending(c => c.Id),
                _ => campaignsQuery.OrderByDescending(c => c.Id)
            };

            var campaigns = campaignsQuery
                .Skip((query.CurrentPage - 1) * AllCampaignsQueryModel.CampaignPerPage)
                .Take(AllCampaignsQueryModel.CampaignPerPage)
                .Select(c => new AllCampaignsModel
                {
                    Id = c.Id,
                    CampaignName = c.Name,
                    GoalPercentage = (double)c.TotalAmountDonated / (double)c.GoalAmount * 100,
                }).ToList();

            query.TotalCampaigns = campaignsQuery.Count();
            query.Campaigns = campaigns;

            return View(query);
        }

        public IActionResult Details(int id)
        {
            var campaign = this.data.Campaigns
                            .Include(c => c.CampaignCreator)
                            .ThenInclude(cc => cc.User)
                            .Include(c => c.Donations)
                            .ThenInclude(d => d.Donator)
                            .FirstOrDefault(c => c.Id == id);

            if (campaign is null)
            {
                return BadRequest();
            }

            var topDonations = campaign.Donations
                                .OrderByDescending(d => d.Amount)
                                .Take(2)
                                .Select(d => new DonationViewModel
                                {
                                    Id = d.Id,
                                    DonatorId = d.DonatorId,
                                    DonatorName = d.Donator.Email,
                                    Amount = d.Amount
                                }).ToList();

            CampaignDetailsViewModel campaignModel = new CampaignDetailsViewModel()
            {
                Id = campaign.Id,
                CampaignName = campaign.Name,
                CampaignDescription = campaign.Description,
                GoalAmount = campaign.GoalAmount,
                TotalDonatedAmount = campaign.TotalAmountDonated,
                CampaignCreatorName = campaign.CampaignCreator.User.Email,
                Donations = campaign.Donations.Select(d => new DonationViewModel
                {
                    Id = d.Id,
                    DonatorId = d.DonatorId,
                    DonatorName = d.Donator.Email,
                    Amount = d.Amount
                }).ToList(),
                TopDonations = topDonations
            };

            return View(campaignModel);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var allCampaigns = new AllCampaignsQueryModel()
            {
                TotalCampaigns = this.data.Campaigns
                .Where(c => c.CampaignCreatorId.ToString() == currentUserId.ToString()).Count(),
                Campaigns = (IEnumerable<AllCampaignsModel>)this.data.Campaigns
                    .Where(c => c.CampaignCreatorId.ToString() == currentUserId.ToString())
                    .Select(c => new AllCampaignsModel()
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

            var model = new AllCampaignsModel()
            {
                CampaignName = campaign.Name,
                GoalPercentage = (double)campaign.TotalAmountDonated / (double)campaign.GoalAmount * 100,
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(AllCampaignsModel model)
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

        public IActionResult DownloadDonations(int id)
        {
            var campaign = this.data.Campaigns
                            .Include(c => c.Donations)
                            .ThenInclude(d => d.Donator)
                            .FirstOrDefault(c => c.Id == id);

            if (campaign == null)
            {
                return NotFound();
            }

            var donations = campaign.Donations.Select(d => $"{d.Donator.Email} -> {d.Amount:C}");

            var sb = new StringBuilder();
            foreach (var donation in donations)
            {
                sb.AppendLine(donation);
            }

            var fileName = $"Donations_{campaign.Name}.txt";
            var fileBytes = Encoding.UTF8.GetBytes(sb.ToString());

            return File(fileBytes, "text/plain", fileName);
        }
    }
}
