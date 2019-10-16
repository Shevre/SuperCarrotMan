using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shev.monoGameUI
{
    class Button
    {
        enum State { Default, Pressed }
        State state = State.Default;


        int Width, Height;

        Vector2 Position;

        public bool ButtonClicked = false;

        ButtonTextures buttonTextures;



        public Button(ButtonTextures buttonTextures, Vector2 pos, string Text, int Width, int Height) //min size is 6x6
        {

            this.buttonTextures = buttonTextures;
            this.Width = Width;
            this.Height = Height;
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
                spriteBatch.Draw(buttonTextures.Default, Position, new Rectangle(0, 0, 3, 3), Color.White);
                spriteBatch.Draw(buttonTextures.Default, new Vector2(Position.X + Width - 3, Position.Y), new Rectangle(4, 0, 3, 3), Color.White);
                spriteBatch.Draw(buttonTextures.Default, new Vector2(Position.X, Position.Y + Height - 3), new Rectangle(0, 4, 3, 3), Color.White);
                spriteBatch.Draw(buttonTextures.Default, new Vector2(Position.X + Width - 3, Position.Y + Height - 3), new Rectangle(4, 4, 3, 3), Color.White);

                for (int i = 3; i < Width - 3; i++)
                {
                    spriteBatch.Draw(buttonTextures.Default, new Vector2(Position.X + i, Position.Y), new Rectangle(3, 0, 1, 3), Color.White);
                    spriteBatch.Draw(buttonTextures.Default, new Vector2(Position.X + i, Position.Y + Height - 3), new Rectangle(3, 0, 1, 3), Color.White);
                }

                for (int i = 3; i < Height - 3; i++)
                {
                    spriteBatch.Draw(buttonTextures.Default, new Vector2(Position.X, Position.Y + i), new Rectangle(0, 3, 3, 1), Color.White);
                    spriteBatch.Draw(buttonTextures.Default, new Vector2(Position.X + Width - 3, Position.Y + i), new Rectangle(0, 3, 3, 1), Color.White);
                }

                for (int y = 3; y < Height - 3; y++)
                {
                    for (int x = 3; x < Width - 3; x++)
                    {
                        spriteBatch.Draw(buttonTextures.Default, new Vector2(Position.X + x, Position.Y + y), new Rectangle(3, 3, 1, 1), Color.White);
                    }
                }


            }
            else if (state == State.Pressed)
            {
                
                spriteBatch.Draw(buttonTextures.Pressed, Position, new Rectangle(0, 0, 3, 3), Color.White);
                spriteBatch.Draw(buttonTextures.Pressed, new Vector2(Position.X + Width - 3, Position.Y), new Rectangle(4, 0, 3, 3), Color.White);
                spriteBatch.Draw(buttonTextures.Pressed, new Vector2(Position.X, Position.Y + Height - 3), new Rectangle(0, 4, 3, 3), Color.White);
                spriteBatch.Draw(buttonTextures.Pressed, new Vector2(Position.X + Width - 3, Position.Y + Height - 3), new Rectangle(4, 4, 3, 3), Color.White);

                for (int i = 3; i < Width - 3; i++)
                {
                    spriteBatch.Draw(buttonTextures.Pressed, new Vector2(Position.X + i, Position.Y), new Rectangle(3, 0, 1, 3), Color.White);
                    spriteBatch.Draw(buttonTextures.Pressed, new Vector2(Position.X + i, Position.Y + Height - 3), new Rectangle(3, 0, 1, 3), Color.White);
                }

                for (int i = 3; i < Height - 3; i++)
                {
                    spriteBatch.Draw(buttonTextures.Pressed, new Vector2(Position.X, Position.Y + i), new Rectangle(0, 3, 3, 1), Color.White);
                    spriteBatch.Draw(buttonTextures.Pressed, new Vector2(Position.X + Width - 3, Position.Y + i), new Rectangle(0, 3, 3, 1), Color.White);
                }

                for (int y = 3; y < Height - 3; y++)
                {
                    for (int x = 3; x < Width - 3; x++)
                    {
                        spriteBatch.Draw(buttonTextures.Pressed, new Vector2(Position.X + x, Position.Y + y), new Rectangle(3, 3, 1, 1), Color.White);
                    }
                }
            }

        }
    }

    struct ButtonTextures
    {
        public Texture2D Default, Pressed;
        public int BorderSize, BodyTileSize;

        public ButtonTextures(Texture2D defaultTex,Texture2D pressedTex,int BorderSize,int BodyTileSize)
        {
            Default = defaultTex;
            Pressed = pressedTex;
            this.BorderSize = BorderSize;
            this.BodyTileSize = BodyTileSize;
        }
    }
}
