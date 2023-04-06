namespace WpfApp1
{
    public class LIBlock : Block
    {
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[] { new(2, 0), new(2, 1), new(2, 2), new(2, 3), new(2, 4)},
            new Position[] { new(0, 2), new(1, 2), new(2, 2), new(3, 2), new(4, 2)}
        };

        public override int Id => 8;

        protected override Position StartOffSet => new Position(0, 3);

        protected override Position[][] Tiles => tiles;
    }
}
