using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperCarrotManv2.Core
{
    public enum CollisionObjectTypes { Generic,Terrain,Player }
    public class CollisionObject : GameObject {

        public Vector2 Position;
        public void setPosition(Vector2 pos) 
        {
            Position = pos;
            vecRecangle.X = pos.X;
            vecRecangle.Y = pos.Y;
        }
        public void setXPosition(float X)
        {
            Position.X = X;
            vecRecangle.setXPosition(X);
        }
        public void setYPosition(float Y)
        {
            Position.Y = Y;
            vecRecangle.setYPosition(Y);
        }
        public void AdjustCollisionBox(float w,float h)
        {
            CollisionBox.X += w;
            vecRecangle.addWidth(w);
            CollisionBox.Y += h;
            vecRecangle.addHeight(h);
        }
        public void SetCollisionBox(float w, float h)
        {
            CollisionBox.X = w;
            vecRecangle.setWidth(w);
            CollisionBox.Y = h;
            vecRecangle.setHeight(h);
        }
        public Vector2 getPosition() => Position;
        private Vector2 CollisionBox;
        public Vector2 getCollisionBox() => CollisionBox;
        public bool GravAffected = true;
        public bool TouchingFloor = false;
        public Vector2 Velocity = new Vector2();
        public CollisionObjectTypes type = CollisionObjectTypes.Generic;
        private VecRectangle vecRecangle;
      
        public VecRectangle GetVecRectangle() => vecRecangle;
        

        public CollisionObject(Vector2 position,Vector2 collisionBox,bool gravAffected = true,CollisionObjectTypes collisionObjectType = CollisionObjectTypes.Generic) 
        {
            Position = position;
            CollisionBox = collisionBox;
            GravAffected = gravAffected;
            vecRecangle = new VecRectangle(position, collisionBox);
            type = collisionObjectType;
        }

        public override void Update() 
        {
            
            
            
            base.Update();
        }

        public void ApplyYVelocity() 
        {
            Position.Y += Velocity.Y;
            vecRecangle.AddY(Velocity.Y);
        }

        public void ApplyXVelocity() 
        {
            Position.X += Velocity.X;
            vecRecangle.AddX(Velocity.X);
        }


        
        
    }

    public class TexturedCollisionObject : CollisionObject,Drawable {

        private Texture2D Texture;
        public Texture2D getTexture() => Texture;
        private Vector2 TextureOffset = new Vector2(0,0);
        public Vector2 getTextureOffset() => TextureOffset;
        public void AdjustTextureOffset(float x, float y)
        {
            TextureOffset.X += x;
            TextureOffset.Y += y;
        }
        public void SetTextureOffset(Vector2 h)
        {
            TextureOffset = h;
        }
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
