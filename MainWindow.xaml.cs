using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        private readonly ImageSource[] Mutes = new ImageSource[]
        {
            new BitmapImage(new Uri("Resorses/mute.png",UriKind.Relative)),
            new BitmapImage(new Uri("Resorses/unmute.png",UriKind.Relative))
        };

        private readonly Image[,] ImageControls;
        private readonly int Maxdelay = 1000;
        private readonly int Mindelay = 75;
        private readonly int DelayDecrease= 25;
        private bool Startgame = false;
        private bool Mute = false;
        private int highestScore = 0;
        private string highestScorePlayer = "";
        MediaPlayer mediaPlayer = new MediaPlayer();
        private int numplayer= 0;

        private GameState gameState = new GameState();
        public MainWindow()
        {
            InitializeComponent();
            mediaPlayer.Open(new Uri("Tetris_Song.mp3", UriKind.Relative));
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
            if (Startgame)
            {
                dibujar(gameState);
                
                while (!gameState.GameOver)
                {
                    int delay = Math.Max(Mindelay, Maxdelay - (gameState.Score * DelayDecrease));
                    await Task.Delay(500);
                    gameState.MoveBlockDown();
                    dibujar(gameState);
                    //mediaPlayer.MediaEnded += Repeat_MediEnded;
                }
                GameOverMenu.Visibility = Visibility.Visible;

                FinalScoreText.Text = $"Score: {gameState.Score}";
                


            }        
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

        private void DibujarTablero(Cuadricula cuadricula, int id)
        {
            for (int fila = 0; fila < cuadricula.Fila(); fila++)
            {
                for (int Colum = 0; Colum < cuadricula.Columna(); Colum++)
                {                    
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

        private void DibujarMute()
        {
            if (Mute)
            {
                MuteImage.Source = Mutes[0];
            }
            else
            {
                MuteImage.Source = Mutes[1];
            }
            
        }
        private void dibujar(GameState game)
        {
            DibujarTablero(game.GameGrid);
            DibujarMute();
            DibujarBloqueFantasma(game.CurrentBlock);
            DibujarBloque(game.CurrentBlock);
            SiguienteBloque(game.BlockQueue);
            Held(game.HeldBlock);
            ScoreText.Text = $"Score: {game.Score}";

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }
           
            if (Startgame)
            {
                
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
                    case Key.R:
                        gameState = new GameState();
                        break;
                    case Key.Space:
                        gameState.DropBlock();
                        break;                    
                    case Key.M:
                        
                        if (Mute == false)
                        {   
                            mediaPlayer.Pause();    
                            Mute = true;
                        }
                        else
                        {
                            Mute= false;
                            mediaPlayer.Play();  
                        }
                        break;  

                    default:
                        return;
                }               
                dibujar(gameState);
            }

            
        }


        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
           
            DibujarTablero(gameState.GameGrid, 0);

        }

        //void Repeat_MediEnded(object sender, EventArgs e)
       // {
            //mediaPlayer.Close();
            //mediaPlayer.Open(new Uri("Tetris_Song.mp3", UriKind.Relative));
            //mediaPlayer.Play();

       // }

        private async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            string userInput = UserNameInput.Text;
            int finalScore = gameState.Score;
            string filePath = "Puntaje.txt";
            Dictionary<string, int> puntajes = new Dictionary<string, int>();
            List<int> numeros = new List<int>();

            if (!File.Exists(filePath))
            {
                using (FileStream fs = File.Create(filePath)) { }
            }

            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine("{0}:{1}", userInput, finalScore);
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {

                using (StreamReader sr = new StreamReader(fs))
                {
                    while(!sr.EndOfStream)
                    {
                        string linea = sr.ReadLine();
                        string[] partes = linea.Split(':');
                        string name = partes[0];

                        if (partes.Length >= 2 && int.TryParse(partes[1], out int numero))
                        {
                            puntajes.TryAdd(name, numero);

                            foreach (KeyValuePair<string, int> kvp in puntajes)
                            {
                                if (kvp.Value > highestScore)
                                {
                                    highestScore = kvp.Value;
                                    highestScorePlayer = kvp.Key;
                                }
                            }

                            HighestScoreText.Text = "Puntaje mas alto: " + highestScore.ToString()+ "\nPor: " + highestScorePlayer;
                        }
                    }
                }
            }


            numplayer += 1;

            UserNameInput.Text = $"Jugador{numplayer}";
            gameState = new GameState();
            GameOverMenu.Visibility=Visibility.Hidden;
            mediaPlayer.Close();
            mediaPlayer.Open(new Uri("Tetris_Song.mp3", UriKind.Relative));
            mediaPlayer.Play();
            await loop();

        }
        private async void Start_Click(object sender, RoutedEventArgs  e)
        {
            
            gameState = new GameState();
            Startgame = true;
            string filePath = "Puntaje.txt";
            Dictionary<string, int> puntajes = new Dictionary<string, int>();
            List<int> numeros = new List<int>();
            if (!File.Exists(filePath))
            {
                using (FileStream fs = File.Create(filePath)) { }
            }

            GameStar.Visibility = Visibility.Hidden;              
            GameCanvas.Visibility = Visibility.Visible;
            ScoreText.Visibility = Visibility.Visible;
            RestartMessage.Visibility = Visibility.Visible;
            Holdtext.Visibility = Visibility.Visible;
            Nexttext.Visibility = Visibility.Visible;
            HighestScoreText.Visibility = Visibility.Visible;

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {

                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        string linea = sr.ReadLine();
                        string[] partes = linea.Split(':');
                        string name = partes[0];

                        if (partes.Length >= 2 && int.TryParse(partes[1], out int numero))
                        {
                            puntajes.TryAdd(name, numero);

                            foreach (KeyValuePair<string, int> kvp in puntajes)
                            {
                                if (kvp.Value > highestScore)
                                {
                                    highestScore = kvp.Value;
                                    highestScorePlayer = kvp.Key;
                                }
                            }

                            HighestScoreText.Text = "Puntaje mas alto: " + highestScore.ToString() + "\nPor: " + highestScorePlayer;
                        }
                    }
                }
            }

            mediaPlayer.Play();
            await loop();
       
        }
    }
}
