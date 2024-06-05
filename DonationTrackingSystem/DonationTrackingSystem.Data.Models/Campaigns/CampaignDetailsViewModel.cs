using DonationTrackingSystem.Data.Models.Donations;

namespace DonationTrackingSystem.Data.Models.Campaigns
{
    public class CampaignDetailsViewModel
    {
        public int Id { get; set; }
        public string CampaignName { get; set; } = null!;
        public string? CampaignDescription { get; set; }
        public decimal GoalAmount { get; set; }
        public decimal TotalDonatedAmount { get; set; }

        public string CampaignCreatorName { get; set; } = null!;

        public List<DonationViewModel> Donations { get; set; } = new List<DonationViewModel>();
        public List<DonationViewModel> TopDonations { get; set; } = new List<DonationViewModel>();
    }
}
