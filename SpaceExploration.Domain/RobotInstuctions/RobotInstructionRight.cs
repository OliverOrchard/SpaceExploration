namespace SpaceExploration.Domain.Models;

public class RobotInstructionRight : RobotInstruction
{ 
    public override RobotInstructionType InstructionType => RobotInstructionType.Right;
    public override Robot ExecuteInstruction(Robot robot)
    {
        robot.Direction = robot.Direction switch
        {
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.South => Direction.West,
            Direction.West => Direction.North,
            _ => robot.Direction
        };
        
        return robot;
    }
}