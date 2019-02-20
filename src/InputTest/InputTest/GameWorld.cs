using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using InputTest.Entities;

namespace InputTest
{
    class GameWorld
    {
        GraphicsDeviceManager graphics;
        public List<GameEntity> GameEntities { get; set; }
        public List<Player> Players { get; set; }
        public Stopwatch GameTimer { get; set; }

        private TimeSpan previousGameTick;

        int tileSize = 32;
        int gridSize = 16;

        private int height;
        private int width;

        //public int Height { get; set; }

        public int Height
        {
            get { return this.height; }            
        }
        public int Width
        {
            get { return this.width; }
        }

        float z = 1;

        Color lastColor;
        Color mainColor = Color.CornflowerBlue;
        Color secondColor = Color.White;

        public float ElapsedMillisecondsSinceLastTick
        {
            get
            {
                return (float)(GameTimer.Elapsed - previousGameTick).TotalMilliseconds;
            }
        }


        public GameWorld()
        {
            this.GameEntities = new List<GameEntity>();
            this.Players = new List<Player>();

            this.GameTimer = new Stopwatch();
            this.GameTimer.Start();

            this.height = this.tileSize * this.gridSize;
            this.width = this.tileSize * this.gridSize;

            var player1 = new Player(this, 1, Color.Black, 16, 64, 50, 2);
            var player2 = new Player(this, 2, Color.Red, 496, 128, 50, 2);
            var ball    = new Ball  (this,    Color.Black, 256, 256, 4);

            this.Players.Add(player1);
            this.Players.Add(player2);
            this.GameEntities.Add(ball);
        }

        //public void AddEntity(GameEntity entity)
        //{
        //    if(entity.GetType() == typeof(Player)) {
        //        this.Players.Add(entity);
        //    }
        //    else {
        //        this.GameEntities.Add(entity);
        //    }
        //}

        public void GameTick(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            KeyboardState keyState = Keyboard.GetState();

            foreach(var player in Players)
            {
                player.GameTick(ElapsedMillisecondsSinceLastTick, keyState);
            }

            foreach (var entity in GameEntities)
            {
                if(entity.GetType() == typeof(Ball)) {
                    entity.GameTick(ElapsedMillisecondsSinceLastTick);
                }
            }

            previousGameTick = GameTimer.Elapsed;
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.Black);

            var spriteBatch = new SpriteBatch(graphicsDevice);
            Texture2D pixel = new Texture2D(graphicsDevice, 1, 1);
            Color[] colorData = { Color.White, };
            pixel.SetData<Color>(colorData);
            float rotation = 0;
            var origin = new Vector2();
            var effects = new SpriteEffects();

            var startColor = mainColor;
            // Draw the grid.
            for (int y = 0; y < this.gridSize; ++y)
            {
                Color color = GetColor();
                for (int x = 0; x < this.gridSize; ++x)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(
                        pixel, 
                        new Rectangle(
                            x * this.tileSize, 
                            y * this.tileSize, 
                            this.tileSize, 
                            this.tileSize
                        ), 
                        null, 
                        GetColor(), 
                        rotation, 
                        origin, 
                        effects, 
                        this.z
                    );
                    spriteBatch.End();
                }
            }
            foreach(var player in Players)
            {
                player.Draw(graphicsDevice);
            }

            foreach (var entity in GameEntities)
            {
                entity.Draw(graphicsDevice);
            }
        }

        protected Color GetColor()
        {
            if (lastColor.Equals(mainColor))
            {
                lastColor = secondColor;
                return secondColor;
            }
            lastColor = mainColor;
            return mainColor;
        }

        protected Color SwitchColor(Color color)
        {
            if (color.Equals(this.mainColor))
            {
                return this.secondColor;
            }
            return mainColor;
        }
    }
}
