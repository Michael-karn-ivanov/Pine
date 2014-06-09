using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pineapple;

namespace PineHome.Tests
{
    /// <summary>
    /// Summary description for LastTurnInPositionCaseTest
    /// </summary>
    [TestClass]
    public class LastTurnInPositionCaseTest
    {
        // max points без учета руки оппонента
        [TestMethod]
        public void TestAgainstAlmostEmptyHand()
        {
            // assign
            var heroHand = InputReader.ReadInput("9h 9s ? Th Ts Td 6s ? Jh Js Jd Qh Qs");
            var villainHand = InputReader.ReadInput("2h 3s 4d 2s 3h 4c 5c 7d 2d 3d 4s 5d 8s");
            var triple = InputReader.ReadInput("9d 6d Ad");

            int outFirstIdx;
            int outSecondIdx;

            // act
            var objectUnderTest = new LastTurnInPosition();
            var score = objectUnderTest.Work(villainHand, heroHand, triple, out outFirstIdx, out outSecondIdx);

            // assert
            Assert.AreEqual(43.5M, score);
            Assert.AreEqual(0, outFirstIdx);
            Assert.AreEqual(1, outSecondIdx);
        }

        // max points без учета руки оппонента, сравнение бонусов
        [TestMethod]
        public void TestBonusCalcAgainstEmpty()
        {
            // assign
            var heroHand = InputReader.ReadInput("Qh 9s 6h Ts Qs As 6s ? Jh Jc Jd Kh ?");
            var villainHand = InputReader.ReadInput("2h 3s 4d 2s 3h 4c 5c 7d 2d 3d 4s 5d 8s");
            var triple = InputReader.ReadInput("Js Tc Td");

            int outFirstIdx;
            int outSecondIdx;

            // act
            var objectUnderTest = new LastTurnInPosition();
            var score = objectUnderTest.Work(villainHand, heroHand, triple, out outFirstIdx, out outSecondIdx);

            // assert
            Assert.AreEqual(16M, score);
            Assert.AreEqual(0, outSecondIdx);
        }

        // max points без учета руки оппонента, сравнение бонусов
        [TestMethod]
        public void TestBonusCalcAgainstEmpty2()
        {
            // assign
            var heroHand = InputReader.ReadInput("Qh 9s 6h Ts Qs As 6s ? Jh Jc Jd Kh ?");
            var villainHand = InputReader.ReadInput("2h 3s 4d 2s 3h 4c 5c 7d 2d 3d 4s 5d 8s");
            var triple = InputReader.ReadInput("Ks Tc Td");

            int outFirstIdx;
            int outSecondIdx;

            // act
            var objectUnderTest = new LastTurnInPosition();
            var score = objectUnderTest.Work(villainHand, heroHand, triple, out outFirstIdx, out outSecondIdx);

            // assert
            Assert.AreEqual(14M, score);
            Assert.AreEqual(0, outFirstIdx);
        }

        // max points без учета руки оппонента с учетом фантазии
        [TestMethod]
        public void TestAgainstEmptyWithFantazy()
        {
            // assign
            var heroHand = InputReader.ReadInput("Qh 9s ? Ts Qs As 6s ? Jh Js Jd Kh Ks");
            var villainHand = InputReader.ReadInput("2h 3s 4d 2s 3h 4c 5c 7d 2d 3d 4s 5d 8s");
            var triple = InputReader.ReadInput("Qs Tc Td");

            int outFirstIdx;
            int outSecondIdx;

            // act
            var objectUnderTest = new LastTurnInPosition();
            var score = objectUnderTest.Work(villainHand, heroHand, triple, out outFirstIdx, out outSecondIdx);

            // assert
            Assert.AreEqual(21.5M, score);
            Assert.AreEqual(0, outFirstIdx);
        }

        // max points без учета руки оппонента с учетом мертвости нашей руки
        [TestMethod]
        public void TestAgainstEmptyToStayAlive()
        {
            // assign
            var heroHand = InputReader.ReadInput("9h 9s ? Th Ts Kd 6s ? Jh Js Jd Qh Qs");
            var villainHand = InputReader.ReadInput("2h 3s 4d 2s 3h 4c 5c 7d 2d 3d 4s 5d 8s");
            var triple = InputReader.ReadInput("9d 6d Ad");

            int outFirstIdx;
            int outSecondIdx;

            // act
            var objectUnderTest = new LastTurnInPosition();
            var score = objectUnderTest.Work(villainHand, heroHand, triple, out outFirstIdx, out outSecondIdx);

            // assert
            Assert.AreEqual(16M, score);
            Assert.AreNotEqual(0, outFirstIdx);
        }

        // бонусы vs. победа в одной линии
        [TestMethod]
        public void TestBonusAgainstOneLineWin()
        {
            // assign
            var heroHand = InputReader.ReadInput("2h 4d ? Jh 8d Td Qs Ks 4h 5h 6h 7h ?");
            var villainHand = InputReader.ReadInput("2h 3s Jd Qd Qc 4c 5c 7d 3h 3d 3c 5d 5s");
            var triple = InputReader.ReadInput("Ah 6c 7c");

            int outFirstIdx;
            int outSecondIdx;

            // act
            var objectUnderTest = new LastTurnInPosition();
            var score = objectUnderTest.Work(villainHand, heroHand, triple, out outFirstIdx, out outSecondIdx);

            // assert
            Assert.AreEqual(-7, score);
            Assert.AreEqual(0, outFirstIdx);
        }

        // бонусы vs. победа в трех линиях
        [TestMethod]
        public void TestBonusAgainstThreeLineWin()
        {
            // assign
            var heroHand = InputReader.ReadInput("Qh 9s 6h Ts Qs As 6s ? Jh Jc Jd Kh ?");
            var villainHand = InputReader.ReadInput("2h 3s Jd Qd Qc 4c 5c 7d 3h 3d 3c 5d 8s");
            var triple = InputReader.ReadInput("Js Tc Td");

            int outFirstIdx;
            int outSecondIdx;

            // act
            var objectUnderTest = new LastTurnInPosition();
            var score = objectUnderTest.Work(villainHand, heroHand, triple, out outFirstIdx, out outSecondIdx);

            // assert
            Assert.AreEqual(14, score);
            Assert.AreEqual(0, outFirstIdx);
        }

        // бонусы vs. победа в трех линиях
        [TestMethod]
        public void TestBonusAgainstThreeLineWin2()
        {
            // assign
            var heroHand = InputReader.ReadInput("Qh 9s 6h Ts Qs As 6s ? Jh Jc Jd Kh ?");
            var villainHand = InputReader.ReadInput("2h 3s 4s 2s 3h 4c 5c 7d 2d 3d 4d 5d 8d");
            var triple = InputReader.ReadInput("Ks Tc Td");

            int outFirstIdx;
            int outSecondIdx;

            // act
            var objectUnderTest = new LastTurnInPosition();
            var score = objectUnderTest.Work(villainHand, heroHand, triple, out outFirstIdx, out outSecondIdx);

            // assert
            Assert.AreEqual(12M, score);
            Assert.AreEqual(0, outSecondIdx);
        }

        // учет мертвости руки оппонента
        [TestMethod]
        public void TestAgainstDead()
        {
            // assign
            var heroHand = InputReader.ReadInput("Ah Ad 6h Ts Qs As 6s ? Jh Jc Jd Kh ?");
            var villainHand = InputReader.ReadInput("Ks Kc 4d Qd Qc 4c 5c 7d 3h 3d 3c 5d 8s");
            var triple = InputReader.ReadInput("Js Ac Td");

            int outFirstIdx;
            int outSecondIdx;

            // act
            var objectUnderTest = new LastTurnInPosition();
            var score = objectUnderTest.Work(villainHand, heroHand, triple, out outFirstIdx, out outSecondIdx);

            // assert
            Assert.AreEqual(16M, score);
            Assert.AreEqual(1, outFirstIdx);
            Assert.AreEqual(0, outSecondIdx);
        }
    }
}
