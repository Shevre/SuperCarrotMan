using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperCarrotMan
{
    class Level
    {
        char[,] level;
        Vector2 playerStartPos;
        public Level(string[] sLevels, Vector2 playerStartPos) 
        {
            level = new char[sLevels.Length, sLevels[0].Length];
            for (int y = 0; y < sLevels.Length; y++)
            {
                for (int x = 0; x < sLevels[0].Length; x++)
                {
                    level[y, x] = sLevels[y][x];
                }
            }
            this.playerStartPos = playerStartPos;
        }
    }
    
}
