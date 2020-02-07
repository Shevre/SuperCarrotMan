

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.XNA;

namespace SuperCarrotManv2.Core
{
    public class VecRectangle
    {
        public float X, Y;
        public float Width, Height;
        public float Bottom, Right;
        private Rectangle Rectangle;
        public Rectangle GetIntRectangle()
        {
            
            return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
        }
        public VecRectangle(Vector2 position, Vector2 size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.X;
            Height = size.Y;
            Bottom = Y + Height;
            Right = X + Width;
           
        }

        public VecRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Bottom = Y + Height;
            Right = X + Width;
        }

        public void setXPosition(float x) 
        {
            X = x;
            Right = X + Width;
            
        }
        public void setYPosition(float y)
        {
            Y = y;
            Bottom = Y + Height;

        }
        public void setWidth(float w)
        {
            Width = w;
            Right = X + Width;

        }
        public void setHeight(float h)
        {
            Height = h;
            Bottom = Y + Height;

        }
        public void AddX(float x) 
        {
            X += x;
            Right = X + Width;
        }
        public void AddY(float y)
        {
            Y += y;
            
            Bottom = Y + Height;
        }
        public void addWidth(float w)
        {
            Width += w;
            Right = X + Width;

        }
        public void addHeight(float h)
        {
            Height += h;
            Bottom = Y + Height;

        }

        public bool Intersects(VecRectangle target) 
        {
            bool y = ((target.Y < Y && target.Bottom > Y) || (target.Y < Bottom && target.Bottom > Bottom) || (target.Y > Y && target.Bottom < Bottom));
            bool x = ((target.X < X && target.Right > X) || (target.X < Right && target.Right > Right) || (target.X > X && target.Right < Right));
            return (x && y);
        }

        public void Draw(SpriteBatch spriteBatch,Color color) 
        {
            spriteBatch.DrawRectangle(new Rectangle((int)X, (int)Y, (int)Width, (int)Height), color);
        }
    }

}
