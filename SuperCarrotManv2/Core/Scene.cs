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
        
        



        public Scene(ContentManager content) 
        {
            
        }

        public void Update() 
        {
            
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            TileMap.Draw(spriteBatch);
            foreach (Entity e in Entities)
            {
                e.Draw(spriteBatch);
            }
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
        public TileMap(int[][] tileArray,int[][] collisionArray, List<Texture2D> tiles,Point tileSize) 
        {
            Tiles.Add(null);
            TileArray = tileArray;
            Width = TileArray[0].Length;
            Height = TileArray.Length;
            Tiles.AddRange(tiles);
            TileSize = tileSize;
            MergeCollision(collisionArray);
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
            //List<Point> Merged = new List<Point>();
            //for (int y = 0; y < Height; y++)
            //{
            //    for (int x = 0; x < Width; x++)
            //    {
            //        Point currentPoint = new Point(x, y);
            //        if (!Merged.Contains(currentPoint)) 
            //        {
            //            if (collisionArray[y][x] == 1) 
            //            {
            //                bool running = true, xChecked = false, yChecked = false;
            //                int xCount = 0, yCount = 0;
            //                while (running)
            //                {
            //                    if (collisionArray[y + 1 + yCount][x] == 1) yCount++;
            //                    else yChecked = true;
            //                    if (collisionArray[y][x + 1 + xCount] == 1) xCount++;
            //                    else xChecked = true;
                                
                                

                                
            //                    running = !(xChecked && yChecked);
            //                }
            //            }
                        
            //        }
            //    }
            //}
        }
    }
}
