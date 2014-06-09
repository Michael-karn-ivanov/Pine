using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pineapple.UnitTest
{
	[TestClass]
	public class InputReaderTest
	{
		[TestMethod]
		public void TestMulti()
		{
			byte[] input = InputReader.ReadInput("Ad Qh ? Qd");
			Assert.AreEqual(4, input.Length);
			Assert.AreEqual(39, input[0]);
			Assert.AreEqual(50, input[1]);
			Assert.AreEqual(0, input[2]);
			Assert.AreEqual(37, input[3]);
		}

		[TestMethod]
		public void TestReadSingle2d()
		{
			Assert.AreEqual(27, InputReader.ReadSingle("2d"));
		}

		[TestMethod]
		public void TestReadSingle3h()
		{
			Assert.AreEqual(41, InputReader.ReadSingle("3h"));
		}

		[TestMethod]
		public void TestReadSingle4s()
		{
			Assert.AreEqual(3, InputReader.ReadSingle("4s"));
		}

		[TestMethod]
		public void TestReadSingle5c()
		{
			Assert.AreEqual(17, InputReader.ReadSingle("5c"));
		}

		[TestMethod]
		public void TestReadSingle6d()
		{
			Assert.AreEqual(31, InputReader.ReadSingle("6d"));
		}

		[TestMethod]
		public void TestReadSingle7h()
		{
			Assert.AreEqual(45, InputReader.ReadSingle("7h"));
		}

		[TestMethod]
		public void TestReadSingle8s()
		{
			Assert.AreEqual(7, InputReader.ReadSingle("8S"));
		}

		[TestMethod]
		public void TestReadSingle9c()
		{
			Assert.AreEqual(21, InputReader.ReadSingle("9c"));
		}

		[TestMethod]
		public void TestReadSingleTd()
		{
			Assert.AreEqual(35, InputReader.ReadSingle("Td"));
		}

		[TestMethod]
		public void TestReadSingleJh()
		{
			Assert.AreEqual(49, InputReader.ReadSingle("Jh"));
		}

		[TestMethod]
		public void TestReadSingleQs()
		{
			Assert.AreEqual(11, InputReader.ReadSingle("QS"));
		}

		[TestMethod]
		public void TestReadSingleKc()
		{
			Assert.AreEqual(25, InputReader.ReadSingle("Kc"));
		}

		[TestMethod]
		public void TestReadSingleAd()
		{
			Assert.AreEqual(39, InputReader.ReadSingle("Ad"));
		}
	}
}
