using DonationTrackingSystem.Common;
using DonationTrackingSystem.Web.ViewModel.Campaigns;
using DonationTrackingSystem.Web.ViewModel.Donation;
using DonationTrackingSystem.Web.ViewModel.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Security.Claims;

namespace DonationTrackingSystem.Controllers
{
    public class DonationsController : Controller
    {
        /*public IActionResult My()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userDonations = RawData.GetDonations().Where(d => d.User == user).ToList();
            var campaigns = userDonations.Select(d => d.Campaign).Distinct().ToList();
            return View(campaigns);
        }

        [HttpPost]
        public IActionResult Donate(int campaignId, decimal amount)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var donation = new Donation { User = user, CampaignId = campaignId, Amount = amount };
            return RedirectToAction(nameof(My));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/

        public IActionResult Index()
        {
            var donations = RawData.GetDonations();
            return View(donations);
        }

        public IActionResult Create()
        {
            var campaigns = RawData.GetCampaigns();

            if (campaigns != null)
            {
                ViewBag.Campaigns = new SelectList(campaigns, "Id", "Name");
            }
            else
            {
                ViewBag.Campaigns = new SelectList(new List<Campaign>(), "Id", "Name");
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("User,Amount,CampaignId")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                var donations = RawData.GetDonations().ToList();
                donation.Id = donations.Count + 1;
                donations.Add(donation);
                return RedirectToAction(nameof(Index));
            }
            return View(donation);
        }
    }
}