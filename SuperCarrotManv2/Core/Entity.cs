using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperCarrotManv2.Core
{
    public class Entity : TexturedCollisionObject
    { 

        public Entity(Vector2 position, Vector2 collisionBox, Texture2D texture, bool gravAffected = true) : base(position, collisionBox, texture,gravAffected)
        {
            type = Core.CollisionObjectTypes.Entity;
        }
        public Entity(Vector2 position, Vector2 collisionBox, Texture2D texture,Vector2 textureOffset, bool gravAffected = true) : base(position, collisionBox, texture,textureOffset,gravAffected)
        {
            type = Core.CollisionObjectTypes.Entity;
        }
        public Entity(Vector2 position, Vector2 collisionBox, AnimationSet animationSet, bool gravAffected = true) : base(position, collisionBox, animationSet, gravAffected)
        {
            type = Core.CollisionObjectTypes.Entity;
        }
        public Entity(Vector2 position, Vector2 collisionBox, AnimationSet animationSet, Vector2 textureOffset, bool gravAffected = true) : base(position, collisionBox, animationSet, textureOffset, gravAffected)
        {
            type = Core.CollisionObjectTypes.Entity;
        }
    }
}
