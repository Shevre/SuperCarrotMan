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

namespace SuperCarrotEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    
    public partial class App : Application
    {

    }

    
    
    public class EditorFrame : WpfGame
    {
        private IGraphicsDeviceService _graphicsDeviceManager;

        public int currentLevel = 1;
        public int TileBrushId = 1;
        SpriteBatch spriteBatch;
        private WpfKeyboard _keyboard;
        private WpfMouse _mouse;
        List<Texture2D> tiles = new List<Texture2D>();
        string LevelPath;
        private List<Level> levels = new List<Level>();
        Camera camera = new Camera();
        TileDrawer tileDrawer = new TileDrawer();

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
            tiles.Add(Content.Load<Texture2D>("Grid"));
            tiles.Add(Content.Load<Texture2D>("Ground1"));
            tiles.Add(Content.Load<Texture2D>("Ground2"));



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
}
