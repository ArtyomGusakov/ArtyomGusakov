using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameAI2;

namespace MyGameLibrary.sdk

{
	public interface InterfacePlugin
	{

		public char Visual { set; get; }
		public string AnimalRole { set; get; }
		public int x { set; get; }
		public int y { set; get; }
		public int ViewDistance { set; get; }
		public int Health { set; get; }

		public int ThinkNextMove();

	}
}
