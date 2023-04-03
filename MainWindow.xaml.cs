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

        private GameState gameState = new GameState();
        public MainWindow()
        {
            InitializeComponent();
            ImageControls = ConstroctorCanvas(gameState.GameGrid);
        }

        private Image[,] ConstroctorCanvas(Cuadricula cuadicula) 
        {
            Image[,] Imagenes = new Image[cuadicula.Fila(),cuadicula.Columna()];
            int TamCeldas = 25;
            for (int fila = 0; fila < cuadicula.Fila(); fila++)
            {
                for (int Colum = 0; Colum < cuadicula.Columna(); Colum++)
                {
                    Image imagen = new Image()
                    {
                        Width = TamCeldas,
                        Height = TamCeldas

                    };
                    Canvas.SetTop(imagen, (fila - 2) * TamCeldas);
                    Canvas.SetLeft(imagen, Colum*TamCeldas);
                    GameCanvas.Children.Add(imagen);
                    Imagenes[fila,Colum]= imagen;
                }
            }

            return Imagenes;
        }

        private async Task loop()
        {
            dibujar(gameState);
            while (!gameState.GameOver)
            {
                await Task.Delay(500);
                gameState.MoveBlockDown();
                dibujar(gameState);
            }
            GameOverMenu.Visibility= Visibility.Visible;
            FinalScoreText.Text = $"Score: {gameState.Score}";
        }

        private void DibujarTablero(Cuadricula cuadricula)
        {
            for (int fila = 0; fila < cuadricula.Fila(); fila++)
            {
                for (int Colum = 0; Colum < cuadricula.Columna(); Colum++)
                {
                    int id = cuadricula[fila,Colum];
                    ImageControls[fila, Colum].Source = Tiles[id];
                }

            }
        }

        private void SiguienteBloque(SecuenciaBloques secuencia)
        {
            Block siguiente = secuencia.NextBlock;
            NextImage.Source = Shapes[siguiente.Id];
        }

        private void DibujarBloque(Block bloque)
        {
            foreach (Position posicion in bloque.TilePositions())
            {
                ImageControls[posicion.Row, posicion.Column].Source = Tiles[bloque.Id];
            }
        }

        private void Held(Block held)
        {
            if (held == null)
            {
                HoldImage.Source = Shapes[0];
            }
        }

        private void dibujar(GameState game)
        {
            DibujarTablero(game.GameGrid);
            DibujarBloque(game.CurrentBlock);
            SiguienteBloque(game.BlockQueue);
            ScoreText.Text = $"Score: {game.Score}";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    gameState.MoveBlockLeft(); 
                    break;
                case Key.Right:
                    gameState.MoveBlockRight();
                    break;
                case Key.Down:
                    gameState.MoveBlockDown();
                    break;
                case Key.Up:
                    gameState.RotateBlockCW();
                    break;
                case Key.Z:
                    gameState.RotateBlockCCW();
                    break;
                case Key.C:
                    gameState.HoldBlock();
                    break;
                default:
                    return;
            }
            dibujar(gameState);
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await loop();
        }

        private async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GameState();
            GameOverMenu.Visibility=Visibility.Hidden;
            await loop();

        }
    }
}
