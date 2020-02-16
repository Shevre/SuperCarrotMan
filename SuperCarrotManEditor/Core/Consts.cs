using System.IO;

namespace SuperCarrotManEditor.Core
{
    public static class Consts
    {
        public static string BACKUPFOLDER { private set; get; } = "bak";

        public static string TILEMAPSTARTID { private set; get; } = "#region TILEMAP" ;
        public static string TILEMAPENDID { private set; get; } = "#endregion TILEMAP";

        public static string TILETEXTURESSTARTID { private set; get; } = "#region TILETEXTURES";
        public static string TILETEXTURESENDID { private set; get; } = "#endregion TILETEXTURES";
        public static string TILTETEXTURESVARID { private set; get; } = "List<Texture2D> TILEMAPTEXTURES = new List<Texture2D>();";
        public static string TILTETEXTURESADDSTARTID { private set; get; } = "TILEMAPTEXTURES.Add(content.Load<Texture2D>(\"";
        public static string TILTETEXTURESADDENDID { private set; get; } = "\"));";

        public static Microsoft.Xna.Framework.Content.ContentManager CONTENT;

        public static void CreateFolders()
        {
            if (!Directory.Exists(BACKUPFOLDER))
                Directory.CreateDirectory(BACKUPFOLDER);
        }

    }
}
