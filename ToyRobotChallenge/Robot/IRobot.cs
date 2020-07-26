using ToyRobotChallenge.Environment;
using ToyRobotChallenge.Geometry;

namespace ToyRobotChallenge
{   
    /// <summary>
    /// A robot
    /// </summary>
    public interface IRobot
    {
        IBoard Board { get; set; }
        bool HasAPosition { get; }
        Vector2d<ulong> Vector { get; set; }

        void Move();
        void Place(Vector2d<ulong> vec);
        void TurnLeft();
        void TurnRight();
    }
}