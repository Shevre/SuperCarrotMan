using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        //Song song;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PhysicsHandler PhysicsHandler = new PhysicsHandler(0.3f);
        DrawingHandler DrawingHandler = new DrawingHandler();
        
        Player player;
        Scene1 scene1;

        Texture2D DebugPixel;
        SpriteFont debugFont;

        BasicEffect basicEffect;

        VecRectangle testRect;

        KeyboardState currentState;
        KeyboardState prevState;
        public static DebugHandler DebugHandler = new DebugHandler();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            testRect = new VecRectangle(5, 8, 20, 30);
            currentState = Keyboard.GetState();
            prevState = Keyboard.GetState();
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(new Vector2(0, 0), new Vector2(24, 105),Content.Load<Texture2D>(@"CarrotMan\Walk\1"),new Vector2(-19,-23));

            scene1 = new Scene1(Content);

            DrawingHandler.AddDrawable(player);
            DrawingHandler.AddDrawable(scene1);
            PhysicsHandler.AddCollisionObject(player);
            PhysicsHandler.AddCollisionObject(scene1);
            DebugPixel = Content.Load<Texture2D>("pixle");
            debugFont = Content.Load<SpriteFont>("File");
            //song = Content.Load<Song>(@"audio\mayojacuzzi");
            //MediaPlayer.Play(song);
            //MediaPlayer.IsRepeating = true;
            basicEffect = new BasicEffect(GraphicsDevice)
            {
                World = Matrix.CreateOrthographicOffCenter(
                0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0, 0, 1)
            };

        }

        protected override void UnloadContent()
        {
        }
        bool Paused = false;
        protected override void Update(GameTime gameTime)
        {
            currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(Keys.LeftControl) && (currentState.IsKeyDown(Keys.P) && prevState.IsKeyUp(Keys.P))) Paused = !Paused;
            if (!Paused) 
            {
                
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                scene1.Update();
                PhysicsHandler.Update();

            }
            DebugHandler.DebugUpdate(currentState, prevState);
            base.Update(gameTime);
            prevState = currentState;
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
            if(DebugHandler.IsDebugging())spriteBatch.DrawString(debugFont, DebugHandler.debugString, new Vector2(0, 0), Color.Magenta);
            DebugHandler.DebugDraw(spriteBatch, PhysicsHandler.GetCollisionObjects());
            spriteBatch.End();
            
            base.Draw(gameTime);
            DebugHandler.ClearString();
            
        }

        
    }
}
