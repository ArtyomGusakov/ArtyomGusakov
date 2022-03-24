using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAI2
{
	public class GameField
	{
		public char Visual = '░';
		public string Contains = "Empty";
		public char GameFieldVisual()
		{
			return Visual;
		}

		public void RefreshField(char C, string Contains2)
		{
			Visual = C;
			Contains = Contains2;
		}
	}
}
