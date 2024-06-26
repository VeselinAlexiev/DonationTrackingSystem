﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonationTrackingSystem.Data.Entities
{
    public class Campaign
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "CampaignCreatorId field is required.")]
        [ForeignKey("CampaignCreatorId")]
        public Guid CampaignCreatorId { get; set; }
        public CampaignCreator CampaignCreator { get; set; } = null!;

        [Required(ErrorMessage = "Name field is required.")]
        [MaxLength(50, ErrorMessage = "Name must be at most 50 characters.")]
        public string Name { get; set; } = null!;

        [MaxLength(500, ErrorMessage = "Description must be at most 500 characters.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "GoalAmount field is required.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal GoalAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmountDonated { get; set; }

        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}
