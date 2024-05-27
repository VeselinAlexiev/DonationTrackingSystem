using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationTrackingSystem.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Donation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Donator")]
        public string DonatorId { get; set; } = null!;
        public IdentityUser Donator { get; set; } = null!;

        [Required(ErrorMessage = "Amount field is required.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "CampaignId field is required.")]
        [ForeignKey("Campaign")]
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }
    }
}
