using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOL_OOP
{
	public class CellClass
	{

		public bool Alive;
		public int NearCells = 0; //How much cells around are alive (0-8)
		public char CellVisual = '░';

		public CellClass(bool SetAlive)
		{
			Alive = SetAlive;

			if (Alive == true)
			{
				CellVisual = '█';
			}
			else
			{
				CellVisual = '░';
			}
		}

		public  char GetCellVisual()
		{
			return CellVisual;
		}

		public bool NextGeneration()
		{
			if (Alive == true)
			{


				if (NearCells < 2 || NearCells > 3)//Condition to die
				{
					Alive = false;
					CellVisual = '░';
					
					return Alive;
				}
				
            }

			if (Alive == false)
			{
				if (NearCells == 3)//Condition to bring cell to live
				{
					Alive = true;
					CellVisual = '█';
				  
					return Alive;
				}
				
			}

			return Alive;
		}

		public bool LiveStatus(bool Alive2)
		{
			Alive = Alive2;

			if (Alive == true)
			{
				CellVisual = '█';
			}
			else
			{
				CellVisual = '░';
			}

			return Alive;

		}

	}
}
