namespace WpfApp1
{
    public class ObBlock : Block
    {
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[] { new (1, 1), new(1, 2), new(1, 3), new(2, 1), new(2, 2) },
            new Position[] { new (1, 1), new(1, 2), new(3, 2), new(2, 1), new(2, 2) },
            new Position[] { new (1, 1), new(1, 2), new(2, 0), new(2, 1), new(2, 2) },
            new Position[] { new (1, 1), new(1, 2), new(0, 1), new(2, 1), new(2, 2) }
        };

        public override int Id => 13;

        protected override Position StartOffSet => new Position(0, 3);

        protected override Position[][] Tiles => tiles;
    }
}
