using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToyRobotChallenge;
using ToyRobotChallenge.Environment;
using ToyRobotChallenge.Geometry;

namespace UnitTests
{
    [TestClass]
    public class RobotTests
    {
        private static void MoveRobotUpTheBoardFromTheBottomPosition(Board board, Robot robot)
        {
            for (var y = (ulong)1; y < board.Height; y++)
            {
                robot.Move();
            }
        }

        private static void WhenSpinningARobotAroundWithinTheBoardDimensionsMakeSureMovesAreGoodOnEachColumn(   Board board, 
                                                                                                                Robot robot, 
                                                                                                                ulong x)
        {
            for (var y = (ulong)0; y < board.Height; y++)
            {
                robot.Place(new Vector2d<ulong>(new Point2d<ulong>(x, y), Direction.North));
                Assert.IsTrue(ComparePosition(robot, x, y, Direction.North));

                SpinRobotLeftAtSpecificBoardPositionAndTestSpinIsGood(robot, x, y);
                SpinRobotRightAtSpecificBoardPositionAndTestSpinIsGood(robot, x, y);
            }
        }

        private static void SpinRobotRightAtSpecificBoardPositionAndTestSpinIsGood(Robot robot, ulong x, ulong y)
        {
            // spin right
            robot.TurnRight();
            Assert.IsTrue(ComparePosition(robot, x, y, Direction.East));

            robot.TurnRight();
            Assert.IsTrue(ComparePosition(robot, x, y, Direction.South));

            robot.TurnRight();
            Assert.IsTrue(ComparePosition(robot, x, y, Direction.West));

            robot.TurnRight();
            Assert.IsTrue(ComparePosition(robot, x, y, Direction.North));
        }

        private static void SpinRobotLeftAtSpecificBoardPositionAndTestSpinIsGood(Robot robot, ulong x, ulong y)
        {
            // spin left
            robot.TurnLeft();
            Assert.IsTrue(ComparePosition(robot, x, y, Direction.West));

            robot.TurnLeft();
            Assert.IsTrue(ComparePosition(robot, x, y, Direction.South));

            robot.TurnLeft();
            Assert.IsTrue(ComparePosition(robot, x, y, Direction.East));

            robot.TurnLeft();
            Assert.IsTrue(ComparePosition(robot, x, y, Direction.North));
        }

        private static void WhenPlacingARobotInMultiplePositionsMakeSureIsPlacedCorrectlyOnEachColumn(  Board board, 
                                                                                                        Robot robot, 
                                                                                                        Direction direction, 
                                                                                                        ulong x)
        {
            for (var y = (ulong)0; y < board.Height; y++)
            {
                robot.Place(new Vector2d<ulong>(new Point2d<ulong>(x, y), direction));

                Assert.IsTrue(robot.HasAPosition);
                Assert.IsTrue(ComparePosition(robot, x, y, direction));
            }
        }

        private static void WhenPlacingARobotInInvalidPositionsMakeSurePlaceCommandIsIgnoredOnEachColumn(   Board board,
                                                                                                            Robot robot,
                                                                                                            Direction direction,
                                                                                                            ulong origRobotX,
                                                                                                            ulong origRobotY,
                                                                                                            Direction origRobotDir,
                                                                                                            ulong extraYDim,
                                                                                                            ulong x)
        {
            for (var y = board.Height; y < board.Height + extraYDim; y++)
            {
                // illegal placement
                // bad is good!  make sure robot has not moved position or direction
                robot.Place(new Vector2d<ulong>(new Point2d<ulong>(x, y), direction));

                // should still have a position though!
                Assert.IsTrue(robot.HasAPosition);

                Assert.AreEqual(origRobotX,     robot.Vector.Position.X);
                Assert.AreEqual(origRobotY,     robot.Vector.Position.Y);
                Assert.AreEqual(origRobotDir,   robot.Vector.Dir);
            }
        }

        public static bool ComparePosition(Robot robot, ulong desiredX, ulong desiredY, Direction desiredDir)
        {
            Assert.AreNotEqual(null, robot);
            Assert.AreEqual(true, robot.HasAPosition);

            return  true
                    && robot.Vector.Position.X == desiredX
                    && robot.Vector.Position.Y == desiredY
                    && robot.Vector.Dir == desiredDir;        
        }

        [TestMethod]
        public void WhenPlacingARobotIn1PositionMakeSureIsPlacedCorrectly()
        {
            // create a robot that know about it's environment
            var board = new Board(5, 5);
            var robot = new ToyRobot(board);

            Assert.IsNotNull(board);
            Assert.IsNotNull(robot);

            // place the robot on various places on the board
            robot.Place(new Vector2d<ulong>(new Point2d<ulong>(0, 0), Direction.North));

            Assert.IsTrue(robot.HasAPosition);
            Assert.IsTrue(ComparePosition(robot, 0, 0, Direction.North));
        }

        [TestMethod]
        public void WhenPlacingARobotInMultiplePositionsMakeSureIsPlacedCorrectly()
        {
            // create a robot that knows about it's environment
            var board = new Board(5, 5);
            var robot = new ToyRobot(board);

            Assert.IsNotNull(board);
            Assert.IsNotNull(robot);

            var direction = Direction.North;

            // test all valid positions within the board
            for (var x = (ulong)0; x < board.Width; x++)
            {
                WhenPlacingARobotInMultiplePositionsMakeSureIsPlacedCorrectlyOnEachColumn(board, robot, direction, x);
            }
        }    

        public void WhenPlacingARobotInInvalidPositionsMakeSurePlaceCommandIsIgnored()
        {
            // create a robot that knows about it's environment
            var board = new Board(5, 5);
            var robot = new ToyRobot(board);

            Assert.IsNotNull(board);
            Assert.IsNotNull(robot);

            var direction = Direction.North;

            robot.Place(new Vector2d<ulong>(new Point2d<ulong>(0, 0), direction));
            Assert.IsTrue(robot.HasAPosition);
            Assert.IsTrue(ComparePosition(robot, 0, 0, direction));

            // get robot current position & direction
            var origRobotX = robot.Vector.Position.X;
            var origRobotY = robot.Vector.Position.Y;
            var origRobotDir = robot.Vector.Dir;
            var extraXDim = (ulong)5;
            var extraYDim = (ulong)5;

            // test some invalid positions
            for (var x = board.Width; x < board.Width + extraXDim; x++)
            {
                WhenPlacingARobotInInvalidPositionsMakeSurePlaceCommandIsIgnoredOnEachColumn(board, robot, direction, origRobotX, origRobotY, origRobotDir, extraYDim, x);
            }
        }

        [TestMethod]
        public void WhenMovingARobotUpAndDownWithinTheBoardDimensionsMakeSureMovesAreGood()
        {
            // create a robot that knows about it's environment
            var board = new Board(5, 5);
            var robot = new ToyRobot(board);

            Assert.IsNotNull(board);
            Assert.IsNotNull(robot);

            // lets place it in the middle facing north
            robot.Place(new Vector2d<ulong>(new Point2d<ulong>(2, 2), Direction.North));
            Assert.IsTrue(ComparePosition(robot, 2, 2, Direction.North));

            // lets move it up, turn around, move down to edge, turn around and move back up
            // move to 2,3
            robot.Move();
            Assert.IsTrue(ComparePosition(robot, 2, 3, Direction.North));

            // move to 2,4
            robot.Move();
            Assert.IsTrue(ComparePosition(robot, 2, 4, Direction.North));

            // turn 90 degrees right
            robot.TurnRight();
            Assert.IsTrue(ComparePosition(robot, 2, 4, Direction.East));

            // turn 90 degrees right again
            robot.TurnRight();
            Assert.IsTrue(ComparePosition(robot, 2, 4, Direction.South));

            // move to 2,3
            robot.Move();
            Assert.IsTrue(ComparePosition(robot, 2, 3, Direction.South));

            // move to 2,2
            robot.Move();
            Assert.IsTrue(ComparePosition(robot, 2, 2, Direction.South));

            // move to 2,1
            robot.Move();
            Assert.IsTrue(ComparePosition(robot, 2, 1, Direction.South));

            // move to 2,0
            robot.Move();
            Assert.IsTrue(ComparePosition(robot, 2, 0, Direction.South));

            // turn 90 degrees left
            robot.TurnLeft();
            Assert.IsTrue(ComparePosition(robot, 2, 0, Direction.East));

            // turn 90 degrees left again
            robot.TurnLeft();
            Assert.IsTrue(ComparePosition(robot, 2, 0, Direction.North));

            // move to 2,1
            robot.Move();
            Assert.IsTrue(ComparePosition(robot, 2, 1, Direction.North));

            // move to 2,2
            robot.Move();
            Assert.IsTrue(ComparePosition(robot, 2, 2, Direction.North));
        }

        [TestMethod]
        public void WhenSpinningARobotAroundWithinTheBoardDimensionsMakeSureMovesAreGood()
        {
            // create a robot that knows about it's environment
            var board = new Board(5, 5);
            var robot = new ToyRobot(board);

            Assert.IsNotNull(board);
            Assert.IsNotNull(robot);

        

            // lets make the robot dizzy!
            // you spin me right round baby, right round like a record player right round, round round...

            // test all valid positions within the board, and spin the robot
            for (var x = (ulong)0; x < board.Width; x++)
            {
                WhenSpinningARobotAroundWithinTheBoardDimensionsMakeSureMovesAreGoodOnEachColumn(board, robot, x);
            }
        }

        [TestMethod]
        public void WhenSpanningARobotUpDownLeftRightOverTheBoardDimensionsMakeSureMovesAreGood()
        {
            // create a robot that knows about it's environment
            var board = new Board(5, 5);
            var robot = new ToyRobot(board);

            Assert.IsNotNull(board);
            Assert.IsNotNull(robot);

            // lets do a left to right bottom to top, top to bottom zig zag of the board
            robot.Place(new Vector2d<ulong>(new Point2d<ulong>(0, 0), Direction.North));
            Assert.IsTrue(ComparePosition(robot, 0, 0, Direction.North));

            // left to right
            for (var x = (ulong)0; x < board.Width; x++)
            {
                MoveRobotUpTheBoardFromTheBottomPosition(board, robot);

                if (x == board.Width - 1)
                {
                    // at the end, can't do a u turn
                    break;
                }

                if (robot.Vector.Dir == Direction.North)
                {
                    // u turn to next row down
                    robot.TurnRight();
                    robot.Move();
                    robot.TurnRight();

                    continue;
                }

                if (robot.Vector.Dir == Direction.South)
                {
                    // u turn to next row down
                    robot.TurnLeft();
                    robot.Move();
                    robot.TurnLeft();

                    continue;
                }

                Assert.IsTrue(ComparePosition(robot, board.Width - 1, board.Width % 2 == 0 ? 0 : board.Height - 1, board.Width % 2 == 0 ? Direction.South : Direction.North));
            }
        }

        [TestMethod]
        public void WhenSpanningARobotLeftRightBottomTopOverTheBoardDimensionsMakeSureMovesAreGood()
        {
            // create a robot that knows about it's environment
            var board = new Board(5, 5);
            var robot = new ToyRobot(board);

            Assert.IsNotNull(board);
            Assert.IsNotNull(robot);

            // lets do a left to right bottom to top, top to bottom zig zag of the board
            robot.Place(new Vector2d<ulong>(new Point2d<ulong>(0, 0), Direction.North));
            Assert.IsTrue(ComparePosition(robot, 0, 0, Direction.North));

            // left to right
            for (var x = (ulong)0; x < board.Width; x++)
            {
                MoveRobotUpTheBoardFromTheBottomPosition(board, robot);

                if (x == board.Width - 1)
                {
                    // at the end, can't do a u turn
                    break;
                }

                if (robot.Vector.Dir == Direction.North)
                {
                    // u turn to next row down
                    robot.TurnRight();
                    robot.Move();
                    robot.TurnRight();

                    continue;
                }

                if (robot.Vector.Dir == Direction.South)
                {
                    // u turn to next row down
                    robot.TurnLeft();
                    robot.Move();
                    robot.TurnLeft();

                    continue;
                }
            }

            Assert.IsTrue(ComparePosition(robot, board.Width - 1, board.Width % 2 == 0 ? 0 : board.Height - 1, board.Width % 2 == 0 ? Direction.South : Direction.North));
        }
    }
}
