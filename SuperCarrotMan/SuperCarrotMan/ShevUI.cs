using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Shev.monoGameUI
{
    enum TextAllign {Center,Right,Left };
    enum ButtonTypes { CloseButton,BackButton,GenericButton,MenuButton }
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
        public List<Menu> subMenus = new List<Menu>();
        public Menu parentMenu = null;
        public bool TopMenu = true;
        public Color backgroundColor;
        public int SelectedMenu = 0; //0 is the menu itself, the others represent submenus.

        public Menu(Color backgroundColor) 
        {
            this.backgroundColor = backgroundColor;
        }

        public void SetParentMenu(Menu menu) 
        {
            TopMenu = false;
            parentMenu = menu;
        }

        public void Update(Game game) 
        {
            if (SelectedMenu != 0)
            {
                subMenus[SelectedMenu - 1].Update(game);
            }
            else 
            {
                foreach (UIElement ui in uIElements)
                {
                    if (ui.GetType() == typeof(Button))
                    {
                        Button b = (Button)ui;
                        b.Update(game, this);
                    }
                }
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (SelectedMenu != 0)
            {
                subMenus[SelectedMenu - 1].Draw(spriteBatch);
            }
            else 
            {
                foreach (UIElement ui in uIElements)
                {
                    if (ui.GetType() == typeof(Button))
                    {
                        Button b = (Button)ui;
                        b.Draw(spriteBatch);
                    }
                    else 
                    {
                        ui.Draw(spriteBatch);
                    }

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
            menu.SetParentMenu(this);
            subMenus.Add(menu);
        }

        public override string ToString()
        {
            return "Menu";
        }
    }

    class Button : UIElement
    {
        enum State { Default, Down }
        State state = State.Default;

        ButtonTypes type;

        public bool ButtonClicked = false;

        Texture2D pressedTexture;

        SpriteFont font;
        string text;
        TextAllign textAllign;

        Menu NavigateTo;

        Texture2D image;

        public Button(string name,Texture2D defaultTexture, Texture2D pressedTexture, Vector2 pos, int width, int height, string Text,SpriteFont font,TextAllign textAllign = TextAllign.Center,ButtonTypes buttonType = ButtonTypes.GenericButton,Menu NavigateTo = null) //min size is 6x6
        {
            this.name = name;
            this.defaultTexture = defaultTexture;
            this.pressedTexture = pressedTexture;
            this.width = width;
            this.height = height;
            Position = pos;
            this.font = font;
            this.text = Text;
            this.textAllign = textAllign;
            type = buttonType;


            if (type == ButtonTypes.MenuButton)
            {
                this.NavigateTo = NavigateTo;
                Console.WriteLine(this.NavigateTo.ToString());
            }

        }
        public Button(string name, Texture2D defaultTexture, Texture2D pressedTexture, Vector2 pos, int width, int height,Texture2D image, TextAllign textAllign = TextAllign.Center, ButtonTypes buttonType = ButtonTypes.GenericButton, Menu NavigateTo = null) //min size is 6x6
        {
            this.name = name;
            this.defaultTexture = defaultTexture;
            this.pressedTexture = pressedTexture;
            this.width = width;
            this.height = height;
            Position = pos;
            
            this.textAllign = textAllign;
            type = buttonType;
            this.image = image;

            if (type == ButtonTypes.MenuButton)
            {
                this.NavigateTo = NavigateTo;
                Console.WriteLine(this.NavigateTo.ToString());
            }

        }
        private bool MouseHold = false;
        public void Update(Game game,Menu menu)
        {
            if (Mouse.GetState().X > Position.X && Mouse.GetState().X < Position.X + width && Mouse.GetState().Y > Position.Y && Mouse.GetState().Y < Position.Y + height)
            {
                state = State.Down;
            }
            else
            {
                state = State.Default;
            }
            if (state == State.Down && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                MouseHold = true;
            }
            else if(state == State.Down && Mouse.GetState().LeftButton == ButtonState.Released && MouseHold) 
            {
                ButtonClicked = true;

                switch (type)
                {
                    case ButtonTypes.CloseButton:
                        game.Exit();
                        break;
                    case ButtonTypes.BackButton:
                        if (!menu.TopMenu)
                        {
                            menu.parentMenu.SelectedMenu = 0;
                        }
                        break;
                    case ButtonTypes.MenuButton:

                        if (NavigateTo != null)
                        {
                            menu.SelectedMenu = menu.subMenus.IndexOf(NavigateTo) + 1;

                        }

                        break;
                    default:
                        break;
                        
                }
                MouseHold = false;
            }
            else
            {
                ButtonClicked = false;
                MouseHold = false;
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
            else if (state == State.Down)
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
                    if (text != null) spriteBatch.DrawString(font, text, new Vector2(Position.X + (width / 2) - (font.MeasureString(text).X / 2), Position.Y + (height / 2) - (font.MeasureString(text).Y / 2)), Color.Black);
                    
                    break;
                case TextAllign.Right:
                    if (text != null) spriteBatch.DrawString(font, text, new Vector2(Position.X, Position.Y + (height / 2) - (font.MeasureString(text).Y / 2)), Color.Black);
                    break;
                case TextAllign.Left:
                    if (text != null) spriteBatch.DrawString(font, text, new Vector2(Position.X + width - font.MeasureString(text).X - 6, Position.Y + (height / 2) - (font.MeasureString(text).Y / 2)), Color.Black);
                    break;
                default:
                    break;
                    
            }
            if (image != null) spriteBatch.Draw(image, new Vector2(Position.X + (width / 2) - (image.Width / 2), (Position.Y + (height / 2) - (image.Height / 2))), Color.White);

        }

        public override string ToString()
        {
            return name;
        }
    }

    class ImageBox : UIElement 
    {
        public ImageBox(string name, Texture2D image, Vector2 pos) 
        {
            defaultTexture = image;
            this.name = name;
            this.Position = pos;
            this.width = image.Width;
            this.height = image.Height;

        }
        public ImageBox(string name, string imageLocation, Vector2 pos)
        {
            
            this.name = name;
            this.Position = pos;
            

        }
    }
    
}
