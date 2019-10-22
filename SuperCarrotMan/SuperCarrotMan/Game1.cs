using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System;
using System.Xml;
using Shev.monoGameUI;

namespace SuperCarrotMan
{

    enum GameState { MainMenu,Paused,Playing};

    public class Game1 : Game
    {
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        string LevelPath = "";

        static int defScreenWidth = 960;
        static int defScreenHeight = 540;

        int _nScreenWidth = defScreenHeight;
        int _nScreenHeight = defScreenHeight;

        Gravity gravity = new Gravity(0.125f);
        
        bool _blDevConsole = false;

        Camera camera = new Camera();

        List<Texture2D> tiles = new List<Texture2D>();
        Texture2D Cursor;

        PlayerKeyboardKeys playerKeyboardKeys = new PlayerKeyboardKeys(Keys.Up, Keys.Right, Keys.Down, Keys.Left, Keys.Space, Keys.Q,Keys.LeftShift,Keys.S);
        PlayerControllerButtons playerControllerButtons = new PlayerControllerButtons(Buttons.DPadUp,Buttons.DPadRight,Buttons.DPadDown,Buttons.DPadLeft,Buttons.B,Buttons.A,Buttons.Y,Buttons.X);

        Player player;

        Menu mainMenu;
        Menu levelSelectMenu;

        int currentLevel = 0;

        GameState gameState = GameState.MainMenu;

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
                configWriter.WriteAttributeString("width", defScreenWidth.ToString());
                configWriter.WriteAttributeString("height", defScreenHeight.ToString());
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

            ContentLoader contentLoader = new ContentLoader();
            AnimationSet playerWalk = contentLoader.GetAnimSet("ContentConfig.xml", "PlayerFrameWalk", Content, 100);
            AnimationSet playerRun = contentLoader.GetAnimSet("ContentConfig.xml", "PlayerFrameRun", Content, 100);
            contentLoader.SetContent("ContentConfig.xml", Content, tiles);
             
            player = new Player(new Vector2(128, 128), playerWalk, playerRun, camera, 0.25f, playerKeyboardKeys, playerControllerButtons);
            
            Cursor = Content.Load<Texture2D>("Cursor");

            #region Menus
            mainMenu = new Menu(new Color(57, 247, 235));

            Texture2D BackImage = Content.Load<Texture2D>("Back");
            Texture2D defaultTexture = Content.Load<Texture2D>("Button");
            Texture2D pressedTexture = Content.Load<Texture2D>("Button_P");


            mainMenu.AddUIElement(new Button("CloseButton",defaultTexture,pressedTexture, new Vector2(_nScreenWidth / 2, 290), 420, 80, "Exit",Content.Load<SpriteFont>("ButtonText"),TextAllign.Center,ButtonTypes.CloseButton));
            mainMenu.AddUIElement(new ImageBox(("TestImage"), tiles[6], new Vector2(20, 20)));

            #region Submenus

            #region Level Select
            levelSelectMenu = new Menu(new Color(201, 201, 70));
            levelSelectMenu.AddUIElement(new Button("BackButton", defaultTexture, pressedTexture, new Vector2(20, _nScreenHeight - 100), 80, 80, BackImage, TextAllign.Center, ButtonTypes.BackButton));

            for (int i = 0; i < levels.Count; i++)
            {
                levelSelectMenu.AddUIElement(new Button($"levelButton{i}", defaultTexture, pressedTexture, new Vector2(((_nScreenWidth / 2) - 210), 80 + i * 90),420,80, levels[i].name,Content.Load<SpriteFont>("ButtonText")));
            }

            #endregion


            #endregion

            mainMenu.AddSubMenu(levelSelectMenu);
            mainMenu.AddUIElement(new Button("SelectButton", defaultTexture, pressedTexture, new Vector2(_nScreenWidth / 2, 200), 600, 80, "Select Level", Content.Load<SpriteFont>("ButtonText"), TextAllign.Center, ButtonTypes.MenuButton, levelSelectMenu));
            #endregion

        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (gameState == GameState.MainMenu)
            {
                mainMenu.Update(this);
                for (int i = 0; i < levels.Count; i++)
                {
                    if (levelSelectMenu.getButton($"levelButton{i}").ButtonClicked)
                    {
                        currentLevel = i;
                        gameState = GameState.Playing;
                    }
                }
                
            }
            else if (gameState == GameState.Playing)
            {
                /*if (Keyboard.GetState().IsKeyDown(Keys.Up))
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
                }*/
                player.Update(gameTime,gravity);
                levels[currentLevel].Update(gameTime, player,gravity);
            }
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (gameState == GameState.MainMenu || gameState == GameState.Paused)
            {
                GraphicsDevice.Clear(mainMenu.backgroundColor);
                
                mainMenu.Draw(spriteBatch);

                spriteBatch.Draw(Cursor, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
            }
            if (gameState == GameState.Playing)
            {
                GraphicsDevice.Clear(levels[currentLevel].skyColor);
                
                levels[currentLevel].Draw(spriteBatch, tiles, camera,_nScreenWidth,_nScreenHeight);
                player.Draw(spriteBatch,gameTime);
            }




            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }

      
    }
    public struct ContentLoader
    {
        public void SetContent(string contentConfigPath, Microsoft.Xna.Framework.Content.ContentManager Content, List<Texture2D> Tiles, AnimationSet playerAnimSet = null,AnimationSet playerRunAnimSet = null, List<Texture2D> EnemyTextures = null, Texture2D Selector = null)
        {
            XmlDocument contentConfig = new XmlDocument();
            contentConfig.Load(contentConfigPath);
            XmlNodeList Tilenodes = contentConfig.SelectNodes("//ContentConfig/Tile");
            foreach (XmlNode node in Tilenodes)
            {
                Tiles.Add(Content.Load<Texture2D>(node.Attributes.GetNamedItem("name").Value));
            }
            if (playerAnimSet != null)
            {
                XmlNodeList PlayerFrameNode = contentConfig.SelectNodes("//ContentConfig/PlayerFrame");
                List<Texture2D> playerRun = new List<Texture2D>();
                List<Texture2D> playerWalk = new List<Texture2D>();
                foreach (XmlNode node in PlayerFrameNode)
                {
                    if (node.Attributes.GetNamedItem("type").Value == "Walk" )
                    {
                        playerWalk.Add(Content.Load<Texture2D>(node.Attributes.GetNamedItem("name").Value));
                    }
                    else if (node.Attributes.GetNamedItem("type").Value == "Run")
                    {
                        playerRun.Add(Content.Load<Texture2D>(node.Attributes.GetNamedItem("name").Value));
                    }
                }
                playerAnimSet = new AnimationSet(playerWalk.ToArray(), playerAnimSet.GetCycleTime());
                if(playerRunAnimSet != null)playerRunAnimSet = new AnimationSet(playerRun.ToArray(), playerRunAnimSet.GetCycleTime());
            }
            if (EnemyTextures != null)
            {

            }
            if (Selector != null)
            {
                Selector = Content.Load<Texture2D>(contentConfig.SelectSingleNode("//ContentConfig/Selector").Attributes.GetNamedItem("name").Value);
            }
        }

        public AnimationSet GetAnimSet(string contentConfigPath,string NodeName, Microsoft.Xna.Framework.Content.ContentManager Content, int cycleTime_ms) 
        {
            XmlDocument contentConfig = new XmlDocument();
            contentConfig.Load(contentConfigPath);
            
            XmlNodeList PlayerFrameNode = contentConfig.SelectNodes("//ContentConfig/" + NodeName);
            
            List<Texture2D> playerWalk = new List<Texture2D>();
            foreach (XmlNode node in PlayerFrameNode)
            {
                
               playerWalk.Add(Content.Load<Texture2D>(node.Attributes.GetNamedItem("name").Value));
               
                
            }
            return new AnimationSet(playerWalk.ToArray(), cycleTime_ms);
                
            
        }
    }
}


