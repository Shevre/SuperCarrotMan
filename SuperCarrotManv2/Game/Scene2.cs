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
    public class Scene2 : Scene
    {

        Entity crib;
        public bool drawIK = false;
        public Scene2(ContentManager content,Game1 game) : base(content,game)
        {
            crib = new Entity(new Vector2(640, -128), new Vector2(93, 89), content.Load<Texture2D>(@"CocoCrib\Walk\1"));
            AddEntity(crib);

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
            TEXTURES.Add(content.Load<Texture2D>("sans"));
            #endregion

            #region TILEMAP
            /*TILES*/
            int[][] tileArray = new int[8][];
            tileArray[0] = new int[] { 0, 0, 7, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0 };
            tileArray[1] = new int[] { 0, 0, 0, 7, 9, 10, 0, 0, 0, 1, 2, 2, 2 };
            tileArray[2] = new int[] { 0, 0, 0, 0, 0, 2, 0, 0, 10, 7, 8, 8, 8 };
            tileArray[3] = new int[] { 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0 };
            tileArray[4] = new int[] { 2, 2, 2, 2, 3, 0, 10, 0, 0, 0, 0, 0, 0 };
            tileArray[5] = new int[] { 5, 5, 5, 5, 9, 0, 0, 0, 0, 5, 0, 0, 0 };
            tileArray[6] = new int[] { 5, 5, 5, 6, 0, 0, 0, 0, 5, 5, 5, 5, 5 };
            tileArray[7] = new int[] { 5, 5, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            /*COLLISION*/
            List<CollisionObject> collisionList = new List<CollisionObject>();
            collisionList.Add(new CollisionObject(new Vector2(-1, -200), new Vector2(1, 712), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(832, -200), new Vector2(1, 712), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(128, 0), new Vector2(64, 64), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(192, 0), new Vector2(128, 128), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(0, 256), new Vector2(256, 256), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(256, 256), new Vector2(64, 128), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(384, 256), new Vector2(64, 32), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(576, 64), new Vector2(256, 128), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(512, 128), new Vector2(64, 32), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(320, 64), new Vector2(64, 32), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(576, 320), new Vector2(64, 65), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(512, 384), new Vector2(320, 64), false, CollisionObjectTypes.Terrain));
            collisionList.Add(new CollisionObject(new Vector2(0, 512), new Vector2(832, 1), false, CollisionObjectTypes.Terrain));

            #endregion

            #region EVENTS
            AreaEventObject testEvent = new AreaEventObject(new Vector2(128, 128), new Vector2(64, 128));
            testEvent.EventTriggered += TestEvent_EventTriggered;
            events.addEventObject(testEvent);


            #endregion


            TileMap = new TileMap(tileArray, collisionList, TEXTURES, new Microsoft.Xna.Framework.Point(64, 64));
            physics.AddCollisionObject(this);

        }

        private void TestEvent_EventTriggered()
        {
            currentGame.ChangeScene(new Vector2(0, 0), Player, this, 0);
        }


       
    }

    
}
