using ToyRobotChallenge.Geometry;

namespace ToyRobotChallenge.Environment
{
    /// <summary>
    /// The board on which our robot can move around.  Please note that as per spec, the origin (0,0) can be (is) considered to be the SOUTH WEST most corner.
    /// </summary>
    public class Board 
        : IBoard
    {
        /// <summary>
        /// The board width, defaults to 5
        /// </summary>
        public ulong Width { get; } = 5;

        /// <summary>
        /// The board height, default to 5
        /// </summary>
        public ulong Height { get; } = 5;

        /// <summary>
        /// default ctor to create a 5x5 board
        /// </summary>
        public Board() // : this(5, 5)
        {
        }

        /// <summary>
        /// ctor to create a board with alterntive dimensions
        /// </summary>
        /// <param name="width">user defined width of the board</param>
        /// <param name="height">user defined height of the board</param>
        public Board(ulong width, ulong height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Is the specified position valid on the board
        /// </summary>
        /// <param name="Point2d<ulong>">position to validate</param>
        /// <returns></returns>
        public bool IsValidPosition(Point2d<ulong> position)
        {
            return position.X < Width && position.Y < Height;
        }
    }
}
