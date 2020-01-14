using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperCarrotManv2.Core
{
    public enum CollisionObjectTypes { Generic,Terrain,Player }
    public class CollisionObject : GameObject {

        private Vector2 Position;
        public void setPosition(Vector2 pos) 
        {
            Position = pos;
        }
        public Vector2 getPosition() => Position;
        private Vector2 CollisionBox;
        public Vector2 getCollisionBox() => CollisionBox;
        public bool GravAffected = true;
        public bool TouchingFloor = false;
        public Vector2 Velocity = new Vector2();
        public CollisionObjectTypes type = CollisionObjectTypes.Generic;

        public VecRectangle GetVecRectangle() => new VecRectangle(Position, CollisionBox);


        public CollisionObject(Vector2 position,Vector2 collisionBox,bool gravAffected = true) 
        {
            Position = position;
            CollisionBox = collisionBox;
            GravAffected = gravAffected;
        }

        public override void Update() 
        {
            
            
            
            base.Update();
        }

        public void ApplyYVelocity() 
        {
            Position.Y += Velocity.Y;
        }

        public void ApplyXVelocity() 
        {
            Position.X += Velocity.X;
        }

        
        
    }

    public class TexturedCollisionObject : CollisionObject,Drawable {

        private Texture2D Texture;
        private Vector2 TextureOffset = new Vector2(0,0);
        public TexturedCollisionObject(Vector2 position, Vector2 collisionBox, Texture2D texture,bool gravAffected = true) : base(position, collisionBox,gravAffected) 
        {
            Texture = texture;
        }

        public TexturedCollisionObject(Vector2 position, Vector2 collisionBox, Texture2D texture,Vector2 textureOffset, bool gravAffected = true) : base(position, collisionBox, gravAffected)
        {
            Texture = texture;
            TextureOffset = textureOffset;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, getPosition() + TextureOffset, Color.White);
            base.Draw(spriteBatch);
        }
    }
}
