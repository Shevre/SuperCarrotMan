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
        public PositionMode PositionMode;
        public bool Visible;
        public Panel(int Width, int Height, Point position, Color backgroundColor, bool visible = true,PositionMode positionMode = PositionMode.Absolute, OverflowMode overflowMode = OverflowMode.Contain)
        {
            Bounds = new Rectangle(position, new Point(Width, Height));
            BackgroundColor = backgroundColor;
            Visible = visible;
            PositionMode = positionMode;
        }
        public void Draw(SpriteBatch spriteBatch, Panel parent = null)
        {
            if(Visible)
            {
                if(parent != null)
                    switch (PositionMode)
                    {
                        case PositionMode.Absolute:
                            spriteBatch.Draw(SinglePixel.getSingleWhitePixelTexture(), Bounds, BackgroundColor);
                            break;
                        case PositionMode.Relative:
                            spriteBatch.Draw(SinglePixel.getSingleWhitePixelTexture(), new Rectangle(Bounds.Location + parent.Bounds.Location,Bounds.Size), BackgroundColor) ;
                            break;
                        default:
                            spriteBatch.Draw(SinglePixel.getSingleWhitePixelTexture(), Bounds, BackgroundColor);
                            break;
                    }
                else spriteBatch.Draw(SinglePixel.getSingleWhitePixelTexture(), Bounds, BackgroundColor);
                foreach (Panel item in Children)
                {
                    item.Draw(spriteBatch, this);
                }
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
