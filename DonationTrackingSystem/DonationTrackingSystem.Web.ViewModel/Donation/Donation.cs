﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonationTrackingSystem.Web.ViewModel.Campaigns;

namespace DonationTrackingSystem.Web.ViewModel.Donation
{
    public class Donation
    {
        public string DonatorId { get; set; }
        public decimal Amount { get; set; }
        public int CampaignId { get; set; }
    }
}
