using FluentAssertions;
using NUnit.Framework;
using SpaceExploration.Domain.Models;

namespace SpaceExploration.Domain.Test;

public class MoveRobotInstructionRight 
{
    [Test]
    public void WhenTurningRightAndDirectionIsNorthThenNewDirectionShouldBeEast()
    {
        // Arrange
        var robot = new Robot()
        {
            Direction = Direction.North
        };
        var instruction = new RobotInstructionRight();
        
        // Act
        var result = instruction.ExecuteInstruction(robot);
        
        // Assert
        result.Direction.Should().Be(Direction.East);
    }
    [Test]
    public void WhenTurningRightAndDirectionIsEastThenNewDirectionShouldBeSouth()
    {
        // Arrange
        var robot = new Robot()
        {
            Direction = Direction.East
        };
        var instruction = new RobotInstructionRight();
        
        // Act
        var result = instruction.ExecuteInstruction(robot);
        
        // Assert
        result.Direction.Should().Be(Direction.South);
    }
    [Test]
    public void WhenTurningRightAndDirectionIsSouthThenNewDirectionShouldBeWest()
    {
        // Arrange
        var robot = new Robot()
        {
            Direction = Direction.South
        };
        var instruction = new RobotInstructionRight();
        
        // Act
        var result = instruction.ExecuteInstruction(robot);
        
        // Assert
        result.Direction.Should().Be(Direction.West);
    }
    [Test]
    public void WhenTurningRightAndDirectionIsWestThenNewDirectionShouldBeNorth()
    {
        // Arrange
        var robot = new Robot()
        {
            Direction = Direction.West
        };
        var instruction = new RobotInstructionRight();
        
        // Act
        var result = instruction.ExecuteInstruction(robot);
        
        // Assert
        result.Direction.Should().Be(Direction.North);
    }
}