using SpaceExploration.Domain.Commands;

namespace SpaceExploration.Domain.Models;

public class RobotInstructionAdvance : RobotInstruction
{ 
    public override RobotInstructionType InstructionType => RobotInstructionType.Advance;
    public override Robot ExecuteInstruction(Robot robot)
    {
        robot.LocationInArea = robot.Direction switch
        {
            Direction.North => robot.LocationInArea with { Y = robot.LocationInArea.Y - 1 },
            Direction.East => robot.LocationInArea with { X = robot.LocationInArea.X + 1 },
            Direction.South => robot.LocationInArea with { Y = robot.LocationInArea.Y + 1 },
            Direction.West => robot.LocationInArea with { X = robot.LocationInArea.X - 1 },
            _ => robot.LocationInArea
        };
        
        return robot;
    }
}