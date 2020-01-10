using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperCarrotManv2.Entities
{
    public class Player : Entity
    {
        public Player(Vector2 position, Vector2 collisionBox, Texture2D texture, bool gravAffected = true) : base(position, collisionBox, texture, gravAffected)
        {

        }
        public Player(Vector2 position, Vector2 collisionBox, Texture2D texture, Vector2 textureOffset, bool gravAffected = true) : base(position, collisionBox, texture, textureOffset, gravAffected)
        {
        }
        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Velocity.X += -0.1f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Velocity.X += 0.1f;
            }
            else Velocity.X = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) 
            {
                Velocity.Y += -0.5f;
                TouchingFloor = false;
            }
            if (getPosition().Y > 300) setPosition(new Vector2(getPosition().X, -100));
            base.Update();
        }
    }
}
