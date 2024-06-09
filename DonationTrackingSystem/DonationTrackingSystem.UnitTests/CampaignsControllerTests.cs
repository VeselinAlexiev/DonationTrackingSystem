using DonationTrackingSystem.Web.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace DonationTrackingSystem.UnitTests
{
    public class CampaignsControllerTests
    {

        private DbContextOptions<DonationTrackingSystemDbContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<DonationTrackingSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Test]
        public void Details_ReturnsViewResult_WithCampaignDetails()
        {
            var options = new DbContextOptionsBuilder<DonationTrackingSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var campaignCreatorId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            using (var context = new DonationTrackingSystemDbContext(options))
            {
                var user = new IdentityUser { Id = userId.ToString(), Email = "creator@test.com", UserName = "creator@test.com" }; // Set UserName property
                var campaignCreator = new CampaignCreator { Id = campaignCreatorId, UserId = userId.ToString(), User = user, Username = "creator@test.com" }; // Set Username property
                var campaign = new Campaign
                {
                    Id = 1,
                    Name = "Test Campaign",
                    Description = "Test Description",
                    GoalAmount = 1000,
                    TotalAmountDonated = 500,
                    CampaignCreator = campaignCreator,
                    CampaignCreatorId = Guid.Parse("de2758f2-1c58-490c-8445-835b9dc348b0"),
                    Donations = new List<Donation>
            {
                new Donation { Id = 1, DonatorId = userId.ToString(), Amount = 200, Donator = user },
                new Donation { Id = 2, DonatorId = userId.ToString(), Amount = 300, Donator = user }
            }
                };

                context.Users.Add(user);
                context.CampaignCreators.Add(campaignCreator);
                context.Campaigns.Add(campaign);
                context.SaveChanges();
            }

            // Act
            using (var context = new DonationTrackingSystemDbContext(options))
            {
                var controller = new CampaignsController(context);
                var result = controller.Details(1) as ViewResult;

                // Assert
                Assert.NotNull(result);
                var model = result.Model as CampaignDetailsViewModel;
                Assert.NotNull(model);
                Assert.That(model.CampaignName, Is.EqualTo("Test Campaign"));
                Assert.That(model.CampaignDescription, Is.EqualTo("Test Description"));
                Assert.That(model.GoalAmount, Is.EqualTo(1000));
                Assert.That(model.TotalDonatedAmount, Is.EqualTo(500));
                Assert.That(model.CampaignCreatorName, Is.EqualTo("creator@test.com"));
                Assert.That(model.Donations.Count, Is.EqualTo(2));
                Assert.That(model.TopDonations.Count, Is.EqualTo(2));
            }
        }

        [Test]
        public void All_ReturnsViewWithCorrectModel()
        {
            using (var context = new DonationTrackingSystemDbContext(_options))
            {
                var controller = new CampaignsController(context);
                var query = new AllCampaignsQueryModel
                {
                    SearchTerm = "test",
                    Sorting = CampaignSorting.Name,
                    CurrentPage = 1
                };

                var result = controller.All(query) as ViewResult;
                var model = result.Model as AllCampaignsQueryModel;

                Assert.NotNull(result);
                Assert.NotNull(model);
                Assert.AreEqual("test", model.SearchTerm);
                Assert.AreEqual(CampaignSorting.Name, model.Sorting);
            }
        }
    }
}