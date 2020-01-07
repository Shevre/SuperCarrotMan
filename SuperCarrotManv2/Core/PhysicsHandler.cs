

using Microsoft.Xna.Framework;
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
            foreach (CollisionObject cObj in CollisionObjects)
            {
                if (cObj.GravAffected) cObj.Velocity.Y += GravIntensity;
                
                if (cObj.Velocity != new Vector2(0, 0) && cObj.GravAffected)
                {
                    Rectangle colliderRect = cObj.GetRectangle();
                    foreach (CollisionObject cObj_ in CollisionObjects)
                    {
                        if(cObj != cObj_) 
                        {
                            Rectangle collideeRect = cObj_.GetRectangle();
                            collideeRect.Y += GravIntensity;
                            if (colliderRect.Intersects(collideeRect))
                            {
                                Console.WriteLine("Interacting!");
                                if(colliderRect.Y < collideeRect.Y) 
                                {
                                    cObj.TouchingFloor = true;
                                    cObj.setPosition(new Vector2(colliderRect.X,collideeRect.Y - colliderRect.Height ));
                                }
                                    
                            }
                            else 
                            {
                                cObj.TouchingFloor = false;             
                            }
                        }
                    }
                    cObj.Update();
                }
                
            }
        }
    }
}
