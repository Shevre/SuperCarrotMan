using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace SuperCarrotMan
{
    class Entity
    {
        public AnimationSet animSet { private set; get; }
        public void SetAnimSet(AnimationSet animSet) { this.animSet = animSet; }

        public Vector2 position { private set; get; }
        public void SetPosition(Vector2 position) { this.position = position; }

        public Vector2 velocity { private set; get; } = new Vector2(0, 0);
        public void SetVelocity(Vector2 velocity) { this.velocity = velocity; }
        public void ApplyVelocity() { position += velocity; }

        public float movementSpeed;

        public void Update(GameTime gameTime) 
        {
            position += velocity;
        }

        public void Draw(SpriteBatch spriteBatch,GameTime gameTime) 
        {
            spriteBatch.Draw(animSet.getFrame(gameTime), position, Color.White);
        }
        
    }
}

