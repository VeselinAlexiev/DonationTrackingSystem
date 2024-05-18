using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationTrackingSystem.Data.Entities
{
    public class Campaign
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "UserId field is required.")]
        public Guid CampaignCreatorId { get; set; }
        public CampaignCreator CampaignCreator { get; set; }

        [Required(ErrorMessage = "Name field is required.")]
        [MaxLength(50, ErrorMessage = "Name must be at most 50 characters.")]
        public string Name { get; set; } = null!;

        [MaxLength(500, ErrorMessage = "Description must be at most 500 characters.")]
        public string Description { get; set; } = null;

        [Required(ErrorMessage = "GoalAmount field is required.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal GoalAmount { get; set; }

        [Required(ErrorMessage = "TotalAmountDonated field is required.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmountDonated => Donations.Sum(d => d.Amount);

        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}
