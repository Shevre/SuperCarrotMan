using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using System.Windows;
using Microsoft.Win32;
using Shev.ExtentionMethods;

namespace SuperCarrotManEditor.Core
{
    public class Editor
    {
        public Scene CurrentScene { private set; get; }
        public Editor()
        {

        }

        public void LoadScene(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "C# Code Files (.cs)|*.cs"; // Filter files by extension
            Nullable<bool> result = dlg.ShowDialog();

            if(result == true)
            {
                CurrentScene = new Scene(File.ReadAllText(dlg.FileName), content);
            }
            else
            {
                ShevConsole.WriteColoredLine("Please select a file",ConsoleColor.Red);
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScene.Draw(spriteBatch);
        }
    }
}
