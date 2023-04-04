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
            new BitmapImage(new Uri("Resorses/TileEmpty.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/TileI.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/TileJ.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/TileL.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/TileO.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/TileS.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/TileT.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/TileZ.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/TileLI.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/TileLJ.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/TileLL.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/TileLLL.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/TileOA.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/TileOB.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/TileLS.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/TileTA.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/TileTB.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/TileLZ.png",UriKind.Relative))
        };

        private readonly ImageSource[] Shapes = new ImageSource[]
        {
            new BitmapImage(new Uri("Resorses/ShapeEmpty.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/ShapeI.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/ShapeJ.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/ShapeL.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/ShapeO.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/ShapeS.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/ShapeT.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/ShapeZ.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/ShapeLI.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/ShapeLJ.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/ShapeLL.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/ShapeLLL.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/ShapeOA.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/ShapeOB.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/ShapeLS.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/ShapeTA.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/ShapeTB.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/ShapeLZ.png",UriKind.Relative))
        };

        private readonly Image[,] ImageControls;
        private readonly int Maxdelay = 1000;
        private readonly int Mindelay = 75;
        private readonly int DelayDecrease= 25;

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
                int delay = Math.Max(Mindelay,Maxdelay-(gameState.Score*DelayDecrease));
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
                    ImageControls[fila, Colum].Opacity = 1;
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
                ImageControls[posicion.Row,posicion.Column].Opacity = 1;
                ImageControls[posicion.Row, posicion.Column].Source = Tiles[bloque.Id];
            }
        }

        private void DibujarBloqueFantasma(Block bloque)
        {
            int distancia = gameState.BlockDropDistance();
            foreach (Position posicion in bloque.TilePositions())
            {                
                ImageControls[posicion.Row + distancia, posicion.Column].Source = Tiles[bloque.Id];
                ImageControls[posicion.Row + distancia, posicion.Column].Opacity = 0.25;
            }
        }

        private void Held(Block held)
        {
            if (held == null)
            {
                HoldImage.Source = Shapes[0];
            }
            else
            {
                HoldImage.Source = Shapes[held.Id];
            }
        }

        private void dibujar(GameState game)
        {
            DibujarTablero(game.GameGrid);
            DibujarBloqueFantasma(game.CurrentBlock);
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
                case Key.X:
                    gameState.RotateBlockCW();
                    break;
                case Key.Z:
                    gameState.RotateBlockCCW();
                    break;
                case Key.H:
                    gameState.HoldBlock();
                    break;
                case Key.Space:
                    gameState.DropBlock();
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
