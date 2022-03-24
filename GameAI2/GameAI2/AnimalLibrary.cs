using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAI2
{
	public interface AnimalLibrary
	{
		public char Visual { set; get; }
		public string AnimalRole { set; get; }
		public int x { set; get; }
		public int y { set; get; }
		public int ViewDistance { set; get; }
		public int Health { set; get; }

		public int ThinkNextMove(GameField[,] GameFieldList2);
	}
}
