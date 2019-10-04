using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System;

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
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        List<Level> levels = new List<Level>();
        protected override void Initialize()
        {

            #region Config stuff

            if (File.Exists("config.cfg")) 
            {
                string[] _sConfig =File.ReadAllText("config.cfg").Split(';');
                
                foreach (string s in _sConfig)
                {
                    Console.WriteLine(s);
                    if (s.Contains("LevelPath="))
                    {
                        LevelPath = s.Replace("LevelPath=", "");
                    }
                    else if (s.Contains("ScreenWidth="))
                    {
                        _nScreenWidth = Convert.ToInt32(s.Replace("ScreenWidth=", ""));
                    }
                    else if (s.Contains("ScreenHeight="))
                    {
                        _nScreenHeight = Convert.ToInt32(s.Replace("ScreenHeight=", ""));
                    }
                    else if (s.Contains("Fullscreen="))
                    {
                        if (s.ToLower().Contains("true")) graphics.IsFullScreen = true;
                        else graphics.IsFullScreen = false;

                    }
                    else if (s.Contains("DevConsole="))
                    {
                        if (s.ToLower().Contains("true")) _blDevConsole = true;
                        else _blDevConsole = false;

                    }
                }
            }
            else 
            {
                File.WriteAllText("config.cfg", @"LevelPath=C:\Users\Leerling\Desktop\Anime Pictures\SuperCarrotMan\SuperCarrotMan\SuperCarrotMan\Levels;" + Environment.NewLine +"ScreenWidth = 1280;\nScreenHeight = 720;\nFullscreen = false;");
            }

            graphics.PreferredBackBufferWidth = _nScreenWidth;
            graphics.PreferredBackBufferHeight = _nScreenHeight;
            graphics.ApplyChanges();
            #endregion
            bool _blLoop = true;
            int i = 1;
            while(_blLoop)
            {
                Console.WriteLine(LevelPath + @"\level" + i.ToString());
                if (File.Exists(LevelPath +  @"\level" + i.ToString() + @"\properties.cfg"))
                {
                    levels.Add(new Level(LevelPath + @"\level" + i.ToString()));
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

            
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

      
    }
}
