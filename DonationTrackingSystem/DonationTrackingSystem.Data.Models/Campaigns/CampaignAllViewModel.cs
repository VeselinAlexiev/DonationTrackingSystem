namespace DonationTrackingSystem.Data.Models.Campaigns
{
    public class CampaignAllViewModel
    {
        public int Id { get; set; }
        public string CampaignName { get; set; } = null!;
        public double GoalPercentage { get; set; }
    }
}
