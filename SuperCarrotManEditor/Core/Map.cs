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
            SceneString = sceneString;
            shevConsole.WriteColoredLine("Full Code:", ConsoleColor.Cyan);
            shevConsole.WriteColoredLine(SceneString + "\n\n", ConsoleColor.Red);
            shevConsole.WriteColoredLine("Only Relevant Code:", ConsoleColor.Cyan);
            int StartIndex = SceneString.IndexOf(Consts.TILEMAPSTARTID);
            int EndIndex = SceneString.IndexOf(Consts.TILEMAPENDID) + Consts.TILEMAPENDID.Length;
            int l = EndIndex - StartIndex;

            string subString = SceneString.Substring(StartIndex, l);
            shevConsole.WriteColoredLine(subString,ConsoleColor.Green);
            shevConsole.WriteColored(subString.Substring(subString.IndexOf(Consts.TILETEXTURESSTARTID), subString.IndexOf(Consts.TILETEXTURESENDID) - subString.IndexOf(Consts.TILETEXTURESSTARTID)), ConsoleColor.Yellow); 
            TileTextures = subString.Substring(subString.IndexOf(Consts.TILETEXTURESSTARTID), subString.IndexOf(Consts.TILETEXTURESENDID) - subString.IndexOf(Consts.TILETEXTURESSTARTID)).Split(';');
            
            foreach (string s in TileTextures)
            {
                shevConsole.WriteColored(s + ';', ConsoleColor.DarkYellow);
            }
            
            
        }

    }
}
