using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using HotelUp.Information.Persistence.Entities;
using Shouldly;
using Xunit.Abstractions;

// ReSharper disable PossibleMultipleEnumeration

namespace HotelUp.Information.Tests.Integration.Controllers;

[Collection(nameof(PlannedDishControllerTests))]
public class PlannedDishControllerTests : IntegrationTestsBase
{
    public PlannedDishControllerTests(TestWebAppFactory factory, ITestOutputHelper testOutputHelper)
        : base(factory, testOutputHelper, "api/information/planned-dish")
    {
    }

    [Fact]
    public async Task GetPlannedDishesByDate_WhenCalled_ShouldReturnPlannedDishesOnlyForThisDay()
    {
        // Arrange
        var adminClaim = new Claim(ClaimTypes.Role, "Admins");
        var httpClient = CreateHttpClientWithToken(Guid.NewGuid(), [adminClaim]);
        var response1 = await httpClient.PostAsync($"{Prefix}/example-data", null);
        response1.StatusCode.ShouldBe(HttpStatusCode.OK);

        // Act
        var response2 = await httpClient.GetFromJsonAsync<IEnumerable<PlannedDish>>(Prefix);

        response2.ShouldNotBeNull();
        var plannedDishes = response2.ToList();
        plannedDishes.ShouldNotBeEmpty();

        var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);
        plannedDishes.All(x => x.ServingDate == today).ShouldBeTrue();
    }
}