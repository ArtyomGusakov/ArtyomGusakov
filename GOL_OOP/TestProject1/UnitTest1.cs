using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GOL_OOP
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		[ExpectedException(typeof(InvalidDataException))]
		public void WrongGameStartInput()
		{
            var UserAnswer = new StringReader("3");
			Console.SetIn(UserAnswer);
			Program.Main(new string[] { });
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidDataException))]
		public void BadSaveFile()//Works only if Save File has problem
		{
			var UserAnswer = new StringReader("2");
			Console.SetIn(UserAnswer);
			Program.Main(new string[] { });
		}

		[TestMethod]
		public void LoadFilePrint()//Print 0 Generation Load File
		{
			var UserAnswer = new StringReader("2");


			string ExpectedOutput =
				$"░░███░░░░░░{Environment.NewLine}" +
				$"░████░░░░░░{Environment.NewLine}" +
				$"░░░░████░░░{Environment.NewLine}" +
				$"░░░░░░░░░░░{Environment.NewLine}" +
				$"░░░░░░░░░░░{Environment.NewLine}" +
				$"░░░░░░░░░░░{Environment.NewLine}" +
				$"░░░░░░░░░░░{Environment.NewLine}" +
				$"░░░░░░░░░░░{Environment.NewLine}" +
				$"░░░░░░░░░░░{Environment.NewLine}" +
				$"░░░░░░░░░░░{Environment.NewLine}" +
				$"░░░░░░░░░░░{Environment.NewLine}";

         	string Output = Program.PrintField();
			Assert.AreEqual(ExpectedOutput, Output);
			
		}

		[TestMethod]
		public void LoadFilePrintFail()//Never should be green
		{
			var UserAnswer = new StringReader("2");


			string ExpectedOutput =
				$"░░░░░░░░░░░{Environment.NewLine}" +
				$"░████░░░░░░{Environment.NewLine}" +
				$"░░░░████░░░{Environment.NewLine}" +
				$"░░░░░░░░░░░{Environment.NewLine}" +
				$"░░░░░░░░░░░{Environment.NewLine}" +
				$"░░░░░░░░░░░{Environment.NewLine}" +
				$"░░░░░░░░░░░{Environment.NewLine}" +
				$"░░░░░░░░░░░{Environment.NewLine}" +
				$"░░░░░░░░░░░{Environment.NewLine}" +
				$"░░░░░░░░░░░{Environment.NewLine}" +
				$"░░░░░░░░░░░{Environment.NewLine}";

			string Output = Program.PrintField();
			Assert.AreEqual(ExpectedOutput, Output);

		}

		
		[TestMethod]
		public void Generation2LivingCellCount()//Generation 2 should have 9 living cells on loading game
		{
			var UserAnswer = new StringReader("2");
			int ExpectedLivingCells = 9;

			if (Program.Generations == 2)
			{
				Assert.AreEqual(ExpectedLivingCells, Program.HowMuchAlive);
			}
		}

		/*
		[TestMethod]
		public void LoadFileGeneration8GameEnds()//
		{

		}
		*/
	}
}