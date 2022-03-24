using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAI2
{
	public abstract class Predator:Animal
	{
		public int ThinkNextMove(GameField[,] GameFieldList2)//Predator look around before taking next move
		{
			//PlayField[2, 2].GameFieldVisual();
			int i = 0;
			int j = 0;
			int i1 = 0;
			int j1 = 0;
			int a = 0;
			int b = 0;
			int a1 = 0;
			int b1 = 0;
			int a2 = 0;
			int b2 = 0;
			int x1 = 0;
			int y1 = 0;
			int oldy = 0;
			int oldx = 0;
			bool FoundPrey = false;
			int CurrentTargetDistance = 100;
			int ClosestTargetDistance = 100;
			int CurrentMovePoint = 100;
			int ClosestMovePoint = 100;
			bool ExtraOne = false;
			char OwnChar = 'N';
			char NearChar = 'N';
			

			int Answer = 0;
			//0 - Default
			//1 - Found prey at range 1
			//2 - Found prey at range 1,5
			//3 - Found prey, too far
			//4 - No prey found, random movement 
			//5 - Death from starvation
			//6 - Birth Process
			//7 - Birth Process ended
			//8 - Birth Started
			//9 - Bositive Birth function

			Health = Health - 5;
			BirthCooldown = BirthCooldown - 1;

			if (BirthTimer < 0)
			{
				BirthTimer = 0;
			}

			if (BirthTimer > 0 && BirthProcess == true)//Skip turn if true
			{
				BirthTimer = BirthTimer - 1;
				Answer = 6;
				//Console.WriteLine("Answer=" + Answer);
				return Answer;
			}
				//Console.WriteLine("Predator started to think next move...");

				for (i1 = x - ViewDistance; i1 <= x + ViewDistance; i1++)//Scan around for prey
			{
				if (i1 >= 0 && i1 < Program.FieldSize1) //Horisontal check
				{
					for (j1 = y - ViewDistance; j1 <= y + ViewDistance; j1++)
					{
						if (j1 >= 0 && j1 < Program.FieldSize2)//Vertical check
						{

							if (GameFieldList2[i1, j1].Contains == "Herbivore")
							{
								//Answer = 1;
								FoundPrey = true;
								a1 = x - i1;
								b1 = y - j1;
								a = Math.Abs(a1);//Distance X
								b = Math.Abs(b1);//Distance y
								CurrentTargetDistance = a + b;

								if (CurrentTargetDistance < ClosestTargetDistance)
								{
									i = i1;
									j = j1;
									ClosestTargetDistance = CurrentTargetDistance;
								}
							}
						}
					}
				}
			}//Scan around ends

			if (FoundPrey == true && ClosestTargetDistance == 1)//If prey on attack range
			{
				Health = 100;
				Answer = 1;
				//Console.WriteLine("Answer=" + Answer);
				Program.Hunt(i, j);
				return (Answer);
			}

			else if (FoundPrey == true && ClosestTargetDistance == 2)//If play on attack range, but range calculated as 2
			{
				if (i == x - 1 && j == y - 1 || i == x - 1 && j == y + 1 || i == x + 1 && j == y - 1 || i == x + 1 && j == y + 1)
				{
					Health = 100;
					Answer = 2;
					//Console.WriteLine("Answer=" + Answer);
					Program.Hunt(i, j);
					return (Answer);
				}
			}

			else if (FoundPrey == true && ClosestTargetDistance > 1)//If pray out of hunt range
			{
				Answer = 3;
				//Console.WriteLine("Answer=" + Answer);
				a1 = 0;
				b1 = 0;

				for (i1 = x - 1; i1 <= x + 1; i1++)//Scan around for movement
				{
					if (i1 >= 0 && i1 < Program.FieldSize1) //Horisontal check
					{
						for (j1 = y - 1; j1 <= y + 1; j1++)
						{
							if (j1 >= 0 && j1 < Program.FieldSize2)//Vertical check
							{

								if (GameFieldList2[i1, j1].Contains == "Empty")
								{
									a1 = i1 - i;
									b1 = j1 - j;
									a2 = Math.Abs(a1);
									b2 = Math.Abs(b1);
									CurrentMovePoint = a2 + b2;
									if (CurrentMovePoint < ClosestMovePoint)
									{
										x1 = i1;
										y1 = j1;
										ClosestMovePoint = CurrentMovePoint;
									}

								}
							}
						}
					}
				}
				x = x1;
				y = y1;
				return (Answer);
			}

			else//if prey is not found, predator moves randomly
			{
				Answer = 4;
				x1 = x;
				y1 = y;

				for (i1 = x - 1; i1 <= x + 1; i1++)//Scan around for movement if no prey
				{
					if (i1 >= 0 && i1 < Program.FieldSize1) //Horisontal check
					{
						for (j1 = y - 1; j1 <= y + 1; j1++)
						{
							if (j1 >= 0 && j1 < Program.FieldSize2)//Vertical check
							{
								//Console.WriteLine("Lion at address:"+x+","+y+" looks at "+i1+","+j1);
								if (GameFieldList2[i1,j1].Visual == GameFieldList2[x,y].Visual)
								{
									if (i1 != x || j1 != y)
									{
										//Console.WriteLine("Lion at address:" + x + "," + y + " found breed mate at " + i1 + "," + j1);
										Answer = 8;
										//Console.WriteLine("Answer=" + Answer);
										if (Program.BirthProcessStarter(x, y, i1, j1) == true)
										{
											Answer = 9;
											return (Answer);
										}
									}
								}
							}
						}
					}
				}

				
			//Console.WriteLine("Answer Random =" + Answer);
				
				for (i1 = x - 1; i1 <= y + 1; i1++)//Scan around for movement if no prey
				{
					if (i1 >= 0 && i1 < Program.FieldSize1) //Horisontal check
					{
						for (j1 = y - 1; j1 <= x + 1; j1++)
						{
							if (j1 >= 0 && j1 < Program.FieldSize2)//Vertical check
							{

								if (GameFieldList2[i1, j1].Contains == "Empty")
								{
									Random Random1 = new Random();
									int Randomint = Random1.Next(1, 10);
									if (Randomint == 1)
									{										
										x = i1;
										y = j1;
										return (Answer);
									}
								}
							}
						}
					}
				}
				
			}
				
			return (Answer);
		}
	}
}
