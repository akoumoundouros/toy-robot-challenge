namespace ToyRobotChallenge.Geometry
{
    public class Point2d<T>
    {
        public T X { get; set; }
        public T Y { get; set; }

        public Point2d(T X, T Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
