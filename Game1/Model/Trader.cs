using System;
using Game1.Screens;
using System.Collections.Generic;
using Game1.Components;

namespace Game1.Model
{
    /// <summary>
    /// Repräsentiert den Händler im Spiel.
    /// </summary>
    internal class Trader : Character, IInteractable, IInventory
    {
        private List<Item> inventory;

        public Action<SimulationComponent, IInteractor, IInteractable> OnInteract { get; set; }

        /// <summary>
        /// Das Händler-Inventar.
        /// </summary>
        public ICollection<Item> Inventory { get { return inventory; } }

        public Trader(int id) : base(id)
        {
            Texture = "trader.png";
            Name = "Hardwig";
            Icon = "tradericon.png";

            inventory = new List<Item>();

            OnInteract = (simulation, player, trader) =>
            {
                    Game1 game = simulation.Game as Game1;
                    simulation.ShowInteractionScreen(player as Player, new TradeScreen(game.Screen, trader as IInventory, player as IInventory));
            };
        }
    }
}

