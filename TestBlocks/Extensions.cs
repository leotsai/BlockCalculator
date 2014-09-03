using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestBlocks
{
    public static class Extensions
    {
        public static int GetSquareSize(this List<Place> places)
        {
            return places.Sum(place => place.GetSquareSize());
        }

        public static Block GetNext(this List<Block> blocks, string currentName)
        {
            var index = blocks.IndexOf(blocks.First(x => x.Name == currentName));
            if (index >= blocks.Count - 1)
            {
                return null;
            }
            return blocks[index + 1];
        }
    }
}
