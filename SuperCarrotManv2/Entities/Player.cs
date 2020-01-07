using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperCarrotManv2.Entities
{
    public class Player : Entity
    {
        public Player(Vector2 position, Vector2 collisionBox, Texture2D texture) : base(position, collisionBox, texture)
        {

        }
    }
}
