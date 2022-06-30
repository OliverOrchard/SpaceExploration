using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using SpaceExploration.Api.Responses;
using SpaceExploration.Domain.Models;
using Xunit;

namespace SpaceExploration.IntegrationTest;

[Collection("Local resources collection")]
public class GetRobotsTest
{
    private readonly SpaceExplorationFixture _fixture;

    public GetRobotsTest(SpaceExplorationFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GivenRobotsInDb_WhenCallingGetRobots_GetRobotsFromDbAndMapToResponseObjects()
    {
        //Arrange
        var fakeDbRobots = _fixture.Fixture.CreateMany<Robot>();
        _fixture.MockDataProvider.Setup(x => x.GetRobotList()).Returns(fakeDbRobots);
        var client = _fixture.CreateClient();
        
        //Act
        var jsonResponse = await client.GetAsync($"Robot");
        var response = await _fixture.ConvertResponseToObject<IEnumerable<RobotResponse>>(jsonResponse);
        
        //Assert
        foreach (var fakeDbRobot in fakeDbRobots)
        {
            response.Should().Contain(actualRobot => 
                actualRobot.Direction == fakeDbRobot.Direction &&
                actualRobot.Id == fakeDbRobot.Id &&
                actualRobot.AssignedArea.Id == fakeDbRobot.AssignedArea.Id &&
                actualRobot.AssignedArea.Size.Height == fakeDbRobot.AssignedArea.Size.Height &&
                actualRobot.AssignedArea.Size.Width == fakeDbRobot.AssignedArea.Size.Width &&
                actualRobot.LocationInArea.X == fakeDbRobot.LocationInArea.X &&
                actualRobot.LocationInArea.Y == fakeDbRobot.LocationInArea.Y
                
            );
        }
    }

    [Fact]
    public async Task GivenNoRobotsInDb_WhenCallingGetRobots_GetRobotsReturnsEmptyList()
    {
        //Arrange
        var client = _fixture.CreateClient();
        _fixture.MockDataProvider.Setup(x => x.GetRobotList()).Returns(Array.Empty<Robot>());
        
        //Act
        var jsonResponse = await client.GetAsync($"Robot");
        var response = await _fixture.ConvertResponseToObject<IEnumerable<RobotResponse>>(jsonResponse);
        
        //Assert
        response.Should().BeEmpty();
    }
}