using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace TestBlocks
{
    public class Node
    {
        public Node Parent { get; set; }
        public Place Place { get; set; }
        public List<Node> Children { get; set; }

        public Node()
        {
            this.Children = new List<Node>();
        }

        public void Calc(List<Block> blocks)
        {
            if (Calculator._max >= Calculator._limit)
            {
                return;
            }
            var places = this.GetAllPlaces();
            foreach (var block in blocks)
            {
                var place = Calculator._background.GetAvailablePlace(places, block);
                Calculator._counter++;
                Console.WriteLine(Calculator._counter);

                if (place != null)
                {
                    var child = new Node()
                    {
                        Parent = this,
                        Place = place
                    };
                    this.Children.Add(child);
                    child.Calc(blocks);
                }
                else
                {
                    var size = places.GetSquareSize();
                    if (size > Calculator._max)
                    {
                        Calculator._max = size;
                        Calculator._maxPlaces = new List<Place>(places);
                    }
                }
            }
        }

        public List<Place> GetAllPlaces()
        {
            var values = new List<Place>();
            values.Add(this.Place);
            var parent = this.Parent;
            while (parent != null)
            {
                values.Add(parent.Place);
                parent = parent.Parent;
            }
            return values;
        } 
    }

    public interface IMoveNode
    {
        bool CanMoveForward();
    }
   
}
