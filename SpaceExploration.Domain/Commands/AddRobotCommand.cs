using System.Drawing;
using MediatR;
using SpaceExploration.Domain.Models;

namespace SpaceExploration.Domain.Commands;

public record AddRobotCommand(int LocationX, int LocationY, int AssignedAreaHeight, int AssignedAreaWidth, int Direction):IRequest<Robot>;