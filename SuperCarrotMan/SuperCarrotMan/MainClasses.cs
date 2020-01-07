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
using Microsoft.Xna.Framework.Content;

namespace SuperCarrotMan
{
    struct LevelData
    {
        public string name, xmlLocation;

        public LevelData(string xmlLocation)
        {

            this.xmlLocation = xmlLocation;
            XmlReader levelReader = XmlReader.Create(xmlLocation);
            name = "";
            while (levelReader.Read())
            {
                if (levelReader.NodeType == XmlNodeType.Element)
                {
                    switch (levelReader.Name)
                    {
                        case "name":
                            name = levelReader.GetAttribute("name");
                            break;
                        default:
                            
                            break;
                    }
                }
            }
        }
    }
    class Level
    {
        int[,] level;
        int[,] collision;
        public Vector2 playerStartPos;
        public string name;
        int tilesetId, tileW, tileH;
        public Color skyColor;
        string levelXml;
        XmlDocument xmlDoc = new XmlDocument();
        List<Texture2D> tiles = new List<Texture2D>();
        List<int> RequiredTiles = new List<int>();
        int[] LocalIds;
        Random peterGriffin = new Random();

        public Level(string levelXml, List<string> tileDatas, ContentManager content)
        {
            this.levelXml = levelXml;
            xmlDoc.Load(levelXml);
            loadXml(levelXml);
            LocalIds = new int[tileDatas.Count];

            foreach (int i in RequiredTiles)
            {
                tiles.Add(content.Load<Texture2D>(tileDatas[i]));
                LocalIds[i] = RequiredTiles.IndexOf(i);
            }
        }

        public void Reload()
        {
            xmlDoc.Load(levelXml);


        }

        public void Exit()
        {

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
                            foreach (string s in _slevel)
                            {
                                Console.WriteLine(s);
                            }
                            Console.WriteLine();
                            level = new int[_slevel.Length, _slevel[0].Length];
                            tileH = _slevel.Length;
                            tileW = _slevel[0].Length;

                            for (int i = 0; i < _slevel.Length; i++)
                            {
                                string[] _ystring = _slevel[i].Split(',');
                                for (int j = 0; j < _ystring.Length; j++)
                                {
                                    level[i, j] = int.Parse(_ystring[j]);
                                    if (!RequiredTiles.Contains(int.Parse(_ystring[j])))
                                    {
                                        RequiredTiles.Add(int.Parse(_ystring[j]));
                                    }
                                }
                            }
                            break;
                        case "Collision":
                            string[] _slevelc = xmlDoc.SelectSingleNode("//level/Collision").InnerText.Split('\n');
                            foreach (string s in _slevelc)
                            {
                                Console.WriteLine(s);
                            }
                            Console.WriteLine();
                            collision = new int[_slevelc.Length, _slevelc[0].Length];
                            tileH = _slevelc.Length;
                            tileW = _slevelc[0].Length;

                            for (int i = 0; i < _slevelc.Length; i++)
                            {
                                string[] _ystring = _slevelc[i].Split(',');
                                for (int j = 0; j < _ystring.Length; j++)
                                {
                                    collision[i, j] = int.Parse(_ystring[j]);
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


        public void Update(GameTime gameTime, Player player, Gravity gravity)
        {
            Vector2 gridPos = GetPositionSafe(new Vector2((player.position.X + player.Xoffset) / 64, (player.position.Y) / 64));
            Vector2 gridPosR = GetPositionSafe(new Vector2((player.position.X + player.Xoffset + player.width) / 64, (player.position.Y) / 64));
            //Console.Clear();
            //Console.WriteLine($"{(int)gridPos.X},{(int)gridPos.Y}");
            if (collision[(int)gridPos.Y + 2, (int)gridPos.X] == 1 || collision[(int)gridPosR.Y + 2, (int)gridPosR.X] == 1)
            {
                gravity.TurnOff();
                //Console.WriteLine("collided.");
            }
            else
            {
                gravity.TurnOn();
            }
            /*if (collision[(int)gridPos.Y + 1, (int)gridPos.X] == 1 || collision[(int)gridPosR.Y + 2, (int)gridPosR.X] == 1)
            {
                player.CanMoveRight = false;
            }
            else 
            {
                player.CanMoveRight = true;
            }*/
            

        }

        private Vector2 GetPositionSafe(Vector2 gridPos)
        {
            Vector2 returnPos = gridPos;
            if (gridPos.X > tileH)
            {
                returnPos.X = tileH;
            }
            if (gridPos.X < 0)
            {
                returnPos.X = 0;
            }
            if (gridPos.Y > tileW)
            {
                returnPos.Y = tileW;
            }
            if (gridPos.Y < 0)
            {
                returnPos.Y = 0;
            }
            return returnPos;
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera, int screenWidth, int screenHeight, float Scale)
        {

            for (int y = 0; y < tileH; y++)
            {
                for (int x = 0; x < tileW; x++)
                {
                    Vector2 pos = new Vector2(x * 64 * Scale, y * 64 * Scale);
                    if (level[y, x] != 0 /*&& !(pos.X > screenWidth) && !(pos.X < 0) && !(pos.Y > screenHeight) && !(pos.Y < 0)*/) spriteBatch.Draw(tiles[LocalIds[level[y, x]]], pos, null, Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
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
        public Matrix Transform { get; private set; }

        public Camera() 
        {
        
        }

        public void Follow(Entity target, float scale) 
        {
        Transform = Matrix.CreateTranslation((-target.position.X * scale) - ((target.animSet.Width * scale) / 2) , (-target.position.Y * scale) - ((target.animSet.Height * scale) / 2) - 120, 0) * Matrix.CreateTranslation(Game1.defScreenWidth / 2, Game1.defScreenWidth / 2, 0);
        }
    }

    public class AnimationSet
    {
        float cycleTime_ms, cycleTimeSpent_ms = 0;
        public float GetCycleTime() { return cycleTime_ms; }
        int currentFrame = 1;
        public int Width, Height;
        Texture2D[] frames;

        public AnimationSet(Texture2D[] frames, float cycleTime_ms)
        {
            this.cycleTime_ms = cycleTime_ms;
            this.frames = frames;
            Width = frames[0].Width;
            Height = frames[0].Height;
        }


        public Texture2D getIdle()
        {
            currentFrame = 1;
            return frames[0];
        }


        public Texture2D getFrame(GameTime gameTime)
        {
            cycleTimeSpent_ms += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (cycleTimeSpent_ms >= cycleTime_ms)
            {
                cycleTimeSpent_ms = 0;
                currentFrame++;
            }
            if (currentFrame >= frames.Length)
            {
                currentFrame = 0;
            }
            return frames[currentFrame];
        }
    }

    public class Gravity
    {
        float G, originalG;
        bool on = true;

        public Gravity(float g)
        {
            G = g;
            originalG = g;
        }

        public Vector2 applyGravity(Vector2 velocity, GameTime gameTime)
        {
            if (on)
            {
                return new Vector2(velocity.X, velocity.Y + G * (float)gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            else
            {
                return new Vector2(velocity.X, 0);
            }

        }

        public void TurnOn()
        {
            on = true;
        }
        public void TurnOff()
        {
            on = false;
        }
    }

    public struct PlayerKeyboardKeys
    {
        public Keys Up;
        public Keys Right;
        public Keys Down;
        public Keys Left;
        public Keys Jump;
        public Keys Attack;
        public Keys Run;
        public Keys Interact;
        public Keys Start;

        public PlayerKeyboardKeys(Keys up, Keys right, Keys down, Keys left, Keys jump, Keys attack, Keys run, Keys interact, Keys start)
        {
            Up = up;
            Right = right;
            Down = down;
            Left = left;
            Jump = jump;
            Attack = attack;
            Run = run;
            Interact = interact;
            Start = start;
        }
    }

    public struct PlayerControllerButtons
    {
        public Buttons Up;
        public Buttons Right;
        public Buttons Down;
        public Buttons Left;
        public Buttons Jump;
        public Buttons Attack;
        public Buttons Run;
        public Buttons Interact;
        public Buttons Start;

        public PlayerControllerButtons(Buttons up, Buttons right, Buttons down, Buttons left, Buttons jump, Buttons attack, Buttons run, Buttons interact, Buttons start)
        {
            Up = up;
            Right = right;
            Down = down;
            Left = left;
            Jump = jump;
            Attack = attack;
            Run = run;
            Interact = interact;
            Start = start;
        }
    }

}