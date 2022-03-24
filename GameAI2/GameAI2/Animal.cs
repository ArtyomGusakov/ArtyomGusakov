using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAI2
{
	interface  Animal
	{
		public int ThinkNextMove(GameField[,] PlayFieldName);
	}
}