using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TestBlocks
{
    public class Block
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }

        public Block()
        {
            
        }

        public Block(string name, int width, int height)
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
        }

        public Block(string name, int width, int height, Color color) : this(name, width, height)
        {
            this.Color = color;
        }

        public int GetSquareSize()
        {
            return this.Width*this.Height;
        }

        public Place GetAvailablePlace(List<Place> places, Block block)
        {
            if (places.Count == 0)
            {
                var newPlace = new Place(block, new Point(0, 0));
                if (this.ContainsPlace(newPlace))
                {
                    return newPlace;
                }
                return null;
            }
            var allPoints = places.SelectMany(x => x.GetCornerPoints()).ToList();
            foreach (var point in allPoints)
            {
                var newPlace = new Place(block, point);
                if (this.ContainsPlace(newPlace) && places.All(p => !p.Overlaps(newPlace)))
                {
                    return newPlace;
                }
            }
            return null;
        }

        public bool ContainsPlace(Place place)
        {
            return place.Width + place.Point.X <= this.Width && place.Height + place.Point.Y <= this.Height;
        }
    }

    public class Place : Block
    {
        public Point Point { get; set; }

        public Place()
        {
            
        }

        public Place(Block block, Point point)
        {
            this.Name = block.Name;
            this.Width = block.Width;
            this.Height = block.Height;
            this.Point = point;
            this.Color = block.Color;
        }

        public string GetDisplayText()
        {
            return string.Format("{0}-{1}x{2} ({3},{4})", Name, Width, Height, Point.X, Point.Y);
        }

        public List<Point> GetCornerPoints()
        {
            return new List<Point>()
            {
                new Point(Point.X, Point.Y),
                new Point(Point.X + Width, Point.Y),
                new Point(Point.X + Width, Point.Y + Height),
                new Point(Point.X, Point.Y + Height)
            };
        }

        public bool Overlaps(Place place)
        {
            var center1 = this.GetCenter();
            var center2 = place.GetCenter();
            var xOverlaps = Math.Abs(center1.X - center2.X) < Math.Abs(this.Width + place.Width)/2;
            var yOverlaps = Math.Abs(center1.Y - center2.Y) < Math.Abs(this.Height + place.Height)/2;
            return xOverlaps && yOverlaps;
        }

        public Point GetCenter()
        {
            return new Point(this.Point.X + this.Width/2, this.Point.Y + this.Height/2);
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point()
        {
            
        }

        public Point(int x, int y)
        {
            this.Y = y;
            this.X = x;
        }

        public bool IsZero()
        {
            return this.Y == 0 && this.X == 0;
        }
    }
}
