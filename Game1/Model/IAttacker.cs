using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Model
{
    /// <summary>
    /// Interface für alle aktiven Angreifer.
    /// </summary>
    internal interface IAttacker
    {
        /// <summary>
        /// Intern geführte Liste aller angreifbaren Elemente in der Nähe.
        /// </summary>
        ICollection<Item> AttackableItems { get; }

        /// <summary>
        /// Angriffsradius in dem Schaden ausgeteilt wird.
        /// </summary>
        float AttackRange { get; }

        /// <summary>
        /// Schaden der pro Angriff verursacht wird.
        /// </summary>
        int AttackValue { get; }
    }
}
