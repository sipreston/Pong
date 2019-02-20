using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InputTest.Entities
{
    class Ball : GameEntity
    {
        int width = 16;
        int height = 16;

        int x;
        int y;

        int velocity;

        string moveDirection;

        Color color;

        public float time { get; set; }

        public int speed { get; set; } 
        public Ball(Color color, int startx, int starty, int velocity)
        {
            this.color = color;
            this.x = startx;
            this.y = starty;
            this.velocity = velocity;
        }

        public override void GameTick(float millisecondsElapsed)
        {
            this.time = this.time + millisecondsElapsed;
            if (this.time > this.speed)
            {
                switch (this.moveDirection)
                {
                    case "UP":
                        break;
                    case "DOWN":
                        break;
                }
                
                this.time = 0;
            }
        }
        public override void Draw(GraphicsDevice graphicsDevice)
        {
            var spriteBatch = new SpriteBatch(graphicsDevice);
            Texture2D pixel = new Texture2D(graphicsDevice, 1, 1);
            Color[] colorData = { Color.White, };
            pixel.SetData<Color>(colorData);

            float rotation = 0;
            var origin = new Vector2();
            var effects = new SpriteEffects();
            float z = 0;

            spriteBatch.Begin();
            spriteBatch.Draw(
                pixel, 
                new Rectangle(
                    this.x, 
                    this.y, 
                    this.width, 
                    this.height
                ), 
                null, 
                GetColor(), 
                rotation, 
                origin, 
                effects, 
                z
            );
            spriteBatch.End();

        }

        public Color GetColor()
        {
            return this.color;
        }

        public void SetRandomStart(bool startx = false)
        {
            System.Threading.Thread.Sleep(10);
            var r = new Random();
            var ranVal = r.Next(0, 16);
            if (startx)
            {
                this.x = this.width * ranVal;
            }
            else
            {
                this.y = this.height * ranVal;
            }

        }
    }
}
