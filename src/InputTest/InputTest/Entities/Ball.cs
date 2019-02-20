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

        Color color;

        Rectangle body;

        GameWorld gameWorld;

        string moveDirection;

        public float time { get; set; }

        public int speed { get; set; } 
        public Ball(GameWorld gameWorld, Color color, int startx, int starty, int velocity)
        {
            this.moveDirection = "LEFT"; // To begin with all games will start with it going left. But we'll want to randomise this later.

            this.gameWorld = gameWorld;
            this.color = color;
            this.x = startx;
            this.y = starty;
            this.velocity = velocity;

            this.body = new Rectangle(
                this.x,
                this.y,
                this.width,
                this.height
            );

            this.speed = 2;
        }

        public override void GameTick(float millisecondsElapsed)
        {
            this.time = this.time + millisecondsElapsed;

            if (this.time > this.speed)
            {
                foreach(var player in this.gameWorld.Players)
                {
                    if (this.body.Intersects(player.Body))
                    {
                        this.SwitchDirection();
                    }

                    if(this.moveDirection == "LEFT")
                    {
                        this.x--;
                    }
                    else
                    {
                        this.x++;
                    }
                }
            }
            
           
           

            body.X = this.x;
            body.Y = this.y;
            body.Width = this.width;
            body.Height = this.height;
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
                body,
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

        public void SwitchDirection()
        {
            if(this.moveDirection == "LEFT")
            {
                this.moveDirection = "RIGHT";
            }
            else
            {
                this.moveDirection = "LEFT";
            }
        }
    }
}
