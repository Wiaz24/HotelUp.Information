using System.Net;
using System.Security.Claims;
using Shouldly;
using Xunit.Abstractions;

namespace HotelUp.Information.Tests.Integration.Controllers;

[Collection(nameof(HotelEventControllerTests))]
public class HotelEventControllerTests : IntegrationTestsBase
{
    public HotelEventControllerTests(TestWebAppFactory factory, ITestOutputHelper testOutputHelper)
        : base(factory, testOutputHelper, "api/information/hotel-event")
    {
    }

    [Fact]
    public async Task GenerateExampleData_WhenUserIsNotAdmin_ShouldReturnForbidden()
    {
        // Arrange
        var notAdminClaim = new Claim(ClaimTypes.Role, "Clients");
        var httpClient = CreateHttpClientWithToken(Guid.NewGuid(), [notAdminClaim]);

        // Act
        var response = await httpClient.PostAsync($"{Prefix}/example-data", null);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GenerateExampleData_WhenUserIsAdmin_ShouldReturnOk()
    {
        // Arrange
        var adminClaim = new Claim(ClaimTypes.Role, "Admins");
        var httpClient = CreateHttpClientWithToken(Guid.NewGuid(), [adminClaim]);

        // Act
        var response = await httpClient.PostAsync($"{Prefix}/example-data", null);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}