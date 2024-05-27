using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationTrackingSystem.Data.Models.Campaigns
{
    public class CampaignAllViewModel
    {
        public int Id { get; set; }
        public string CampaignName { get; set; } = null!;
        public double GoalPercentage { get; set; }
    }
}
