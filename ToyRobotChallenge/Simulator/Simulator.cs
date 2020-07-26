using System.Collections.Generic;
using System.Diagnostics;

using ToyRobotChallenge.Geometry;

namespace ToyRobotChallenge
{
    /// <summary>
    /// This is where it all comes together.  Create a simulator and give it a robot to control
    /// </summary>
    public partial class Simulator
    {
        /// <summary>
        /// A command list, please implement and fill in the manner as you see fit
        /// </summary>
        public abstract class CommandList 
            : List<IRobotCommand>
        {
        }

        /// <summary>
        /// Get the current robot that the simulator is running
        /// </summary>
        public IRobot TheRobot { get; }

        /// <summary>
        /// create a simualtor with a robot
        /// </summary>
        /// <param name="robot">the robot</param>
        public Simulator(IRobot robot)
        {
            Debug.Assert(robot != null);

            this.TheRobot = robot;
        }

        public void Execute(List<IRobotCommand> commands, IStringResponse response = null)
        {
            // so why use classes to represent commands and execute them here?
            //
            // 1.   It means that the simulator is now open to the existance of other commands
            //      that are NOT available via the command string.
            // 2.   Also the encapsulation of the commands and their execution means that we can now implement specialisation through aggregation
            // 3.   There is now the potential for the simple optimisation of the commands, ie. the deletion of duplicate PLACE commands one after the other

            Debug.Assert(commands != null);

            foreach (var command in commands)
            {
                Execute(command, response);
            }
        }

        public void Execute(IRobotCommand command, IStringResponse response = null)
        {
            command?.Execute(TheRobot, response);
        }
    }


    /// <summary>
    /// Stock Simulator commands
    /// </summary>
    public partial class Simulator
    {
        /// <summary>
        /// It's possible for an executed command return a response via this interface
        /// </summary>
        public interface IStringResponse
        {
            void Write(string response);
        }

        /// <summary>
        /// a robot command
        /// </summary>
        public interface IRobotCommand
        {

            void Execute(IRobot robot, IStringResponse respond = null);
        }

        /// <summary>
        /// Stock version of a place command
        /// </summary>
        public class PlaceCommand
            : IRobotCommand
        {
            private Vector2d<ulong> _vec;

            /// <summary>
            /// default ctor
            /// </summary>
            /// <param name="position">x/y position</param>
            /// <param name="direction">direction</param>
            public PlaceCommand(Vector2d<ulong> vec)
            {
                this._vec = vec;
            }

            public virtual void Execute(IRobot robot, IStringResponse respond = null)
            {
                robot?.Place(this._vec);
            }
        }

        /// <summary>
        /// Stock version of a robot position & direction validation command
        /// </summary>
        public class ValidatePosAndDirCommand
            : IRobotCommand
        {
            private Vector2d<ulong> _vec;

            /// <summary>
            /// default ctor
            /// </summary>
            /// <param name="position">x/y position</param>
            /// <param name="direction">direction</param>
            public ValidatePosAndDirCommand(Vector2d<ulong> vec)
            {
                this._vec = vec;
            }

            public virtual void Execute(IRobot robot, IStringResponse respond = null)
            {
                Debug.Assert(robot != null);

                if (!robot.HasAPosition)
                {
                    respond?.Write($"Robot position has not been set!");
                    return;
                }

                // validate the robot position & facing
                if (robot.Vector.Position.X != _vec.Position.X)
                {
                    respond?.Write($"Validation of robot position ({_vec.Position.X}, {_vec.Position.Y}) facing {_vec.Dir} failed.  Robot x position is {robot.Vector.Position.X}!");
                }

                if (robot.Vector.Position.Y != _vec.Position.Y)
                {
                    respond?.Write($"Validation of robot position ({_vec.Position.X}, {_vec.Position.Y}) facing {_vec.Dir} failed.  Robot y position is {robot.Vector.Position.Y}!");
                }

                if (robot.Vector.Dir != _vec.Dir)
                {
                    respond?.Write($"Validation of robot position ({_vec.Position.X}, {_vec.Position.Y}) facing {_vec.Dir} failed.  Robot is facing {robot.Vector.Dir}!");
                }
            }
        }

        /// <summary>
        /// Stock version of a move command
        /// </summary>
        public class MoveCommand
            : IRobotCommand
        {
            public virtual void Execute(IRobot robot, IStringResponse respond = null)
            {
                Debug.Assert(robot != null);

                if (!robot.HasAPosition)
                    return;

                robot.Move();
            }
        }

        /// <summary>
        /// Stock version of a left turn command
        /// </summary>
        public class LeftTurnCommand
            : IRobotCommand
        {
            public virtual void Execute(IRobot robot, IStringResponse respond = null)
            {
                Debug.Assert(robot != null);

                if (!robot.HasAPosition)
                    return;

                robot.TurnLeft();
            }
        }

        /// <summary>
        /// Stock version of a right turn command
        /// </summary>
        public class RightTurnCommand
            : IRobotCommand
        {
            public virtual void Execute(IRobot robot, IStringResponse respond = null)
            {
                Debug.Assert(robot != null);

                if (!robot.HasAPosition)
                    return;

                robot.TurnRight();
            }
        }

        /// <summary>
        /// stock version of a report command
        /// </summary>
        public class ReportCommand
            : IRobotCommand
        {
            public virtual void Execute(IRobot robot, IStringResponse respond = null)
            {
                Debug.Assert(robot != null);

                if (!robot.HasAPosition)
                    return;

                respond?.Write($"{robot.Vector.Position.X},{robot.Vector.Position.Y},{robot.Vector.Dir.ToString().ToUpper()}");
            }
        }
    }
}
