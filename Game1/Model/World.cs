using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Model
{
    /// <summary>
    /// Repräsentiert eine Instanz der Welt.
    /// </summary>
    internal class World
    {
        /// <summary>
        /// Auflistung aller Areas
        /// </summary>
        public List<Area> Areas { get; private set; }

        public World()
        {
            Areas = new List<Area>();
        }
    }
}
