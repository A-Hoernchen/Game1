using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Model
{
    /// <summary>
    /// Repräsentiert die Münzen im Spiel.
    /// </summary>
    internal class Diamant : Item
    {
        public Diamant()
        {
            // Standard-Masse für Diamanten
            Mass = 0.5f;
            Texture = "coin_silver.png";
        }
    }
}
