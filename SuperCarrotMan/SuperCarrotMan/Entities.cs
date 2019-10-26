using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace SuperCarrotMan
{
    public class Entity
    {
        public AnimationSet animSet { private set; get; }
        public void SetAnimSet(AnimationSet animSet) { this.animSet = animSet; }

        public Vector2 position { private set; get; }
        public void SetPosition(Vector2 position) { this.position = position; }

        public Vector2 velocity { private set; get; } = new Vector2(0, 0);
        public void SetVelocity(Vector2 velocity) { this.velocity = velocity; }
        public void ApplyVelocity() { position += velocity; }

        public float movementSpeed;

        public int width;
        public int height;
        public int Xoffset;
        public int Yoffset;

        public void Update(GameTime gameTime, Gravity gravity) 
        {
            position += velocity;
        }

        public void Draw(SpriteBatch spriteBatch,GameTime gameTime,float Scale) 
        {
            spriteBatch.Draw(animSet.getFrame(gameTime), position, null, Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
        }
        
    }
}

