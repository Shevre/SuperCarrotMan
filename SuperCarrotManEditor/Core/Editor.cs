using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SuperCarrotManEditor.Core
{
    public class Editor
    {
        Scene CurrentScene;
        public Editor()
        {

        }

        public void LoadScene(string sceneLocation,Microsoft.Xna.Framework.Content.ContentManager content)
        {
            CurrentScene = new Scene(File.ReadAllText(sceneLocation),content);
        }
    }
}
