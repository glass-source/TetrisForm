namespace WpfApp1
{
    public class Cuadricula
    {
        private readonly int[,] cuadricula;
        private int Filas { get; }
        private int Columnas { get; }

        public int this[int r, int c] //Indexer, facil acceso al arreglo
        {
            get => cuadricula[r, c];
            set => cuadricula[r, c] = value;
        }

        //Constructor(Tendra el numero de Filas y Columnas como parametros)
        public Cuadricula(int filas, int columnas)
        {
            Columnas = columnas;
            Filas = filas;
            cuadricula = new int[filas, columnas];
        }

        //METODOS	
        public int Fila()
        {
            return Filas;
        }
        public int Columna()
        {
            return Columnas;
        }
        public bool adentro(int r, int c) //Verifica si las filas y las columnas están adentro de la cuadricula o no
        {
            return r >= 0 && r < Filas && c >= 0 && c < Columnas;
        }
        public bool vacia(int r, int c) //Verifica si una celda está vacía o no
        {
            return adentro(r, c) && cuadricula[r, c] == 0;
            //Debe estar adentro del arreglo y el valor de la celda debe ser cero.
        }
        public bool Filallena(int r) //Verifica si una fila está llena 
        {
            for (int c = 0; c < Columnas; c++)
            {
                if (cuadricula[r, c] == 0)
                {
                    return false;
                }
            }

            return true;
        }
        public bool Filavacia(int r) //Verifica si una fila está vacia

        {
            for (int c = 0; c < Columnas; c++)
            {
                if (cuadricula[r, c] != 0)
                {
                    return false;
                }
            }

            return true;
        }


        private void limpiarFila(int r)//Limpiar las filas
        {
            for (int c = 0; c < Columnas; c++)
            {
                cuadricula[r, c] = 0;

            }
        }
        public void moverFilaAbajo(int r, int numFilas) //Mueve hacia abajo las filas un numero determinado asignado.
        {
            for (int c = 0; c < Columnas; c++)
            {
                cuadricula[r + numFilas, c] = cuadricula[r, c];
                cuadricula[r, c] = 0;
            }
        }
        public int limpiarFilaCompleta() // Si la fila se encuentra llena, la eliminará y le sumará 1 a la variable limpiada, se bajará la siguente fila la cantidad de enteros Limpiada existan.
        {
            int Limpiada = 0;
            for (int r = Filas - 1; r >= 0; r--)
            {
                if (Filallena(r))
                {
                    limpiarFila(r);
                    Limpiada++;
                }
                else if (Limpiada > 0)
                {
                    moverFilaAbajo(r, Limpiada);
                }
            }
            return Limpiada;
        }

    }
}