using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperCarrotManv2.Core
{
    public class CollisionObject : GameObject {

        private Vector2 Position;
        public Vector2 getPosition() => Position;
        private Vector2 CollisionBox;
        public Vector2 getCollisionBox() => CollisionBox;
        public bool GravAffected = true;
        public bool TouchingFloor = false;
        public Vector2 Velocity = new Vector2();
        public CollisionObject(Vector2 position,Vector2 collisionBox,bool gravAffected = true) 
        {
            Position = position;
            CollisionBox = collisionBox;
            GravAffected = gravAffected;
        }

        public override void Update() 
        {
            Position += Velocity;
            if (TouchingFloor) Velocity.Y = 0;
            base.Update();
        }
    }

    public class TexturedCollisionObject : CollisionObject,Drawable {

        private Texture2D Texture;
        public TexturedCollisionObject(Vector2 position, Vector2 collisionBox, Texture2D texture,bool gravAffected = true) : base(position, collisionBox,gravAffected) 
        {
            Texture = texture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, getPosition(), Color.White);
            base.Draw(spriteBatch);
        }
    }
}
