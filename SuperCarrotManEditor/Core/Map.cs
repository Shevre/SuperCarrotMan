using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shev.ExtentionMethods;

namespace SuperCarrotManEditor.Core
{
    public class Scene
    {
        string SceneString;
        string[] Tiles;
        string[] CollisionObjects;
        string[] TileTextures;
        public Scene(string sceneString)
        {
            Console.WriteLine("Ayo");
            SceneString = sceneString;
            ShevConsole.WriteColoredLine("Full Code:", ConsoleColor.Cyan);
            ShevConsole.WriteColoredLine(SceneString + "\n\n", ConsoleColor.Red);
            ShevConsole.WriteColoredLine("Only Relevant Code:", ConsoleColor.Cyan);
            int StartIndex = SceneString.IndexOf(Consts.TILEMAPSTARTID);
            int EndIndex = SceneString.IndexOf(Consts.TILEMAPENDID) + Consts.TILEMAPENDID.Length;
            int l = EndIndex - StartIndex;

            string subString = SceneString.Substring(StartIndex, l);
            ShevConsole.WriteColoredLine(subString,ConsoleColor.Green);

            string[] tempArray;
            tempArray = subString.Substring(subString.IndexOf(Consts.TILETEXTURESSTARTID), subString.IndexOf(Consts.TILETEXTURESENDID) - subString.IndexOf(Consts.TILETEXTURESSTARTID)).Split(';');
            TileTextures = new string[tempArray.Length - 2];
            for (int i = 1; i < tempArray.Length - 1; i++)
            {
                TileTextures[i - 1] = tempArray[i].Replace("\n","").Replace(" ","") + ';';
            }
            foreach (string s in TileTextures)
            {
                ShevConsole.WriteColoredLine(s, ConsoleColor.DarkYellow);
            }
            Console.Read();        
            }

    }
}
