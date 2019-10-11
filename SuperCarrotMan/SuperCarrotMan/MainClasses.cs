using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Xml;

namespace SuperCarrotMan
{
    public class Level
    {
        int[,] level;
        Vector2 playerStartPos;
        public string name;
        int tilesetId, tileW, tileH;
        public Color skyColor;
        string levelXml;
        XmlDocument xmlDoc = new XmlDocument();
        Random peterGriffin = new Random();
        
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

        public void setName(string name)
        {
            this.name = name;
        }

        private void loadXml(string levelXml)
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

                            level = new int[_slevel.Length, _slevel[0].Length];
                            tileH = _slevel.Length;
                            tileW = _slevel[0].Length;

                            for (int i = 0; i < _slevel.Length; i++)
                            {
                                string[] _ystring = _slevel[i].Split(',');
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

        public void Draw(SpriteBatch spriteBatch,List<Texture2D> tiles,Camera camera)
        {

            for (int y = 0; y < tileH; y++)
            {
                for (int x = 0; x < tileW; x++)
                {
                    if (level[y, x] != 0) spriteBatch.Draw(tiles[level[y, x]], camera.applyCamera(new Vector2(x * 64 , y * 64)), Color.White);
                }
            }
        }
        public override string ToString()
        {
            return name;
        }
        
    }

    

    public class Camera
    {
        public float offsetX = 0, offsetY = 0;
        public Camera(float offsetX = 0,float offsetY = 0)
        {
            this.offsetX = offsetX;
            this.offsetY = offsetY;
        }

        public Vector2 applyCamera(Vector2 v)
        {
            return new Vector2(v.X + offsetX, v.Y + offsetY);
        }
    }
    
}
