using SpaceExploration.Domain.Models;

namespace SpaceExploration.Api.Requests;

public class RobotRequest
{
    public int LocationX { get; set; }
    public int LocationY { get; set; }
    public int AssignedAreaHeight { get; set; }
    public int AssignedAreaWidth { get; set; }    
    public Direction Direction { get; set; }
}