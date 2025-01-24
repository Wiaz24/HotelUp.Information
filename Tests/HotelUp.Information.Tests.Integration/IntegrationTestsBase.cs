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
}