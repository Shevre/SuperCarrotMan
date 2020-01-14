

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SuperCarrotManv2.Core
{
    public class PhysicsHandler {
        List<CollisionObject> CollisionObjects = new List<CollisionObject>();
        private float GravIntensity;
        public PhysicsHandler(float gravIntensity) 
        {
            GravIntensity = gravIntensity;

        }

        public void AddCollisionObject(CollisionObject collisionObject) 
        {
            CollisionObjects.Add(collisionObject);
        }

        public void Update() 
        {
            foreach (CollisionObject cObj in CollisionObjects)
            {
                /*if (cObj.GravAffected && !cObj.TouchingFloor)
                { 
                    cObj.Velocity.Y += GravIntensity;
                    if (GravIntensity == 0) cObj.Velocity.Y = 0;
                }
                
                
                if (cObj.Velocity != new Vector2(0,0) && cObj.GravAffected)
                {
                    VecRectangle colliderRect = cObj.GetVecRectangle();
                    bool collidedT = false;
                    bool collidedR = false;
                    foreach (CollisionObject cObj_ in CollisionObjects)
                    {
                        if(cObj != cObj_) 
                        {
                            VecRectangle collideeRect = cObj_.GetVecRectangle();
                            colliderRect.Y += GravIntensity;
                            if (colliderRect.Intersects(collideeRect))
                            {

                                Game1.DebugHandler.Log("Yes");
                                if (colliderRect.Y < collideeRect.Y && !collidedT)
                                {
                                    collidedT = true;
                                    cObj.TouchingFloor = true;
                                    cObj.setPosition(new Vector2(cObj.getPosition().X, collideeRect.Y - colliderRect.Height));
                                }
                                else if (colliderRect.X > collideeRect.X && !collidedR)
                                {
                                    collidedR = true;
                                    cObj.setPosition(new Vector2(collideeRect.Right, cObj.getPosition().Y));
                                    
                                }
                                
                                    
                            }
                            
                        }
                    }
                    if (!collidedT) 
                    {
                        cObj.TouchingFloor = false;
                        
                    }
                    
                }
                
                cObj.Update();*/

                cObj.ApplyXVelocity();


                cObj.ApplyYVelocity();

            }
            

        }
    }
}
