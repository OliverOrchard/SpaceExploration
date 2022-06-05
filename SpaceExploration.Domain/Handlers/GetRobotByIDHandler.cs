using MediatR;
using SpaceExploration.Domain.Data;
using SpaceExploration.Domain.Models;
using SpaceExploration.Domain.Queries;

namespace SpaceExploration.Domain.Handlers;

public class GetRobotByIDHandler : IRequestHandler<GetRobotByIdQuery, Robot>
{
    private readonly IDataProvider _dataProvider;

    public GetRobotByIDHandler(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public Task<Robot> Handle(GetRobotByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataProvider.GetRobotById(request.Id));
    }
}