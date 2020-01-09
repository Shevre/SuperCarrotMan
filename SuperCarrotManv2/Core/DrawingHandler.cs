using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarrotManv2.Core
{
    public class DrawingHandler {

        List<Drawable> Drawables = new List<Drawable>();

        public DrawingHandler()
        {
        }

        public void AddDrawable(Drawable drawable)
        {
            Drawables.Add(drawable);
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            foreach(Drawable d in Drawables) 
            {
                d.Draw(spriteBatch);
                
            }
        }
    }
}
