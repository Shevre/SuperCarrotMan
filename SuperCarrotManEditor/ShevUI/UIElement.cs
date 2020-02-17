using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shev.XNA.UI
{
    public class UIElement
    {
        public List<UIElement> Children = new List<UIElement>();
        public Color BackgroundColor;
        public Rectangle Bounds;
        public Texture2D WhitePixel;
        public OverflowMode OverFlowMode;
        public UIElement(int Width, int Height, Point position, Color backgroundColor, GraphicsDevice device, OverflowMode overflowMode = OverflowMode.Contain)
        {
            Bounds = new Rectangle(position, new Point(Width, Height));
            BackgroundColor = backgroundColor;
            WhitePixel = device.getSingleWhitePixelTexture();
            OverFlowMode = overflowMode;
        }
        public void Draw(SpriteBatch spriteBatch,UIElement parent = null) 
        {
            spriteBatch.Draw(WhitePixel, Bounds, WhitePixel.Bounds, BackgroundColor);
            foreach (UIElement item in Children)
            {
                item.Draw(spriteBatch, this);
            }
        }
        public void Update(){ }
    }
}
