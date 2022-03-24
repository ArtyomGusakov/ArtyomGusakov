using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAI2
{
	public abstract class Herbivore : Animal
	{
		public char Visual = 'N';
		public string AnimalRole = "Empty";
		public int x = 0;
		public int y = 0;
		public int ViewDistance = 3;
		public int Health = 100;
		public int BirthTimer = 0;
		public int BirthCooldown = 0;
		public bool BirthProcess = false;
		public bool BirthStarter = false;

		
		public int ThinkNextMove(GameField[,] GameFieldList2)//Antelope look around before taking next move
		{
			int i = 0;
			int j = 0;
			int i1 = 0;
			int j1 = 0;
			int a = 0;
			int b = 0;
			int a1 = 0;
			int b1 = 0;
			int x1 = 0;
			int y1 = 0;
			int a2 = 0;
			int b2 = 0;
			bool FoundPredator = false;
			int CurrentLionDistance = 0;
			int ClosestLionDistance = 0;
			int CurrentMovePoint = 0;
			int ClosestMovePoint = 0;
			char OwnChar = 'N';
			char NearChar = 'N';

			Health = Health - 5;
			int PointlessReturn = 1;


			for (i1 = x - ViewDistance; i1 <= x + ViewDistance; i1++)//Scan around for predator
			{
				if (i1 >= 0 && i1 < Program.FieldSize1) //Horisontal check
				{
					for (j1 = y - ViewDistance; j1 <= y + ViewDistance; j1++)
					{
						if (j1 >= 0 && j1 < Program.FieldSize2)//Vertical check
						{
							if (GameFieldList2[i1, j1].Contains == "Predator")
							{

								FoundPredator = true;
								a1 = x - i1;
								b1 = y - j1;
								a = Math.Abs(a1);
								b = Math.Abs(b1);
								CurrentLionDistance = a + b;
								if (CurrentLionDistance < ClosestLionDistance)
								{
									i = i1;
									j = j1;
									ClosestLionDistance = CurrentLionDistance;
								}
							}
						}
					}
				}
			}//Scan ended

			BirthCooldown = BirthCooldown - 1;

			if (BirthTimer < 0)
			{
				BirthTimer = 0;
			}

			if (BirthTimer > 0 && BirthProcess == true)//Skip turn if true
			{
				BirthTimer = BirthTimer - 1;
				return PointlessReturn;
			}

			if (FoundPredator == true)
			{
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

									if (CurrentMovePoint > ClosestMovePoint)
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
				return PointlessReturn;
			}

			else//Random movement
			{
				Health = Health + 5;
				x1 = x;
				y1 = y;

				if (FoundPredator == false)
				{
					for (i1 = x - 1; i1 <= x + 1; i1++)//Scan around for movement if no prey
					{
						if (i1 >= 0 && i1 < Program.FieldSize1) //Horisontal check
						{
							for (j1 = y - 1; j1 <= y + 1; j1++)
							{
								if (j1 >= 0 && j1 < Program.FieldSize2)//Vertical check
								{
									if (GameFieldList2[i1, j1].Visual == GameFieldList2[x, y].Visual)
									{
										if (i1 != x || j1 != y)
										{
											if (Program.BirthProcessStarter(x, y, i1, j1) == true)
											{
												return PointlessReturn;
											}
										}
									}
								}
							}
						}
					}
				}

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
									Random Random1 = new Random();
									int Randomint = Random1.Next(1, 10);
									if (Randomint == 1)
									{
										x= i1;
										y = j1;
										return PointlessReturn;
									}
								}
							}
						}
					}
				}
			}//Random movement ends
			return PointlessReturn;
		}
	}
}
