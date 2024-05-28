using System.ComponentModel.DataAnnotations;

namespace DonationTrackingSystem.Data.Models.Campaigns
{
    public class CampaignFormModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; } = null!;

        [StringLength(200, MinimumLength = 15)]
        public string Description { get; set; }

        [Required]
        [Range(0.00, 1000000,
            ErrorMessage = "Goal amount must be a positive number and less than {2}$.")]
        [Display(Name = "Goal amount")]
        public decimal GoalAmount { get; set; }
    }
}
