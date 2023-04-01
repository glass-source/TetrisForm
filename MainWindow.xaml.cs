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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] Tiles = new ImageSource[]
        {
            new BitmapImage(new Uri("Resorse/TileEmpty.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/TileI.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/TileJ.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/TileL.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/TileO.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/TileS.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/TileT.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/TileZ.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/TileLI.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/TileLJ.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/TileLL.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/TileLLL.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/TileOA.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/TileOB.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/TileLS.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/TileTA.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/TileTB.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/TileLZ.png",UriKind.Relative))
        };

        private readonly ImageSource[] Shapes = new ImageSource[]
        {
            new BitmapImage(new Uri("Resorse/ShapeEmpty.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/ShapeI.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/ShapeJ.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/ShapeL.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/ShapeO.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/ShapeS.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/ShapeT.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/ShapeZ.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/ShapeLI.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/ShapeLJ.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/ShapeLL.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/ShapeLLL.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/ShapeOA.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/ShapeOB.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/ShapeLS.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/ShapeTA.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/ShapeTB.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorse/ShapeLZ.png",UriKind.Relative))
        };

        private readonly Image[,] ImageControls;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void GameCanvas_Loaded(object sender, KeyEventArgs e)
        {

        }
    }
}
