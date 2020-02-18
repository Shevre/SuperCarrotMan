using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shev.XNA.UI
{

    public class UIGame : Game 
    {
        public static GraphicsDevice GRAPHICS;
        protected override void Initialize()
        {
            GRAPHICS = GraphicsDevice;
            base.Initialize();
        }

        protected override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }

    }
}