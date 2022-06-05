using SpaceExploration.Domain.Commands;

namespace SpaceExploration.Domain.Models;

public abstract class RobotInstruction
{
    public abstract RobotInstructionType InstructionType { get; }

    public abstract Robot ExecuteInstruction(Robot robot);
}