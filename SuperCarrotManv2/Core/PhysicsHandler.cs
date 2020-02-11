

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SuperCarrotManv2.Core
{
    public class PhysicsHandler {
        List<CollisionObject> CollisionObjects = new List<CollisionObject>();
        public List<CollisionObject> GetCollisionObjects() => CollisionObjects;
        List<CollisionArea> collisionAreas = new List<CollisionArea>();
        public List<CollisionArea> GetCollisionAreas() => collisionAreas;
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
            collisionAreas.AddRange(scene.collisionAreas);
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

                
                Collider.Update();
                
                
                Collider.ApplyYVelocity();
                if (Collider.Velocity.Y != ExtentionMethods.getEmptyVector().Y)
                {
                    foreach (CollisionObject Collidee in CollisionObjects)
                    {
                        if(Collider != Collidee && Collidee.type != CollisionObjectTypes.Player && Collidee.type != CollisionObjectTypes.Entity) 
                            if (Collider.GetVecRectangle().Intersects(Collidee.GetVecRectangle())) 
                            {
                                Game1.DebugHandler.Log("yes ");
                                if(Collider.Velocity.Y > 0)
                                {
                                    
                                    
                                    Collider.TouchingFloor = true;
                                    Collider.Velocity.Y = 0f;
                                    Collider.setYPosition(Collidee.Position.Y - Collider.GetVecRectangle().Height);
                                }
                                else
                                {
                                    Collider.setYPosition(Collidee.GetVecRectangle().Bottom);
                                    Collider.Velocity.Y = 0f;
                                    if (Collider.type == CollisionObjectTypes.Player)
                                    {
                                        ((Entities.Player)Collider).jumpCounter = 0;

                                        ((Entities.Player)Collider).jumped = true;
                                    }
                                }
                            }
                    }
                    foreach(CollisionArea collisionArea in collisionAreas)
                    {
                        if (Collider.GetVecRectangle().Intersects(collisionArea))
                        {
                            foreach (CollisionObject Collidee in collisionArea.CollisionObjects)
                            {
                                if (Collider != Collidee && Collidee.type != CollisionObjectTypes.Player && Collidee.type != CollisionObjectTypes.Entity)
                                    if (Collider.GetVecRectangle().Intersects(Collidee.GetVecRectangle()))
                                    {
                                        Game1.DebugHandler.Log("yes ");
                                        if (Collider.Velocity.Y > 0)
                                        {


                                            Collider.TouchingFloor = true;
                                            Collider.Velocity.Y = 0f;
                                            Collider.setYPosition(Collidee.Position.Y - Collider.GetVecRectangle().Height);
                                        }
                                        else
                                        {
                                            Collider.setYPosition(Collidee.GetVecRectangle().Bottom);
                                            Collider.Velocity.Y = 0f;
                                            if (Collider.type == CollisionObjectTypes.Player)
                                            {
                                                ((Entities.Player)Collider).jumpCounter = 0;

                                                ((Entities.Player)Collider).jumped = true;
                                            }
                                        }
                                    }
                            }
                        }
                    }
                }
                
                Collider.ApplyXVelocity();
                if (Collider.Velocity.X != ExtentionMethods.getEmptyVector().X)
                {
                    foreach (CollisionObject Collidee in CollisionObjects)
                    {
                        if (Collider != Collidee && Collidee.type != CollisionObjectTypes.Player && Collidee.type != CollisionObjectTypes.Entity)
                            if (Collider.GetVecRectangle().Intersects(Collidee.GetVecRectangle()))
                            {
                                Game1.DebugHandler.Log("yesX ");
                                if (Collider.Velocity.X > 0)
                                {

                                    Game1.DebugHandler.Log("yesX2 ");
                                    
                                    Collider.Velocity.X = 0f;
                                    Collider.setXPosition(Collidee.Position.X - Collider.GetVecRectangle().Width);
                                }
                                else
                                {
                                    Collider.Velocity.X = 0f;
                                    Collider.setXPosition(Collidee.GetVecRectangle().Right);
                                }
                            }
                    }
                    foreach (CollisionArea collisionArea in collisionAreas)
                    {
                        if (Collider.GetVecRectangle().Intersects(collisionArea))
                        {
                            foreach (CollisionObject Collidee in collisionArea.CollisionObjects)
                            {
                                if (Collider != Collidee && Collidee.type != CollisionObjectTypes.Player && Collidee.type != CollisionObjectTypes.Entity)
                                    if (Collider.GetVecRectangle().Intersects(Collidee.GetVecRectangle()))
                                    {
                                        Game1.DebugHandler.Log("yesX ");
                                        if (Collider.Velocity.X > 0)
                                        {

                                            Game1.DebugHandler.Log("yesX2 ");

                                            Collider.Velocity.X = 0f;
                                            Collider.setXPosition(Collidee.Position.X - Collider.GetVecRectangle().Width);
                                        }
                                        else
                                        {
                                            Collider.Velocity.X = 0f;
                                            Collider.setXPosition(Collidee.GetVecRectangle().Right);
                                        }
                                    }
                            }
                        }
                    }

                }



            }
            


        }
    }

    public class CollisionArea : VecRectangle
    {
        public List<CollisionObject> CollisionObjects { private set; get; }
        public CollisionArea(List<CollisionObject> collisionObjects,VecRectangle boundingBox) : base(boundingBox.X,boundingBox.Y,boundingBox.Width,boundingBox.Height)
        {
            CollisionObjects = collisionObjects;
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
