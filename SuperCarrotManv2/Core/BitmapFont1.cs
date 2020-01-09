using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarrotManv2.Core
{
    public class BitmapFont
    {
        Texture2D FontTexture;
        Point FontSize;
        Point FontTextureDim;

        public BitmapFont(Texture2D fontTexture, Point fontSize, Point fontTextureDim)
        {
            FontTexture = fontTexture;
            FontSize = fontSize;
            FontTextureDim = fontTextureDim;
        }
    }
}
