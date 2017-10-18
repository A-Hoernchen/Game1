using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Components
{
    internal class SoundComponent : GameComponent
    {
        private Game1 game;

        private SoundEffect click;

        private float volume;

        public SoundComponent(Game1 game) : base(game)
        {
            this.game = game;
            volume = 0.5f;

            click = game.Content.Load<SoundEffect>("click");
        }

        public void PlayClick()
        {
            click.Play(volume, 0f, 0f);
        }
    }
}
