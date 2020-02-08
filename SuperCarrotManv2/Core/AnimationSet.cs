using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarrotManv2.Core
{
    public class AnimationSet
    {
        int CurrentIndex = 0;
        public int Height, Width;
        Texture2D[] Frames;
        public AnimationSet(Texture2D[] frames)
        {
            Frames = frames;
            Height = frames[0].Height;
            Width = frames[0].Width;
        }

        public Texture2D getCurrentFrame()
        {
            return Frames[CurrentIndex];
        }

        public void Advance()
        {
            if (!(CurrentIndex >= Frames.Length - 1))
                CurrentIndex++;
            else CurrentIndex = 0;
            
        }

        public void Reset()
        {
            CurrentIndex = 0;
        }
    }
}
