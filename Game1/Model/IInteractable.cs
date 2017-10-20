using System;
using Game1.Components;

namespace Game1.Model
{
    /// <summary>
    /// Interface für alle Spielelemente mit denen interagiert werden kann.
    /// </summary>
    internal interface IInteractable
    {
        /// <summary>
        /// Delegat für aktiven Interaktionsversuch des Spielers.
        /// </summary>
        Action<SimulationComponent, IInteractor, IInteractable> OnInteract { get; }
    }
}

