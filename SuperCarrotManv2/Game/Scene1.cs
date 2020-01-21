using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SuperCarrotManv2.Core;

namespace SuperCarrotManv2.GAME
{
    class Scene1 : Scene
    {

        public Scene1(ContentManager content) : base(content)
        {
            List<Texture2D> texture2Ds = new List<Texture2D>();
            texture2Ds.Add(content.Load<Texture2D>(@"Grass\00"));
            texture2Ds.Add(content.Load<Texture2D>(@"Grass\01"));
            texture2Ds.Add(content.Load<Texture2D>(@"Grass\02"));
            texture2Ds.Add(content.Load<Texture2D>(@"Grass\10"));
            texture2Ds.Add(content.Load<Texture2D>(@"Grass\11"));
            texture2Ds.Add(content.Load<Texture2D>(@"Grass\12"));
            texture2Ds.Add(content.Load<Texture2D>(@"Grass\20"));
            texture2Ds.Add(content.Load<Texture2D>(@"Grass\21"));
            texture2Ds.Add(content.Load<Texture2D>(@"Grass\22"));

            //TILEMAP
            int[][] tileArray = new int[8][];
            tileArray[0] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            tileArray[1] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            tileArray[2] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2 };
            tileArray[3] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 8, 8, 8 };
            tileArray[4] = new int[] { 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0 };
            tileArray[5] = new int[] { 5, 5, 5, 5, 9, 0, 0, 0, 0, 0, 0, 0, 0 };
            tileArray[6] = new int[] { 5, 5, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            tileArray[7] = new int[] { 5, 5, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            int[][] collisionArray = new int[8][];
            collisionArray[0] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            collisionArray[1] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            collisionArray[2] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 };
            collisionArray[3] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 };
            collisionArray[4] = new int[] { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 };
            collisionArray[5] = new int[] { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 };
            collisionArray[6] = new int[] { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            collisionArray[7] = new int[] { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            TileMap = new TileMap(tileArray,collisionArray, texture2Ds, new Microsoft.Xna.Framework.Point(64, 64));
        }
    }
}
