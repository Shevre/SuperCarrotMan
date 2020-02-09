using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarrotManv2.Core
{
    public class Scene : Drawable
    {
        public List<Entity> Entities = new List<Entity>();

        public TileMap TileMap;

        public PhysicsHandler physics;
        public EventsHandler events;
        
        private Camera Camera = new Camera();
        public Matrix getCameraTransform() => Camera.Transform;

        public float gravIntensity = 0.3f;

        public Entities.Player Player;

        public Scene(ContentManager content) 
        {
            physics = new PhysicsHandler(gravIntensity);
            events = new EventsHandler();
        }

        public void Update() 
        {
            physics.Update();
            events.Update(this);
            Camera.Follow(Player);

        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            
            TileMap.Draw(spriteBatch);
            foreach (Entity e in Entities)
            {
                e.Draw(spriteBatch);
            }
            Player.Draw(spriteBatch);

        }

        public void AddPlayer(Entities.Player player) 
        {
            Player = player;
            physics.AddCollisionObject(player);
            
        }

        public void AddEntity(Entity entity)
        {
            Entities.Add(entity);
            physics.AddCollisionObject(entity);
        }
    }

    public class TileMap : Drawable
    {
        int[][] TileArray;
        public int Width { private set; get; }
        public int Height { private set; get; }
        public List<Texture2D> Tiles { private set; get; } = new List<Texture2D>();
        Point TileSize;
        public List<CollisionObject> CollisionObjects = new List<CollisionObject>();
        public TileMap(int[][] tileArray,List<CollisionObject> collisionObjects, List<Texture2D> tiles,Point tileSize) 
        {
            Tiles.Add(null);
            TileArray = tileArray;
            Width = TileArray[0].Length;
            Height = TileArray.Length;
            Tiles.AddRange(tiles);
            TileSize = tileSize;
            CollisionObjects.AddRange(collisionObjects);
            Console.WriteLine("funny");
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            for (int y = 0; y < TileArray.Length; y++)
            {
                for (int x = 0; x < TileArray[y].Length; x++)
                {
                    if (TileArray[y][x] != 0) spriteBatch.Draw(Tiles[TileArray[y][x]], new Vector2(x * TileSize.X, y * TileSize.Y), Color.White);
                }
            }
            
        }

        void MergeCollision(int[][] collisionArray) 
        {
            List<List<Point>> p = new List<List<Point>>();
            int pointOffset = 0;
            p.Add(new List<Point>());
            for (int y = 0; y < Height; y++)
            {
                if(p[pointOffset].Count > 0) 
                {
                    p.Add(new List<Point>());
                    pointOffset++;
                }
                
                for (int x = 0; x < Width; x++)
                {
                    //if(collisionArray[y][x] != 0)CollisionObjects.Add(new CollisionObject(new Vector2(x * 64, y * 64), new Vector2(64, 64),false));
                    if(collisionArray[y][x] == 1) 
                    {
                        p[pointOffset].Add(new Point(x, y));
                    }
                    else if(collisionArray[y][x] == 0 && p[pointOffset].Count > 0) 
                    {
                        p.Add(new List<Point>());
                        pointOffset++;
                    }
                    
                }
            }
            foreach (List<Point> PointList in p)
            {
                if (PointList.Count > 0) CollisionObjects.Add(new CollisionObject(new Vector2(PointList[0].X * TileSize.X, PointList[0].Y * TileSize.Y), new Vector2(((PointList[PointList.Count - 1].X - PointList[0].X) + 1) * TileSize.X, TileSize.Y), false));

            }

            //for (int i = 0; i < p.Count; i++)
            //{
            //    if (p[i].Count > 0) CollisionObjects.Add(new CollisionObject(new Vector2(p[i][0].X * TileSize.X, p[i][0].Y * TileSize.Y), new Vector2(((p[i][p[i].Count - 1].X - p[i][0].X) + 1) * TileSize.X, TileSize.Y), false));


            //}

        }
    }
}
