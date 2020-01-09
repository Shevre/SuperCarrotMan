

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SuperCarrotManv2.Core
{
    public class PhysicsHandler {
        List<CollisionObject> CollisionObjects = new List<CollisionObject>();
        private int GravIntensity;
        public PhysicsHandler(int gravIntensity) 
        {
            GravIntensity = gravIntensity;

        }

        public void AddCollisionObject(CollisionObject collisionObject) 
        {
            CollisionObjects.Add(collisionObject);
        }

        public void Update() 
        {
            if (Keyboard.GetState().IsKeyDown(Keys.O)) GravIntensity = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.P)) GravIntensity = 1;
            foreach (CollisionObject cObj in CollisionObjects)
            {
                if (cObj.GravAffected && !cObj.TouchingFloor)
                { 
                    cObj.Velocity.Y += GravIntensity;
                    if (GravIntensity == 0) cObj.Velocity.Y = 0;
                }
                
                
                if (cObj.Velocity != new Vector2(0,0) && cObj.GravAffected)
                {
                    Rectangle colliderRect = cObj.GetRectangle();
                    bool collided = false;
                    foreach (CollisionObject cObj_ in CollisionObjects)
                    {
                        if(cObj != cObj_) 
                        {
                            Rectangle collideeRect = cObj_.GetRectangle();
                            colliderRect.Y += GravIntensity;
                            if (colliderRect.Intersects(collideeRect))
                            {

                                Game1.DebugHandler.Log("Yes");
                                if (colliderRect.Y< collideeRect.Y) 
                                {
                                    collided = true;
                                    cObj.TouchingFloor = true;
                                    cObj.setPosition(new Vector2(colliderRect.X,collideeRect.Y - colliderRect.Height ));
                                }
                                    
                            }
                            
                        }
                    }
                    if (!collided) 
                    {
                        cObj.TouchingFloor = false;
                        
                    }
                    
                }
                
                cObj.Update();

            }
        }
    }
}
