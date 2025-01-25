using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using HotelUp.Information.Tests.Integration.Utils;
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
        var httpClient = CreateHttpClientWithToken(Guid.NewGuid(),[notAdminClaim]);
        
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
    
    [Fact]
    public async Task GetHotelEventsByDate_WithDeltaInstalled_ShouldProvideAndUseETags()
    {
        // Arrange1
        var adminClaim = new Claim(ClaimTypes.Role, "Admins");
        var httpClient = CreateHttpClientWithToken(Guid.NewGuid(), [adminClaim]);
        
        // Act1
        var response = await httpClient.GetAsync($"{Prefix}");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var eTag = response.Headers.ETag;
        
        // Assert1
        eTag.ShouldNotBeNull();
        
        // Arrange2
        var request = new HttpRequestMessage(HttpMethod.Get, $"{Prefix}");
        request.Headers.IfNoneMatch.Add(eTag);
        
        // Act2
        var response2 = await httpClient.SendAsync(request);
        
        // Assert2
        response2.StatusCode.ShouldBe(HttpStatusCode.NotModified);
    }
}