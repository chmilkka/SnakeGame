using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Snake
    {
        private const ConsoleColor _headColor = ConsoleColor.White;
        private const ConsoleColor _bodyColor = ConsoleColor.Green;
        public Snake(int xPos, int yPos, int bodyLength = 4)
        {         
            Head = new Pixel(xPos, yPos, ConsoleColor.Gray);
            for (int i = bodyLength; i >= 0; i--)
            {
                Body.Enqueue(new Pixel(Head.X, yPos + i + 1, ConsoleColor.Green));
            }
        }

        public Pixel Head { get; private set; }
        public Queue<Pixel> Body { get;} = new Queue<Pixel>();

        public void CreateSnake()
        {
            Head.Draw();
            foreach (var pixel in Body)
            {
                pixel.Draw();
            }
        }
        public void RemoveSnake()
        {
            Head.Remove();
            foreach (var pixel in Body)
            {
                pixel.Remove();
            }
        }
        public void Move(Direction direction)
        {
            RemoveSnake();
            Body.Enqueue(new Pixel (Head.X, Head.Y, _bodyColor));
            Body.Dequeue();
            Head = direction switch
            {
                Direction.Up => new Pixel(Head.X, Head.Y - 1, _headColor),
                Direction.Down => new Pixel(Head.X, Head.Y + 1, _headColor),
                Direction.Right => new Pixel(Head.X + 1, Head.Y, _headColor),
                Direction.Left => new Pixel(Head.X - 1, Head.Y, _headColor),
                _ => Head
            };           
            CreateSnake();
        }

    }
}
