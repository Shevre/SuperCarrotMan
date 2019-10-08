using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System;
using System.Xml;

namespace SuperCarrotMan
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        string LevelPath = "";
        int _nScreenWidth = 1280;
        int _nScreenHeight = 720;
        bool _blDevConsole = false;
        Camera camera = new Camera();
        List<Texture2D> tiles = new List<Texture2D>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        List<Level> levels = new List<Level>();
        
        protected override void Initialize()
        {

            #region Config stuff

            if (File.Exists("config.xml")) 
            {
                XmlReader configReader = XmlReader.Create("config.xml");

                while (configReader.Read())
                {
                    if ((configReader.NodeType == XmlNodeType.Element))
                    {
                        switch (configReader.Name)
                        {
                            case "levelPath":

                                LevelPath = configReader.GetAttribute("path");

                                break;
                            case "screen":

                                _nScreenHeight = int.Parse(configReader.GetAttribute("height"));
                                _nScreenWidth = int.Parse(configReader.GetAttribute("width"));
                                if (configReader.GetAttribute("fullscreen") == "true") graphics.IsFullScreen = true;

                                break;

                            case "devConsole":
                                if (configReader.GetAttribute("enabled") == "true") _blDevConsole = true;
                                else _blDevConsole = false;
                                
                                break;
                            default:
                                break;
                        }
                    }
                }
                if (_blDevConsole)
                {
                    Console.WriteLine("devconsole!!!");
                }
                
            }
            else 
            {
                XmlWriter configWriter = XmlWriter.Create("config.xml");

                configWriter.WriteStartDocument();

                configWriter.WriteStartElement("Config");
                configWriter.WriteStartElement("levelPath");
                configWriter.WriteAttributeString("path", "Levels");
                configWriter.WriteEndElement();

                configWriter.WriteStartElement("screen");
                configWriter.WriteAttributeString("width", "1280");
                configWriter.WriteAttributeString("height", "720");
                configWriter.WriteAttributeString("fullscreen", "false");
                

                configWriter.WriteEndDocument();
                configWriter.Close();
            }

            graphics.PreferredBackBufferWidth = _nScreenWidth;
            graphics.PreferredBackBufferHeight = _nScreenHeight;
            graphics.ApplyChanges();
            #endregion

            bool _blLoop = true;
            int i = 1;
            while(_blLoop)
            {
                Console.WriteLine(LevelPath + @"\level" + i.ToString() + ".xml");
                if (File.Exists(LevelPath +  @"\level" + i.ToString() + ".xml"))
                {
                    levels.Add(new Level(LevelPath + @"\level" + i.ToString() + ".xml"));
                }
                else 
                {
                    _blLoop = false;
                }
                i++;
            }
            foreach (Level l in levels)
            {
                Console.WriteLine(l);
            }
            
            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tiles.Add(null);
            tiles.Add(Content.Load<Texture2D>("Ground1"));
            tiles.Add(Content.Load<Texture2D>("Ground2"));


        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                camera.offsetY += 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                camera.offsetY -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                camera.offsetX -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                camera.offsetX += 2;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(levels[0].skyColor);
            spriteBatch.Begin();
            levels[0].Draw(spriteBatch, tiles,camera);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

      
    }
}
