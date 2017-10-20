using System;
using Game1.Components;

namespace Game1.Model
{
    /// <summary>
    /// Interface für alle einsammelbaren Items.
    /// </summary>
    internal interface ICollectable
    {
        /// <summary>
        /// Action die aufgerufen wird, wenn das Item eingesammelt wird.
        /// </summary>
        Action<SimulationComponent, Item> OnCollect { get; }
    }
}

