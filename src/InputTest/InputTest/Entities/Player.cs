﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InputTest.Entities
{
    class Player : GameEntity
    {
        int Number;

        int height = 64;

        int width = 8;

        Color color;

        Rectangle body;

        public int x { get; set; }

        public int y { get; set; }

        public int defaultMovement = 32;

        public string moveDirection { get; set; }

        public int moveSpeed { get; set; }

        public float time { get; set; }

        public int speed { get; set; } 

        public Player(int playerNumber, Color color, int? startx, int? starty, int? velocity, int? movement)
        {
            this.Number = playerNumber;
            this.color = color;
            // horizontal start position
            if(startx is int) {
                this.x = (int)startx;
            }
            // vertical start position
            if(starty is int) {
                this.y = (int)starty;
            }
            // How often does this update?
            if(velocity is int) {
                this.speed = (int)velocity;
            }

            // howw many pixels does it move?
            if(movement is int) {
                this.moveSpeed = (int)movement;
            }
            else {
                this.moveSpeed = this.defaultMovement;
            }            
        }

        public override void GameTick(float millisecondsElapsed, KeyboardState keyState)
        {
            this.time = this.time + millisecondsElapsed;
            moveDirection = "";

            if(this.Number == 1)
            {
                if (keyState.IsKeyDown(Keys.W))
                {
                    moveDirection = "UP";
                }
                else if (keyState.IsKeyDown(Keys.S))
                {
                    moveDirection = "DOWN";
                }
            }
            else if(this.Number == 2)
            {
                if (keyState.IsKeyDown(Keys.Up))
                {
                    moveDirection = "UP";
                }
                else if (keyState.IsKeyDown(Keys.Down))
                {
                    moveDirection = "DOWN";
                }
            }
            
            if (this.time > this.speed)
            {
                switch (moveDirection)
                {
                    case "UP":
                        this.y = y - speed;
                        break;
                    case "DOWN":
                        this.y = y + speed;
                        break;
                    default:
                        // do nothing?
                        break;
                }
                /*
                if (this.moveDirection == "ACROSS")
                {
                    if (this.x > 512 - moveSpeed)
                    {
                        this.x = 0;
                        this.y = y + 32;
                    }
                    else
                    {
                        this.x = x + moveSpeed;
                    }
                    if (this.y > 512 - 32)
                    {
                        this.y = 0;
                    }
                }
                else
                {
                    if (this.y > 512 - moveSpeed)
                    {
                        this.y = 0;
                        this.x = x + 32;
                    }
                    else
                    {
                        this.y = y + moveSpeed;
                    }
                    if (this.x > 512 - 32)
                    {
                        this.x = 0;
                    }
                }
                */
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