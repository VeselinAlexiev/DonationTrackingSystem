using DonationTrackingSystem.Web.ViewModel;

namespace DonationTrackingSystem.Data.Models.Campaigns
{
    public class AllCampaignsModel
    {
        public int Id { get; set; }
        public string CampaignName { get; set; } = null!;
        public double GoalPercentage { get; set; }
    }
}
