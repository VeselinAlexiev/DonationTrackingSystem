using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationTrackingSystem.Data.Entities
{
    public class CampaignCreator
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string UserId { get; set; } = null!;
        public IdentityUser User { get; init; } = null!;
        public IEnumerable<Campaign> Campaigns { get; set; } = new List<Campaign>();
    }
}
