using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Model
{
    /// <summary>
    /// Repräsentiert eine sich eigenständig bewegende Spieleinheit.
    /// </summary>
    internal class Character : Item
    {
        /// <summary>
        /// Geschwindigkeitsvektor.
        /// </summary>
        public Vector2 Velocity { get; set; }

        public Character()
        {
        }
    }
}
