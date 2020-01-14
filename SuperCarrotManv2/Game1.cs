using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SuperCarrotManv2.Core;
using SuperCarrotManv2.Entities;

namespace SuperCarrotManv2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Song song;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PhysicsHandler PhysicsHandler = new PhysicsHandler(0.3f);
        DrawingHandler DrawingHandler = new DrawingHandler();
        
        Player player;
        TexturedCollisionObject floorTest0;
        TexturedCollisionObject floorTest1;
        TexturedCollisionObject floorTest2;
        TexturedCollisionObject floorTest3;
        TexturedCollisionObject floorTest4;
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
            floorTest0 = new TexturedCollisionObject(new Vector2(0, 260), new Vector2(64, 64), Content.Load<Texture2D>("01"),false);
            floorTest1 = new TexturedCollisionObject(new Vector2(64, 364), new Vector2(64, 64), Content.Load<Texture2D>("01"), false);
            floorTest2 = new TexturedCollisionObject(new Vector2(128, 260), new Vector2(64, 64), Content.Load<Texture2D>("01"), false);
            floorTest3 = new TexturedCollisionObject(new Vector2(320, 368), new Vector2(64, 64), Content.Load<Texture2D>("01"), false);
            floorTest4 = new TexturedCollisionObject(new Vector2(256, 364), new Vector2(64, 64), Content.Load<Texture2D>("01"), false);
            DrawingHandler.AddDrawable(player);
            DrawingHandler.AddDrawable(floorTest0);
            DrawingHandler.AddDrawable(floorTest1);
            DrawingHandler.AddDrawable(floorTest2);
            DrawingHandler.AddDrawable(floorTest3);
            DrawingHandler.AddDrawable(floorTest4);
            
            PhysicsHandler.AddCollisionObject(player);
            PhysicsHandler.AddCollisionObject(floorTest0);
            PhysicsHandler.AddCollisionObject(floorTest1);
            PhysicsHandler.AddCollisionObject(floorTest2);
            PhysicsHandler.AddCollisionObject(floorTest4);
            PhysicsHandler.AddCollisionObject(floorTest3);
            
            DebugPixel = Content.Load<Texture2D>("pixle");
            debugFont = Content.Load<SpriteFont>("File");
            //song = Content.Load<Song>(@"audio\mayojacuzzi");
            //MediaPlayer.Play(song);
            //MediaPlayer.IsRepeating = true;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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
