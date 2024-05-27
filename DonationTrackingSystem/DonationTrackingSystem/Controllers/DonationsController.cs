using DonationTrackingSystem.Common;
using DonationTrackingSystem.Data;
using DonationTrackingSystem.Data.Models.Donations;
using DonationTrackingSystem.Web.ViewModel.Campaigns;
using DonationTrackingSystem.Web.ViewModel.Donation;
using DonationTrackingSystem.Web.ViewModel.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Donation = DonationTrackingSystem.Data.Entities.Donation;

namespace DonationTrackingSystem.Controllers
{
    public class DonationsController : Controller
    {
        private readonly DonationTrackingSystemDbContext _dbContext;

        public DonationsController(DonationTrackingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            try
            {
                var donations = _dbContext.Donations.Include(d => d.Campaign).ToList();
                return View(donations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving donations.");
            }
        }

        public IActionResult Create()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }

                var campaigns = _dbContext.Campaigns
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList();

                var viewModel = new DonationViewModel
                {
                    Campaigns = campaigns
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while loading campaigns.");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("Amount,CampaignId")] DonationViewModel viewModel)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                viewModel.DonatorId = userId;
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Identity/Account/Login");
                }

                Donation donation = new Donation
                    {
                        DonatorId = viewModel.DonatorId,
                        Amount = viewModel.Amount,
                        CampaignId = viewModel.CampaignId
                    };
                
                _dbContext.Add(donation);
                    _dbContext.SaveChanges();

               if (!ModelState.IsValid)
                {
                    foreach (var modelStateEntry in ModelState.Values)
                    {
                        foreach (var error in modelStateEntry.Errors)
                        {
                            Console.WriteLine(error.Exception);
                        }
                    }
                }
                return View(viewModel);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the donation.");
            }
        }
    }
}