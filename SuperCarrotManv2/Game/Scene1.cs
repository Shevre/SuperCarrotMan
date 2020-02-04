using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SuperCarrotManv2.Core;

namespace SuperCarrotManv2.GAME
{
    class Scene1 : Scene
    {

        public Scene1(ContentManager content) : base(content)
        {
            #region TEXTURES
            List<Texture2D> TEXTURES = new List<Texture2D>();
            TEXTURES.Add(content.Load<Texture2D>(@"Grass\00"));
            TEXTURES.Add(content.Load<Texture2D>(@"Grass\01"));
            TEXTURES.Add(content.Load<Texture2D>(@"Grass\02"));
            TEXTURES.Add(content.Load<Texture2D>(@"Grass\10"));
            TEXTURES.Add(content.Load<Texture2D>(@"Grass\11"));
            TEXTURES.Add(content.Load<Texture2D>(@"Grass\12"));
            TEXTURES.Add(content.Load<Texture2D>(@"Grass\20"));
            TEXTURES.Add(content.Load<Texture2D>(@"Grass\21"));
            TEXTURES.Add(content.Load<Texture2D>(@"Grass\22"));
            #endregion

            #region TILEMAP
            /*TILES*/int[][] tileArray = new int[8][];
            tileArray[0] = new int[] { 0, 0, 7, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0 };
            tileArray[1] = new int[] { 0, 0, 0, 7, 9, 0, 0, 0, 0, 1, 2, 2, 2 };
            tileArray[2] = new int[] { 0, 0, 0, 0, 0, 5, 0, 0, 0, 7, 8, 8, 8 };
            tileArray[3] = new int[] { 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0 };
            tileArray[4] = new int[] { 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0 };
            tileArray[5] = new int[] { 5, 5, 5, 5, 9, 0, 0, 0, 0, 5, 0, 0, 0 };
            tileArray[6] = new int[] { 5, 5, 5, 6, 0, 0, 0, 0, 5, 5, 5, 5, 5 };
            tileArray[7] = new int[] { 5, 5, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            /*COLLISION*/
            List<CollisionObject> collisionList = new List<CollisionObject>();
            collisionList.Add(new CollisionObject(new Vector2(128, 0), new Vector2(64, 64), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(192, 0), new Vector2(128, 128), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(0, 256), new Vector2(256, 256), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(320, 128), new Vector2(64, 128), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(256, 256), new Vector2(64, 128), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(576, 64), new Vector2(256,128), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(576, 320), new Vector2(64, 65), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(512, 384), new Vector2(320, 64), false, CollisionObjectTypes.Terrain));

            #endregion
            TileMap = new TileMap(tileArray,collisionList, TEXTURES, new Microsoft.Xna.Framework.Point(64, 64));
        }
    }
}
