using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarrotManEditor.Core
{
    public static class Consts
    {
        public static string TILEMAPSTARTID { private set; get; } = "#region TILEMAP" ;
        public static string TILEMAPENDID { private set; get; } = "#endregion TILEMAP";

        public static string TILETEXTURESSTARTID { private set; get; } = "#region TILETEXTURES";
        public static string TILETEXTURESENDID { private set; get; } = "#endregion TILETEXTURES";
    }
}
