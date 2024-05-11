using DonationTrackingSystem.Common;
using Microsoft.AspNetCore.Mvc;

namespace DonationTrackingSystem.Controllers
{
    public class CampaignsController : Controller
    {
        public IActionResult All()
        {
            var campaigns = RawData.GetCampaigns();
            return View(campaigns);
        }

        public IActionResult Details(int id)
        {
            var campaign = RawData.GetCampaigns().FirstOrDefault(c => c.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }

            var totalDonatedAmount = RawData.GetDonations().Where(d => d.CampaignId == id).Sum(d => d.Amount);

            ViewBag.TotalDonatedAmount = totalDonatedAmount;
            return View(campaign);
        }
    }
}
