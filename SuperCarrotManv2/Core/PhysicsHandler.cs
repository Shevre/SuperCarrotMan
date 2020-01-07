using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (cObj.GravAffected) cObj.Velocity.Y += GravIntensity;
                cObj.Update();
            }
        }
    }
}
