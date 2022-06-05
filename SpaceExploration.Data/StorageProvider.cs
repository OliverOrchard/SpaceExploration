using System.Data.SQLite;
using SpaceExploration.Domain.Models;

namespace SpaceExploration.Data;

public class StorageProvider : IStorageProvider
{
    private const string SqlLiteFile = "space-exploration.sqlite";
    
    public SQLiteConnection GetConnection()
    {
        var firstRun = !File.Exists(SqlLiteFile);
        var connection = new SQLiteConnection($@"Data Source={SqlLiteFile};Pooling=true;FailIfMissing=false");
        connection.Open();
        if (firstRun)
        {
            CreateMissingTables(connection);
            AddData(connection);
        }
        return connection;
    }

    private void CreateMissingTables(SQLiteConnection connection)
    {
        using var sqLiteCommand = new SQLiteCommand(connection);
            
        sqLiteCommand.CommandText = @"CREATE TABLE Robot(Id INTEGER PRIMARY KEY AUTOINCREMENT, AssignedAreaId INTEGER, LocationY INTEGER, LocationX INTEGER, Direction INTEGER)";
        sqLiteCommand.ExecuteNonQuery();
            
        sqLiteCommand.CommandText = @"CREATE TABLE AssignedArea(Id INTEGER PRIMARY KEY AUTOINCREMENT, Height INTEGER, Width INTEGER)";
        sqLiteCommand.ExecuteNonQuery();
    }

    private void AddData(SQLiteConnection connection)
    {
        using var sqLiteCommand = new SQLiteCommand(connection);
        
        var assignedAreaId = InsertAssignedArea(connection, new AssignedAreaDto(){Width = 100, Height = 100});
        InsertRobot(connection, new RobotDto(){AssignedAreaId = assignedAreaId, LocationX = 5, LocationY = 7, Direction = 1});
    }
    
    public static long InsertAssignedArea(SQLiteConnection connection, AssignedAreaDto assignedAreaDto)
    {
        using var sqLiteCommand = new SQLiteCommand(connection);
        sqLiteCommand.CommandText = $@"
            INSERT INTO AssignedArea(Height,Width) 
            VALUES(
                {assignedAreaDto.Height},
                {assignedAreaDto.Width}
            ); 
            SELECT last_insert_rowid();";
        return (long)sqLiteCommand.ExecuteScalar();
    }
    
    public static long InsertRobot(SQLiteConnection connection, RobotDto robotDto)
    {
        using var sqLiteCommand = new SQLiteCommand(connection);
        sqLiteCommand.CommandText = $@"
            INSERT INTO Robot(AssignedAreaId,LocationY,LocationX,Direction) 
            VALUES(
                {robotDto.AssignedAreaId},
                {robotDto.LocationY},
                {robotDto.LocationX},
                {robotDto.Direction}
            ); 
            SELECT last_insert_rowid();";
        return (long)sqLiteCommand.ExecuteScalar();
    }
}