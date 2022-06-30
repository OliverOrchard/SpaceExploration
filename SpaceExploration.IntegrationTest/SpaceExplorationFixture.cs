using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Newtonsoft.Json;
using SpaceExploration.Api.Responses;
using SpaceExploration.Domain.Data;
using Xunit;

namespace SpaceExploration.IntegrationTest;

public class SpaceExplorationFixture : WebApplicationFactory<Program>
{
    public Mock<IDataProvider> MockDataProvider { get; } = new();
    
    public readonly IFixture Fixture;

    public SpaceExplorationFixture()
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        Fixture = fixture;
    }

    public async Task<T> ConvertResponseToObject<T>(HttpResponseMessage httpResponseMessage)
    {
        return JsonConvert.DeserializeObject<T>(await httpResponseMessage.Content.ReadAsStringAsync());
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton(MockDataProvider.Object);
        });
    }
}