using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shev.XNA.UI
{
    public static class SinglePixel
    {
        static Texture2D WhitePixel;

        public static Texture2D getSingleWhitePixelTexture(this GraphicsDevice device)
        {
            if(WhitePixel != null)
            {
                return WhitePixel;
            }
            else
            {
                WhitePixel = new Texture2D(device, 1, 1);
                WhitePixel.SetData(new Color[] { Color.White });
                return WhitePixel;
            }
        }
    }
}