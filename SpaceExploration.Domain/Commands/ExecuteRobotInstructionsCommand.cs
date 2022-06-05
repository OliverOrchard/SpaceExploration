using System.Drawing;
using MediatR;
using SpaceExploration.Domain.Models;

namespace SpaceExploration.Domain.Commands;

public record ExecuteRobotInstructionsCommand(long RobotId,IEnumerable<RobotInstructionType> Instructions):IRequest<(Robot? robot, String ExecutionInformation)>;