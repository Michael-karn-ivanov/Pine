using BonusEV2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Pineapple;

namespace PineHome.Tests
{
    
    
    /// <summary>
    ///This is a test class for PredictorTest and is intended
    ///to contain all PredictorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PredictorTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Evaluate
        ///</summary>
        [TestMethod()]
        public void EvaluateTest()
        {
            Predictor target = new Predictor(); // TODO: Initialize to an appropriate value
            byte[] heroHand = InputReader.ReadInput("Qs Qc 6c 2c 3c 6d Ad ? 7d 7c 7s 8c ?");
            byte[] deck = InputReader.ReadDeck("Ac Js Th Td Tc Jh");
            Decimal expected = new Decimal(); // TODO: Initialize to an appropriate value
            Decimal actual;
            actual = target.Evaluate(heroHand, deck);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void EvaluateTest2()
        {
            Predictor target = new Predictor(); // TODO: Initialize to an appropriate value
            byte[] heroHand = InputReader.ReadInput("6d Qs ? 7h Kh Ks 4d 3h Ad Jd 8d 2d ?");
            byte[] deck = InputReader.ReadDeck("Qd 3d 5d 3s 4s 7s");
            Decimal expected = new Decimal(); // TODO: Initialize to an appropriate value
            Decimal actual;
            actual = target.Evaluate(heroHand, deck);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
