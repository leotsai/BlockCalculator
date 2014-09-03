using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TestBlocks
{
    class Calculator
    {
        public  static Block _background;
        public static  int _max = 0;
        public static List<Place> _maxPlaces;
        public static int _limit = 0;

        public static int _counter = 0;

        public static int Calculate(Block background, List<Block> blocks)
        {
            _background = background;
            _maxPlaces = new List<Place>();
            _limit = background.Width*background.Height;
            foreach (var block in blocks)
            {
                var place = Calculator._background.GetAvailablePlace(new List<Place>(), block);
                if (place == null)
                {
                    continue;
                }
                var node = new Node();
                node.Place = new Place(block, new Point(0, 0));
                node.Calc(blocks);
            }
            return _max;
        }

    }
}
