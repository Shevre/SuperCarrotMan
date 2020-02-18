using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using System.Windows;
using Microsoft.Win32;
using Microsoft.Xna.Framework.Input;
using Shev.ExtentionMethods;

namespace SuperCarrotManEditor.Core
{
    public class Editor
    {
        public Scene CurrentScene { private set; get; }
        bool Editing = false;
        public Editor()
        {

        }

        public void LoadScene(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "C# Code Files (.cs)|*.cs"; // Filter files by extension
            bool? result = dlg.ShowDialog();
            
            if(result == true)
            {
                CurrentScene = new Scene(File.ReadAllText(dlg.FileName), content);
                Editing = true;
            }
            else
            {
                ShevConsole.WriteColoredLine("Please select a file",ConsoleColor.Red);
                Editing = false;
            }
            
        }

        public void Update()
        {
            if (Game1.CurrentState.IsKeyDown(Keys.LeftControl) && Game1.CurrentState.IsKeyDown(Keys.O) && Game1.PrevState.IsKeyUp(Keys.O)) LoadScene(Consts.CONTENT);
            if (Game1.CurrentState.IsKeyDown(Keys.LeftControl) && Game1.CurrentState.IsKeyDown(Keys.S) && Game1.PrevState.IsKeyUp(Keys.S)) Save("", "Gaming.cs");
        }

        public void Save(string saveLocation,string fileName)
        {
            if (Editing) CurrentScene.Save(saveLocation, fileName);
            else ShevConsole.WriteColoredLine("No file currently open", ConsoleColor.Red);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(Editing)CurrentScene.Draw(spriteBatch);

        }
    }
}
