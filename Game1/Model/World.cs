using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Model
{
	public class World
	{
		public List<Area> Areas
		{
			get;
			private set;
		}
		public World()
		{
			Areas = new List<Area>();
		}
	}
}
