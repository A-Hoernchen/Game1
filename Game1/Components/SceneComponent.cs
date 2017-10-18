﻿using Game1.Model;
using Game1.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Components
{
    /// <summary>
    /// Game Komponente zur Ausgabe der Spiel-Szene an den Bildschirm.
    /// </summary>
    internal class SceneComponent : DrawableGameComponent
    {
        private readonly Game1 game;

        private readonly Dictionary<string, Texture2D> tilesetTextures;

        private readonly Dictionary<string, Texture2D> itemTextures;

        private readonly Dictionary<Item, ItemRenderer> itemRenderer;

        private SpriteBatch spriteBatch;

        /// <summary>
        /// Kamera-Einstellungen für diese Szene.
        /// </summary>
        public Camera Camera { get; private set; }

        public SceneComponent(Game1 game)
            : base(game)
        {
            this.game = game;
            tilesetTextures = new Dictionary<string, Texture2D>();
            itemTextures = new Dictionary<string, Texture2D>();
            itemRenderer = new Dictionary<Item, ItemRenderer>();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Camera = new Camera(GraphicsDevice.Viewport.Bounds.Size);

            // Initiale Kameraposition (temporär)
            Vector2 areaSize = new Vector2(
                game.Simulation.World.Areas[0].Width,
                game.Simulation.World.Areas[0].Height);
            Camera.SetFocusExplizit(game.Simulation.Player.Position, areaSize);

            // Erforderliche Texturen ermitteln
            List<string> requiredTilesetTextures = new List<string>();
            List<string> requiredItemTextures = new List<string>();
            foreach (var area in game.Simulation.World.Areas)
            {
                // Tile-Textures
                foreach (var tile in area.Tiles.Values)
                    if (!requiredTilesetTextures.Contains(tile.Texture))
                        requiredTilesetTextures.Add(tile.Texture);

                // Item Textures
                foreach (var item in area.Items)
                    if (!string.IsNullOrEmpty(item.Texture) && !requiredItemTextures.Contains(item.Texture))
                        requiredItemTextures.Add(item.Texture);
            }

            // Erforderlichen Tileset-Texturen direkt aus dem Stream laden
            string mapPath = Path.Combine(Environment.CurrentDirectory, "Maps");
            foreach (var textureName in requiredTilesetTextures)
            {
                using (Stream stream = File.OpenRead(mapPath + "\\" + textureName))
                {
                    Texture2D texture = Texture2D.FromStream(GraphicsDevice, stream);
                    tilesetTextures.Add(textureName, texture);
                }
            }

            // Erforderliche Item-Texturen direkt aus dem Stream laden
            mapPath = Path.Combine(Environment.CurrentDirectory, "Content");
            foreach (var textureName in requiredItemTextures)
            {
                using (Stream stream = File.OpenRead(mapPath + "\\" + textureName))
                {
                    Texture2D texture = Texture2D.FromStream(GraphicsDevice, stream);
                    itemTextures.Add(textureName, texture);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Platziert den Kamerafokus auf den Spieler.
            Vector2 areaSize = new Vector2(
                game.Simulation.World.Areas[0].Width,
                game.Simulation.World.Areas[0].Height);
            Camera.SetFocus(game.Simulation.Player.Position, areaSize);
        }

        public override void Draw(GameTime gameTime)
        {
            // Erste Area referenzieren (versuchsweise)
            Area area = game.Simulation.World.Areas[0];

            // Bildschirm leeren
            GraphicsDevice.Clear(area.Background);

            spriteBatch.Begin();

            // Berechnet den Render-Offset mit Hilfe der Kamera-Einstellungen
            Point offset = (Camera.Offset * Camera.Scale).ToPoint();

            // Alle Layer der Render-Reihenfolge nach durchlaufen
            for (int l = 0; l < area.Layers.Length; l++)
            {
                RenderLayer(area, area.Layers[l], offset);
                if (l == 4)
                    RenderItems(area, offset, gameTime);
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Rendert einen Layer der aktuellen Szene
        /// </summary>
        private void RenderLayer(Area area, Layer layer, Point offset)
        {
            // TODO: Nur den sichtbaren Bereich rendern
            for (int x = 0; x < area.Width; x++)
            {
                for (int y = 0; y < area.Height; y++)
                {
                    // Prüfen, ob diese Zelle ein Tile enthält
                    int tileId = layer.Tiles[x, y];
                    if (tileId == 0)
                        continue;

                    // Tile ermitteln
                    Tile tile = area.Tiles[tileId];
                    Texture2D texture = tilesetTextures[tile.Texture];

                    // Position ermitteln
                    int offsetX = (int)(x * Camera.Scale) - offset.X;
                    int offsetY = (int)(y * Camera.Scale) - offset.Y;

                    // Zelle mit der Standard-Textur (Gras) ausmalen
                    spriteBatch.Draw(texture, new Rectangle(offsetX, offsetY, (int)Camera.Scale, (int)Camera.Scale), tile.SourceRectangle, Color.White);
                }
            }
        }

        /// <summary>
        /// Rendert die Spielelemente der aktuellen Szene
        /// </summary>
        private void RenderItems(Area area, Point offset, GameTime gameTime)
        {
            // Items von hinten nach vorne rendern
            foreach (var item in area.Items.OrderBy(i => i.Position.Y))
            {
                // Renderer ermitteln und ggf. neu erzeugen
                ItemRenderer renderer;
                if (!itemRenderer.TryGetValue(item, out renderer))
                {
                    // ACHTUNG: Hier können potentiell neue Items nachträglich hinzu kommen zu denen die Textur noch fehlt
                    // Das muss geprüft und ggf nachgeladen werden.
                    Texture2D texture = itemTextures[item.Texture];

                    if (item is Character)
                        renderer = new CharacterRenderer(item as Character, Camera, texture);
                    else
                        renderer = new SimpleItemRenderer(item, Camera, texture);

                    itemRenderer.Add(item, renderer);
                }

                // Item rendern
                renderer.Draw(spriteBatch, offset, gameTime);
            }

            // TODO: Nicht mehr verwendete Renderer entfernen
        }
    }
}
