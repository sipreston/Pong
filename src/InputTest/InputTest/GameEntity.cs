using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InputTest
{
    public abstract class GameEntity
    {
        public Vector2 Position { get; set; }

        public Vector2 Velocity { get; set; }

        public virtual void Draw(GraphicsDevice graphicsDevice) { }

        public virtual void GameTick(float millisecondsElapsed) { }

        public virtual void GameTick(float millisecondsElapsed, KeyboardState keyState) { }
    }
}
