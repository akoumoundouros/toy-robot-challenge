namespace ToyRobotChallenge.Geometry
{
    /// <summary>
    /// Simple 2D vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Vector2d<T>
    {
        public Point2d<T> Position { get; set; }
        public Direction Dir { get; set; }

        public Vector2d(Point2d<T> position, Direction dir)
        {
            Position = position;
            Dir = dir;
        }
    }
}
