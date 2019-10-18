using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Shev.monoGameUI
{
    enum TextAllign {Center,Right,Left };
    enum ButtonTypes { CloseButton,BackButton,GenericButton }
    class UIElement 
    {
        public Vector2 Position;
        public string name;
        public int width, height;
        public Texture2D defaultTexture;

        public void Update() 
        {
            
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(defaultTexture, Position, Color.White);
        }
    }

    

    class Menu 
    {
        List<UIElement> uIElements = new List<UIElement>();
        List<Menu> subMenus = new List<Menu>();
        Menu parentMenu = null;
        public bool TopMenu = false;
        public Color backgroundColor;

        public Menu(Color backgroundColor,Menu parentMenu = null) 
        {
            this.backgroundColor = backgroundColor;

            if (parentMenu == null) TopMenu = true;
            else this.parentMenu = parentMenu;
        }

        public void Update(Game game) 
        {
            foreach (UIElement ui in uIElements)
            {
                if (ui.GetType() == typeof(Button))
                {
                    Button b = (Button)ui;
                    b.Update(game);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            foreach (UIElement ui in uIElements)
            {
                if(ui.GetType() == typeof(Button)) 
                {
                    Button b = (Button)ui;
                    b.Draw(spriteBatch);
                }
               
            }
        }

        public Button getButton(string name) 
        {
            foreach (Button b in uIElements)
            {
                if (b.name == name)
                {
                    return b;
                }
            }
            return null;
        }

        public void AddUIElement(UIElement ui) 
        {
            uIElements.Add(ui);
        }

        public void AddSubMenu(Menu menu) 
        {
            subMenus.Add(menu);
        }

    }

    class Button : UIElement
    {
        enum State { Default, Pressed }
        State state = State.Default;

        ButtonTypes type;

        public bool ButtonClicked = false;

        Texture2D pressedTexture;

        SpriteFont font;
        string text;
        TextAllign textAllign;

        public Button(string name,Texture2D defaultTexture, Texture2D pressedTexture, Vector2 pos, int width, int height, string Text,SpriteFont font,TextAllign textAllign,ButtonTypes buttonType = ButtonTypes.GenericButton) //min size is 6x6
        {
            this.name = name;
            this.defaultTexture = defaultTexture;
            this.pressedTexture = pressedTexture;
            this.width = width;
            this.height = height;
            Position = new Vector2(pos.X - (width / 2), pos.Y - (height / 2));
            this.font = font;
            this.text = Text;
            this.textAllign = textAllign;
            type = buttonType;




        }

        public void Update(Game game)
        {
            if (Mouse.GetState().X > Position.X && Mouse.GetState().X < Position.X + width && Mouse.GetState().Y > Position.Y && Mouse.GetState().Y < Position.Y + height)
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

                switch (type)
                {
                    case ButtonTypes.CloseButton:
                        game.Exit();
                        break;
                    case ButtonTypes.BackButton:
                        
                        break;
                    default:
                        break;
                }

            }
            else
            {
                ButtonClicked = false;
            }

        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            if (state == State.Default)
            {
                spriteBatch.Draw(defaultTexture, Position, new Rectangle(0, 0, 3, 3), Color.White);
                spriteBatch.Draw(defaultTexture, new Vector2(Position.X + width - 3, Position.Y), new Rectangle(4, 0, 3, 3), Color.White);
                spriteBatch.Draw(defaultTexture, new Vector2(Position.X, Position.Y + height - 3), new Rectangle(0, 4, 3, 3), Color.White);
                spriteBatch.Draw(defaultTexture, new Vector2(Position.X + width - 3, Position.Y + height - 3), new Rectangle(4, 4, 3, 3), Color.White);

                for (int i = 3; i < width - 3; i++)
                {
                    spriteBatch.Draw(defaultTexture, new Vector2(Position.X + i, Position.Y), new Rectangle(3, 0, 1, 3), Color.White);
                    spriteBatch.Draw(defaultTexture, new Vector2(Position.X + i, Position.Y + height - 3), new Rectangle(3, 0, 1, 3), Color.White);
                }

                for (int i = 3; i < height - 3; i++)
                {
                    spriteBatch.Draw(defaultTexture, new Vector2(Position.X, Position.Y + i), new Rectangle(0, 3, 3, 1), Color.White);
                    spriteBatch.Draw(defaultTexture, new Vector2(Position.X + width - 3, Position.Y + i), new Rectangle(0, 3, 3, 1), Color.White);
                }

                for (int y = 3; y < height - 3; y++)
                {
                    for (int x = 3; x < width - 3; x++)
                    {
                        spriteBatch.Draw(defaultTexture, new Vector2(Position.X + x, Position.Y + y), new Rectangle(3, 3, 1, 1), Color.White);
                    }
                }


            }
            else if (state == State.Pressed)
            {
                
                spriteBatch.Draw(pressedTexture, Position, new Rectangle(0, 0, 3, 3), Color.White);
                spriteBatch.Draw(pressedTexture, new Vector2(Position.X + width - 3, Position.Y), new Rectangle(4, 0, 3, 3), Color.White);
                spriteBatch.Draw(pressedTexture, new Vector2(Position.X, Position.Y + height - 3), new Rectangle(0, 4, 3, 3), Color.White);
                spriteBatch.Draw(pressedTexture, new Vector2(Position.X + width - 3, Position.Y + height - 3), new Rectangle(4, 4, 3, 3), Color.White);

                for (int i = 3; i < width - 3; i++)
                {
                    spriteBatch.Draw(pressedTexture, new Vector2(Position.X + i, Position.Y), new Rectangle(3, 0, 1, 3), Color.White);
                    spriteBatch.Draw(pressedTexture, new Vector2(Position.X + i, Position.Y + height - 3), new Rectangle(3, 0, 1, 3), Color.White);
                }

                for (int i = 3; i < height - 3; i++)
                {
                    spriteBatch.Draw(pressedTexture, new Vector2(Position.X, Position.Y + i), new Rectangle(0, 3, 3, 1), Color.White);
                    spriteBatch.Draw(pressedTexture, new Vector2(Position.X + width - 3, Position.Y + i), new Rectangle(0, 3, 3, 1), Color.White);
                }

                for (int y = 3; y < height - 3; y++)
                {
                    for (int x = 3; x < width - 3; x++)
                    {
                        spriteBatch.Draw(pressedTexture, new Vector2(Position.X + x, Position.Y + y), new Rectangle(3, 3, 1, 1), Color.White);
                    }
                }
            }
            switch (textAllign)
            {
                case TextAllign.Center:
                    spriteBatch.DrawString(font, text, new Vector2(Position.X + (width / 2) - (font.MeasureString(text).X / 2) ,Position.Y + (height / 2) - (font.MeasureString(text).Y / 2)), Color.Black);
                    break;
                case TextAllign.Right:
                    spriteBatch.DrawString(font, text, new Vector2(Position.X, Position.Y + (height / 2) - (font.MeasureString(text).Y / 2)), Color.Black);
                    break;
                case TextAllign.Left:
                    spriteBatch.DrawString(font, text, new Vector2(Position.X + width - font.MeasureString(text).X - 6, Position.Y + (height / 2) - (font.MeasureString(text).Y / 2)), Color.Black);
                    break;
                default:
                    break;
                    
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
