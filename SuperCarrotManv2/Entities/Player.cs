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
        public bool jumped = false;
        public int jumpCounter = 0;
        public int MaxJump = 10;
        int currentDrawHeight;
        float baseSpeed = 3f, runSpeed = 4f,accelVal = 0.3f;
        float maxVelocity;
        float TexSizeOffsetRatioY = 0.8203125f;
        bool downPressed = false;
        bool COMPRESSED = false;
        bool GoingLeft = false;
        int AnimCounter = 0;

        public Player(Vector2 position, Vector2 collisionBox, Core.AnimationSet animationSet, bool gravAffected = true) : base(position, collisionBox, animationSet, gravAffected)
        {
            maxVelocity = baseSpeed;
            type = Core.CollisionObjectTypes.Player;
            currentDrawHeight = animationSet.Height;
            StandardTextureOffset = getTextureOffset();
            
        }
        public Player(Vector2 position, Vector2 collisionBox, Core.AnimationSet animationSet, Vector2 textureOffset, bool gravAffected = true) : base(position, collisionBox, animationSet, textureOffset, gravAffected)
        {
            maxVelocity = baseSpeed;
            type = Core.CollisionObjectTypes.Player;
            currentDrawHeight = animationSet.Height;
            StandardTextureOffset = getTextureOffset();
            

        }
        Vector2 StandardTextureOffset;
        
        public override void Update()
        {
            if (TouchingFloor) jumped = false;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) maxVelocity = runSpeed;
            else maxVelocity = baseSpeed;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                currentDrawHeight = 64;
                if (!downPressed)
                {
                    setYPosition(Position.Y + 52.5f);
                    downPressed = true;
                }
                SetTextureOffset(new Vector2(StandardTextureOffset.X, StandardTextureOffset.Y / 2));
                //AdjustTextureOffset(0, 1f);
                SetCollisionBox(getCollisionBox().X, 52.5f);
                COMPRESSED = true;
            }
            else
            {
                if (downPressed)
                {
                    setYPosition(Position.Y - 52.5f);
                    downPressed = false;
                }
                currentDrawHeight = 128;
                SetTextureOffset(new Vector2(StandardTextureOffset.X, StandardTextureOffset.Y));
                SetCollisionBox(getCollisionBox().X, 105f);
                COMPRESSED = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (Velocity.X > -maxVelocity) Velocity.X += -accelVal;
                else Velocity.X = -maxVelocity;
                GoingLeft = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (Velocity.X < maxVelocity) Velocity.X += accelVal;
                else Velocity.X = maxVelocity;
                GoingLeft = false;
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
                if(jumpCounter > MaxJump) 
                {
                    jumped = true;
                    
                    jumpCounter = 0;
                }
            }
            if (Velocity.X != 0f)
            {
                AnimCounter++;
                if (AnimCounter == 4)
                {
                    AnimationSet.Advance();
                    AnimCounter = 0;
                }
            }
            else AnimationSet.Reset();
            Game1.DebugHandler.Log($"player1 velocity: {Velocity} position: {Position}");


            base.Update();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (COMPRESSED && GoingLeft) spriteBatch.Draw(getTexture(), new Rectangle(GetVecRectangle().GetIntRectangle().X + (int)getTextureOffset().X, GetVecRectangle().GetIntRectangle().Y + (int)getTextureOffset().Y, 64, currentDrawHeight),getTexture().Bounds, Color.White,0f,Core.ExtentionMethods.getEmptyVector(),SpriteEffects.FlipHorizontally,0f);
            else if (COMPRESSED) spriteBatch.Draw(getTexture(), new Rectangle(GetVecRectangle().GetIntRectangle().X + (int)getTextureOffset().X, GetVecRectangle().GetIntRectangle().Y + (int)getTextureOffset().Y, 64, currentDrawHeight), Color.White);
            else if (GoingLeft) base.Draw(spriteBatch, SpriteEffects.FlipHorizontally);
            else base.Draw(spriteBatch);
        }
    }
}
