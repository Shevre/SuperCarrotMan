using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace SuperCarrotManEditor.Core
{
    public class Editor
    {
        public Scene CurrentScene { private set; get; }
        public Editor()
        {

        }

        public void LoadScene(string sceneLocation,Microsoft.Xna.Framework.Content.ContentManager content)
        {
            CurrentScene = new Scene(File.ReadAllText(sceneLocation),content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScene.Draw(spriteBatch);
        }
    }
}
