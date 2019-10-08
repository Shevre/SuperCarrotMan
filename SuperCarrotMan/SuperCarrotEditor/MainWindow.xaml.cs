using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperCarrotEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            Editor.Levels[Editor.currentLevel].Reload();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Editor.Levels[Editor.currentLevel].name = NameBox.Text;
            Editor.Levels[Editor.currentLevel].save();
        }

        private void TileIdSelector0_Checked(object sender, RoutedEventArgs e)
        {
            Editor.TileBrushId = 0;
        }

        private void TileIdSelector1_Checked(object sender, RoutedEventArgs e)
        {
            Editor.TileBrushId = 1;
        }

        private void TileIdSelector2_Checked(object sender, RoutedEventArgs e)
        {
            Editor.TileBrushId = 2;
        }
    }
}
