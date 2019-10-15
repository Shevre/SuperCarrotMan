using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperCarrotMan
{
    class MainMenu
    {
        Button StartButton;
        Button CloseButton;
        
        public MainMenu(int ScreenWidth, int ScreenHeight,ContentManager content) 
        {
            ButtonTextures buttonTextures = new ButtonTextures(content);
            StartButton = new Button(buttonTextures, new Vector2(ScreenWidth / 2, 200),"Start");
            CloseButton = new Button(buttonTextures, new Vector2(ScreenWidth / 2, 270), "Exit");

        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            StartButton.Draw(spriteBatch);
            CloseButton.Draw(spriteBatch);
        }
    }

    class Button
    {
        enum State { Default, Pressed }
        State state = State.Default;
        

        int Width, Height;

        Vector2 Position;

        public bool ButtonClicked = false;

        ButtonTextures buttonTextures;

        public Button(ButtonTextures buttonTextures,Vector2 pos,string Text)
        {

            this.buttonTextures = buttonTextures;
            Width =  buttonTextures.Default.Width;
            Height = buttonTextures.Default.Height;
            Position = new Vector2(pos.X - (Width / 2), pos.Y - (Height / 2));

        }

        public void Update()
        {
            if (Mouse.GetState().X > Position.X && Mouse.GetState().X < Position.X + Width && Mouse.GetState().Y > Position.Y && Mouse.GetState().Y < Position.Y + Height)
            {
                state = State.Pressed;
            }
            else
            {
                state = State.Default;
            }
            if (state == State.Pressed && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                ButtonClicked = true;
            }
            else
            {
                ButtonClicked = false;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (state == State.Default)
            {
                spriteBatch.Draw(buttonTextures.Default, Position, Color.White);

            }
            else if(state == State.Pressed) 
            {
                spriteBatch.Draw(buttonTextures.Pressed, Position, Color.White);
            }

        }
    }

    struct ButtonTextures 
    {
        public Texture2D Default, Pressed;
        public ButtonTextures(ContentManager content) 
        {
            Default = content.Load<Texture2D>("Button");
            Pressed = content.Load<Texture2D>("Button_P");
        }
    }
}

