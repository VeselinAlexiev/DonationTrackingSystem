using DonationTrackingSystem.Data.Models.Donations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationTrackingSystem.Data.Models.Campaigns
{
    public class CampaignDetailsViewModel
    {
        public int Id { get; set; }
        public string CampaignName { get; set; } = null!;
        public string? CampaignDescription { get; set; }
        public decimal GoalAmount { get; set; }
        public decimal TotalDonatedAmount { get; set; }
        public List<DonationViewModel> Donations { get; set; } = new List<DonationViewModel>();
    }
}
