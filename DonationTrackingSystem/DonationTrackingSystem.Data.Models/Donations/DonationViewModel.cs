using Microsoft.AspNetCore.Mvc.Rendering;

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
