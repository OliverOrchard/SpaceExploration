using System.Drawing;
using SpaceExploration.Domain.Models;

namespace SpaceExploration.Api.Responses;

public class RobotInstructedResponse
{
    public RobotInstructedResponse((Robot? robot, String ExecutionInformation) robotInstructed)
    {
        ExecutionInformation = robotInstructed.ExecutionInformation;
        if (robotInstructed.robot is not null)
        {
            DirectionOfRobot = robotInstructed.robot.Direction;
            RobotId = robotInstructed.robot.Id;
            FinalPositionOfRobot = robotInstructed.robot.LocationInArea;
        }
    }

    public long? RobotId { get; set; }
    public Direction? DirectionOfRobot { get; set; }
    public Point? FinalPositionOfRobot { get; set; }
    public string ExecutionInformation { get; set; }
}