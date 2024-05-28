using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
