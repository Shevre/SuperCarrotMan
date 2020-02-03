

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SuperCarrotManv2.Core
{
    public class PhysicsHandler {
        List<CollisionObject> CollisionObjects = new List<CollisionObject>();
        public List<CollisionObject> GetCollisionObjects() => CollisionObjects;
        private float GravIntensity;
        public PhysicsHandler(float gravIntensity) 
        {
            GravIntensity = gravIntensity;

        }

        public void AddCollisionObject(CollisionObject collisionObject) 
        {
            CollisionObjects.Add(collisionObject);
        }

        public void AddCollisionObject(Scene scene)
        {
            CollisionObjects.AddRange(scene.TileMap.CollisionObjects);
        }

        public void Update() 
        {
            foreach (CollisionObject Collider in CollisionObjects)
            {
                if (Collider.GravAffected)
                { 
                    Collider.Velocity.Y += GravIntensity;
                    if (GravIntensity == 0) Collider.Velocity.Y = 0;
                }

                /*
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
                */
                Collider.Update();

                
                Collider.ApplyYVelocity();
                if (Collider.Velocity.Y != ExtentionMethods.getEmptyVector().Y)
                {
                    foreach (CollisionObject Collidee in CollisionObjects)
                    {
                        if(Collider != Collidee) 
                            if (Collider.GetVecRectangle().Intersects(Collidee.GetVecRectangle())) 
                            {
                                Game1.DebugHandler.Log("yes ");
                                if(Collider.Position.Y - Collidee.GetVecRectangle().Y > Collider.Position.Y - Collidee.GetVecRectangle().Bottom)
                                {
                                    //Intersect doesnt get detected correctly?
                                    Game1.DebugHandler.Log("yes2 ");
                                    Collider.TouchingFloor = true;
                                    Collider.Velocity.Y = 0f;
                                    Collider.setYPosition(Collidee.Position.Y - Collider.GetVecRectangle().Height);
                                }
                                else
                                {
                                    Collider.setYPosition(Collidee.GetVecRectangle().Bottom); 
                                }
                            }
                    }
                }
                
                Collider.ApplyXVelocity();
                if (Collider.Velocity.X != ExtentionMethods.getEmptyVector().X)
                {

                }



            }
            

        }
    }

    public static partial class ExtentionMethods 
    {
        static Vector2 emptyVector;
        public static Vector2 getEmptyVector() 
        {
            if(emptyVector == null) 
            {
                emptyVector = new Vector2(0, 0);
                
            }
            return emptyVector;
        }
    }
}
