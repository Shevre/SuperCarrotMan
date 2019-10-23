using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace SuperCarrotMan
{
    class Player : Entity
    {
        enum MovementStates {Idle,Walking,Running}
        enum Direction {Left,Right }
        #region Base Stuff
        Camera camera;
        
        MovementStates movementState = MovementStates.Idle;
        Direction direction = Direction.Right;
        bool jumping = false;
        
        public int width { private set; get; }
        public int height { private set; get; }
        public int Xoffset { private set; get; }
        public int Yoffset { private set; get; }

        PlayerKeyboardKeys playerKeyboardKeys;
        PlayerControllerButtons playerControllerButtons;
        AnimationSet animSetRun;

        float runningMultiplier = 1;

        public Player(Vector2 startPos, AnimationSet animSetWalk,AnimationSet animSetRun,Camera camera,float movementSpeed,PlayerKeyboardKeys keyboardKeys, PlayerControllerButtons controllerButtons, int WidthReduction = 0, int HeightReduction = 0,int Xoffset = 0,int Yoffset = 0) 
        {
            SetPosition(startPos);
            SetAnimSet(animSetWalk);
            this.camera = camera;
            playerControllerButtons = controllerButtons;
            playerKeyboardKeys = keyboardKeys;
            this.movementSpeed = movementSpeed;
            this.animSetRun = animSetRun;
            width = animSetWalk.getIdle().Width - WidthReduction;
            height = animSetWalk.getIdle().Height - HeightReduction;
            this.Xoffset = Xoffset;
            this.Yoffset = Yoffset;
            
        }

        public new void Update(GameTime gameTime,Gravity gravity) 
        {
            CheckMovement(gameTime,gravity);
            ApplyVelocity();
        }

        public new void Draw(SpriteBatch spriteBatch, GameTime gameTime,float Scale) 
        {
            if (movementState == MovementStates.Walking)
            {
                if (direction == Direction.Right)
                {
                    spriteBatch.Draw(animSet.getFrame(gameTime), position * Scale, null, Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
                }
                else 
                {
                    spriteBatch.Draw(animSet.getFrame(gameTime), position * Scale, null, Color.White, 0, new Vector2(0,0), Scale, SpriteEffects.FlipHorizontally, 0);
                }
                
            }
            else if(movementState == MovementStates.Running)
            {
                if (direction == Direction.Right)
                {
                    spriteBatch.Draw(animSetRun.getFrame(gameTime), position * Scale, null, Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(animSetRun.getFrame(gameTime), position * Scale, null, Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.FlipHorizontally, 0);
                }
                
                
            }
            else 
            {
                if (direction == Direction.Right)
                {
                    spriteBatch.Draw(animSet.getIdle(), position * Scale,null, Color.White,0,new Vector2(0,0),Scale,SpriteEffects.None,0);
                }
                else
                {
                    spriteBatch.Draw(animSet.getIdle(), position * Scale, null, Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.FlipHorizontally, 0);
                }

                
            }
        }
        #endregion

        #region Movement

        private void CheckMovement (GameTime gameTime,Gravity gravity)
        {
            if (Keyboard.GetState().IsKeyDown(playerKeyboardKeys.Run)) runningMultiplier = 1.5f;
            else runningMultiplier = 1;
            if (Keyboard.GetState().IsKeyDown(playerKeyboardKeys.Left)) SetVelocity(new Vector2(-movementSpeed * runningMultiplier * (float)gameTime.ElapsedGameTime.TotalMilliseconds,velocity.Y  ));
            else if (Keyboard.GetState().IsKeyDown(playerKeyboardKeys.Right)) SetVelocity(new Vector2(movementSpeed * runningMultiplier * (float)gameTime.ElapsedGameTime.TotalMilliseconds, velocity.Y));
            else SetVelocity(new Vector2(0, velocity.Y));

            SetVelocity(gravity.applyGravity(velocity, gameTime));

            if (Keyboard.GetState().IsKeyDown(playerKeyboardKeys.Jump)) SetVelocity(new Vector2(velocity.X, -0.25f * (float)gameTime.ElapsedGameTime.TotalMilliseconds));
            

            if (velocity.X != 0)
            {
                if (runningMultiplier > 1) movementState = MovementStates.Running;
                else movementState = MovementStates.Walking;

                if (velocity.X > 0)
                {
                    direction = Direction.Right;
                }
                else if(velocity.X < 0) 
                {
                    direction = Direction.Left;
                }
            }
            else 
            {
                movementState = MovementStates.Idle;
            }
        }

        #endregion
    }

}
