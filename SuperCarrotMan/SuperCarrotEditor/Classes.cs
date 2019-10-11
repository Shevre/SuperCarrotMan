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
    public class Level
    {
        public int[,] level;
        Vector2 playerStartPos;
        public string name;
        int tilesetId, tileW, tileH;
        public Color skyColor;
        string levelXml;
        XmlDocument xmlDoc = new XmlDocument();
        public Level(string levelXml)
        {
            this.levelXml = levelXml;
            xmlDoc.Load(levelXml);
            loadXml(levelXml);
            
        }

        public void Reload()
        {
            xmlDoc.Load(levelXml);
            loadXml(levelXml);
            
        }

        public void loadXml(string levelXml)
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
                            string[] _slevel = xmlDoc.SelectSingleNode("//level/terrain").InnerText.Split('\n');
                            string[] _ystring = _slevel[0].Split(',');
                            level = new int[_slevel.Length, _ystring.Length];
                            tileH = _slevel.Length;
                            tileW = _ystring.Length;

                            for (int i = 0; i < _slevel.Length; i++)
                            {
                                _ystring = _slevel[i].Split(',');
                                for (int j = 0; j < _ystring.Length; j++)
                                {
                                    level[i, j] = int.Parse(_ystring[j]);
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

        public void Draw(SpriteBatch spriteBatch, List<Texture2D> tiles, Camera camera)
        {

            for (int y = 0; y < tileH; y++)
            {
                for (int x = 0; x < tileW; x++)
                {
                    if (level[y, x] != 0) spriteBatch.Draw(tiles[level[y,x]], camera.applyCamera(new Vector2(x * 64, y * 64)), Color.White);
                    spriteBatch.Draw(tiles[0], camera.applyCamera(new Vector2(x * 64, y * 64)), Color.White);
                }
            }
        }
        public override string ToString()
        {
            return name;
        }
        public void save()
        {
            xmlDoc.SelectSingleNode("//level/name").Attributes.GetNamedItem("name").Value = name;
            string terrainString = "";

            for (int y = 0; y < tileH; y++)
            {
                for (int x = 0; x < tileW; x++)
                {
                    terrainString += level[y, x].ToString() + ",";
                }
                terrainString = terrainString.Remove(terrainString.Length - 1);
                terrainString += Environment.NewLine;
            }
            terrainString = terrainString.Remove(terrainString.Length - 2);

            xmlDoc.SelectSingleNode("//level/terrain").InnerText = terrainString;
            xmlDoc.Save(levelXml);
        }
    }

    

    public class Camera 
    {
        int cameraoffsetX = 0, cameraoffsetY = 0;
        public Camera() 
        { 

        }
        //private int oldX = 0, oldY = 0;
        private int firstx = 0, firsty = 0, oldcamoffsetX = 0, oldcamoffsetY = 0;
        private bool firstgo = false;
        public Vector2 applyCamera(Vector2 v) 
        {
            return new Vector2(v.X + cameraoffsetX, v.Y + cameraoffsetY);
        }
        public Vector2 revApplyCamera(Vector2 v)
        {
            return new Vector2(v.X - cameraoffsetX, v.Y - cameraoffsetY);
        }

        public void update(WpfMouse wpfMouse) 
        {
            if (wpfMouse.GetState().RightButton == ButtonState.Pressed && firstgo)
            {
                
                int x = wpfMouse.GetState().X;
                int y = wpfMouse.GetState().Y;
                cameraoffsetX = x - firstx + oldcamoffsetX;
                cameraoffsetY = y - firsty + oldcamoffsetY;
                //Console.WriteLine(cameraoffsetX);
                //Console.WriteLine(cameraoffsetY);
            }
            else if (wpfMouse.GetState().RightButton == ButtonState.Pressed && !firstgo)
            {
                firstgo = true;
                firstx = wpfMouse.GetState().X;
                firsty = wpfMouse.GetState().Y;
                oldcamoffsetX = cameraoffsetX;
                oldcamoffsetY = cameraoffsetY;
            }
            else 
            {
                firstgo = false;
            }

        }
    }

    public class TileDrawer
    {
        public TileDrawer()
        {

        }

        public void update(WpfMouse wpfMouse, Camera camera, int[,] levelArray,int tileDrawerId)
        {
            if (wpfMouse.GetState().LeftButton == ButtonState.Pressed)
            {
                int x = wpfMouse.GetState().X, y = wpfMouse.GetState().Y;
                Vector2 drawPos = camera.revApplyCamera(new Vector2(x, y));
                levelArray[(int)(drawPos.Y / 64), (int)(drawPos.X / 64)] = tileDrawerId;
            }
        }

        public void update(WpfMouse wpfMouse, Camera camera, int[,] levelArray, List<List<int>> tileDrawerIds)
        {
            if (wpfMouse.GetState().LeftButton == ButtonState.Pressed)
            {
                int x = wpfMouse.GetState().X, y = wpfMouse.GetState().Y;
                Vector2 drawPos = camera.revApplyCamera(new Vector2(x, y));
                for (int yy = 0; yy < tileDrawerIds.Count; yy++)
                {
                    for (int xx = 0; xx < tileDrawerIds[yy].Count; xx++)
                    {
                        int xxx = (int)(drawPos.X / 64) + xx;
                        int yyy = (int)(drawPos.Y / 64) + yy;
                        levelArray[yyy, xxx] = tileDrawerIds[yy][xx];
                    }
                }
            }
        }
    }
}
