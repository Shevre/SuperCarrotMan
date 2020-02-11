using Microsoft.Xna.Framework;
using SuperCarrotManv2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarrotManv2.Core
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public Camera()
        {

        }

        public void Follow(Entity target)
        {
            Transform = Matrix.CreateTranslation((-target.getPosition().X  ) - ((target.GetVecRectangle().Width  ) / 2), (-target.getPosition().Y  ) - ((target.GetVecRectangle().Height  ) / 2) - 120, 0) * Matrix.CreateTranslation(Game1.defScreenWidth / 2, Game1.defScreenWidth / 2, 0);
        }
        public void Follow(CameraEntity target)
        {
            Transform = Matrix.CreateTranslation((-target.getPosition().X) - ((target.GetVecRectangle().Width) / 2), (-target.getPosition().Y) - ((target.GetVecRectangle().Height) / 2) - 120, 0) * Matrix.CreateTranslation(Game1.defScreenWidth / 2, Game1.defScreenWidth / 2, 0);
        }
    }
}
