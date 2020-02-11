using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperCarrotManv2.Core
{

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
        public AnimationSet AnimationSet { private set; get; }
        public Texture2D getTexture() 
        {
            if (Animated) return AnimationSet.getCurrentFrame();
            else return Texture;
        }
        
        private Vector2 TextureOffset = new Vector2(0,0);
        public Vector2 getTextureOffset() => TextureOffset;
        bool Animated = false;
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
        public TexturedCollisionObject(Vector2 position, Vector2 collisionBox, AnimationSet animationSet, bool gravAffected = true) : base(position, collisionBox, gravAffected)
        {
            AnimationSet = animationSet;
            Animated = true;
        }
        public TexturedCollisionObject(Vector2 position, Vector2 collisionBox, AnimationSet animationSet, Vector2 textureOffset, bool gravAffected = true) : base(position, collisionBox, gravAffected)
        {
            AnimationSet = animationSet;
            TextureOffset = textureOffset;
            Animated = true;
        }

        public void Draw(SpriteBatch spriteBatch,SpriteEffects spriteEffects = SpriteEffects.None)
        {
            
            if (Animated)
                spriteBatch.Draw(AnimationSet.getCurrentFrame(), getPosition() + TextureOffset,AnimationSet.getCurrentFrame().Bounds, Color.White,0f,ExtentionMethods.getEmptyVector(),1f,spriteEffects,0f);
            else
                spriteBatch.Draw(Texture, getPosition() + TextureOffset,Color.White);

            base.Draw(spriteBatch);
        }
        public void Draw(SpriteBatch spriteBatch,AnimationSet animOverride, SpriteEffects spriteEffects = SpriteEffects.None)
        {

            if (Animated)
                spriteBatch.Draw(animOverride.getCurrentFrame(), getPosition() + TextureOffset, AnimationSet.getCurrentFrame().Bounds, Color.White, 0f, ExtentionMethods.getEmptyVector(), 1f, spriteEffects, 0f);
            else
                spriteBatch.Draw(Texture, getPosition() + TextureOffset, Color.White);

            base.Draw(spriteBatch);
        }
    }
}
