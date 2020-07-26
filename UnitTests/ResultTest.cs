using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ToyRobotChallenge;
using ToyRobotChallenge.Utilities;
using ToyRobotChallenge.Environment;

namespace UnitTests
{
    [TestClass]
    public class ResultTest
    {
        [TestMethod]
        public void ExerciseResults()
        {
            Result result = new Result();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Good);

            string badResultMsg1 = "This is a test for failure #1!";
            string badResultMsg2 = "This is a test for failure #2!";
            string badResultMsg1And2 = $"{badResultMsg1}\n{badResultMsg2}";

            // test for 1 failure
            {
                Result result2 = result.Append(new Result(Result.ResultFlag.Failed, badResultMsg1));

                // they should be the one and the same!
                Assert.IsNotNull(result2 != null);
                Assert.IsTrue(result2.Bad);
                Assert.AreEqual(result2.Message, badResultMsg1);
                Assert.ReferenceEquals(result2, result);
            }

            // test for 2 failures
            {
                Result result2 = result.Append(new Result(Result.ResultFlag.Failed, badResultMsg2));

                // they should be the one and the same!
                Assert.IsNotNull(result2 != null);
                Assert.IsTrue(result2.Bad);
                Assert.AreEqual(result2.Message, badResultMsg1And2);
                Assert.ReferenceEquals(result2, result);
            }
        }
    }
}
