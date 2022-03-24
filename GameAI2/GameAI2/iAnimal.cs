using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAI2
{
	interface iAnimal
	{
		public int LookAround(GameField[,] PlayFieldName);
		public int Move(int x,int y);
		public int Starve(int Health);
		public iAnimal Breed(iAnimal partner);
		public int GetKilled();
		public int Eat();
	}
}
