using System;

namespace Game1.Model
{
    /// <summary>
    /// Handschuhe für den Spieler die er sich niemals leisten kann.
    /// </summary>
    internal class Gloves : Item
    {
        public Gloves(int id) : base(id)
        {
            Name = "Gloves";
            Icon = "glovesicon.png";
            Value = 20;
        }
    }
}

