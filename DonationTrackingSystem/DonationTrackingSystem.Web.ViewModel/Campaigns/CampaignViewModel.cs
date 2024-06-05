using System.ComponentModel;

namespace DonationTrackingSystem.Web.ViewModel.Campaigns
{
    public class CampaignViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [DisplayName("Goal Amount")]
        public decimal GoalAmount { get; set; }

        [DisplayName("Total Amount Donated")]
        public decimal TotalAmountDonated { get; set; }
    }
}
