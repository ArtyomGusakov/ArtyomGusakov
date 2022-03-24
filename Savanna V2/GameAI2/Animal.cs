using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAI2
{
	public abstract class Animal
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
	}
}