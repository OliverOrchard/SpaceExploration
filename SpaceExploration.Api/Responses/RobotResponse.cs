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
        if (robot is not null)
        {
            Id = robot.Id;
            LocationInArea = robot.LocationInArea;
            Direction = robot.Direction;
            if (robot.AssignedArea is not null)
            {
                AssignedArea = new AssignedAreaResponse()
                {
                    Id = robot.AssignedArea.Id,
                    Size = robot.AssignedArea.Size
                };
            }
        }
    }
}