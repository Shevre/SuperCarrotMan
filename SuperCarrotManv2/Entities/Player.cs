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
    public class Player : Core.Entity
    {
        bool jumped = false;
        int jumpCounter = 0;

        float baseSpeed = 3f, runSpeed = 4f,accelVal = 0.3f;
        float maxVelocity;
        public Player(Vector2 position, Vector2 collisionBox, Texture2D texture, bool gravAffected = true) : base(position, collisionBox, texture, gravAffected)
        {
            maxVelocity = baseSpeed;
            type = Core.CollisionObjectTypes.Player;
        }
        public Player(Vector2 position, Vector2 collisionBox, Texture2D texture, Vector2 textureOffset, bool gravAffected = true) : base(position, collisionBox, texture, textureOffset, gravAffected)
        {
            maxVelocity = baseSpeed;
            type = Core.CollisionObjectTypes.Player;
        }
        public override void Update()
        {
            if (TouchingFloor) jumped = false;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) maxVelocity = runSpeed;
            else maxVelocity = baseSpeed;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (Velocity.X > -maxVelocity) Velocity.X += -accelVal;
                else Velocity.X = -maxVelocity;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (Velocity.X < maxVelocity) Velocity.X += accelVal;
                else Velocity.X = maxVelocity;
            }
            else
            {
                if (0.1 > Velocity.X && Velocity.X > -0.1) Velocity.X = 0;
                else if (Velocity.X < 0) Velocity.X += 0.2f;
                else if (Velocity.X > 0) Velocity.X -= 0.2f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !jumped && Velocity.Y < 3f) 
            {
                Velocity.Y += -1f;
                TouchingFloor = false;
                jumpCounter++;
                if(jumpCounter > 8) 
                {
                    jumped = true;
                    
                    jumpCounter = 0;
                }
            }
            Game1.DebugHandler.Log($"player1 velocity: {Velocity}");
            base.Update();
        }
    }
}
