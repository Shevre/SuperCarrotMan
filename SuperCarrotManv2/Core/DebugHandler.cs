using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarrotManv2.Core
{
    public class DebugHandler
    {
        public string debugString = "";
        private bool Debugging = false;
        public bool IsDebugging() => Debugging;
        public void Log(string s) 
        {
            debugString += s;
        }

        public void ClearString() 
        {
            debugString = "";
        }

        public void DebugUpdate(KeyboardState currentState, KeyboardState prevState) 
        {
            if (currentState.IsKeyDown(Keys.LeftControl) && (currentState.IsKeyDown(Keys.D) && prevState.IsKeyUp(Keys.D))) Debugging = !Debugging;
        }

        public void DebugDraw(SpriteBatch spriteBatch,List<CollisionObject> collisionObjects = null,List<AreaEventObject> areaEventObjects = null) 
        {
            if (Debugging) 
            {
                if(collisionObjects != null)
                    foreach (CollisionObject item in collisionObjects)
                    {
                        if(item.type == CollisionObjectTypes.Player) item.GetVecRectangle().Draw(spriteBatch, Color.Goldenrod);
                        else item.GetVecRectangle().Draw(spriteBatch,Color.Red);
                    };
                if (areaEventObjects != null)
                    foreach (AreaEventObject item in areaEventObjects)
                    {
                        item.BoundingBox.Draw(spriteBatch, Color.Black);
                    };
            }

            
        }
    }

}
