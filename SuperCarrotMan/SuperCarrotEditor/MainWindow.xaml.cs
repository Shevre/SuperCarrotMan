using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
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
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
            
            
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

        

       

        private void WindowMain_Initialized(object sender, EventArgs e)
        {
            
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {

            epiclabel.Content = $"{TileViewer.initSelectX},{TileViewer.initSelectY}";
           
        }

        private void TileViewer_Initialized(object sender, EventArgs e)
        {
            //TileViewer.SetTilesList(Editor.getTilesList());
        }

        private void TileViewer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void TileViewer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TileViewer.brushMode == TileBrushMode.Single)
            {
                Editor.TileBrushId = TileViewer.selectedId;
                Editor.brushMode = TileBrushMode.Single;
            }
            else if (TileViewer.brushMode == TileBrushMode.Selection)
            {
                Editor.SelectedIds = TileViewer.selectedIds;
                Editor.brushMode = TileBrushMode.Selection;
            }
            
            
            
        }

        private void LayerSelector_TargetUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void Editor_Initialized(object sender, EventArgs e)
        {
            //for (int i = 0; i < Editor.Levels[Editor.currentLevel].levelLayers.Count(); i++)
            //{
            //    LayerSelector.Items.Add(i);
            //}
        }

        private void ChangeLayer_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            Editor.Levels[Editor.currentLevel].CurrentLayer = int.Parse(b.Content.ToString());
        }
    }
}
