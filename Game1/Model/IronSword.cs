using System;

namespace Game1.Model
{
    /// <summary>
    /// Ein Eisenschwert das sich der Spieler niemals leisten kann.
    /// </summary>
    internal class IronSword : Item
    {
        public IronSword(int id) : base(id)
        {
            Name = "Irony";
            Icon = "ironswordicon.png";
            Value = 10;
        }
    }
}

