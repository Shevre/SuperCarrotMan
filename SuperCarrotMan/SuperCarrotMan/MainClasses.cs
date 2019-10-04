using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace SuperCarrotMan
{
    class Level
    {
        char[,] level;
        Vector2 playerStartPos;
        string name;
        public Level(string LevelFolder) 
        {
            string[] _s = File.ReadAllText(LevelFolder + @"\properties.cfg").Split(';');
            foreach (string s in _s)
            {
                if (s.Contains("Name="))
                {
                    name = s.Replace("Name=", "");
                }
                else if (s.Contains("StartPos=")) 
                {
                    string[] _i = s.Replace("StartPos=", "").Split(',');
                    playerStartPos = new Vector2(Convert.ToInt32(_i[0]), Convert.ToInt32(_i[1]));
                }
            }
            string[] sLevel = File.ReadAllText(LevelFolder + @"\terrain.txt").Split(',');
            level = new char[sLevel.Length, sLevel[0].Length];
            for (int y = 0; y < sLevel.Length; y++)
            {
                for (int x = 0; x < sLevel[0].Length; x++)
                {
                    level[y, x] = sLevel[y][x];
                }
            }
            
        }

        public override string ToString()
        {
            return name;
        }
    }
    
}
