using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MonoGame.Framework.WpfInterop;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework.WpfInterop.Input;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework.Input;

namespace SuperCarrotEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    
    public partial class App : Application
    {

    }

    public enum TileBrushMode {Single,Selection };
    
    public class EditorFrame : WpfGame
    {
        private IGraphicsDeviceService _graphicsDeviceManager;

        public int currentLevel = 1;
        public int TileBrushId = 1;
        public TileBrushMode brushMode = TileBrushMode.Single;
        SpriteBatch spriteBatch;
        private WpfKeyboard _keyboard;
        private WpfMouse _mouse;
        List<Texture2D> tiles = new List<Texture2D>();
        string LevelPath;
        private List<Level> levels = new List<Level>();
        Camera camera = new Camera();
        TileDrawer tileDrawer = new TileDrawer();
        List<List<int>> SelectedIds = new List<List<int>>();

        internal List<Level> Levels { get => levels; set => levels = value; }

        protected override void Initialize()
        {
            
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
                            
                              
                            default:
                                break;
                        }
                    }
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
            // must be initialized. required by Content loading and rendering (will add itself to the Services)
            // note that MonoGame requires this to be initialized in the constructor, while WpfInterop requires it to
            // be called inside Initialize (before base.Initialize())
            _graphicsDeviceManager = new WpfGraphicsDeviceService(this);
           
            
            // wpf and keyboard need reference to the host control in order to receive input
            // this means every WpfGame control will have it's own keyboard & mouse manager which will only react if the mouse is in the control
            _keyboard = new WpfKeyboard(this);
            _mouse = new WpfMouse(this);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // must be called after the WpfGraphicsDeviceService instance was created
            base.Initialize();

            // content loading now possible
            Content.RootDirectory = "Content";
            ContentLoader contentLoader = new ContentLoader();
            contentLoader.SetContent("ContentConfig.xml", Content, tiles);
            


            bool _blLoop = true;
            int i = 1;
            levels.Add(null);
            while (_blLoop)
            {
                Console.WriteLine(LevelPath + @"\level" + i.ToString() + ".xml");
                if (File.Exists(LevelPath + @"\level" + i.ToString() + ".xml"))
                {
                    Levels.Add(new Level(LevelPath + @"\level" + i.ToString() + ".xml"));
                }
                else
                {
                    _blLoop = false;
                }
                i++;
            }
            foreach (Level l in Levels)
            {
                Console.WriteLine(l);
            }
            
        }

        public List<Texture2D> getTilesList()
        {
            return tiles;
        }

        protected override void Update(GameTime time)
        {
            // every update we can now query the keyboard & mouse for our WpfGame
            var mouseState = _mouse.GetState();
            var keyboardState = _keyboard.GetState();
            camera.update(_mouse);
            tileDrawer.update(_mouse,camera,levels[currentLevel].level,TileBrushId);
        }

        protected override void Draw(GameTime time)
        {
            //GraphicsDevice.Clear(new Color(234, 234, 234));
            GraphicsDevice.Clear(Levels[currentLevel].skyColor);
            spriteBatch.Begin();
            Levels[currentLevel].Draw(spriteBatch, tiles, camera);
            spriteBatch.End();
        }
    }

    public class TileViewer : WpfGame
    {
        private IGraphicsDeviceService _graphicsDeviceManager;
        private WpfKeyboard _keyboard;
        private WpfMouse _mouse;
        List<Texture2D> tiles = new List<Texture2D>();
        private Texture2D selector;
        SpriteBatch spriteBatch;
        public int Mode = 0;
        int selectedX = 0, selectedY = 0;
        public int selectedId = 0;
        int scrolloffset = 0;
        int mousewheeloffset = 0;
        bool canscroll = false;
        int extraRows;
        public TileBrushMode brushMode = TileBrushMode.Single;

        protected override void Initialize()
        {
            // must be initialized. required by Content loading and rendering (will add itself to the Services)
            // note that MonoGame requires this to be initialized in the constructor, while WpfInterop requires it to
            // be called inside Initialize (before base.Initialize())
            _graphicsDeviceManager = new WpfGraphicsDeviceService(this);

            // wpf and keyboard need reference to the host control in order to receive input
            // this means every WpfGame control will have it's own keyboard & mouse manager which will only react if the mouse is in the control
            _keyboard = new WpfKeyboard(this);
            _mouse = new WpfMouse(this);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            // must be called after the WpfGraphicsDeviceService instance was created
            base.Initialize();

            // content loading now possible
            Content.RootDirectory = "Content";
            selector = Content.Load<Texture2D>("Selector");
            
            ContentLoader contentLoader = new ContentLoader();
            contentLoader.SetContent("ContentConfig.xml", Content, tiles,null,selector);
            
            Console.WriteLine($"{this.Width},{this.Height}");
            Console.WriteLine(tiles.Count);
            Console.WriteLine((this.Width / 64) * (this.Height / 64));
            if (tiles.Count > (this.Width / 64) * (this.Height / 64)) 
            {
                canscroll = true;
                extraRows = (int)((tiles.Count - ((this.Width / 64) * (this.Height / 64))) / (this.Width / 64)) + 1;
                Console.WriteLine(extraRows);
            }
        }

        public void SetTilesList(List<Texture2D> tiles)
        {
            this.tiles = tiles;
        }


        int PrevScrollval = 0;

        protected override void Update(GameTime time)
        {
            // every update we can now query the keyboard & mouse for our WpfGame
            var mouseState = _mouse.GetState();
            var keyboardState = _keyboard.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                selectedX = (int)(mouseState.X / 64);
                selectedY = (int)((mouseState.Y - scrolloffset) / 64);
                
                selectedId = selectedX + ((int)(this.Width / 64) * selectedY);
                if (selectedId > tiles.Count - 1)
                {
                    selectedId = 0;
                    
                }
                Console.WriteLine(selectedId);
            }
            if (mouseState.LeftButton == ButtonState.Pressed && keyboardState.IsKeyDown(Keys.LeftControl))
            {
                selectedX = (int)(mouseState.X / 64);
                selectedY = (int)((mouseState.Y - scrolloffset) / 64);

                selectedId = selectedX + ((int)(this.Width / 64) * selectedY);
                if (selectedId > tiles.Count - 1)
                {
                    selectedId = 0;

                }
                Console.WriteLine(selectedId);
            }
            if (canscroll)
            {
                
                int scrollVal = mouseState.ScrollWheelValue;
                int scrollValDelta = scrollVal - PrevScrollval;
                if (scrollValDelta < 0)
                {
                    if (!(scrolloffset <= -extraRows * 64)) scrolloffset -= 32;


                }
                else if (scrollValDelta > 0)
                {
                    if (!(scrolloffset >= 0)) scrolloffset += 32;
                    
                    
                }
                PrevScrollval = scrollVal;
            }
            else
            {
                mousewheeloffset = mouseState.ScrollWheelValue;
            }
            
            //Console.WriteLine(scrolloffset);

        }

        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(new Color(175, 175, 175));
            spriteBatch.Begin();
            if (Mode == 0)
            {
                int x = 0;
                int y = 0;
                foreach (Texture2D tile in tiles)
                {
                    if(!(x == 0 && y == 0))spriteBatch.Draw(tile, new Vector2(x * 64, (y * 64) + scrolloffset), Color.White);
                    x++;
                    if (x * 64 > this.Width - 64)
                    {
                        x = 0;
                        y++;
                    }

                }

            }
            spriteBatch.Draw(selector, new Vector2(selectedX * 64, selectedY * 64 + scrolloffset), Color.White);

            spriteBatch.End();
        }
    }
    
    public struct ContentLoader
    {
        public void SetContent(string contentConfigPath, Microsoft.Xna.Framework.Content.ContentManager Content, List<Texture2D> Tiles, List<Texture2D> EnemyTextures = null, Texture2D Selector = null)
        {
            XmlDocument contentConfig = new XmlDocument();
            contentConfig.Load(contentConfigPath);
            XmlNodeList Tilenodes = contentConfig.SelectNodes("//ContentConfig/Tile");
            foreach (XmlNode node in Tilenodes)
            {
                Tiles.Add(Content.Load<Texture2D>(node.Attributes.GetNamedItem("name").Value));
            }
            if(EnemyTextures != null)
            {

            }
            if(Selector != null)
            {
                Selector = Content.Load<Texture2D>(contentConfig.SelectSingleNode("//ContentConfig/Selector").Attributes.GetNamedItem("name").Value);
            }
        }
    }

    
}
