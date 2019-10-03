using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System.IO;

namespace SuperCarrotMan
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        Level level;
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

            level = new Level(gamers, new Vector2(200, 200));
            File.WriteAllText("level.json", JsonConvert.SerializeObject(level,Formatting.Indented));
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
