using System.Collections.Generic;
using System.Windows.Automation;

namespace WpfApp1
{
    public abstract class Block
    {
        //Representa todas las cuadriculas que ocupan el bloque
        protected abstract Position[][] Tiles { get; }
        //Posicion inicial del bloque
        protected abstract Position StartOffSet { get; }
        public abstract int Id { get; }

        private int rotationState;
        private Position offset;

        public Block()
        {
            offset = new Position(StartOffSet.Row, StartOffSet.Column);
        }

        //Funcion para retornar una posicion para cada cuadricula.
        public IEnumerable<Position> TilePositions()
        {
            foreach(Position p in Tiles[rotationState])
            {
                //Retorna una posicion en cada iteracion del foreach
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
            }
        }

        
        public void RotateCW()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }

        public void RotateCCW()
        {
            if (rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }

        public void Move (int rows, int columns)
        {
            offset.Row += rows;
            offset.Column += columns;
        }

        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffSet.Row;
            offset.Column = StartOffSet.Column;
        }
    }
}

