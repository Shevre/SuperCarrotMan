using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SuperCarrotManv2.Core;
using SuperCarrotManv2.Entities;
using SuperCarrotManv2.GAME;
using System.Collections.Generic;

namespace SuperCarrotManv2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Song song;
        public static int defScreenWidth = 1280;
        public static int defScreenHeight = 720;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        static DrawingHandler DrawingHandler = new DrawingHandler();

        RenderTarget2D GameLayer;
        RenderTarget2D UIlayer;
        RenderTarget2D Background;

        

        public static IK IK;

        Scene currentScene;

        Texture2D DebugPixel;
        SpriteFont debugFont;



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
            currentState = Keyboard.GetState();
            prevState = Keyboard.GetState();
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = defScreenHeight;
            graphics.PreferredBackBufferWidth = defScreenWidth;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Player player = new Player(new Vector2(256, -128), new Vector2(24, 105),new AnimationSet(new Texture2D[] 
            {
                Content.Load<Texture2D>(@"CarrotMan\Walk\1"), 
                Content.Load<Texture2D>(@"CarrotMan\Walk\2"),
                Content.Load<Texture2D>(@"CarrotMan\Walk\3"),
                Content.Load<Texture2D>(@"CarrotMan\Walk\4"),
                Content.Load<Texture2D>(@"CarrotMan\Walk\5"),
                Content.Load<Texture2D>(@"CarrotMan\Walk\6"),
                Content.Load<Texture2D>(@"CarrotMan\Walk\7"),
                Content.Load<Texture2D>(@"CarrotMan\Walk\8"),
            }),
            new AnimationSet(new Texture2D[] {
                Content.Load<Texture2D>(@"CarrotMan\Run\1"),
                Content.Load<Texture2D>(@"CarrotMan\Run\2"),
                Content.Load<Texture2D>(@"CarrotMan\Run\3"),
                Content.Load<Texture2D>(@"CarrotMan\Run\4"),
                Content.Load<Texture2D>(@"CarrotMan\Run\5"),
                Content.Load<Texture2D>(@"CarrotMan\Run\6"),
            }),
            new Vector2(-19,-23));
            IK = new IK(Content);
            currentScene = new Scene1(Content,this);


            
            currentScene.AddPlayer(player);
            DrawingHandler.currentDrawScene = currentScene;
            DebugPixel = Content.Load<Texture2D>("pixle");
            debugFont = Content.Load<SpriteFont>("File");
            //song = Content.Load<Song>(@"audio\mayojacuzzi");
            //MediaPlayer.Play(song);
            //MediaPlayer.IsRepeating = true;

            GameLayer = new RenderTarget2D(GraphicsDevice, defScreenWidth, defScreenHeight);
            UIlayer = new RenderTarget2D(GraphicsDevice, defScreenWidth, defScreenHeight);
            Background = new RenderTarget2D(GraphicsDevice, defScreenWidth, defScreenHeight);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
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
                currentScene.Update();
                

            }
            DebugHandler.DebugUpdate(currentState, prevState);
            base.Update(gameTime);
            prevState = currentState;
        }
        
        protected override void Draw(GameTime gameTime)
        {



            spriteBatch.GraphicsDevice.SetRenderTarget(Background);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();
            IK.Draw(spriteBatch);
            spriteBatch.End();
            spriteBatch.GraphicsDevice.SetRenderTarget(GameLayer);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin(transformMatrix: currentScene.getCameraTransform());
            
            DrawingHandler.Draw(spriteBatch);
            DebugHandler.Log(" Touching Floor: ");
            if (currentScene.Player.TouchingFloor)
            {
                DebugHandler.Log( "Yes ");
            }
            else 
            {
                DebugHandler.Log("No ");
            }
            
            DebugHandler.DebugDraw(spriteBatch, currentScene.physics.GetCollisionObjects(), currentScene.physics.GetCollisionAreas(),currentScene.events.areaEventObjects);
            
            spriteBatch.End();
            spriteBatch.GraphicsDevice.SetRenderTarget(UIlayer);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();
            if (DebugHandler.IsDebugging()) spriteBatch.DrawString(debugFont, DebugHandler.debugString, new Vector2(0, 0), Color.Magenta);
            
            spriteBatch.End();
            spriteBatch.GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Draw(Background, ExtentionMethods.getEmptyVector(), Color.White);
            spriteBatch.Draw(GameLayer, ExtentionMethods.getEmptyVector(), Color.White);
            spriteBatch.Draw(UIlayer, ExtentionMethods.getEmptyVector(), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
            DebugHandler.ClearString();
            
        }
        public void ChangeScene(Vector2 position,Scene CurrentScene,int NextScene)
        {
            CurrentScene.Player.setPosition(position);
            CurrentScene.Player.Velocity = ExtentionMethods.getEmptyVector();
            switch (NextScene)
            {
                case 0:

                    currentScene = new Scene1(Content,this);
                    currentScene.AddPlayer(CurrentScene.Player);
                    DrawingHandler.currentDrawScene = currentScene;
                    break;
                case 1:

                    currentScene = new Scene2(Content,this);
                    currentScene.AddPlayer(CurrentScene.Player);
                    DrawingHandler.currentDrawScene = currentScene;
                    break;
                default:
                    throw new System.Exception("Please select a valid scene");
                    break;
            }
        }

        
    }
}
