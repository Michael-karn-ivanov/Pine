using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pineapple.UnitTest
{
	[TestClass]
	public class LastTurnInPositionTest
    {
        #region тесты рабочих методов
        #endregion

        #region тесты утилитарных методов
        [TestMethod]
        public void TestScoreCompareDeadVSDead()
        {
            // assign
            var heroShortHand = InputReader.ReadInput("Qh Jd Ts");
            var heroShortSorted = new byte[3];
            var heroMiddleHand = InputReader.ReadInput("Ac 2s As 2d 3h");
            var heroMiddleSorted = new byte[5];
            var heroTopHand = InputReader.ReadInput("Th 9d 9c 8c Ts");
            var heroTopSorted = new byte[5];

            var villainShortHand = InputReader.ReadInput("8d 8s 2c");
            var villainShortSorted = new byte[3];
            var villainMiddleHand = InputReader.ReadInput("7d 6c 7s 5c 3h");
            var villainMiddleSorted = new byte[5];
            var villainTopHand = InputReader.ReadInput("Ad Kd Qd Jd Td");
            var villainTopSorted = new byte[5];

            var objectUnderTest = new LastTurnInPosition();
            LineValue heroShortPower = objectUnderTest.SortLine(heroShortHand, heroShortSorted, 0, 3);
            LineValue heroMiddlePower = objectUnderTest.SortLine(heroMiddleHand, heroMiddleSorted, 0, 5);
            LineValue heroTopPower = objectUnderTest.SortLine(heroTopHand, heroTopSorted, 0, 5);
            LineValue villainShortPower = objectUnderTest.SortLine(villainShortHand, villainShortSorted, 0, 3);
            LineValue villainMiddlePower = objectUnderTest.SortLine(villainMiddleHand, villainMiddleSorted, 0, 5);
            LineValue villainTopPower = objectUnderTest.SortLine(villainTopHand, villainTopSorted, 0, 5);

            var isHeroHandDead = objectUnderTest.IsHandDead(heroShortSorted, heroMiddleSorted, heroTopSorted, 
                heroShortPower, heroMiddlePower, heroTopPower);
            var isVillainHandDead = objectUnderTest.IsHandDead(villainShortSorted, villainMiddleSorted, villainTopSorted, 
                villainShortPower, villainMiddlePower, villainTopPower);

            //act
            var score = objectUnderTest.ScoreCompare(isHeroHandDead, isVillainHandDead, heroShortSorted, heroMiddleSorted,
                heroTopSorted, heroShortPower, heroMiddlePower, heroTopPower, villainShortSorted, villainMiddleSorted,
                villainTopSorted, villainShortPower, villainMiddlePower, villainTopPower);

            // assert
            Assert.AreEqual(0, score);
        }

        [TestMethod]
        public void TestScoreCompareDeadvsAlive()
        {
            // assign
            var heroShortHand = InputReader.ReadInput("Qh Jd Ts");
            var heroShortSorted = new byte[3];
            var heroMiddleHand = InputReader.ReadInput("Ac 2s As 2d 3h");
            var heroMiddleSorted = new byte[5];
            var heroTopHand = InputReader.ReadInput("Th 9d 9c 8c Ts");
            var heroTopSorted = new byte[5];

            var villainShortHand = InputReader.ReadInput("8d 8s 2c");
            var villainShortSorted = new byte[3];
            var villainMiddleHand = InputReader.ReadInput("9d 6c 9s 5c 3h");
            var villainMiddleSorted = new byte[5];
            var villainTopHand = InputReader.ReadInput("Ad Kd Qd Jd Td");
            var villainTopSorted = new byte[5];

            var objectUnderTest = new LastTurnInPosition();
            LineValue heroShortPower = objectUnderTest.SortLine(heroShortHand, heroShortSorted, 0, 3);
            LineValue heroMiddlePower = objectUnderTest.SortLine(heroMiddleHand, heroMiddleSorted, 0, 5);
            LineValue heroTopPower = objectUnderTest.SortLine(heroTopHand, heroTopSorted, 0, 5);
            LineValue villainShortPower = objectUnderTest.SortLine(villainShortHand, villainShortSorted, 0, 3);
            LineValue villainMiddlePower = objectUnderTest.SortLine(villainMiddleHand, villainMiddleSorted, 0, 5);
            LineValue villainTopPower = objectUnderTest.SortLine(villainTopHand, villainTopSorted, 0, 5);

            var isHeroHandDead = objectUnderTest.IsHandDead(heroShortSorted, heroMiddleSorted, heroTopSorted,
                heroShortPower, heroMiddlePower, heroTopPower);
            var isVillainHandDead = objectUnderTest.IsHandDead(villainShortSorted, villainMiddleSorted, villainTopSorted,
                villainShortPower, villainMiddlePower, villainTopPower);

            //act
            var score = objectUnderTest.ScoreCompare(isHeroHandDead, isVillainHandDead, heroShortSorted, heroMiddleSorted,
                heroTopSorted, heroShortPower, heroMiddlePower, heroTopPower, villainShortSorted, villainMiddleSorted,
                villainTopSorted, villainShortPower, villainMiddlePower, villainTopPower);

            // assert
            Assert.AreEqual(-6, score);
        }

        [TestMethod]
        public void TestScoreCompareAliveVsDead()
        {
            // assign
            var heroShortHand = InputReader.ReadInput("Qh Jd Ts");
            var heroShortSorted = new byte[3];
            var heroMiddleHand = InputReader.ReadInput("Ac 2s As 2d 3h");
            var heroMiddleSorted = new byte[5];
            var heroTopHand = InputReader.ReadInput("Th 9d 9c Tc Ts");
            var heroTopSorted = new byte[5];

            var villainShortHand = InputReader.ReadInput("8d 8s 2c");
            var villainShortSorted = new byte[3];
            var villainMiddleHand = InputReader.ReadInput("7d 6c 7s 5c 3h");
            var villainMiddleSorted = new byte[5];
            var villainTopHand = InputReader.ReadInput("Ad Kd Qd Jd Td");
            var villainTopSorted = new byte[5];

            var objectUnderTest = new LastTurnInPosition();
            LineValue heroShortPower = objectUnderTest.SortLine(heroShortHand, heroShortSorted, 0, 3);
            LineValue heroMiddlePower = objectUnderTest.SortLine(heroMiddleHand, heroMiddleSorted, 0, 5);
            LineValue heroTopPower = objectUnderTest.SortLine(heroTopHand, heroTopSorted, 0, 5);
            LineValue villainShortPower = objectUnderTest.SortLine(villainShortHand, villainShortSorted, 0, 3);
            LineValue villainMiddlePower = objectUnderTest.SortLine(villainMiddleHand, villainMiddleSorted, 0, 5);
            LineValue villainTopPower = objectUnderTest.SortLine(villainTopHand, villainTopSorted, 0, 5);

            var isHeroHandDead = objectUnderTest.IsHandDead(heroShortSorted, heroMiddleSorted, heroTopSorted,
                heroShortPower, heroMiddlePower, heroTopPower);
            var isVillainHandDead = objectUnderTest.IsHandDead(villainShortSorted, villainMiddleSorted, villainTopSorted,
                villainShortPower, villainMiddlePower, villainTopPower);

            //act
            var score = objectUnderTest.ScoreCompare(isHeroHandDead, isVillainHandDead, heroShortSorted, heroMiddleSorted,
                heroTopSorted, heroShortPower, heroMiddlePower, heroTopPower, villainShortSorted, villainMiddleSorted,
                villainTopSorted, villainShortPower, villainMiddlePower, villainTopPower);

            // assert
            Assert.AreEqual(6, score);
        }

        [TestMethod]
        public void TestScoreCompare30Alive()
        {
            // assign
            var heroShortHand = InputReader.ReadInput("Qh Qd Ts");
            var heroShortSorted = new byte[3];
            var heroMiddleHand = InputReader.ReadInput("Ac 2s As 2d 3h");
            var heroMiddleSorted = new byte[5];
            var heroTopHand = InputReader.ReadInput("Th 9d 9c Tc Ts");
            var heroTopSorted = new byte[5];

            var villainShortHand = InputReader.ReadInput("Jd Js 2c");
            var villainShortSorted = new byte[3];
            var villainMiddleHand = InputReader.ReadInput("7d 6c 7s 6c 3h");
            var villainMiddleSorted = new byte[5];
            var villainTopHand = InputReader.ReadInput("Ad Ks Qd Jd Td");
            var villainTopSorted = new byte[5];

            var objectUnderTest = new LastTurnInPosition();
            LineValue heroShortPower = objectUnderTest.SortLine(heroShortHand, heroShortSorted, 0, 3);
            LineValue heroMiddlePower = objectUnderTest.SortLine(heroMiddleHand, heroMiddleSorted, 0, 5);
            LineValue heroTopPower = objectUnderTest.SortLine(heroTopHand, heroTopSorted, 0, 5);
            LineValue villainShortPower = objectUnderTest.SortLine(villainShortHand, villainShortSorted, 0, 3);
            LineValue villainMiddlePower = objectUnderTest.SortLine(villainMiddleHand, villainMiddleSorted, 0, 5);
            LineValue villainTopPower = objectUnderTest.SortLine(villainTopHand, villainTopSorted, 0, 5);

            var isHeroHandDead = objectUnderTest.IsHandDead(heroShortSorted, heroMiddleSorted, heroTopSorted,
                heroShortPower, heroMiddlePower, heroTopPower);
            var isVillainHandDead = objectUnderTest.IsHandDead(villainShortSorted, villainMiddleSorted, villainTopSorted,
                villainShortPower, villainMiddlePower, villainTopPower);

            //act
            var score = objectUnderTest.ScoreCompare(isHeroHandDead, isVillainHandDead, heroShortSorted, heroMiddleSorted,
                heroTopSorted, heroShortPower, heroMiddlePower, heroTopPower, villainShortSorted, villainMiddleSorted,
                villainTopSorted, villainShortPower, villainMiddlePower, villainTopPower);

            // assert
            Assert.AreEqual(6, score);
        }

        [TestMethod]
        public void TestScoreCompare03()
        {
            // assign
            var heroShortHand = InputReader.ReadInput("Qh Qd Ts");
            var heroShortSorted = new byte[3];
            var heroMiddleHand = InputReader.ReadInput("Ac 2s As 2d 3h");
            var heroMiddleSorted = new byte[5];
            var heroTopHand = InputReader.ReadInput("Th 9d 9c Tc Ts");
            var heroTopSorted = new byte[5];

            var villainShortHand = InputReader.ReadInput("Kd Ks 2c");
            var villainShortSorted = new byte[3];
            var villainMiddleHand = InputReader.ReadInput("7d 6c 7s 7c 3h");
            var villainMiddleSorted = new byte[5];
            var villainTopHand = InputReader.ReadInput("Ad Kd Qd Jd Td");
            var villainTopSorted = new byte[5];

            var objectUnderTest = new LastTurnInPosition();
            LineValue heroShortPower = objectUnderTest.SortLine(heroShortHand, heroShortSorted, 0, 3);
            LineValue heroMiddlePower = objectUnderTest.SortLine(heroMiddleHand, heroMiddleSorted, 0, 5);
            LineValue heroTopPower = objectUnderTest.SortLine(heroTopHand, heroTopSorted, 0, 5);
            LineValue villainShortPower = objectUnderTest.SortLine(villainShortHand, villainShortSorted, 0, 3);
            LineValue villainMiddlePower = objectUnderTest.SortLine(villainMiddleHand, villainMiddleSorted, 0, 5);
            LineValue villainTopPower = objectUnderTest.SortLine(villainTopHand, villainTopSorted, 0, 5);

            var isHeroHandDead = objectUnderTest.IsHandDead(heroShortSorted, heroMiddleSorted, heroTopSorted,
                heroShortPower, heroMiddlePower, heroTopPower);
            var isVillainHandDead = objectUnderTest.IsHandDead(villainShortSorted, villainMiddleSorted, villainTopSorted,
                villainShortPower, villainMiddlePower, villainTopPower);

            //act
            var score = objectUnderTest.ScoreCompare(isHeroHandDead, isVillainHandDead, heroShortSorted, heroMiddleSorted,
                heroTopSorted, heroShortPower, heroMiddlePower, heroTopPower, villainShortSorted, villainMiddleSorted,
                villainTopSorted, villainShortPower, villainMiddlePower, villainTopPower);

            // assert
            Assert.AreEqual(-6, score);
        }

        [TestMethod]
        public void TestScoreCompare21()
        {
            // assign
            var heroShortHand = InputReader.ReadInput("Qh Qd Ts");
            var heroShortSorted = new byte[3];
            var heroMiddleHand = InputReader.ReadInput("Ac 2s As 2d 3h");
            var heroMiddleSorted = new byte[5];
            var heroTopHand = InputReader.ReadInput("Th 9d 9c Tc Ts");
            var heroTopSorted = new byte[5];

            var villainShortHand = InputReader.ReadInput("Jd Js 2c");
            var villainShortSorted = new byte[3];
            var villainMiddleHand = InputReader.ReadInput("7d 6c 7s 7c 3h");
            var villainMiddleSorted = new byte[5];
            var villainTopHand = InputReader.ReadInput("Ad Kd Qs Jd Td");
            var villainTopSorted = new byte[5];

            var objectUnderTest = new LastTurnInPosition();
            LineValue heroShortPower = objectUnderTest.SortLine(heroShortHand, heroShortSorted, 0, 3);
            LineValue heroMiddlePower = objectUnderTest.SortLine(heroMiddleHand, heroMiddleSorted, 0, 5);
            LineValue heroTopPower = objectUnderTest.SortLine(heroTopHand, heroTopSorted, 0, 5);
            LineValue villainShortPower = objectUnderTest.SortLine(villainShortHand, villainShortSorted, 0, 3);
            LineValue villainMiddlePower = objectUnderTest.SortLine(villainMiddleHand, villainMiddleSorted, 0, 5);
            LineValue villainTopPower = objectUnderTest.SortLine(villainTopHand, villainTopSorted, 0, 5);

            var isHeroHandDead = objectUnderTest.IsHandDead(heroShortSorted, heroMiddleSorted, heroTopSorted,
                heroShortPower, heroMiddlePower, heroTopPower);
            var isVillainHandDead = objectUnderTest.IsHandDead(villainShortSorted, villainMiddleSorted, villainTopSorted,
                villainShortPower, villainMiddlePower, villainTopPower);

            //act
            var score = objectUnderTest.ScoreCompare(isHeroHandDead, isVillainHandDead, heroShortSorted, heroMiddleSorted,
                heroTopSorted, heroShortPower, heroMiddlePower, heroTopPower, villainShortSorted, villainMiddleSorted,
                villainTopSorted, villainShortPower, villainMiddlePower, villainTopPower);

            // assert
            Assert.AreEqual(1, score);
        }

        [TestMethod]
        public void TestScoreCompare12()
        {
            // assign
            var heroShortHand = InputReader.ReadInput("Qh Qd Ts");
            var heroShortSorted = new byte[3];
            var heroMiddleHand = InputReader.ReadInput("Ac 2s As 2d 3h");
            var heroMiddleSorted = new byte[5];
            var heroTopHand = InputReader.ReadInput("Th 9d 9c Tc Ts");
            var heroTopSorted = new byte[5];

            var villainShortHand = InputReader.ReadInput("Kd Ks 2c");
            var villainShortSorted = new byte[3];
            var villainMiddleHand = InputReader.ReadInput("7d 6c 7s 7c 3h");
            var villainMiddleSorted = new byte[5];
            var villainTopHand = InputReader.ReadInput("Ad Kd Qs Jd Td");
            var villainTopSorted = new byte[5];

            var objectUnderTest = new LastTurnInPosition();
            LineValue heroShortPower = objectUnderTest.SortLine(heroShortHand, heroShortSorted, 0, 3);
            LineValue heroMiddlePower = objectUnderTest.SortLine(heroMiddleHand, heroMiddleSorted, 0, 5);
            LineValue heroTopPower = objectUnderTest.SortLine(heroTopHand, heroTopSorted, 0, 5);
            LineValue villainShortPower = objectUnderTest.SortLine(villainShortHand, villainShortSorted, 0, 3);
            LineValue villainMiddlePower = objectUnderTest.SortLine(villainMiddleHand, villainMiddleSorted, 0, 5);
            LineValue villainTopPower = objectUnderTest.SortLine(villainTopHand, villainTopSorted, 0, 5);

            var isHeroHandDead = objectUnderTest.IsHandDead(heroShortSorted, heroMiddleSorted, heroTopSorted,
                heroShortPower, heroMiddlePower, heroTopPower);
            var isVillainHandDead = objectUnderTest.IsHandDead(villainShortSorted, villainMiddleSorted, villainTopSorted,
                villainShortPower, villainMiddlePower, villainTopPower);

            //act
            var score = objectUnderTest.ScoreCompare(isHeroHandDead, isVillainHandDead, heroShortSorted, heroMiddleSorted,
                heroTopSorted, heroShortPower, heroMiddlePower, heroTopPower, villainShortSorted, villainMiddleSorted,
                villainTopSorted, villainShortPower, villainMiddlePower, villainTopPower);

            // assert
            Assert.AreEqual(-1, score);
        }

        [TestMethod]
        public void TestCompareOneAnotherVariousPower()
        {
            // assign
            var oneHand = InputReader.ReadInput("Qh Qd Jd 2c Js");
            var otherHand = InputReader.ReadInput("4c 8h 5s 6c 7d");
            var oneSorted = new byte[5];
            var otherSorted = new byte[5];

            var objectUnderTest = new LastTurnInPosition();
            LineValue onePower = objectUnderTest.SortLine(oneHand, oneSorted, 0, 5);
            LineValue otherPower = objectUnderTest.SortLine(otherHand, otherSorted, 0, 5);

            // act
            int result = objectUnderTest.CompareOneOther(oneSorted, otherSorted, onePower, otherPower);

            // assert
            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void TestCompareOneAnotherFlash()
        {
            // assign
            var oneHand = InputReader.ReadInput("Ah Qh Jh 3h 7h");
            var otherHand = InputReader.ReadInput("Ac Tc Qc 5c 4c");
            var oneSorted = new byte[5];
            var otherSorted = new byte[5];

            var objectUnderTest = new LastTurnInPosition();
            LineValue onePower = objectUnderTest.SortLine(oneHand, oneSorted, 0, 5);
            LineValue otherPower = objectUnderTest.SortLine(otherHand, otherSorted, 0, 5);

            // act
            int result = objectUnderTest.CompareOneOther(oneSorted, otherSorted, onePower, otherPower);

            // assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void TestCompareOneAnotherLen()
        {
            // assign
            var oneHand = InputReader.ReadInput("Ah Qh Jh");
            var otherHand = InputReader.ReadInput("Ac Jc Qc 5s 4c");
            var oneSorted = new byte[3];
            var otherSorted = new byte[5];

            var objectUnderTest = new LastTurnInPosition();
            LineValue onePower = objectUnderTest.SortLine(oneHand, oneSorted, 0, 3);
            LineValue otherPower = objectUnderTest.SortLine(otherHand, otherSorted, 0, 5);

            // act
            int result = objectUnderTest.CompareOneOther(oneSorted, otherSorted, onePower, otherPower);

            // assert
            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void TestCompareOneAnotherOnPower()
        {
            // assign
            var oneHand = InputReader.ReadInput("Ah Ad Ac 3h 4d");
            var otherHand = InputReader.ReadInput("7s 5c 5s 5d 5h");
            var oneSorted = new byte[5];
            var otherSorted = new byte[5];

            var objectUnderTest = new LastTurnInPosition();
            LineValue onePower = objectUnderTest.SortLine(oneHand, oneSorted, 0, 5);
            LineValue otherPower = objectUnderTest.SortLine(otherHand, otherSorted, 0, 5);

            // act
            int result = objectUnderTest.CompareOneOther(oneSorted, otherSorted, onePower, otherPower);

            // assert
            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void TestCompareOneAnotherEqualLen()
        {
            // assign
            var oneHand = InputReader.ReadInput("Ah Qh Jh 5d 4d");
            var otherHand = InputReader.ReadInput("Ac Jc Qc 5s 4c");
            var oneSorted = new byte[5];
            var otherSorted = new byte[5];

            var objectUnderTest = new LastTurnInPosition();
            LineValue onePower = objectUnderTest.SortLine(oneHand, oneSorted, 0, 5);
            LineValue otherPower = objectUnderTest.SortLine(otherHand, otherSorted, 0, 5);

            // act
            int result = objectUnderTest.CompareOneOther(oneSorted, otherSorted, onePower, otherPower);

            // assert
            Assert.IsTrue(result == 0);
        }

		[TestMethod]
		public void TestIsHandDeadWhenAliveVariousCombo()
		{
			// assign
			var shortHand = InputReader.ReadInput("Qh Qd Jd");
			var shortSorted = new byte[3];
			var middleHand = InputReader.ReadInput("Ah Kd Qs Jd Tc");
			var middleSorted = new byte[5];
			var topHand = InputReader.ReadInput("6d 6c 6s 2s 2c");
			var topSorted = new byte[5];

			var objectUnderTest = new LastTurnInPosition();
			LineValue shortPower = objectUnderTest.SortLine(shortHand, shortSorted, 0, 3);
			LineValue middlePower = objectUnderTest.SortLine(middleHand, middleSorted, 0, 5);
			LineValue topPower = objectUnderTest.SortLine(topHand, topSorted, 0, 5);

			// act
			var isDead = objectUnderTest.IsHandDead(shortSorted, middleSorted, topSorted, shortPower, middlePower, topPower);

			// assert
			Assert.IsFalse(isDead);
		}

		[TestMethod]
		public void TestIsHandDeadWhenAliveByKicker()
		{
			// assign
			var shortHand = InputReader.ReadInput("Qh Qd Jd");
			var shortSorted = new byte[3];
			var middleHand = InputReader.ReadInput("Ah Qc Qs Jd Tc");
			var middleSorted = new byte[5];
			var topHand = InputReader.ReadInput("Kd Kc 6s 2s 3c");
			var topSorted = new byte[5];

			var objectUnderTest = new LastTurnInPosition();
			LineValue shortPower = objectUnderTest.SortLine(shortHand, shortSorted, 0, 3);
			LineValue middlePower = objectUnderTest.SortLine(middleHand, middleSorted, 0, 5);
			LineValue topPower = objectUnderTest.SortLine(topHand, topSorted, 0, 5);

			// act
			var isDead = objectUnderTest.IsHandDead(shortSorted, middleSorted, topSorted, shortPower, middlePower, topPower);

			// assert
			Assert.IsFalse(isDead);
		}

		[TestMethod]
		public void TestIsHandDeadWhenDeadByKicker()
		{
			// assign
			var shortHand = InputReader.ReadInput("Qh Qd Jd");
			var shortSorted = new byte[3];
			var middleHand = InputReader.ReadInput("Qs Qc Jd Tc 9s");
			var middleSorted = new byte[5];
			var topHand = InputReader.ReadInput("Kd Kc 6s 2s 3c");
			var topSorted = new byte[5];

			var objectUnderTest = new LastTurnInPosition();
			LineValue shortPower = objectUnderTest.SortLine(shortHand, shortSorted, 0, 3);
			LineValue middlePower = objectUnderTest.SortLine(middleHand, middleSorted, 0, 5);
			LineValue topPower = objectUnderTest.SortLine(topHand, topSorted, 0, 5);

			// act
			var isDead = objectUnderTest.IsHandDead(shortSorted, middleSorted, topSorted, shortPower, middlePower, topPower);

			// assert
			Assert.IsFalse(isDead);
		}

		[TestMethod]
		public void TestIsHandDeadWhenDeadByLastCardKicker()
		{
			// assign
			var shortHand = InputReader.ReadInput("Qh Jd Ts");
			var shortSorted = new byte[3];
			var middleHand = InputReader.ReadInput("Qs Jc Td Kc 3s");
			var middleSorted = new byte[5];
			var topHand = InputReader.ReadInput("Qd Js Th Ks 2s");
			var topSorted = new byte[5];

			var objectUnderTest = new LastTurnInPosition();
			LineValue shortPower = objectUnderTest.SortLine(shortHand, shortSorted, 0, 3);
			LineValue middlePower = objectUnderTest.SortLine(middleHand, middleSorted, 0, 5);
			LineValue topPower = objectUnderTest.SortLine(topHand, topSorted, 0, 5);

			// act
			var isDead = objectUnderTest.IsHandDead(shortSorted, middleSorted, topSorted, shortPower, middlePower, topPower);

			// assert
			Assert.IsTrue(isDead);
		}

		[TestMethod]
		public void TestIsHandDeadWhenLiveByEqual()
		{
			// assign
			var shortHand = InputReader.ReadInput("Qh Jd Ts");
			var shortSorted = new byte[3];
			var middleHand = InputReader.ReadInput("Qs Jc Td 4c 3s");
			var middleSorted = new byte[5];
			var topHand = InputReader.ReadInput("Qd Js Th 4s 3c");
			var topSorted = new byte[5];

			var objectUnderTest = new LastTurnInPosition();
			LineValue shortPower = objectUnderTest.SortLine(shortHand, shortSorted, 0, 3);
			LineValue middlePower = objectUnderTest.SortLine(middleHand, middleSorted, 0, 5);
			LineValue topPower = objectUnderTest.SortLine(topHand, topSorted, 0, 5);

			// act
			var isDead = objectUnderTest.IsHandDead(shortSorted, middleSorted, topSorted, shortPower, middlePower, topPower);

			// assert
			Assert.IsFalse(isDead);
		}

		[TestMethod]
		public void TestIsHandDeadWhenDeadByHighCardKicker()
		{
			// assign
			var shortHand = InputReader.ReadInput("Qh Ad Jd");
			var shortSorted = new byte[3];
			var middleHand = InputReader.ReadInput("Qs 7c Jd Tc 9s");
			var middleSorted = new byte[5];
			var topHand = InputReader.ReadInput("Kd Kc 6s 2s 3c");
			var topSorted = new byte[5];

			var objectUnderTest = new LastTurnInPosition();
			LineValue shortPower = objectUnderTest.SortLine(shortHand, shortSorted, 0, 3);
			LineValue middlePower = objectUnderTest.SortLine(middleHand, middleSorted, 0, 5);
			LineValue topPower = objectUnderTest.SortLine(topHand, topSorted, 0, 5);

			// act
			var isDead = objectUnderTest.IsHandDead(shortSorted, middleSorted, topSorted, shortPower, middlePower, topPower);

			// assert
			Assert.IsTrue(isDead);
		}

		[TestMethod]
		public void TestCalculateBonusShortSet()
		{
			// assign
			var data = InputReader.ReadInput("Ad Ah Ac");
			var result = new byte[3];

			// act
			var objectUnderTest = new LastTurnInPosition();
			LineValue power = objectUnderTest.SortLine(data, result, 0, 3);
			decimal score = objectUnderTest.CalculateBonusShort(result, power);

			// assert
			Assert.AreEqual(24.5M, score);
		}

		[TestMethod]
		public void TestCalculateBonusShortPairWithFantazy()
		{
			// assign
			var data = InputReader.ReadInput("Qd Qh Ac");
			var result = new byte[3];

			// act
			var objectUnderTest = new LastTurnInPosition();
			LineValue power = objectUnderTest.SortLine(data, result, 0, 3);
			decimal score = objectUnderTest.CalculateBonusShort(result, power);

			// assert
			Assert.AreEqual(9.5M, score);
		}

		[TestMethod]
		public void TestCalculateBonusShortPairNoFantazy()
		{
			// assign
			var data = InputReader.ReadInput("7d 7h Ac");
			var result = new byte[3];

			// act
			var objectUnderTest = new LastTurnInPosition();
			LineValue power = objectUnderTest.SortLine(data, result, 0, 3);
			decimal score = objectUnderTest.CalculateBonusShort(result, power);

			// assert
			Assert.AreEqual(2, score);
		}

		[TestMethod]
		public void TestCalculateBonusShortHighCard()
		{
			// assign
			var data = InputReader.ReadInput("2d 7h Ac");
			var result = new byte[3];

			// act
			var objectUnderTest = new LastTurnInPosition();
			LineValue power = objectUnderTest.SortLine(data, result, 0, 3);
			decimal score = objectUnderTest.CalculateBonusShort(result, power);

			// assert
			Assert.AreEqual(0, score);
		}

		[TestMethod]
		public void TestSortLineShortSet()
		{
			// assign
			var data = InputReader.ReadInput("Ad Ah Ac");
			var result = new byte[3];

			// act
			var objectUnderTest = new LastTurnInPosition();
			LineValue power = objectUnderTest.SortLine(data, result, 0, 3);

			// assert
			Assert.AreEqual(LineValue.Set, power);
			Assert.AreEqual(13, result[0]);
			Assert.AreEqual(13, result[1]);
			Assert.AreEqual(13, result[2]);
		}

		[TestMethod]
		public void TestSortLineShortPair()
		{
			// assign
			var data = InputReader.ReadInput("Qd Qh Ac");
			var result = new byte[3];

			// act
			var objectUnderTest = new LastTurnInPosition();
			LineValue power = objectUnderTest.SortLine(data, result, 0, 3);

			// assert
			Assert.AreEqual(LineValue.Pair, power);
			Assert.AreEqual(11, result[0]);
			Assert.AreEqual(11, result[1]);
			Assert.AreEqual(13, result[2]);
		}

		[TestMethod]
		public void TestSortLineShortHighCard()
		{
			// assign
			var data = InputReader.ReadInput("Jd Qh Ac");
			var result = new byte[3];

			// act
			var objectUnderTest = new LastTurnInPosition();
			LineValue power = objectUnderTest.SortLine(data, result, 0, 3);

			// assert
			Assert.AreEqual(LineValue.HighCard, power);
			Assert.AreEqual(13, result[0]);
			Assert.AreEqual(11, result[1]);
			Assert.AreEqual(10, result[2]);
		}

		[TestMethod]
		public void TestSortLineRoyal()
		{
			// assign
			var data = InputReader.ReadInput("2s Qd Td Jd Kd Ad 3s");
			var result = new byte[5];

			// act
			var objectUnderTest = new LastTurnInPosition();
			LineValue power = objectUnderTest.SortLine(data, result, 1, 5);

			// assert
			Assert.AreEqual(LineValue.Royal, power);
			Assert.AreEqual(13, result[0]);
			Assert.AreEqual(12, result[1]);
			Assert.AreEqual(11, result[2]);
			Assert.AreEqual(10, result[3]);
			Assert.AreEqual(9, result[4]);
		}

		[TestMethod]
		public void TestSortLineStraightFlush()
		{
			// assign
			var data = InputReader.ReadInput("2s 2d 3d 4d 5d Ad 3s");
			var result = new byte[5];

			// act
			var objectUnderTest = new LastTurnInPosition();
			LineValue power = objectUnderTest.SortLine(data, result, 1, 5);

			// assert
			Assert.AreEqual(LineValue.StraightFlash, power);
			Assert.AreEqual(4, result[0]);
			Assert.AreEqual(3, result[1]);
			Assert.AreEqual(2, result[2]);
			Assert.AreEqual(1, result[3]);
			Assert.AreEqual(13, result[4]);
		}

		[TestMethod]
		public void TestSortLineFlash()
		{
			// assign
			var data = InputReader.ReadInput("2s 3h Qd 2d Jd Kd Ad 3s");
			var result = new byte[5];

			// act
			var objectUnderTest = new LastTurnInPosition();
			LineValue power = objectUnderTest.SortLine(data, result, 2, 5);

			// assert
			Assert.AreEqual(LineValue.Flash, power);
			Assert.AreEqual(13, result[0]);
			Assert.AreEqual(12, result[1]);
			Assert.AreEqual(11, result[2]);
			Assert.AreEqual(10, result[3]);
			Assert.AreEqual(1, result[4]);
		}

		[TestMethod]
		public void TestSortLineCare()
		{
			// assign
			var data = InputReader.ReadInput("2s 2d 3c 2h 2c 3s");
			var result = new byte[5];

			// act
			var objectUnderTest = new LastTurnInPosition();
			LineValue power = objectUnderTest.SortLine(data, result, 0, 5);

			// assert
			Assert.AreEqual(LineValue.Care, power);
			Assert.AreEqual(1, result[0]);
			Assert.AreEqual(1, result[1]);
			Assert.AreEqual(1, result[2]);
			Assert.AreEqual(1, result[3]);
			Assert.AreEqual(2, result[4]);
		}

		[TestMethod]
		public void TestSortLineFullHouse()
		{
			// assign
			var data = InputReader.ReadInput("5h 6d 2s 2d 3c 2c 3s");
			var result = new byte[5];

			// act
			var objectUnderTest = new LastTurnInPosition();
			LineValue power = objectUnderTest.SortLine(data, result, 2, 5);

			// assert
			Assert.AreEqual(LineValue.FullHouse, power);
			Assert.AreEqual(1, result[0]);
			Assert.AreEqual(1, result[1]);
			Assert.AreEqual(1, result[2]);
			Assert.AreEqual(2, result[3]);
			Assert.AreEqual(2, result[4]);
		}

		[TestMethod]
		public void TestSortLineSet()
		{
			// assign
			var data = InputReader.ReadInput("5h 6d 2s 2d Ac 2c 3s");
			var result = new byte[5];

			// act
			var objectUnderTest = new LastTurnInPosition();
			LineValue power = objectUnderTest.SortLine(data, result, 2, 5);

			// assert
			Assert.AreEqual(LineValue.Set, power);
			Assert.AreEqual(1, result[0]);
			Assert.AreEqual(1, result[1]);
			Assert.AreEqual(1, result[2]);
			Assert.AreEqual(13, result[3]);
			Assert.AreEqual(2, result[4]);
		}

		[TestMethod]
		public void TestSortLineStraight()
		{
			// assign
			var data = InputReader.ReadInput("5h 6d 4s 3d 2c As 3s");
			var result = new byte[5];

			// act
			var objectUnderTest = new LastTurnInPosition();
			LineValue power = objectUnderTest.SortLine(data, result, 0, 5);

			// assert
			Assert.AreEqual(LineValue.Straight, power);
			Assert.AreEqual(5, result[0]);
			Assert.AreEqual(4, result[1]);
			Assert.AreEqual(3, result[2]);
			Assert.AreEqual(2, result[3]);
			Assert.AreEqual(1, result[4]);
		}

		[TestMethod]
		public void TestSortLineTwoPairs()
		{
			// assign
			var data = InputReader.ReadInput("5h 5d As Ac 3d");
			var result = new byte[5];

			// act
			var objectUnderTest = new LastTurnInPosition();
			LineValue power = objectUnderTest.SortLine(data, result, 0, 5);

			// assert
			Assert.AreEqual(LineValue.TwoPair, power);
			Assert.AreEqual(13, result[0]);
			Assert.AreEqual(13, result[1]);
			Assert.AreEqual(4, result[2]);
			Assert.AreEqual(4, result[3]);
			Assert.AreEqual(2, result[4]);
		}

		[TestMethod]
		public void TestSortLinePair()
		{
			// assign
			var data = InputReader.ReadInput("5h 6d As Ac 3d");
			var result = new byte[5];

			// act
			var objectUnderTest = new LastTurnInPosition();
			LineValue power = objectUnderTest.SortLine(data, result, 0, 5);

			// assert
			Assert.AreEqual(LineValue.Pair, power);
			Assert.AreEqual(13, result[0]);
			Assert.AreEqual(13, result[1]);
			Assert.AreEqual(5, result[2]);
			Assert.AreEqual(4, result[3]);
			Assert.AreEqual(2, result[4]);
		}

		[TestMethod]
		public void TestSortLineHighCard()
		{
			// assign
			var data = InputReader.ReadInput("5h 6d Ks Ac 3d");
			var result = new byte[5];

			// act
			var objectUnderTest = new LastTurnInPosition();
			LineValue power = objectUnderTest.SortLine(data, result, 0, 5);

			// assert
			Assert.AreEqual(LineValue.HighCard, power);
			Assert.AreEqual(13, result[0]);
			Assert.AreEqual(12, result[1]);
			Assert.AreEqual(5, result[2]);
			Assert.AreEqual(4, result[3]);
			Assert.AreEqual(2, result[4]);
        }

        #endregion
    }
}
