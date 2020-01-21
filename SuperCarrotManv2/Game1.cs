using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperCarrotManv2.Core;
using SuperCarrotManv2.Entities;
using SuperCarrotManv2.GAME;

namespace SuperCarrotManv2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PhysicsHandler PhysicsHandler = new PhysicsHandler(0.3f);
        DrawingHandler DrawingHandler = new DrawingHandler();

        Player player;
        Scene1 scene1;

        Texture2D DebugPixel;
        SpriteFont debugFont;
        public static DebugHandler DebugHandler = new DebugHandler();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(new Vector2(0, 0), new Vector2(24, 105),Content.Load<Texture2D>(@"CarrotMan\Walk\1"),new Vector2(-19,-23));

            scene1 = new Scene1(Content);

            DrawingHandler.AddDrawable(player);
            DrawingHandler.AddDrawable(scene1);
            DebugPixel = Content.Load<Texture2D>("pixle");
            debugFont = Content.Load<SpriteFont>("File");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            scene1.Update();
            PhysicsHandler.Update();
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            DrawingHandler.Draw(spriteBatch);
            DebugHandler.Log(" Touching Floor: ");
            if (player.TouchingFloor)
            {
                DebugHandler.Log( "Yes ");
            }
            else 
            {
                DebugHandler.Log("No ");
            }
            spriteBatch.DrawString(debugFont, DebugHandler.debugString, new Vector2(0, 0), Color.Magenta);
            spriteBatch.End();
            base.Draw(gameTime);
            DebugHandler.ClearString();
        }
    }
}
