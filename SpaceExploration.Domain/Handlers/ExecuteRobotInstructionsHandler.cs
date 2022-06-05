using MediatR;
using SpaceExploration.Domain.Commands;
using SpaceExploration.Domain.Data;
using SpaceExploration.Domain.Models;

namespace SpaceExploration.Domain.Handlers;

public class ExecuteRobotInstructionsHandler : IRequestHandler<ExecuteRobotInstructionsCommand, (Robot? robot, String ExecutionInformation)>
{
    private readonly IDataProvider _dataProvider;

    public ExecuteRobotInstructionsHandler(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public Task<(Robot? robot, string ExecutionInformation)> Handle(ExecuteRobotInstructionsCommand request, CancellationToken cancellationToken)
    {
        var robot = _dataProvider.GetRobotById(request.RobotId);
        if (robot is null)
        {
            (Robot robot, String ExecutionInformation) emptyReturn = (null, "Robot Not Found Unable To Execute Instructions");
            return Task.FromResult(emptyReturn);
        }
        var robotToPerformInstructionsOn = (Robot)robot.Clone();
        var instructions = request.Instructions.Select(MapToRobotInstruction);
        foreach (var robotInstruction in instructions)
        {
             robotInstruction.ExecuteInstruction(robotToPerformInstructionsOn);
        }

        var isRobotInsideAssignedArea = IsRobotInsideAssignedArea(robotToPerformInstructionsOn);
        if (isRobotInsideAssignedArea.Value)
        {
            robot = _dataProvider.UpdateRobot(robotToPerformInstructionsOn);
        }
        return Task.FromResult((robot, isRobotInsideAssignedArea.ValidationMessage));
    }

    private (bool Value, string ValidationMessage) IsRobotInsideAssignedArea(Robot robot)
    {
        if (robot.LocationInArea.X > robot.AssignedArea.Size.Width)
        {
            return (false, $"{nameof(Robot.LocationInArea.X)} Cannot be greater than {nameof(Robot.AssignedArea.Size.Width)}");
        }
        
        if (robot.LocationInArea.Y > robot.AssignedArea.Size.Height)
        {
            return (false, $"{nameof(Robot.LocationInArea.Y)} Cannot be greater than {nameof(Robot.AssignedArea.Size.Height)}");
        }
        
        if (robot.LocationInArea.X < 1)
        {
            return (false, $"{nameof(Robot.LocationInArea.X)} Cannot be less than 1");
        }
        
        if (robot.LocationInArea.Y < 1)
        {
            return (false, $"{nameof(Robot.LocationInArea.Y)} Cannot be less than 1");
        }

        return (true, "Commands Executed Successfully");
    }

    private RobotInstruction MapToRobotInstruction(RobotInstructionType robotInstructionType)
    {
        return robotInstructionType switch
        {
            RobotInstructionType.Advance => new RobotInstructionAdvance(),
            RobotInstructionType.Left => new RobotInstructionLeft(),
            RobotInstructionType.Right => new RobotInstructionRight(),
            _ => new RobotInstructionAdvance()
        };
    }
}