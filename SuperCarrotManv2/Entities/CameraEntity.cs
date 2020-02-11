using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SuperCarrotManv2.Core;

namespace SuperCarrotManv2.Entities
{
    public class CameraEntity
    {
        Vector2 position;
        public Vector2 getPosition() => position;
        VecRectangle Rect = new VecRectangle(0,0,0,0);
        public VecRectangle GetVecRectangle() => Rect;
        Texture2D debugTexture;
        Vector2 SceneBounds;
        public CameraEntity(Player player, Vector2 sceneBounds)
        {
            position = player.GetVecRectangle().getCenterPos();
            SceneBounds = sceneBounds;
        }

    }
}