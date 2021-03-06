﻿using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Game1.Screens;
using Microsoft.Xna.Framework.Graphics;
using Game1.Rendering;
using System.IO;
using Game1.Model;

namespace Game1.Components
{
    /// <summary>
    /// Komponente zur Verwaltung von Screen-Overlays.
    /// </summary>
    internal class ScreenComponent : DrawableGameComponent
    {
        private readonly string contentPath = ".\\Content";

        private readonly Stack<Screen> screens;

        private SpriteBatch spriteBatch;

        private Dictionary<string, Texture2D> icons;

        #region Shared Resources

        /// <summary>
        /// Ein einzelner Pixel
        /// </summary>
        public Texture2D Pixel { get; private set; }

        /// <summary>
        /// Standard Auswahlpfeil
        /// </summary>
        /// <value>The arrow.</value>
        public Texture2D Arrow { get; private set; }

        /// <summary>
        /// Standard-Schriftart für Dialoge
        /// </summary>
        public SpriteFont Font { get; private set; }

        /// <summary>
        /// Standard Hintergrund für Panels
        /// </summary>
        public NineTileRenderer Panel { get; private set; }

        public NineTileRenderer Bolt_Panel1 { get; private set; }
        public NineTileRenderer Bolt_Panel2 { get; private set; }
        public NineTileRenderer Bolt_Panel3 { get; private set; }
        public NineTileRenderer Bolt_Panel4 { get; private set; }
        public NineTileRenderer Bolt_Panel5 { get; private set; }
        public NineTileRenderer Bolt_Panel6 { get; private set; }
        public NineTileRenderer Bolt_Panel7 { get; private set; }
        public NineTileRenderer Bolt_Panel8 { get; private set; }

        public NineTileRenderer Small_Panel1 { get; private set; }
        public NineTileRenderer Small_Panel2 { get; private set; }
        public NineTileRenderer Small_Panel3 { get; private set; }
        public NineTileRenderer Small_Panel4 { get; private set; }
        public NineTileRenderer Small_Panel5 { get; private set; }
        public NineTileRenderer Small_Panel6 { get; private set; }
        public NineTileRenderer Small_Panel7 { get; private set; }
        public NineTileRenderer Small_Panel8 { get; private set; }

        /// <summary>
        /// Standard Hintergrund für Buttons
        /// </summary>
        public NineTileRenderer Button { get; private set; }

        /// <summary>
        /// Standard Hintergrund für selektierte Buttons.
        /// </summary>
        public NineTileRenderer ButtonHovered { get; private set; }

        /// <summary>
        /// Standard Hintergrund für einen Rahmen.
        /// </summary>
        public NineTileRenderer Border { get; private set; }

        /// <summary>
        /// Textur für die Wartemünze
        /// </summary>
        public Texture2D WaitingCoin { get; private set; }

        #endregion

        /// <summary>
        /// Liefert den aktuellen Screen oder null zurück.
        /// </summary>
        public Screen ActiveScreen
        {
            get { return screens.Count > 0 ? screens.Peek() : null; }
        }

        /// <summary>
        /// Referenz auf das Game (Überschrieben mit spezialisiertem Type)
        /// </summary>
        public new Game1 Game { get; private set; }

        public ScreenComponent(Game1 game)
            : base(game)
        {
            Game = game;
            screens = new Stack<Screen>();
            icons = new Dictionary<string, Texture2D>();
        }

        /// <summary>
        /// Zeigt den übergebenen Screen an.
        /// </summary>
        public void ShowScreen(Screen screen)
        {
            screens.Push(screen);
            screen.OnShow();
        }

        /// <summary>
        /// Entfernt den obersten Screen.
        /// </summary>
        public void CloseScreen()
        {
            if (screens.Count > 0)
            {
                var screen = screens.Pop();
                screen.OnHide();
            }
        }

        protected override void LoadContent()
        {
            // Sprite Batch erstellen
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Standard Pixel erstellen
            Pixel = new Texture2D(GraphicsDevice, 1, 1);
            Pixel.SetData(new [] { Color.White });

            // Schriftart laden
            Font = Game.Content.Load<SpriteFont>("HudFont");

            // Hintergründe laden
            Texture2D texture;

            texture = Game.Content.Load<Texture2D>("ui_elements\\ui_theme");
            Bolt_Panel1 = new NineTileRenderer(texture, new Rectangle(0, 288, 512, 128), new Point(30, 30));
            Bolt_Panel2 = new NineTileRenderer(texture, new Rectangle(0, 432, 512, 128), new Point(30, 30));
            Bolt_Panel3 = new NineTileRenderer(texture, new Rectangle(0, 576, 512, 128), new Point(30, 30));
            Bolt_Panel4 = new NineTileRenderer(texture, new Rectangle(0, 720, 512, 128), new Point(30, 30));

            Bolt_Panel5 = new NineTileRenderer(texture, new Rectangle(800, 288, 512, 128), new Point(30, 30));
            Bolt_Panel6 = new NineTileRenderer(texture, new Rectangle(800, 432, 512, 128), new Point(30, 30));
            Bolt_Panel7 = new NineTileRenderer(texture, new Rectangle(800, 576, 512, 128), new Point(30, 30));
            Bolt_Panel8 = new NineTileRenderer(texture, new Rectangle(800, 720, 512, 128), new Point(30, 30));

            Small_Panel1 = new NineTileRenderer(texture, new Rectangle(528, 544, 256, 64), new Point(15, 15));
            Small_Panel2 = new NineTileRenderer(texture, new Rectangle(528, 624, 256, 64), new Point(15, 15));
            Small_Panel3 = new NineTileRenderer(texture, new Rectangle(528, 704, 256, 64), new Point(15, 15));
            Small_Panel4 = new NineTileRenderer(texture, new Rectangle(528, 784, 256, 64), new Point(15, 15));

            Small_Panel5 = new NineTileRenderer(texture, new Rectangle(1328, 544, 256, 64), new Point(15, 15));
            Small_Panel6 = new NineTileRenderer(texture, new Rectangle(1328, 624, 256, 64), new Point(15, 15));
            Small_Panel7 = new NineTileRenderer(texture, new Rectangle(1328, 704, 256, 64), new Point(15, 15));
            Small_Panel8 = new NineTileRenderer(texture, new Rectangle(1328, 784, 256, 64), new Point(15, 15));

            texture = Game.Content.Load<Texture2D>("ui");
            Panel = new NineTileRenderer(texture, new Rectangle(190, 100, 100, 100), new Point(30, 30));
            Border = new NineTileRenderer(texture, new Rectangle(283, 200, 93, 94), new Point(30, 30));
            Button = new NineTileRenderer(texture, new Rectangle(0, 282, 190, 49), new Point(10, 10));
            ButtonHovered = new NineTileRenderer(texture, new Rectangle(0, 143, 190, 45), new Point(10, 10));
            

            // Arrow
            Rectangle source = new Rectangle(325, 486, 22, 21);
            Color[] buffer = new Color[source.Width * source.Height];
            texture.GetData(0, source, buffer, 0, buffer.Length);
            Arrow = new Texture2D(GraphicsDevice, source.Width, source.Height);
            Arrow.SetData(buffer);

            // Waiting Coin
            WaitingCoin = Game.Content.Load<Texture2D>("coin_gold");
        }

        public Texture2D GetIcon(string name)
        {
            // Leere Strings ignorieren
            if (string.IsNullOrEmpty(name))
                return null;

            // Bereits geladene Texturen ignorieren
            Texture2D result;
            if (!icons.TryGetValue(name, out result))
            {
                // Textur nachladen
                using (Stream stream = File.OpenRead(contentPath + "\\" + name))
                {
                    result = Texture2D.FromStream(GraphicsDevice, stream);
                    icons.Add(name, result);
                }
            }

            return result;
        }

        public override void Update(GameTime gameTime)
        {
            // Nur wenn Komponente aktiviert wurde.
            if (!Enabled)
                return;
            
            Screen activeScreen = ActiveScreen;
            if (activeScreen != null)
            {
                foreach (var control in activeScreen.Controls)
                    control.Update(gameTime);
                activeScreen.Update(gameTime);
                Game.Input.Handled = true;
            }

            // Hauptmenü öffnen
            if (!Game.Input.Handled)
            {
                if (Game.Input.Close)
                {
                    ShowScreen(new MainMenuScreen(this));
                    Game.Input.Handled = true;
                }
            }

            // Inventar öffnen
            if (!Game.Input.Handled)
            {
                if (Game.Input.Inventory)
                {
                    ShowScreen(new InventoryScreen(this));
                    Game.Input.Handled = true;
                }
            }

            // Hauptmenü öffnen, wenn sonst nichts offen ist
            if (activeScreen == null && Game.Simulation.World == null)
            {
                ShowScreen(new MainMenuScreen(this));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // Nur wenn Komponente sichtbar ist.
            if (!Visible)
                return;
            
            spriteBatch.Begin(samplerState: SamplerState.LinearWrap);
            var list = screens.ToArray();
            Array.Reverse(list);
            foreach (var screen in list)
                screen.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}

