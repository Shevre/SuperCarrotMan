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

        public void DebugDraw(SpriteBatch spriteBatch,List<CollisionObject> collisionObjects) 
        {
            if (Debugging) 
            {
                foreach (CollisionObject item in collisionObjects)
                {
                    item.GetVecRectangle().Draw(spriteBatch);
                }
            }

            
        }
    }

}
