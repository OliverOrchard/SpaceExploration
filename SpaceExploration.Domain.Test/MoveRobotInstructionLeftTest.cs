using FluentAssertions;
using NUnit.Framework;
using SpaceExploration.Domain.Models;

namespace SpaceExploration.Domain.Test;

public class MoveRobotInstructionLeftTest
{
    [Test]
    public void WhenTurningLeftAndDirectionIsNorthThenNewDirectionShouldBeWest()
    {
        // Arrange
        var robot = new Robot()
        {
            Direction = Direction.North
        };
        var instruction = new RobotInstructionLeft();
        
        // Act
        var result = instruction.ExecuteInstruction(robot);
        
        // Assert
        result.Direction.Should().Be(Direction.West);
    }
    [Test]
    public void WhenTurningLeftAndDirectionIsEastThenNewDirectionShouldBeNorth()
    {
        // Arrange
        var robot = new Robot()
        {
            Direction = Direction.East
        };
        var instruction = new RobotInstructionLeft();
        
        // Act
        var result = instruction.ExecuteInstruction(robot);
        
        // Assert
        result.Direction.Should().Be(Direction.North);
    }
    [Test]
    public void WhenTurningLeftAndDirectionIsSouthThenNewDirectionShouldBeEast()
    {
        // Arrange
        var robot = new Robot()
        {
            Direction = Direction.South
        };
        var instruction = new RobotInstructionLeft();
        
        // Act
        var result = instruction.ExecuteInstruction(robot);
        
        // Assert
        result.Direction.Should().Be(Direction.East);
    }
    [Test]
    public void WhenTurningLeftAndDirectionIsWestThenNewDirectionShouldBeSouth()
    {
        // Arrange
        var robot = new Robot()
        {
            Direction = Direction.West
        };
        var instruction = new RobotInstructionLeft();
        
        // Act
        var result = instruction.ExecuteInstruction(robot);
        
        // Assert
        result.Direction.Should().Be(Direction.South);
    }
}