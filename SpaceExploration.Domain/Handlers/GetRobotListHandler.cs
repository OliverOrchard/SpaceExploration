using MediatR;
using SpaceExploration.Domain.Data;
using SpaceExploration.Domain.Models;
using SpaceExploration.Domain.Queries;

namespace SpaceExploration.Domain.Handlers;

public class GetRobotListHandler : IRequestHandler<GetRobotListQuery, IEnumerable<Robot>>
{
    private readonly IDataProvider _dataProvider;

    public GetRobotListHandler(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public Task<IEnumerable<Robot>> Handle(GetRobotListQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataProvider.GetRobotList());
    }
}