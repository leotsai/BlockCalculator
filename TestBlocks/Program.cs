using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace TestBlocks
{
    class Program
    {
        private static void Main(string[] args)
        {
            var background = new Block("Background", 300, 300);

            var blocks = new List<Block>();

            blocks.Add(new Block("A", 90, 110, Color.SaddleBrown));
            blocks.Add(new Block("B", 100, 100, Color.Salmon));
            blocks.Add(new Block("C", 200, 200, Color.SandyBrown));
            blocks.Add(new Block("D", 100, 200, Color.SeaGreen));
            blocks.Add(new Block("E", 120, 80, Color.SkyBlue));
            blocks.Add(new Block("F", 400, 300, Color.SlateBlue));
            blocks.Add(new Block("G", 250, 150, Color.Yellow));
            blocks.Add(new Block("H", 170, 150, Color.Blue));
            blocks.Add(new Block("I", 200, 50, Color.Chartreuse));
            blocks.Add(new Block("J", 80, 120, Color.DarkSlateGray));
            
            var size = Calculator.Calculate(background, blocks);
            //Write(Calculator._maxPlaces, size);
            Paint(background, Calculator._maxPlaces, size);
            
            Console.WriteLine("done");
            Console.Read();
        }

        private static void Write(IEnumerable<Place> places, int size)
        {
            foreach (var place in places)
            {
                Console.WriteLine(place.GetDisplayText());
            }
            Console.WriteLine("面积：" + size);
        }

        private static void Paint(Block block, List<Place> places, int size)
        {
            var path = @"d:\test\blocks.jpeg";
            var bitmap = new Bitmap(block.Width, block.Height);
            var g = Graphics.FromImage(bitmap);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, bitmap.Width, bitmap.Height);
            foreach (var place in places)
            {
                g.FillRectangle(new SolidBrush(place.Color), place.Point.X, place.Point.Y, place.Width, place.Height);
                g.DrawRectangle(new Pen(Color.Black, 1), place.Point.X, place.Point.Y, place.Width, place.Height);
                var center = place.GetCenter();
                g.DrawString(place.Name, new Font("宋体", 12), new SolidBrush(Color.Black), center.X, center.Y);
            }
            g.DrawRectangle(new Pen(Color.Black, 2), 0, 0, bitmap.Width, bitmap.Height);
            g.DrawString("面积：" + size, new Font("宋体", 12), new SolidBrush(Color.Black), 0, 0);
            bitmap.Save(path, ImageFormat.Jpeg);
        }
    }


}
