using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace SuperCarrotMan
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        string LevelPath = "";
        int _nScreenWidth = 1280;
        int _nScreenHeight = 720;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        List<Level> levels = new List<Level>();
        protected override void Initialize()
        {
            string[] gamers = new string[10];
            gamers[0] = "..........";
            gamers[1] = "..........";
            gamers[2] = "..........";
            gamers[3] = "G.........";
            gamers[4] = "GGG..GGGGG";
            gamers[5] = "GGG..GGGGG";
            gamers[6] = "GGG..GGGGG";
            gamers[7] = "GGG..GGGGG";
            gamers[8] = "GGG..GGGGG";
            gamers[9] = "GGG..GGGGG";

            #region Startup Config read

            if (File.Exists("config.cfg")) 
            {
               LevelPath = getBetween(File.ReadAllText("config.cfg"), "LevelPath=", "\n");
               _nScreenHeight = int.Parse(getBetween(File.ReadAllText("config.cfg"), "ScreenWidth=", "\n"));
                _nScreenHeight = int.Parse(getBetween(File.ReadAllText("config.cfg"), "ScreenHeight=", "\n"));
            }
            else 
            {
                File.WriteAllText("config.cfg", "");
            }

            #endregion

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

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
    }
}
