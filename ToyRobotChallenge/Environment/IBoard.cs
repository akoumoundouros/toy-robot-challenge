using ToyRobotChallenge.Geometry;

namespace ToyRobotChallenge.Environment
{
    /// <summary>
    /// A board for the robot to roam on
    /// </summary>
    public interface IBoard
    {
        ulong Height { get; }
        ulong Width { get; }

        bool IsValidPosition(Point2d<ulong> position);
    }
}