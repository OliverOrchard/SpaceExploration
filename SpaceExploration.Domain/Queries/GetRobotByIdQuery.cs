using MediatR;
using SpaceExploration.Domain.Models;

namespace SpaceExploration.Domain.Queries;

public record GetRobotByIdQuery(int Id):IRequest<Robot?>;