using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace GameAI2
{
	public class Program
	{
		public static int FieldSize1 = 20;//Row
		public static int FieldSize2 = 20;//Column
		public static string FieldOutput = "";
		public static bool paused = false;
		public static int Timer = 0;
		public static int LionCount = 0;
		public static int AntelopeCount = 0;
		public static int PluginFound = 0;

		public enum UserInput { }//Unused

		public static GameField[,] GameFieldList = new GameField[FieldSize1, FieldSize2];

		public static List<Predator> PredatorList = new List<Predator>
		{
			new Lion(0,0)
			,new Lion (0,6)
		};

		public static List<Herbivore> HerbivoreList = new List<Herbivore>
		{
		   new Antelope(10,10)
		   ,new Antelope(10,17)
		};

		static List <AnimalLibrary> GamePluginList = null;

		public static void Main(string[] args)
		{
			GamePluginList = ReadGamePlugins();

			foreach (var GamePlugin in GamePluginList)
			{
				Console.WriteLine($"{GamePlugin.Visual} | {GamePlugin.AnimalRole}");
			}
			/*
			foreach (var GamePlugin in GamePluginList)
			{
				GamePlugin.ThinkNextMove();
			}
			*/
				GenerateField();

			//Console.WriteLine("pre while stage");
			do//DO WHILE user press key
			{
				while (Timer < 10)
				{
					//Console.WriteLine("While..."); 
					if (Console.KeyAvailable)//Main process input 
					{
						int temp = 1;
						while (temp == 1)
						{
							if (Console.ReadKey(true).Key == ConsoleKey.L)
							{
								Console.WriteLine("You pressed L button");
								AddLion();
								LionCount++;
								temp++;
							}

							if (Console.ReadKey(true).Key == ConsoleKey.A)
							{
								Console.WriteLine("You pressed A button");
								AddAntelope();
								AntelopeCount++;
								temp++;
							}

							else if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
							{
								Console.WriteLine("You pressed Spacebar button");

								temp++;
							}
							break;
						}
					}//Main process input ends

					
					PredatorTurn();
					HerbivoreTurn();
					HealthCheck();
					BirthProcessTimer();
					RenewField();
					Console.WriteLine(PrintField());
					System.Threading.Thread.Sleep(1000);
				}//Main process ends

			} while (Console.ReadKey(true).Key != ConsoleKey.Spacebar || Console.ReadKey(true).Key != ConsoleKey.L || Console.ReadKey(true).Key != ConsoleKey.A);
		}//Main ends

		public static void GenerateField()//Generate game field
		{

			for (int i = 0; i < FieldSize1; i++)
			{
				for (int j = 0; j < FieldSize2; j++)
				{
					GameFieldList[i, j] = new GameField();
				}
			}
		}//GenerateField ends

		public static string PrintField()//Show game field
		{
			FieldOutput = "";

			for (int i = 0; i < FieldSize1; i++)
			{
				for (int j = 0; j < FieldSize1; j++)
				{
					FieldOutput = FieldOutput + GameFieldList[i, j].GameFieldVisual();
				}
				FieldOutput = FieldOutput + $"{Environment.NewLine}";
			}
			return FieldOutput;
		}//PrintField ends

		public static void Hunt(int x1, int y1)
		{
			foreach (var HerbivoreTemp in HerbivoreList)
			{
				if (HerbivoreTemp.x == x1 && HerbivoreTemp.y == y1)
				{
					HerbivoreList.Remove(HerbivoreTemp);
					Console.WriteLine("Antelope found!");
					return;
				}
			}
		}//Hunt ends
		public static void PredatorTurn()
		{
			foreach (var Predator2 in PredatorList)
			{
				Predator2.ThinkNextMove(GameFieldList);
			}
		}//PredatorTurn ends

		public static void HerbivoreTurn()
		{
			foreach (var Herbivore2 in HerbivoreList)
			{
				Herbivore2.ThinkNextMove(GameFieldList);
			}
		}//PredatorTurn ends

		public static void RenewField()
		{
			char empty = '░';
			char lion = 'L';
			char antelope = 'A';
			string AddPredator = "Predator";
			string AddHerbivore = "Herbivore";
			string AddEmpty = "Empty";
			int temp = 0;


			for (int i = 0; i < FieldSize1; i++)
			{
				for (int j = 0; j < FieldSize2; j++)
				{
					GameFieldList[i, j].RefreshField(empty, AddEmpty);
				}
			}

			for (var i = 0; i < PredatorList.Count; i++)
			{
				temp = PredatorList[i].x;
				GameFieldList[PredatorList[i].x, PredatorList[i].y].RefreshField(lion, AddPredator);
			}

			for (var i = 0; i < HerbivoreList.Count; i++)
			{
				temp = HerbivoreList[i].x;
				GameFieldList[HerbivoreList[i].x, HerbivoreList[i].y].RefreshField(antelope, AddHerbivore);
			}

		}//RefreshField ends

		public static void FastRenewField(int x, int y, int x1, int y1, char C1, string AnimalRole2)
		{
			char empty = '░';
			string AddEmpty = "Empty";

			GameFieldList[x, y].RefreshField(empty, AddEmpty);
			GameFieldList[x1, y1].RefreshField(C1, AnimalRole2);
		}//FastRenewField ends

		public static void HealthCheck()
		{

			foreach (var PredatorTemp in PredatorList)
			{
				if (PredatorTemp.Health == 0)
				{
					PredatorList.Remove(PredatorTemp);
					return;
				}
			}

			foreach (var HerbivorTemp in HerbivoreList)
			{
				if (HerbivorTemp.Health == 0)
				{
					HerbivoreList.Remove(HerbivorTemp);
					return;
				}
			}

		}//HealthCheck ends

		public static bool BirthProcessStarter(int x1, int y1, int x2, int y2)
		{
			int Health1 = 0;
			int Health2 = 0;
			int BirthCooldown1 = 100;
			int BirthCooldown2 = 100;
			bool BirthProcess1 = false;
			bool BirthProcess2 = false;
			bool BirthProcessPredator = false;
			char C1 = 'N';
			char C2 = 'N';


			//Console.WriteLine("BirthProcessPredator Started...");
			//Console.WriteLine("Animal One address:"+x1+","+y1);
			//Console.WriteLine("Animal Two address:" + x2 + "," + y2);

			foreach (var PredatorTemp in PredatorList)//Starter
			{
				if (PredatorTemp.x == x1 && PredatorTemp.y == y1)
				{
					Health1 = PredatorTemp.Health;
					C1 = PredatorTemp.Visual;
					BirthCooldown1 = PredatorTemp.BirthCooldown;
					BirthProcess1 = PredatorTemp.BirthProcess;
					//Console.WriteLine("BirthProcessPredator found first animal in list...");
				}
			}

			foreach (var PredatorTemp2 in PredatorList)//Partner
			{
				if (PredatorTemp2.x == x2 && PredatorTemp2.y == y2)
				{
					Health2 = PredatorTemp2.Health;
					C2 = PredatorTemp2.Visual;
					BirthCooldown2 = PredatorTemp2.BirthCooldown;
					BirthProcess2 = PredatorTemp2.BirthProcess;
					//Console.WriteLine("BirthProcessPredator found second animal in list...");
				}
			}

			//Console.WriteLine("BirthProcessPredator collected all variables...");
			//Console.WriteLine("Animal Starter HP=" + Health1 + " and BirthCooldown =" + BirthCooldown1);
			//Console.WriteLine("Animal Partner HP=" + Health2 + " and BirthCooldown =" + BirthCooldown2);
			if (Health1 > 40 && Health2 > 40 && BirthCooldown1 <= 0 && BirthCooldown2 <= 0 && BirthProcess1 == false && BirthProcess2 == false && C1 == C2)//If both Predators okay
			{
				//Console.WriteLine("BirthProcessPredator started breed process...");
				foreach (var PredatorTemp in PredatorList)
				{
					if (PredatorTemp.x == x1 && PredatorTemp.y == y1)
					{
						PredatorTemp.BirthStarter = true;
						PredatorTemp.BirthTimer = 3;
						PredatorTemp.BirthProcess = true;
					}

					if (PredatorTemp.x == x2 && PredatorTemp.y == y2)
					{
						PredatorTemp.BirthTimer = 3;
						PredatorTemp.BirthProcess = true;
					}
				}
				BirthProcessPredator = true;
				return BirthProcessPredator;
			}
			return BirthProcessPredator;

		}//BirthProcess ends

		
		public static void BirthProcessTimer()
		{

			int x1 = 0;
			int y1 = 0;
			bool FoundStarter = false;
			char C = 'N';
			string Contains2 = "Empty";

			for (int i = 0; i < FieldSize1; i++)
			{
				for (int j = 0; j < FieldSize2; j++)
				{
					if (GameFieldList[i, j].Contains != "Empty")
					{				
						foreach (var PredatorTemp in PredatorList)
						{
							if (PredatorTemp.x == i && PredatorTemp.y == j)
							{
								if (PredatorTemp.BirthProcess == true && PredatorTemp.BirthStarter == true && PredatorTemp.BirthTimer == 0)//For Starter
								{
									x1 = PredatorTemp.x;
									y1 = PredatorTemp.y;
									C = PredatorTemp.Visual;
									Contains2 = PredatorTemp.AnimalRole;
									PredatorTemp.BirthStarter = false;
									PredatorTemp.BirthProcess = false;
									PredatorTemp.BirthCooldown = 10;
									FoundStarter = true;
								}

								if (PredatorTemp.BirthProcess == true && PredatorTemp.BirthTimer == 0)
								{
									PredatorTemp.BirthProcess = false;
									PredatorTemp.BirthCooldown = 10;
								}
							}
						}

						if (FoundStarter == true)
						{
							for (int i1 = x1 - 1; i1 <= x1 + 1; i1++)//Scan around for movement if no prey
							{
								if (i1 >= 0 && i1 < Program.FieldSize1) //Horisontal check
								{
									for (int j1 = y1 - 1; j1 <= y1 + 1; j1++)
									{
										if (j1 >= 0 && j1 < Program.FieldSize2)//Vertical check
										{

											if (GameFieldList[i1, j1].Contains == "Empty")
											{
												PredatorList.Add(new Lion(i1, j1));
												GameFieldList[i1, i1].Visual = C;
												GameFieldList[i1, i1].Contains = Contains2;
												C = 'N';
												Contains2 = "Empty";
												FoundStarter = false;
												break;
											}
										}
									}
								}
							}
						}

					}
				}
			}

		}//BirthProcessTimer  ends

		public static bool AddLion()
		{
			bool found = false;
			char Lion = 'L';
			string Animal = "Predator";

			while (found == false)
			{
				for (int j = 0; j < FieldSize1; j++)
				{
					if (GameFieldList[0, j].Contains == "Empty")
					{
						PredatorList.Add(new Lion(0, j));
						GameFieldList[0, j].RefreshField(Lion, Animal);
						found = true;
						return found;
					}

				}
			}
			return found;
		}//AddLion ends

		public static bool AddAntelope()
		{
			bool found = false;
			char Antelope = 'A';
			string Animal = "Herbivore";

			while (found == false)
			{
				for (int j = 0; j < FieldSize1; j++)
				{
					if (GameFieldList[0, j].Contains == "Empty")
					{
						HerbivoreList.Add(new Antelope(17, j));
						GameFieldList[15, j].RefreshField(Antelope, Animal);
						found = true;
						return found;
					}

				}
			}
			return found;
		}//AddAntelope ends

		static List<AnimalLibrary> ReadGamePlugins()//bs
		{
			var PluginFiles = Directory.GetFiles("DLC", "*.dll");//Read files with .dll
			var PluginList = new List<AnimalLibrary>();

			foreach (var File in PluginFiles)
			{
				var CurrentAssembly = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), File));
				//explain here

				var PluginTypes = CurrentAssembly.GetTypes().Where(t => typeof(AnimalLibrary).IsAssignableFrom(t) && !t.IsInterface).ToArray();
				//explain here

				foreach (var PluginType in PluginTypes)
				{
					var PluginInstance = Activator.CreateInstance(PluginType) as AnimalLibrary;
					//Explain here
					PluginList.Add(PluginInstance);

				}

			}
			return PluginList;
		}//ReadPlugins end


	}
}