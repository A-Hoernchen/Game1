using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Model
{
    /// <summary>
    /// Repräsentiert eine Kachel einer Area.
    /// </summary>
    internal class Tile
    {
        /// <summary>
        /// Name der Textur
        /// </summary>
        public string Texture { get; set; }

        /// <summary>
        /// Lage des Tiles innerhalb der Tiles-Textur.
        /// </summary>
        public Rectangle SourceRectangle { get; set; }

        /// <summary>
        /// Gibt an ob diese Tile den Spieler an der Bewegung hindert.
        /// </summary>
        public bool Blocked { get; set; }
    }
}
