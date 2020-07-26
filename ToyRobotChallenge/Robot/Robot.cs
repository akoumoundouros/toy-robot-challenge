using System;
using System.Diagnostics;
using ToyRobotChallenge.Environment;
using ToyRobotChallenge.Geometry;

namespace ToyRobotChallenge
{
    /// <summary>
    /// A robot class
    /// </summary>
    public abstract class Robot 
        : IRobot
    {
        /// <summary>
        /// get/set the board that the robot is to move around in
        /// </summary>
        public IBoard Board { get; set; }

        /// <summary>
        /// What is the position & direction of the robot?
        /// </summary>
        public Vector2d<ulong> Vector { get; set; } = null;

        /// <summary>
        /// We don't care about if the position is valid on the board, only if the robot has a position in the first place!
        /// </summary>
        public bool HasAPosition => Vector is null == false;


        /// <summary>
        /// Robot ctor
        /// </summary>
        /// <param name="theBoard"></param>
        public Robot(IBoard theBoard)
        {
            Debug.Assert(theBoard != null);

            Board = theBoard;
        }

        /// <summary>
        /// Place the robot on the board
        /// </summary>
        /// <param name="x">The x position to place the robot</param>
        /// <param name="y">The y position to place the robot</param>
        /// <param name="dir">The direction the robot is to face when placed</param>
        /// <returns></returns>
        public void Place(Vector2d<ulong> vec)
        {
            if (vec is null)
                throw new ArgumentNullException(nameof(vec));

            if (Board == null)
                throw new NullReferenceException("board cannot be null!");

            if (!Board.IsValidPosition(vec.Position))
                return;

            // get to here then the x, y & direction are all good!
            Vector = vec;
        }

        /// abstract methods to override        
        public abstract void Move();
        public abstract void TurnLeft();
        public abstract void TurnRight();
    }
}
