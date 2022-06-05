using MediatR;
using SpaceExploration.Domain.Commands;
using SpaceExploration.Domain.Data;
using SpaceExploration.Domain.Models;

namespace SpaceExploration.Domain.Handlers;

public class ExecuteRobotInstructionsOnAllRobotsHandler : IRequestHandler<ExecuteRobotInstructionsOnAllRobotsCommand, IEnumerable<(Robot robot, String ExecutionInformation)>>
{
    private readonly IDataProvider _dataProvider;
    private readonly IMediator _mediator;

    public ExecuteRobotInstructionsOnAllRobotsHandler(IDataProvider dataProvider, IMediator mediator)
    {
        _dataProvider = dataProvider;
        _mediator = mediator;
    }

    public async Task<IEnumerable<(Robot robot, String ExecutionInformation)>> Handle(ExecuteRobotInstructionsOnAllRobotsCommand request, CancellationToken cancellationToken)
    {
        var currentRobots = _dataProvider.GetRobotList();
        var newRobots = new List<(Robot robot, String ExecutionInformation)>();
        
        foreach (var currentRobot in currentRobots)
        {
            var newRobot = await _mediator.Send(new ExecuteRobotInstructionsCommand(currentRobot.Id,request.Instructions));
            newRobots.Add(newRobot);
        }
        
        return newRobots;
    }
}