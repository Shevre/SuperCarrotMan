using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shev.XNA.UI
{
    public class Panel
    {
        public List<Panel> Children = new List<Panel>();
        public Color BackgroundColor;
        public Rectangle Bounds;
        public Texture2D WhitePixel;
        public OverflowMode OverFlowMode;
        public Panel(int Width, int Height, Point position, Color backgroundColor, GraphicsDevice device, OverflowMode overflowMode = OverflowMode.Contain)
        {
            Bounds = new Rectangle(position, new Point(Width, Height));
            BackgroundColor = backgroundColor;
            WhitePixel = device.getSingleWhitePixelTexture();
            OverFlowMode = overflowMode;
        }
        public void Draw(SpriteBatch spriteBatch, Panel parent = null)
        {
            switch (OverFlowMode)
            {
                case OverflowMode.Contain:
                    if (parent.Bounds.Intersects(Bounds)) ;
                    break;
                default:
                    spriteBatch.Draw(WhitePixel, Bounds, WhitePixel.Bounds, BackgroundColor);
                    break;
            }

            foreach (Panel item in Children)
            {
                item.Draw(spriteBatch, this);
            }
        }
        public void Update()
        {
            foreach (Panel item in Children)
            {
                item.Update();
            }
        }


        public void AddChild(Panel uIElement)
        {
            Children.Add(uIElement);
        }
    }
}
