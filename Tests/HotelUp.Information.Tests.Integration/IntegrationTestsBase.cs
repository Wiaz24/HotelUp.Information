using System.Net.Http.Headers;
using System.Security.Claims;
using HotelUp.Information.Tests.Integration.Utils;
using Xunit.Abstractions;

namespace HotelUp.Information.Tests.Integration;

public abstract class IntegrationTestsBase : IClassFixture<TestWebAppFactory>
{
    protected TestWebAppFactory Factory { get; }
    protected readonly IServiceProvider ServiceProvider;
    protected readonly ITestOutputHelper TestOutputHelper;
    protected readonly string Prefix;

    protected IntegrationTestsBase(TestWebAppFactory factory, ITestOutputHelper testOutputHelper, string prefix)
    {
        Factory = factory;
        TestOutputHelper = testOutputHelper;
        Prefix = prefix;
        ServiceProvider = factory.Services;
    }
    
    protected HttpClient CreateHttpClientWithToken(Guid clientId, IEnumerable<Claim> claims)
    {
        var httpClient = Factory.CreateClient();
        var token = MockJwtTokens.GenerateJwtToken(new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, clientId.ToString()),
        }.Concat(claims));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return httpClient;
    }
}