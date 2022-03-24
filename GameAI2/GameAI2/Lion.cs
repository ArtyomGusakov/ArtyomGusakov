using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAI2
{
	public class Lion : Predator
	{
		public Lion(int x1, int y1)
		{
		Visual = 'L';
		AnimalRole = "Predator";
		x = x1;
		y = y1;
		ViewDistance = 3;
		Health = 100;
		
	    }
	}
}
