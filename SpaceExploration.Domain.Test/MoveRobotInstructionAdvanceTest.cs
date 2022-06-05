using System.Drawing;
using NUnit.Framework;
using SpaceExploration.Domain.Models;
using FluentAssertions;

namespace SpaceExploration.Domain.Test;

public class MoveRobotInstructionAdvanceTest
{
    [Test]
    public void WhenAdvancingAndDirectionIsNorthThenLocationInAreaYShouldBeMinus1()
    {
        // Arrange
        var robot = new Robot()
        {
            Direction = Direction.North,
            LocationInArea = new Point(1,1)
        };
        var instruction = new RobotInstructionAdvance();
        
        // Act
        var result = instruction.ExecuteInstruction(robot);
        
        // Assert
        result.LocationInArea.X.Should().Be(1);
        result.LocationInArea.Y.Should().Be(0);
    }
    
    [Test]
    public void WhenAdvancingAndDirectionIsEastThenLocationInAreaXShouldBePlus1()
    {
        // Arrange
        var robot = new Robot()
        {
            Direction = Direction.East,
            LocationInArea = new Point(1,1)
        };
        var instruction = new RobotInstructionAdvance();
        
        // Act
        var result = instruction.ExecuteInstruction(robot);
        
        // Assert
        result.LocationInArea.X.Should().Be(2);
        result.LocationInArea.Y.Should().Be(1);
    }
    
    [Test]
    public void WhenAdvancingAndDirectionIsSouthThenLocationInAreaYShouldBePlus1()
    {
        // Arrange
        var robot = new Robot()
        {
            Direction = Direction.South,
            LocationInArea = new Point(1,1)
        };
        var instruction = new RobotInstructionAdvance();
        
        // Act
        var result = instruction.ExecuteInstruction(robot);
        
        // Assert
        result.LocationInArea.X.Should().Be(1);
        result.LocationInArea.Y.Should().Be(2);
    }
    
    [Test]
    public void WhenAdvancingAndDirectionIsWestThenLocationInAreaXShouldBeMinus1()
    {
        // Arrange
        var robot = new Robot()
        {
            Direction = Direction.West,
            LocationInArea = new Point(1,1)
        };
        var instruction = new RobotInstructionAdvance();
        
        // Act
        var result = instruction.ExecuteInstruction(robot);
        
        // Assert
        result.LocationInArea.X.Should().Be(0);
        result.LocationInArea.Y.Should().Be(1);
    }
}