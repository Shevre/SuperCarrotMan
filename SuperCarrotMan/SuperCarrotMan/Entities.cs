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
        AnimationSet animSet;
        Vector2 position;
        public void draw(SpriteBatch spriteBatch,GameTime gameTime) 
        {
            spriteBatch.Draw(animSet.getFrame(gameTime), position, Color.White);
        }
        public void ChangePosition(Vector2 changeVector) 
        {
            position += changeVector;
        }
    }
}
