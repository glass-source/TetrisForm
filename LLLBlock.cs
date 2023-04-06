﻿namespace WpfApp1
{
    public class LLLBlock : Block
    {
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[] { new (0, 0), new(1, 0), new(2, 0), new(2, 1), new(2, 2) },
            new Position[] { new (0, 2), new(0, 1), new(0, 0), new(1, 0), new(2, 0) },
            new Position[] { new (2, 2), new(1, 2), new(0, 2), new(0, 1), new(0, 0) },
            new Position[] { new (2, 0), new(2, 1), new(2, 2), new(1, 2), new(0, 2) }
        };

        public override int Id => 11;

        protected override Position StartOffSet => new Position(0, 3);

        protected override Position[][] Tiles => tiles;
    }
}
