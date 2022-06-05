using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpaceExploration.Api.Requests;
using SpaceExploration.Api.Responses;
using SpaceExploration.Domain.Commands;
using SpaceExploration.Domain.Models;
using SpaceExploration.Domain.Queries;

namespace SpaceExploration.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RobotController : ControllerBase
{
    private readonly IMediator _mediator;

    public RobotController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<IEnumerable<RobotResponse>> Get()
    {
        var robots = await _mediator.Send(new GetRobotListQuery());
        return robots.Select(robot => new RobotResponse(robot));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RobotResponse), 200)]
    public async Task<ActionResult> GetWithId([FromRoute] int id)
    {
        var robot = await _mediator.Send(new GetRobotByIdQuery(id));
        if (robot is null)
        {
            return NotFound();
        }
        return Ok(new RobotResponse(robot));
    }

    [HttpPost]
    public async Task<RobotResponse> Post([FromBody] RobotRequest request)
    {
        var robot = await _mediator.Send(new AddRobotCommand(
            request.LocationX,
            request.LocationY,
            request.AssignedAreaHeight,
            request.AssignedAreaWidth,
            (int)request.Direction
        ));
        return new RobotResponse(robot);
    }
    
    [HttpPost("{id}")]
    public async Task<RobotInstructedResponse> InstructRobot([FromRoute] int id, [FromBody] IEnumerable<RobotInstructionType> instructions)
    {
        var robotWithExecutionInformation = await _mediator.Send(new ExecuteRobotInstructionsCommand(id,instructions));
        return new RobotInstructedResponse(robotWithExecutionInformation);
    }
    
    [HttpPost("all")]
    public async Task<IEnumerable<RobotInstructedResponse>> InstructAllRobots([FromBody] IEnumerable<RobotInstructionType> instructions)
    {
        var robotsWithExecutionInformation = await _mediator.Send(new ExecuteRobotInstructionsOnAllRobotsCommand(instructions));
        return robotsWithExecutionInformation.Select(robotWithExecutionInformation => new RobotInstructedResponse(robotWithExecutionInformation));
    }
}
