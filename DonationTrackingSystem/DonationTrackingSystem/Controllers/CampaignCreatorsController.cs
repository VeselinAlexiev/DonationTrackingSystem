using DonationTrackingSystem.Data;
using DonationTrackingSystem.Data.Entities;
using DonationTrackingSystem.Data.Models.CampaignCreators;
using DonationTrackingSystem.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DonationTrackingSystem.Controllers
{
    [Authorize]
    public class CampaignCreatorsController : Controller
    {
        private readonly DonationTrackingSystemDbContext data;

        public CampaignCreatorsController(DonationTrackingSystemDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult Become()
        {
            if (this.data.CampaignCreators.Any(cc => cc.UserId == this.User.Id()))
            {
                return BadRequest();
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Become(CampaignCreatorFormModel model)
        {
            if (this.data.CampaignCreators.Any(cc => cc.UserId == this.User.Id()))
            {
                return BadRequest();
            }

            if (this.data.CampaignCreators.Any(cc => cc.Username == model.Username))
            {
                ModelState.AddModelError(nameof(model.Username), "Username already exists. Please enter another one.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var campaignCreator = new CampaignCreator()
            {
                UserId = this.User.Id()!,
                Username = model.Username
            };

            this.data.CampaignCreators.Add(campaignCreator);
            this.data.SaveChanges();

            return RedirectToAction(nameof(CampaignsController.All), "Campaigns");
        }
    }
}
