using Microsoft.Xna.Framework.Graphics;
using Shev.ExtentionMethods;
using System;
using System.Collections.Generic;
using System.IO;

namespace SuperCarrotManEditor.Core
{
    public class Scene
    {
        string SceneString;
        string[] Tiles;
        string[] CollisionObjects;
        string[] TileTextures;
        string TilteTexturesSaveString;
        List<Texture2D> TileTex = new List<Texture2D>();
        public Scene(string sceneString, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            SceneString = sceneString;
            int StartIndex = SceneString.IndexOf(Consts.TILEMAPSTARTID);
            int EndIndex = SceneString.IndexOf(Consts.TILEMAPENDID) + Consts.TILEMAPENDID.Length;
            int l = EndIndex - StartIndex;

            string subString = SceneString.Substring(StartIndex, l);
            ShevConsole.WriteColoredLine(subString, ConsoleColor.Green);

            TilteTexturesSaveString = subString.Substring(subString.IndexOf(Consts.TILETEXTURESSTARTID), subString.IndexOf(Consts.TILETEXTURESENDID) - subString.IndexOf(Consts.TILETEXTURESSTARTID));
            string[] tempArray;
            tempArray = subString.Substring(subString.IndexOf(Consts.TILETEXTURESSTARTID), subString.IndexOf(Consts.TILETEXTURESENDID) - subString.IndexOf(Consts.TILETEXTURESSTARTID)).Split(';');
            TileTextures = new string[tempArray.Length - 2];
            for (int i = 1; i < tempArray.Length - 1; i++)
                TileTextures[i - 1] = tempArray[i].Replace("\n", "").Replace("\r","").Replace(" ", "") + ';';
            for(int i = 0; i < TileTextures.Length; i++)
            {
                TileTextures[i] = TileTextures[i].Replace(Consts.TILTETEXTURESADDSTARTID, "").Replace(Consts.TILTETEXTURESADDENDID, "");
                ShevConsole.WriteColoredLine(TileTextures[i], ConsoleColor.Blue);
                TileTex.Add(content.Load<Texture2D>(TileTextures[i])) ;
            }
            


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < TileTex.Count; i++)
            {
                spriteBatch.Draw(TileTex[i], new Microsoft.Xna.Framework.Vector2(i * 64, 0), Microsoft.Xna.Framework.Color.White);
            }

        }

        public void Save(string SaveLocation,string FileName)
        {
            string TextureOverrwriteString = Consts.TILEMAPSTARTID + "\n";
            TextureOverrwriteString += Consts.TILTETEXTURESVARID + "\n";
            foreach (string s in TileTextures)
            {
                TextureOverrwriteString += Consts.TILTETEXTURESADDSTARTID + s + Consts.TILTETEXTURESADDENDID + "\n";
            }
            TextureOverrwriteString += Consts.TILETEXTURESENDID;
            File.WriteAllText(Consts.BACKUPFOLDER + "\\" + FileName,SceneString);
            ShevConsole.WriteColoredLine("Saved Scene to " + SaveLocation + FileName,ConsoleColor.Cyan);
            
            SceneString = SceneString.Replace(TilteTexturesSaveString, TextureOverrwriteString);
            File.WriteAllText(SaveLocation + FileName, SceneString);
        }
    }
}
