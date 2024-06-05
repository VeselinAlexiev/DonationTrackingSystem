using DonationTrackingSystem.Web.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace DonationTrackingSystem.Data.Models.Campaigns
{
    public class AllCampaignsQueryModel
    {
        public const int CampaignPerPage = 3;

        [Display(Name = "Search by text")]
        public string SearchTerm { get; set; } = null!;
        public CampaignSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalCampaigns { get; set; }

        public int TotalDonations { get; set; }

        public IEnumerable<AllCampaignsModel> Campaigns { get; set; } =
            new List<AllCampaignsModel>();
    }
}
