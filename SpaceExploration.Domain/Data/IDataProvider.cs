using SpaceExploration.Domain.Models;

namespace SpaceExploration.Domain.Data;

public interface IDataProvider
{
    IEnumerable<Robot> GetRobotList();
    Robot? GetRobotById(long requestId);
    Robot? AddRobot(int locationX, int locationY, int assignedAreaHeight, int assignedAreaWidth, int direction);
    Robot? UpdateRobot(Robot robot);
}