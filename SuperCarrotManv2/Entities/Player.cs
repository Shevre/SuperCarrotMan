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
        float baseSpeed = 4f, runSpeed = 8f,baseAccelVal = 0.3f,runAccelVal = 0.5f;
        float maxVelocity,accelVal;
        //float TexSizeOffsetRatioY = 0.8203125f;
        //bool downPressed = false;
        //bool COMPRESSED = false;
        bool Running = false;
        bool GoingLeft = false;
        int AnimCounter = 0;
        Core.AnimationSet RunSet;
        Vector2 runOffset;


        public Player(Vector2 position, Vector2 collisionBox, Core.AnimationSet animationSet,Core.AnimationSet runAnimation, bool gravAffected = true) : base(position, collisionBox, animationSet, gravAffected)
        {
            maxVelocity = baseSpeed;
            type = Core.CollisionObjectTypes.Player;
            currentDrawHeight = animationSet.Height;
            StandardTextureOffset = getTextureOffset();
            RunSet = runAnimation;
            
        }
        public Player(Vector2 position, Vector2 collisionBox, Core.AnimationSet animationSet, Core.AnimationSet runAnimation, Vector2 textureOffset, bool gravAffected = true) : base(position, collisionBox, animationSet, textureOffset, gravAffected)
        {
            maxVelocity = baseSpeed;
            type = Core.CollisionObjectTypes.Player;
            currentDrawHeight = animationSet.Height;
            StandardTextureOffset = getTextureOffset();
            runOffset = new Vector2(getTextureOffset().X + 9, getTextureOffset().Y);
            RunSet = runAnimation;

        }
        Vector2 StandardTextureOffset;

        public override void Update()
        {
            if (TouchingFloor) jumped = false;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                maxVelocity = runSpeed;
                accelVal = runAccelVal;
                Running = Velocity.X != 0f;
                //if(Running)SetTextureOffset(runOffset);
            }
            else
            {
                maxVelocity = baseSpeed;
                accelVal = baseAccelVal;
                Running = false;
                //SetTextureOffset(StandardTextureOffset);
            }
            //if (Keyboard.GetState().IsKeyDown(Keys.Down))
            //{
            //    currentDrawHeight = 64;
            //    if (!downPressed)
            //    {
            //        setYPosition(Position.Y + 52.5f);
            //        downPressed = true;
            //    }
            //    SetTextureOffset(new Vector2(StandardTextureOffset.X, StandardTextureOffset.Y / 2));
            //    //AdjustTextureOffset(0, 1f);
            //    SetCollisionBox(getCollisionBox().X, 52.5f);
            //    COMPRESSED = true;
            //}
            //else
            //{
            //    if (downPressed)
            //    {
            //        setYPosition(Position.Y - 52.5f);
            //        downPressed = false;
            //    }
            //    currentDrawHeight = 128;
            //    SetTextureOffset(new Vector2(StandardTextureOffset.X, StandardTextureOffset.Y));
            //    SetCollisionBox(getCollisionBox().X, 105f);
            //    COMPRESSED = false;
            //}
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
            if(Velocity.X != 0f && Running)
            {
                AnimCounter++;
                if (AnimCounter == 6)
                {
                    RunSet.Advance();
                    AnimCounter = 0;
                }
            }
            else if (Velocity.X != 0f)
            {
                AnimCounter++;
                if (AnimCounter == 6)
                {
                    AnimationSet.Advance();
                    AnimCounter = 0;
                }
            }
            else AnimationSet.Reset();
            Game1.DebugHandler.Log($"player1 velocity: {Velocity} position: {Position}");


            base.Update();
        }
        public new void Draw(SpriteBatch spriteBatch)
        {
            if (GoingLeft && Running) base.Draw(spriteBatch,RunSet, SpriteEffects.FlipHorizontally);
            else if (Running) base.Draw(spriteBatch,RunSet);
            else if (GoingLeft) base.Draw(spriteBatch, SpriteEffects.FlipHorizontally);
            else base.Draw(spriteBatch);
        }
    }
}
