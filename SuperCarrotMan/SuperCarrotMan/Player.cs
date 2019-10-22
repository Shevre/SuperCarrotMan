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
        PlayerKeyboardKeys playerKeyboardKeys;
        PlayerControllerButtons playerControllerButtons;
        AnimationSet animSetRun;

        public Player(Vector2 startPos, AnimationSet animSetWalk,AnimationSet animSetRun,Camera camera,float movementSpeed,PlayerKeyboardKeys keyboardKeys, PlayerControllerButtons controllerButtons) 
        {
            SetPosition(startPos);
            SetAnimSet(animSetWalk);
            this.camera = camera;
            playerControllerButtons = controllerButtons;
            playerKeyboardKeys = keyboardKeys;
            this.movementSpeed = movementSpeed;
            this.animSetRun = animSetRun;
        }

        public new void Update(GameTime gameTime) 
        {
            CheckMovement(gameTime);
            ApplyVelocity();
        }

        public new void Draw(SpriteBatch spriteBatch, GameTime gameTime) 
        {
            if (movementState == MovementStates.Walking)
            {
                if (direction == Direction.Right)
                {
                    spriteBatch.Draw(animSet.getFrame(gameTime), position, Color.White);
                }
                else 
                {
                    spriteBatch.Draw(animSet.getFrame(gameTime), position, null, null,null,0, null, null, SpriteEffects.FlipHorizontally, 0);
                }
                
            }
            else if(movementState == MovementStates.Running)
            {
                if (direction == Direction.Right)
                {
                    spriteBatch.Draw(animSetRun.getFrame(gameTime), position, Color.White);
                }
                else
                {
                    spriteBatch.Draw(animSetRun.getFrame(gameTime), position, null, null, null, 0, null, null, SpriteEffects.FlipHorizontally, 0);
                }

                
            }
            else 
            {
                if (direction == Direction.Right)
                {
                    spriteBatch.Draw(animSet.getIdle(), position, Color.White);
                }
                else
                {
                    spriteBatch.Draw(animSet.getIdle(), position, null, null, null, 0, null, null, SpriteEffects.FlipHorizontally, 0);
                }

                
            }
        }
        #endregion

        #region Movement

        private void CheckMovement (GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(playerKeyboardKeys.Left)) SetVelocity(new Vector2(-movementSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds,velocity.Y  ));
            else if (Keyboard.GetState().IsKeyDown(playerKeyboardKeys.Right)) SetVelocity(new Vector2(movementSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds, velocity.Y));
            else SetVelocity(new Vector2(velocity.Y, 0));


            if (velocity.X != 0)
            {
                movementState = MovementStates.Walking;
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
