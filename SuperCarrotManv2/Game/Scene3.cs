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
    public class Scene3 : Scene
    {

        Entity crib;

        public bool drawIK = false;
        public Scene3(ContentManager content,Game1 game) : base(content,game)
        {
            Area = Area.Outside;
            //crib = new Entity(new Vector2(640, -128), new Vector2(93, 89), content.Load<Texture2D>(@"CocoCrib\Walk\1"));
            //AddEntity(crib);

            #region TILEMAP

            #region TILETEXTURES
            List<Texture2D> TILEMAPTEXTURES = new List<Texture2D>();
            TILEMAPTEXTURES.Add(content.Load<Texture2D>("Grass/00"));
            TILEMAPTEXTURES.Add(content.Load<Texture2D>("Grass/01"));
            TILEMAPTEXTURES.Add(content.Load<Texture2D>("Grass/02"));
            TILEMAPTEXTURES.Add(content.Load<Texture2D>("Grass/10"));
            TILEMAPTEXTURES.Add(content.Load<Texture2D>("Grass/11"));
            TILEMAPTEXTURES.Add(content.Load<Texture2D>("Grass/12"));
            TILEMAPTEXTURES.Add(content.Load<Texture2D>("Grass/20"));
            TILEMAPTEXTURES.Add(content.Load<Texture2D>("Grass/21"));
            TILEMAPTEXTURES.Add(content.Load<Texture2D>("Grass/22"));
            TILEMAPTEXTURES.Add(content.Load<Texture2D>("sans"));
            TILEMAPTEXTURES.Add(content.Load<Texture2D>("CarrotMan/Walk/1"));
            #endregion TILETEXTURES

            #region TILES
            int[][] TILEARRAY = new int[8][];
            TILEARRAY[0] = new int[] { 0, 0, 7, 5, 6, 0 , 0 , 0, 0 , 0, 0, 0, 0 };
            TILEARRAY[1] = new int[] { 0, 0, 0, 7, 9, 10, 0 , 0, 0 , 1, 2, 2, 2 };
            TILEARRAY[2] = new int[] { 0, 0, 0, 0, 0, 0 , 0 , 0, 10, 7, 8, 8, 8 };
            TILEARRAY[3] = new int[] { 0, 0, 0, 0, 0, 0 , 0 , 0, 0 , 0, 0, 0, 0 };
            TILEARRAY[4] = new int[] { 2, 2, 2, 2, 3, 0 , 10, 0, 0 , 0, 0, 0, 0 };
            TILEARRAY[5] = new int[] { 5, 5, 5, 5, 9, 0 , 0 , 0, 0 , 5, 0, 0, 0 };
            TILEARRAY[6] = new int[] { 5, 5, 5, 6, 0, 0 , 0 , 0, 5 , 5, 5, 5, 5 };
            TILEARRAY[7] = new int[] { 5, 5, 5, 6, 0, 0 , 0 , 0, 0 , 0, 0, 0, 0 };
            #endregion TILES

            #region COLLISION
            List<CollisionObject> collisionObjects = new List<CollisionObject>();
            collisionObjects.Add(new CollisionObject(new Vector2(-1 ,-200), new Vector2(1  , 712), false, CollisionObjectTypes.Terrain));
            collisionObjects.Add(new CollisionObject(new Vector2(832,-200), new Vector2(1  , 712), false, CollisionObjectTypes.Terrain));
            collisionObjects.Add(new CollisionObject(new Vector2(128, 0  ), new Vector2(64 , 64 ), false, CollisionObjectTypes.Terrain));
            collisionObjects.Add(new CollisionObject(new Vector2(192, 0  ), new Vector2(128, 128), false, CollisionObjectTypes.Terrain));
            collisionObjects.Add(new CollisionObject(new Vector2(0  , 256), new Vector2(256, 256), false, CollisionObjectTypes.Terrain));
            collisionObjects.Add(new CollisionObject(new Vector2(256, 256), new Vector2(64 , 128), false, CollisionObjectTypes.Terrain));
            collisionObjects.Add(new CollisionObject(new Vector2(384, 256), new Vector2(64 , 32 ), false, CollisionObjectTypes.Terrain));
            collisionObjects.Add(new CollisionObject(new Vector2(576, 64 ), new Vector2(256, 128), false, CollisionObjectTypes.Terrain));
            collisionObjects.Add(new CollisionObject(new Vector2(512, 128), new Vector2(64 , 32 ), false, CollisionObjectTypes.Terrain));
            collisionObjects.Add(new CollisionObject(new Vector2(320, 64 ), new Vector2(64 , 32 ), false, CollisionObjectTypes.Terrain));
            collisionObjects.Add(new CollisionObject(new Vector2(576, 320), new Vector2(64 , 65 ), false, CollisionObjectTypes.Terrain));
            collisionObjects.Add(new CollisionObject(new Vector2(512, 384), new Vector2(320, 64 ), false, CollisionObjectTypes.Terrain));
            collisionObjects.Add(new CollisionObject(new Vector2(0  , 512), new Vector2(832, 1  ), false, CollisionObjectTypes.Terrain));
            CollisionArea colArea1 = new CollisionArea(collisionObjects, new VecRectangle(0, -200, 832, 712));
            #endregion COLLISION
            #endregion TILEMAP

            #region EVENTS
            AreaEventObject testEvent = new AreaEventObject(new Vector2(128, 128), new Vector2(64, 128));
            testEvent.EventTriggered += TestEvent_EventTriggered;
            events.addEventObject(testEvent);

            AreaEventObject testEvent2 = new AreaEventObject(new Vector2(768, -64), new Vector2(64, 128));
            testEvent2.EventTriggered += TestEvent2_EventTriggered;
            events.addEventObject(testEvent2);
            #endregion

            TileMap = new TileMap(TILEARRAY, TILEMAPTEXTURES, new Point(64, 64));
            collisionAreas.Add(colArea1);
            physics.AddCollisionObject(this);
            bounds = new Vector2(20000, 20000);
        }

        private void TestEvent_EventTriggered()
        {
            Game1.IK.YES = true;
            
        }

        private void TestEvent2_EventTriggered()
        {
            Game1.IK.YES = false;
            
            currentGame.ChangeScene(new Vector2(0, 0), this, 1);
        }

       
    }

    
}
