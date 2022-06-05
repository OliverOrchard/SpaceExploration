using System.ComponentModel.DataAnnotations;
using MediatR;
using SpaceExploration.Domain.Commands;
using SpaceExploration.Domain.Data;
using SpaceExploration.Domain.Models;

namespace SpaceExploration.Domain.Handlers;

public class AddRobotHandler : IRequestHandler<AddRobotCommand, Robot?>
{
    private readonly IDataProvider _dataProvider;

    public AddRobotHandler(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public Task<Robot?> Handle(AddRobotCommand command, CancellationToken cancellationToken)
    {
        ValidateCommand(command);
        return Task.FromResult(_dataProvider.AddRobot(
            command.LocationX, 
            command.LocationY, 
            command.AssignedAreaHeight, 
            command.AssignedAreaWidth,
            command.Direction)
        );
    }

    private void ValidateCommand(AddRobotCommand command)
    {
        if (command.LocationX > command.AssignedAreaWidth)
        {
            throw new ValidationException($"{nameof(command.LocationX)} cannot be greater than {nameof(command.AssignedAreaWidth)}");
        }
        if (command.LocationY > command.AssignedAreaHeight)
        {
            throw new ValidationException($"{nameof(command.LocationY)} cannot be greater than {nameof(command.AssignedAreaHeight)}");
        }
    }
}