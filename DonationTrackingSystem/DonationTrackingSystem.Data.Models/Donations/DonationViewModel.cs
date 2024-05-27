using DonationTrackingSystem.Data.Entities;
using DonationTrackingSystem.Data.Models.Campaigns;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationTrackingSystem.Data.Models.Donations
{
    public class DonationViewModel
    {
        public int Id { get; set; }
        public string DonatorId { get; set; } = null!;
        public string DonatorName { get; set; }
        public decimal Amount { get; set; }
        public int CampaignId { get; set; }
        public IEnumerable<SelectListItem> Campaigns { get; set; } = new List<SelectListItem>();
    }
}
