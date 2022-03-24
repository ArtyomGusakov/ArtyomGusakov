using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GOL_OOP
{
	public class Program
	{

		public static int FieldRow = 8; //Row
		public static int FieldColumn = 8; //Column
		public static int Time = 0; //Generation counter
		public static int i = 0;//Row counter
		public static int j = 0;//Column counter
		public static int UserAnswer = 0;//User interface question
		public static int Generations = 5;//Max number of Generations
		public static int HowLong = 0;//Used to read loading file
		public static string PrintingReturn = "";

		
		public static int HowMuchAlive = 0;

		public char SaveIt = 'N';

		static CellClass[,] PlayField = new CellClass[FieldRow, FieldColumn];



		public static void Main(string[] args)
		{
			Console.WriteLine("1 - New game");
			Console.WriteLine("2 - Load game");
			UserAnswer = int.Parse(Console.ReadLine());

			if (UserAnswer == 1)//If player choose New Game
			{
				Console.WriteLine("Choose Field size (Minimal 8x8)");

				Console.Write("Choose number of Rows: ");
				FieldRow = int.Parse(Console.ReadLine());

				Console.Write("Choose number of Columns: ");
				FieldColumn = int.Parse(Console.ReadLine());

				Console.Write("Choose number of Generations: ");
				Generations = int.Parse(Console.ReadLine());

				Console.WriteLine("Game fiels size:" + FieldRow + "x" + FieldColumn);

				PlayField = new CellClass[FieldRow, FieldColumn];

				Console.WriteLine("Printing your array...");

				GenerateCells();
				PrintField();

			}

			else if (UserAnswer == 2)//If player choose Load Game
			{
				Console.WriteLine("Reading save file...");
				string[] lines = File.ReadAllLines("D:\\ReadThisFile.txt");
				

				try
				{
                    foreach (string LoadGameString in System.IO.File.ReadLines("D:\\ReadThisFile.txt"))//Read file and move it in one long string.
					{
						System.Console.WriteLine(LoadGameString);
						HowLong++;
					}
				}

				catch
				{
					throw new InvalidDataException("Save file cant be loaded");
				}

				FieldRow = Int32.Parse(lines[0]);//Read line 1 for Row size
				FieldColumn = Int32.Parse(lines[1]);//Read line 2 for Column size
				PlayField = new CellClass[FieldRow, FieldColumn];

				GenerateField();

				if (HowLong > 2)//if save file has more then 2 rows, it means it has living cells
				{
					int temp = 2;

					while (temp < HowLong)
					{
						string temp2 = lines[temp];
						char i1 = temp2[0];
						char j1 = temp2[2];
						int i2 = (int)Char.GetNumericValue(i1);
						int j2 = (int)Char.GetNumericValue(j1);

						PlayField[i2, j2].LiveStatus(true);
						CalculateNearCells();

						temp++;
					}
				}
				else
				{
				 throw new InvalidDataException("Save file has no living cells");
				}

				Console.WriteLine("Save file loaded");
				Console.WriteLine("Choose number of Generations: ");
				Generations = int.Parse(Console.ReadLine());
			}


			else //Start game error catch
			{
				throw new InvalidDataException("Wrong user input");
			}

			do
			{

				while (Time <= Generations)
				{
					Console.Clear();
					if (Console.KeyAvailable)//Pause menu
					{
						Console.WriteLine("Game was paused");
						Console.WriteLine("1 - Unpause");
						Console.WriteLine("2 - Save game");
						Console.WriteLine("3 - Exit");
						UserAnswer = int.Parse(Console.ReadLine());

						if (UserAnswer == 1)
						{
							Console.WriteLine("Game unpaused");
						}

						else if (UserAnswer == 2)
						{
							Console.WriteLine("Game being saved...");

							SaveGame();

							Console.Clear();
						}

						else if (UserAnswer == 3)
						{
							Console.WriteLine("Game closing");
							Environment.Exit(0);
						}

						else
						{
							throw new InvalidDataException("Wrong user input");
						}

					}
				
					//Main part 
					Console.WriteLine("Generation: " + Time);//Output current generation
					
					
					PrintField();//Output game field
					
					Console.WriteLine("Number of living cells: " + GetNumberOfLivingCells());

					Console.WriteLine("Printing String of field:"+ "\n" + PrintField());
					

					CalculateNearCells();//Each cell get number of living cells near self (from 0 to 8)

					

					GetNumberOfLivingCells();//Calculate number of living cells in game

					

					GetNextGeneration();//Calculate next generation 

			
					//Console.WriteLine("Number of living cells with INT: " + HowMuchAlive);
					HowMuchAlive = 0;
					System.Threading.Thread.Sleep(1000);//1 second delay
                    Time++;
				}
			}
			while (Console.ReadKey(true).Key != ConsoleKey.Escape);

		}//Main ends

		public static void GenerateCells()//New Game generation
		{
			for (int i = 0; i < FieldRow; i++)
			{
				for (int j = 0; j < FieldColumn; j++)
				{

					if (i == 2 && j == 1 || i == 0 && j == 2 || i == 6 && j == 0 || i == 7 && j == 0 || i == 2 && j == 5 || i == 5 && j == 5 || i == 7 && j == 7 || i == 7 && j == 6 || i == 6 && j == 6 || i == 5 && j == 6 || i == 4 && j == 7)//On paper simulation positions
					{
						PlayField[i, j] = new CellClass(true);

					}

					else
					{
						PlayField[i, j] = new CellClass(false);
					}
				}
			}
		}//GenerateCells ends

		public static string GenerateField()//Create and fill Game Field with Cells
		{
			string field = "";

			for (int i = 0; i < FieldRow; i++)
			{
				for (int j = 0; j < FieldColumn; j++)
				{
                 
					PlayField[i, j] = new CellClass(false);
					field = field + PlayField[i, j].CellVisual;
				}
			}
			return field;
		}//GenerateCell


		public static string PrintField()//Show game field
		{
			PrintingReturn = "";

			for (int i = 0; i < FieldRow; i++)
			{
				for (int j = 0; j < FieldColumn; j++)
				{
                
					//Console.Write(PlayField[i, j].GetCellVisual());
					PrintingReturn = PrintingReturn + PlayField[i, j].GetCellVisual();

				}

				//Console.WriteLine("");
				PrintingReturn = PrintingReturn + $"{Environment.NewLine}";
			}
			return PrintingReturn;
		}//PrintField ends

		public static void GetNextGeneration()
		{
			for (i = 0; i < FieldRow; i++)
			{
				for (j = 0; j < FieldColumn; j++)
				{
					PlayField[i, j].NextGeneration();
			        PlayField[i, j].NearCells = 0;
				}
			}
		}

		public static int GetNumberOfLivingCells()
		{
			HowMuchAlive = 0;
			for (i = 0; i < FieldRow; i++)
			{
				for (j = 0; j < FieldColumn; j++)
				{
					if (PlayField[i, j].Alive == true)
					{
						HowMuchAlive++;
						//Console.Write(HowMuchAlive);
					}
					
				}
				//Console.WriteLine("");
			}
			return HowMuchAlive;
		}

		public static void CalculateNearCells()//Count living cells around current cell
		{
			int i = 0;
			int j = 0;
			int i1 = 0;
			int j1 = 0;

			for ( i = 0; i < FieldRow; i++) //Calculate how many Alive Cells near each Cell
			{
				for ( j = 0; j < FieldColumn; j++)
				{
					
					PlayField[i, j].NearCells = 0;

					for  (i1 = i - 1; i1 <= i + 1; i1++)//Get info 1 cell around
					{
						if (i1 >= 0 && i1 < FieldRow) //Horisontal check
						{
							for ( j1 = j - 1; j1 <= j + 1; j1++)
							{
								if (j1 >= 0 && j1 < FieldColumn)//Vertical check
								{
									
									if (PlayField[i1, j1].Alive == true)//If all good get +1 living near cell
									{
									  
										PlayField[i, j].NearCells++;
									}
								}

							}
						}
					}

					if (PlayField[i, j].Alive == true)//Lazy way to get -1 from near living cell, because it count self too.
					{
						PlayField[i, j].NearCells--;
					}

					if (PlayField[i, j].NearCells < 0)//Because function above, dead cell counter can go -1, so it gets 0.
					{
						PlayField[i, j].NearCells = 0;
					}
					
					
				}
			}

        }//CalculateNearCells ends


		public static string SaveGame()
		{
			string AllGood = "File saved";
			try
			{
				using (StreamWriter writer = new StreamWriter("D:\\SaveGOL.txt"))
				{
					writer.WriteLine(FieldRow);
					writer.WriteLine(FieldColumn);
					for (i = 0; i < FieldRow; i++)

						for (j = 0; j < FieldColumn; j++)
						{
							if (PlayField[i, j].Alive == true)
							{
							writer.WriteLine(i + "x" + j);
							}
                        }
					return AllGood;
				}
			}

			catch
			{
				throw new InvalidDataException("Save file cant be created");
			}
		}//SaveGame ends
	}
	
}