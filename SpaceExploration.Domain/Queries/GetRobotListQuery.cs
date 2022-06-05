using MediatR;
using SpaceExploration.Domain.Models;

namespace SpaceExploration.Domain.Queries;

public record GetRobotListQuery():IRequest<IEnumerable<Robot>>;