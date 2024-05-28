﻿namespace DonationTrackingSystem.Web.ViewModel.Campaigns
{
    public class Campaign
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal GoalAmount { get; set; }
        public decimal TotalAmountDonated { get; set; }
    }
}
