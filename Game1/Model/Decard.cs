using System;
using Game1.Screens;
using Game1.Components;

namespace Game1.Model
{
    /// <summary>
    /// Repräsentiert den Helfer am Brunnen.
    /// </summary>
    internal class Decard : Character, IInteractable
    {
        /// <summary>
        /// Delegat für aktiven Interaktionsversuch des Spielers.
        /// </summary>
        public Action<SimulationComponent, IInteractor, IInteractable> OnInteract { get; set; }

        public Decard(int id) : base(id)
        {
            Texture = "decard.png";
            Name = "Decard";
            Icon = "decardicon.png";

            OnInteract = DoInteract;

            Ai = new WalkingAi(this, 0.4f);
        }

        private void DoInteract(SimulationComponent simulation, IInteractor interactor, IInteractable interactable)
        {
            Game1 game = simulation.Game as Game1;
            simulation.ShowInteractionScreen(interactor as Player, new ShoutScreen(game.Screen, this, "Bleib ein Weilchen und hoer zu!"));
        }
    }
}

