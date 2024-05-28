using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DonationTrackingSystem.Data.Entities
{
    public class CampaignCreator
    {
        public CampaignCreator()
        {
            this.Id = Guid.NewGuid();
            ManagedCampaigns = new HashSet<Campaign>();
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;

        public ICollection<Campaign> ManagedCampaigns { get; set; }
    }

}
