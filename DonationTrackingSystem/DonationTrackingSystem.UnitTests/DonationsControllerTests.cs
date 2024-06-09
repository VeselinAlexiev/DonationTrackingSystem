using DonationTrackingSystem.Data.Models.Donations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DonationTrackingSystem.UnitTests
{
    public class DonationsControllerTests
    {
        [Test]
        public void CreatePost_ReturnsRedirectToLogin_WhenUserNotAuthenticated()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DonationTrackingSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new DonationTrackingSystemDbContext(options))
            {
                // Create a mock controller context with an unauthenticated user
                var mockHttpContext = new DefaultHttpContext();
                var controllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext
                };

                var controller = new DonationsController(context)
                {
                    ControllerContext = controllerContext
                };

                var viewModel = new DonationViewModel { Amount = 100, CampaignId = 1 };

                // Act
                var result = controller.Create(viewModel) as RedirectResult;

                // Assert
                Assert.NotNull(result); // Ensure that the result is not null
                Assert.That(result?.Url, Is.EqualTo("/Identity/Account/Login")); // Check if the redirect URL matches the expected login page URL
            }
        }
    }
}
