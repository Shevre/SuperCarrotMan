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

        public static GAME.Area Area;

        public PhysicsHandler physics;
        public EventsHandler events;
        public Game1 currentGame;

        public Vector2 bounds;

        public List<CollisionArea> collisionAreas = new List<CollisionArea>();

        private Camera Camera = new Camera();
        public Matrix getCameraTransform() => Camera.Transform;

        public float gravIntensity = 0.3f;

        public Entities.Player Player;

        public Scene(ContentManager content,Game1 game) 
        {
            currentGame = game;
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

        public void Dispose()
        {
            TileMap.Dispose();
        }
    }

    public class TileMap : Drawable
    {
        int[][] TileArray;
        public int Width { private set; get; }
        public int Height { private set; get; }
        public List<Texture2D> Tiles { private set; get; } = new List<Texture2D>();
        Point TileSize;
        
        public TileMap(int[][] tileArray, List<Texture2D> tiles,Point tileSize) 
        {
            Tiles.Add(null);
            TileArray = tileArray;
            Width = TileArray[0].Length;
            Height = TileArray.Length;
            Tiles.AddRange(tiles);
            TileSize = tileSize;
            
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

        
        public void Dispose()
        {
            foreach(Texture2D t2D in Tiles)
            {
                if(t2D != null)t2D.Dispose();
            }
        }
    }
}
