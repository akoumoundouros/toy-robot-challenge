using System;
using System.Diagnostics;
using ToyRobotChallenge.Environment;
using ToyRobotChallenge.Geometry;

namespace ToyRobotChallenge
{
    /// <summary>
    /// Our toy robot implementation
    /// </summary>
    public class ToyRobot 
        : Robot
    {
        /// <summary>
        /// Constract a toy robot
        /// </summary>
        /// <param name="theBoard"></param>
        public ToyRobot(IBoard theBoard)
            : base(theBoard)
        {            
        }

        /// <summary>
        /// Move the robot 1 step forward
        /// </summary>
        /// <returns>a result object</returns>
        public override void Move()
        {
            Debug.Assert(Board != null);

            if (Board == null)
                throw new NullReferenceException("Board cannot be null!");

            if (!HasAPosition)
                return;

            // calculate the desired board position
            var newX = Vector.Dir == Direction.East
                                // robot facing east, increment x
                                ? Vector.Position.X + 1
                                : Vector.Dir == Direction.West
                                    // robot facing west, decrement x
                                    ? Vector.Position.X - 1 
                                    // robot facing neither east or west, so don't change x.
                                    : Vector.Position.X;

            var newY = Vector.Dir == Direction.North 
                                // robot facing north, increment y
                                ? Vector.Position.Y + 1
                                : Vector.Dir == Direction.South
                                    // robot facing south, decrement y
                                    ? Vector.Position.Y - 1
                                    // robot facing neither north or south, so don't change Y.
                                    : Vector.Position.Y;

            var newPosition = new Point2d<ulong>(newX, newY);

            if (!Board.IsValidPosition(newPosition))
                return;

            // update position
            Vector.Position = newPosition;
        }

        /// <summary>
        /// Turn the robot 90 degrees left
        /// </summary>
        /// <returns>a result object</returns>
        public override void TurnLeft()
        {
            Debug.Assert(Board != null);

            if (Board == null)
                throw new NullReferenceException("Board cannot be null!");

            if (!HasAPosition)
                return;

            // turning won't cause any issues...
            switch(Vector.Dir)
            {
                case Direction.North:   Vector.Dir = Direction.West;    break;
                case Direction.East:    Vector.Dir = Direction.North;   break;
                case Direction.South:   Vector.Dir = Direction.East;    break;
                case Direction.West:    Vector.Dir = Direction.South;   break;
            }
        }

        /// <summary>
        /// Turn the robot 90 degrees right
        /// </summary>
        /// <returns>a result object</returns>
        public override void TurnRight()
        {
            Debug.Assert(Board != null);

            if (Board == null)
                throw new NullReferenceException("Board cannot be null!");

            if (!HasAPosition)
                return;

            // turning won't cause any issues...
            switch (Vector.Dir)
            {
                case Direction.North:   Vector.Dir = Direction.East;    break;
                case Direction.East:    Vector.Dir = Direction.South;   break;
                case Direction.South:   Vector.Dir = Direction.West;    break;
                case Direction.West:    Vector.Dir = Direction.North;   break;
            }
        }
    }
}
