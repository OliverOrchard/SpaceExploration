using MediatR;
using SpaceExploration.Domain.Models;

namespace SpaceExploration.Domain.Commands;

public record ExecuteRobotInstructionsOnAllRobotsCommand(IEnumerable<RobotInstructionType> Instructions):IRequest<IEnumerable<(Robot? robot, String ExecutionInformation)>>;