﻿using System;
using System.Collections.Generic;

namespace Game1.Model
{
    /// <summary>
    /// Interface für alle Items mit Inventar.
    /// </summary>
    internal interface IInventory
    {
        /// <summary>
        /// Auflistung der Items im Inventar.
        /// </summary>
        ICollection<Item> Inventory { get; }
    }
}

