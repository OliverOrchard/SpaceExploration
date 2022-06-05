using System.Drawing;

namespace SpaceExploration.Domain.Models;

public class Robot : ICloneable
{
    public long Id { get; set; }
    public Point LocationInArea { get; set; }
    public Direction Direction { get; set; }
    public AssignedArea? AssignedArea { get; set; }
    
    public object Clone()
    {
        return this.MemberwiseClone();
    }
}