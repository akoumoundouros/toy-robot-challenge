using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ToyRobotChallenge.Environment;
using ToyRobotChallenge.Geometry;
using ToyRobotChallenge;

namespace UnitTests
{
    [TestClass]
    public class SimulatorTests
    {
        private class Response
            : Simulator.IStringResponse
        {
            private string _text;

            public string Test => _text;

            public void Write(string response)
            {
                _text += $"{response}\n";
            }
        }
        /// <summary>
        /// Do nothing command list, no parsing, no anything.... just a list
        /// </summary>
        private class TestCommandList : Simulator.CommandList
        {
        }

        private bool ValidateReport(Simulator simulator)
        {
            var response = new Response();
            var robot = simulator.TheRobot;

            simulator.Execute(new TestCommandList() { new Simulator.ReportCommand() }, response);

            var desiredReport = $"{ robot.Vector.Position.X},{ robot.Vector.Position.Y},{ robot.Vector.Dir.ToString().ToUpper()}";

            return string.Compare(response.Test.Trim(), desiredReport) == 0;
        }

        [TestMethod]
        public void WhenControllingARobotOnTheBoardWithSingleCommandObjectsMakeSureItEndsUpWhereItIsSupposedToBe()
        {
            var board = new Board(5, 5);
            var robot = new ToyRobot(board);
            var simulator = new Simulator(robot);

            Assert.IsNotNull(board);
            Assert.IsNotNull(robot);
            Assert.IsNotNull(simulator);

            // move the robot in the simulator via explicit commands              
            simulator.Execute(new Simulator.PlaceCommand(new Vector2d<ulong>(new Point2d<ulong>(2, 2), Direction.North)));
            Assert.IsTrue(robot.HasAPosition);
            Assert.IsTrue(RobotTests.ComparePosition(robot, 2, 2, Direction.North));

            simulator.Execute(new Simulator.MoveCommand());
            Assert.IsTrue(robot.HasAPosition);
            Assert.IsTrue(RobotTests.ComparePosition(robot, 2, 3, Direction.North));

            simulator.Execute(new Simulator.MoveCommand());
            Assert.IsTrue(robot.HasAPosition);
            Assert.IsTrue(RobotTests.ComparePosition(robot, 2, 4, Direction.North));

            simulator.Execute(new Simulator.RightTurnCommand());
            Assert.IsTrue(robot.HasAPosition);
            Assert.IsTrue(RobotTests.ComparePosition(robot, 2, 4, Direction.East));

            simulator.Execute(new Simulator.RightTurnCommand());
            Assert.IsTrue(robot.HasAPosition);
            Assert.IsTrue(RobotTests.ComparePosition(robot, 2, 4, Direction.South));

            simulator.Execute(new Simulator.MoveCommand());
            Assert.IsTrue(robot.HasAPosition);
            Assert.IsTrue(RobotTests.ComparePosition(robot, 2, 3, Direction.South));

            simulator.Execute(new Simulator.MoveCommand());
            Assert.IsTrue(robot.HasAPosition);
            Assert.IsTrue(RobotTests.ComparePosition(robot, 2, 2, Direction.South));

            Assert.IsTrue(ValidateReport(simulator));
        }

        [TestMethod]
        public void WhenControllingARobotOnTheBoardWithACommandObjectListsMakeSureItEndsUpWhereItIsSupposedToBe()
        {
            var board = new Board(5, 5);
            var robot = new ToyRobot(board);
            var simulator = new Simulator(robot);

            Assert.IsNotNull(board);
            Assert.IsNotNull(robot);
            Assert.IsNotNull(simulator);

            // move the robot in the simulator via command list or more than 1 command
            simulator.Execute(  new TestCommandList()
                                {
                                    new Simulator.PlaceCommand(new Vector2d<ulong>(new Point2d<ulong>(2, 2), Direction.North)),
                                    new Simulator.MoveCommand(),
                                    new Simulator.MoveCommand(),
                                    new Simulator.LeftTurnCommand(),
                                    new Simulator.LeftTurnCommand(),
                                    new Simulator.MoveCommand(),
                                    new Simulator.MoveCommand(),
                                    new Simulator.RightTurnCommand(),
                                    new Simulator.MoveCommand()
                                });

            Assert.IsTrue(robot.HasAPosition);
            Assert.IsTrue(RobotTests.ComparePosition(robot, 1, 2, Direction.West));
            Assert.IsTrue(ValidateReport(simulator));
        }

        [TestMethod]
        public void WhenCreatingASimulatorWithARobotOnATableMakeSureCreationSucceeds()
        {
            var board = new Board(5, 5);
            var robot = new ToyRobot(board);
            var simulator = new Simulator(robot);

            Assert.IsNotNull(board);
            Assert.IsNotNull(robot);
            Assert.IsNotNull(simulator);
        }
    }
}
