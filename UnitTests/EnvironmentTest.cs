using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ToyRobotChallenge.Environment;
using ToyRobotChallenge.Geometry;

namespace UnitTests
{
    [TestClass]
    public class EnvironmentTest
    {
        private void WhenCreatingA5x5BoardCaptureBadPositionsOnAColumn(Board theBoard, ulong x)
        {
            var extraYDim = (ulong)5;

            for (var y = theBoard.Height; y < theBoard.Height + extraYDim; y++)
            {
                Assert.IsFalse(theBoard.IsValidPosition(new Point2d<ulong>(x, y)));
            }
        }

        private void WhenCreatingA5x5BoardValidateGoodPositionsOnAColumn(Board theBoard, ulong x)
        {
            for (var y = (ulong)0; y < theBoard.Height; y++)
            {
                Assert.IsTrue(theBoard.IsValidPosition(new Point2d<ulong>(x, y)));
            }
        }

        [TestMethod]
        public void WhenCreatingA5x5BoardValidateGoodPositions()
        {
            // create a 5x5 board, shouln't fail!
            var theBoard = new Board(5, 5);

            Assert.IsNotNull(theBoard);

            // test all valid positions within the board
            for (ulong x = 0; x < theBoard.Width; x++)
            {
                WhenCreatingA5x5BoardValidateGoodPositionsOnAColumn(theBoard, x);
            }
        }


        [TestMethod]
        public void WhenCreatingA5x5BoardCaptureBadPositions()
        {
            // create a 5x5 board, shouln't fail!
            var theBoard = new Board(5, 5);

            Assert.IsNotNull(theBoard);

            var extraXDim = (ulong)5;
                
            // test some invalid positions
            for (ulong x = theBoard.Width; x < theBoard.Width + extraXDim; x++)
            {
                WhenCreatingA5x5BoardCaptureBadPositionsOnAColumn(theBoard, x);
            }
        }

        [TestMethod]
        public void WhenCreatingBoardsOfVariousSizesMakeSureTheBoardDimensionsAreCorrect()
        {
            // test for various board sizes, no need to test for out of bound's since
            // the use of a ulong will let the compiler prevent -ve values, and values over uint64 max
            var boardSizes = new List<Tuple<ulong, ulong>>
            {
                new Tuple<ulong, ulong>(ulong.MaxValue, ulong.MaxValue),
                new Tuple<ulong, ulong>(ulong.MinValue, ulong.MaxValue),
                new Tuple<ulong, ulong>(ulong.MaxValue, ulong.MinValue),
                new Tuple<ulong, ulong>(ulong.MinValue, ulong.MinValue),
            };


            foreach (var dim in boardSizes)
            {
                var theBoard = new Board(dim.Item1, dim.Item2);

                // should still be able to create the board, even though it is the wrong dimensions
                Assert.IsNotNull(theBoard);
                Assert.AreEqual(dim.Item1, theBoard.Width);
                Assert.AreEqual(dim.Item2, theBoard.Height);                        
            }
        }

        [TestMethod]
        public void WhenCreatingBoardsA5x5BoardMakeSureTheBoardDimensionsAreCorrect()
        {
            // create a board of the required size
            var theBoard = new Board(5, 5);

            Assert.IsNotNull(theBoard);
            Assert.AreEqual(5UL, theBoard.Height);
            Assert.AreEqual(5UL, theBoard.Width);
        }
    }
}
