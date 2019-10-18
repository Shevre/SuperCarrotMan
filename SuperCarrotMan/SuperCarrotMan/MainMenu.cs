using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shev.monoGameUI;

namespace SuperCarrotMan
{
    class MainMenu
    {
        Button StartButton;
        Button CloseButton;
        
        public MainMenu(int ScreenWidth, int ScreenHeight,ContentManager content) 
        {
            ButtonTextures buttonTextures = new ButtonTextures(content.Load<Texture2D>("Button"),content.Load<Texture2D>("Button_P"),3,1);
            StartButton = new Button("StartButton",buttonTextures, new Vector2(ScreenWidth / 2, 200), 420, 70, "Start",content.Load<SpriteFont>("ButtonText"),TextAllign.Center);
            CloseButton = new Button("CloseButton",buttonTextures, new Vector2(ScreenWidth / 2, 280), 420, 70, "Exit",content.Load<SpriteFont>("ButtonText"),TextAllign.Right);

        }

        public void Update() 
        {
            StartButton.Update();
            CloseButton.Update();
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            StartButton.Draw(spriteBatch);
            CloseButton.Draw(spriteBatch);
        }

        
    }

    
}

