namespace DonationTrackingSystem.Data.Models.Campaigns
{
    public class AllViewModel
    {
        public int TotalCampaigns { get; set; }
        public int TotalDonations { get; set; }
        public IEnumerable<CampaignAllViewModel> Campaigns { get; set; } =
            new List<CampaignAllViewModel>();
    }
}
