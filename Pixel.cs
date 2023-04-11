using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{   
    internal class Pixel
    {
        private const char pixel = '█';
        public Pixel(int x, int y, ConsoleColor color)
        {       
            X = x;
            Y = y;
            PixelColor = color;
        }
        
        public int X { get; }
        public int Y { get; }

        public ConsoleColor PixelColor { get; private set; }

        public void Draw()
        {
            Console.ForegroundColor = PixelColor;
            Console.SetCursorPosition(X, Y);
            Console.Write(pixel);
        }
        public void Remove()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(" ");
        }

    }
}
