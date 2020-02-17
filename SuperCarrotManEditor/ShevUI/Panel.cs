using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shev.XNA.UI
{
    public class Panel : UIElement
    {
        public Panel(int Width, int Height, Point position, Color backgroundColor, GraphicsDevice device, OverflowMode overflowMode = OverflowMode.Contain) : base(Width, Height, position, backgroundColor, device, overflowMode)
        {
        }

        public void AddChild(UIElement uIElement)
        {
            Children.Add(uIElement);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            
        }

        public new void Update()
        {
        }
    }
}
