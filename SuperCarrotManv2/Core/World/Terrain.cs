using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperCarrotManv2.Core
{
    public class Terrain : Drawable
    {
        public List<TerrainCollisionObject> TerrainCollisionObjects = new List<TerrainCollisionObject>();
        public List<TerrainTexture> TerrainTextures = new List<TerrainTexture>();

        public Terrain(XmlNode terrainNode)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

    }

    public class TerrainCollisionObject : CollisionObject
    {
        public TerrainCollisionObject(Vector2 position, Vector2 collisionBox) : base(position, collisionBox)
        {
            
        }
    }

    public struct TerrainTexture
    {
        public Vector2 Position { private set; get; }
        public int Id;

        public TerrainTexture(Vector2 position, int id)
        {
            Position = position;
            Id = id;
        }
    }
     
}
