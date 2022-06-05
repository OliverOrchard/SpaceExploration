namespace SpaceExploration.Domain.Models;

public class RobotInstructionLeft : RobotInstruction
{ 
    public override RobotInstructionType InstructionType => RobotInstructionType.Left;
    public override Robot ExecuteInstruction(Robot robot)
    {
        robot.Direction = robot.Direction switch
        {
            Direction.North => Direction.West,
            Direction.East => Direction.North,
            Direction.South => Direction.East,
            Direction.West => Direction.South,
            _ => robot.Direction
        };
        
        return robot;
    }
}