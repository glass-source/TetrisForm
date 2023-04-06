using System;

namespace WpfApp1
{
    public class SecuenciaBloques
    {
        private readonly Block[] blocks = new Block[] 
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock(),
            new LIBlock(),
            new LJBlock(),
            new LLBlock(),
            new LLLBlock(),
            new OABlock(),
            new ObBlock(),
            new LSBlock(),
            new TABlock(),
            new TbBlock(),
            new LZBlock()
        };
         


        private readonly Random random = new Random();

        public Block NextBlock { get; private set; }

        public SecuenciaBloques()
        {
            NextBlock = RandomBlock();
        }

        private Block RandomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }

        public Block GetAndUpdate()
        {
            Block block = NextBlock;
            do
            {
                NextBlock = RandomBlock();
            }
            while (block.Id == NextBlock.Id);

            return block;
        }
    };

    


}