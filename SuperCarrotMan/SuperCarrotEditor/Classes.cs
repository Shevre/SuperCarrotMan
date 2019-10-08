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
using System.Xml;
using Microsoft.Xna.Framework.Input;

namespace SuperCarrotEditor
{
    class Level
    {
        char[,] level;
        Vector2 playerStartPos;
        string name;
        int tilesetId, tileW, tileH;
        public Color skyColor;
        public Level(string levelXml)
        {
            XmlReader levelReader = XmlReader.Create(levelXml);
            while (levelReader.Read())
            {
                if (levelReader.NodeType == XmlNodeType.Element)
                {
                    switch (levelReader.Name)
                    {
                        case "name":
                            name = levelReader.GetAttribute("name");
                            break;
                        case "tileset":
                            tilesetId = int.Parse(levelReader.GetAttribute("id"));
                            Console.WriteLine(tilesetId);
                            break;
                        case "startPos":
                            playerStartPos = new Vector2(int.Parse(levelReader.GetAttribute("x")), int.Parse(levelReader.GetAttribute("y")));
                            break;
                        case "terrain":
                            string[] _slevel = levelReader.GetAttribute("terrainValue").Split(',');

                            level = new char[_slevel.Length, _slevel[0].Length];
                            tileH = _slevel.Length;
                            tileW = _slevel[0].Length;
                            for (int y = 0; y < _slevel.Length; y++)
                            {
                                Console.WriteLine(_slevel[y]);
                                for (int x = 0; x < _slevel[y].Length; x++)
                                {
                                    level[y, x] = _slevel[y][x];

                                }

                            }
                            break;
                        case "skyColor":
                            string[] _srgb = levelReader.GetAttribute("rgbval").Split(',');
                            skyColor = new Color(int.Parse(_srgb[0]), int.Parse(_srgb[1]), int.Parse(_srgb[2]));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, TilesetCollection tilesetCollection, Camera camera)
        {

            for (int y = 0; y < tileH; y++)
            {
                for (int x = 0; x < tileW; x++)
                {
                    if (level[y, x] == 'G') spriteBatch.Draw(tilesetCollection.GroundTiles[tilesetId], camera.applyCamera(new Vector2(x * 64, y * 64)), Color.White);
                }
            }
        }
        public override string ToString()
        {
            return name;
        }
    }

    class TilesetCollection
    {
        public List<Texture2D> GroundTiles = new List<Texture2D>();
        public TilesetCollection()
        {

        }
    }

    class Camera 
    {
        int cameraoffsetX = 0, cameraoffsetY = 0;
        public Camera() 
        { 

        }
        private int oldX = 0, oldY = 0;
        public Vector2 applyCamera(Vector2 v) 
        {
            return new Vector2(v.X + cameraoffsetX, v.Y + cameraoffsetY);
        }

        public void update(WpfMouse wpfMouse) 
        {
            if (wpfMouse.GetState().RightButton == ButtonState.Pressed)
            {
                int x = wpfMouse.GetState().X;
                int y = wpfMouse.GetState().Y;
                cameraoffsetX += x - oldX;
                cameraoffsetY += y - oldY;
                oldX = x;
                oldY = y;
                Console.WriteLine(cameraoffsetX);
                Console.WriteLine(cameraoffsetY);
            }
        }
    }

}
