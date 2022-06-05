using System.Data;
using System.Data.SQLite;
using System.Drawing;
using SpaceExploration.Domain.Data;
using SpaceExploration.Domain.Models;
using SpaceExploration.Domain.Queries;

namespace SpaceExploration.Data;

public class DataProvider : IDataProvider
{
    private readonly IStorageProvider _storageProvider;

    public DataProvider(IStorageProvider storageProvider)
    {
        _storageProvider = storageProvider;
    }

    public Robot? GetRobotById(long requestId)
    {
        using var connection = _storageProvider.GetConnection();
        return GetRobot(connection, requestId);
    }

    public Robot AddRobot(
        int locationX,
        int locationY,
        int assignedAreaHeight,
        int assignedAreaWidth, 
        int direction)
    {
        using var connection = _storageProvider.GetConnection();
        var assignedAreaId = StorageProvider.InsertAssignedArea(connection, new AssignedAreaDto(){Width = assignedAreaWidth, Height= assignedAreaHeight});
        var id = StorageProvider.InsertRobot(connection, new RobotDto(){AssignedAreaId = assignedAreaId, LocationX = locationX, LocationY = locationY, Direction = direction});
        
        return GetRobot(connection, id);
    }

    public Robot? UpdateRobot(Robot robot)
    {
        using var connection = _storageProvider.GetConnection();
        var robotResult = GetRobotByIdFromDb(connection, robot.Id);
        if (robotResult is null)
        {
            return null;
        }
        using var sqLiteCommand = new SQLiteCommand(connection);
        sqLiteCommand.Parameters.AddWithValue("@LocationY", robot.LocationInArea.Y);
        sqLiteCommand.Parameters.AddWithValue("@LocationX", robot.LocationInArea.X);
        sqLiteCommand.Parameters.AddWithValue("@Direction", (int)robot.Direction);
        sqLiteCommand.Parameters.AddWithValue("@RobotID", robot.Id);
        sqLiteCommand.CommandText = @"UPDATE Robot SET LocationY = @LocationY, LocationX = @LocationX, [Direction] = @Direction WHERE id = @RobotID";
        sqLiteCommand.ExecuteNonQuery();
        return GetRobot(connection, robot.Id);
    }

    public IEnumerable<Robot> GetRobotList()
    {
        using var connection = _storageProvider.GetConnection();
        var robotResults = GetRobotListFromDb(connection);
        var assignedAreaResults = GetAssignedAreaListFromDb(connection);
        return robotResults.Select(robot => MapToRobot(robot, assignedAreaResults.Single(assignedArea => assignedArea.Id == robot.AssignedAreaId)));
    }

    private Robot? GetRobot(SQLiteConnection connection, long id)
    {
        var robotResult = GetRobotByIdFromDb(connection, id);
        if (robotResult is null)
        {
            return null;
        }
        var assignedAreaResult = GetAssignedAreaByIdFromDb(connection,robotResult.AssignedAreaId);
        return MapToRobot(robotResult, assignedAreaResult);
    }

    private Robot MapToRobot(RobotDto robotDto, AssignedAreaDto assignedAreaDto)
    {
        return new Robot()
        {
            Id = robotDto.Id,
            LocationInArea = new Point(robotDto.LocationX,robotDto.LocationY),
            Direction = (Direction)robotDto.Direction,
            AssignedArea = new AssignedArea()
            {
               Id = assignedAreaDto.Id,
               Size = new Size(assignedAreaDto.Width, assignedAreaDto.Height)
            }
        };
    }

    private IEnumerable<RobotDto> GetRobotListFromDb(SQLiteConnection connection)
    {
        using var sqLiteCommand = new SQLiteCommand(connection);
        sqLiteCommand.CommandText = @"Select id, AssignedAreaId, LocationY, LocationX, Direction FROM Robot";
        using SQLiteDataReader sqLiteDataReader = sqLiteCommand.ExecuteReader();
        var robotDtos = new List<RobotDto>();
        while (sqLiteDataReader.Read())
        {
            robotDtos.Add(new RobotDto()
            {
                Id = sqLiteDataReader.GetInt32(0),
                AssignedAreaId = sqLiteDataReader.GetInt32(1),
                LocationY = sqLiteDataReader.GetInt32(2),
                LocationX = sqLiteDataReader.GetInt32(3),
                Direction = sqLiteDataReader.GetInt32(4)
            });
        }
        return robotDtos;
    }

    private IEnumerable<AssignedAreaDto> GetAssignedAreaListFromDb(SQLiteConnection connection)
    {
        using var sqLiteCommand = new SQLiteCommand(connection);
        sqLiteCommand.CommandText = @"Select id, Width, Height FROM AssignedArea";
        using SQLiteDataReader sqLiteDataReader = sqLiteCommand.ExecuteReader();
        var assignedAreaDtos = new List<AssignedAreaDto>();
        while (sqLiteDataReader.Read())
        {
            assignedAreaDtos.Add(new AssignedAreaDto()
                {
                    Id = sqLiteDataReader.GetInt32(0),
                    Width = sqLiteDataReader.GetInt32(1),
                    Height = sqLiteDataReader.GetInt32(2),
                }
            );
        }
        return assignedAreaDtos;
    }
    
    private RobotDto? GetRobotByIdFromDb(SQLiteConnection connection, long Id)
    {
        using var sqLiteCommand = new SQLiteCommand(connection);
        sqLiteCommand.Parameters.AddWithValue("@RobotID", Id);
        sqLiteCommand.CommandText = @"Select id, AssignedAreaId, LocationY, LocationX, Direction FROM Robot WHERE id = @RobotID";
        using SQLiteDataReader sqLiteDataReader = sqLiteCommand.ExecuteReader();
        sqLiteDataReader.Read();
        try
        {
            return new RobotDto()
            {
                Id = sqLiteDataReader.GetInt32(0),
                AssignedAreaId = sqLiteDataReader.GetInt32(1),
                LocationY = sqLiteDataReader.GetInt32(2),
                LocationX = sqLiteDataReader.GetInt32(3),
                Direction = sqLiteDataReader.GetInt32(4)
            };
        }
        catch (Exception e)
        {
            return null;
        }
    }

    private AssignedAreaDto GetAssignedAreaByIdFromDb(SQLiteConnection connection, long Id)
    {
        using var sqLiteCommand = new SQLiteCommand(connection);
        sqLiteCommand.Parameters.AddWithValue("@AssignedAreaID", Id);
        sqLiteCommand.CommandText = @"Select id, Width, Height FROM AssignedArea WHERE id = @AssignedAreaID";
        using SQLiteDataReader sqLiteDataReader = sqLiteCommand.ExecuteReader();
        sqLiteDataReader.Read();
        return new AssignedAreaDto()
        {
            Id = sqLiteDataReader.GetInt32(0),
            Width = sqLiteDataReader.GetInt32(1),
            Height = sqLiteDataReader.GetInt32(2),
        };
    }
}

public class AssignedAreaDto
{
    public long Id { get; init; }
    public int Height { get; init; }
    public int Width { get; init; }
}

public class RobotDto
{
    public long Id { get; init; }
    public long AssignedAreaId { get; init; }
    public int LocationY { get; init; }
    public int LocationX { get; init; }
    public int Direction { get; init; }
}