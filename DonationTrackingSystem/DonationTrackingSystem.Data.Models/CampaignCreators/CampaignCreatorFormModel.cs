using System.ComponentModel.DataAnnotations;

namespace DonationTrackingSystem.Data.Models.CampaignCreators
{
    public class CampaignCreatorFormModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [Display(Name = "Username")]
        public string Username { get; init; } = null!;
    }
}
