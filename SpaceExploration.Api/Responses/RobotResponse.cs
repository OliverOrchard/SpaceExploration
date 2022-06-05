using System.Drawing;
using SpaceExploration.Domain.Models;

namespace SpaceExploration.Api.Responses;

public class RobotResponse
{
    public long Id { get; set; }
    public Point LocationInArea { get; set; }
    public Direction Direction { get; set; }
    public AssignedAreaResponse AssignedArea { get; set; }

    public RobotResponse(Robot robot)
    {
        Id = robot.Id;
        LocationInArea = robot.LocationInArea;
        Direction = robot.Direction;
        AssignedArea = new AssignedAreaResponse()
        {
            Id = robot.AssignedArea.Id,
            Size = robot.AssignedArea.Size
        };
    }
}