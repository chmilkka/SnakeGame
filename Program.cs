using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;

namespace SnakeGame
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    internal class Program
    {
        private const int windowWidth = 90;
        private const int windowHeight = 30;
        private const int FrameMilliseconds = 200;

        public static void StartScreen()
        {

            string[] startScreen = new string[8];
            startScreen[0] = "  ███   █    █   ███ █   █   ██████";
            startScreen[1] = " █   █  █    █  █  █ █  █    █     ";
            startScreen[2] = "  █     ██   █ █   █ █ █     █     ";
            startScreen[3] = "   █    █ █  █ █████ ██      ██████";
            startScreen[4] = "    █   █  █ █ █   █ █ █     █     ";
            startScreen[5] = "     █  █   ██ █   █ █  █    █     ";
            startScreen[6] = "█    █  █    █ █   █ █   █   █     ";
            startScreen[7] = "  ███   █    █ █   █ █    █  ██████";

            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < startScreen.Length; i++)
                for (int j = 0; j < startScreen[i].Length; j++)
                {
                    Console.SetCursorPosition(j + 25, i + 10);
                    Console.Write(startScreen[i][j]);
                    Thread.Sleep(1);
                }
            Console.SetCursorPosition(30, 25);
            Console.Write("Press any key to start");
            Console.ResetColor();

        }
        public static void DrawWalls()
        {
            
            for (int i = 0; i < windowWidth; i++)
            {
                new Pixel (i, 0, ConsoleColor.DarkRed).Draw();
            }
            for (int i = 0;i < windowWidth; i++)
            {
                new Pixel (i, windowHeight - 1, ConsoleColor.DarkRed).Draw();
            }
            for (int i = 0; i < windowHeight; i++)
            {
                new Pixel(0, i, ConsoleColor.DarkRed).Draw();
            }
            for (int i = 0; i < windowHeight; i++)
            {
                new Pixel(windowWidth - 1, i, ConsoleColor.DarkRed).Draw();
            }

        }
        public static Direction ReadMovement(Direction currentDirection)
        {
            if(!Console.KeyAvailable)
            {
                return currentDirection;
            }
            ConsoleKey key = Console.ReadKey(true).Key;
            currentDirection = key switch
            {
                ConsoleKey.UpArrow when currentDirection != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when currentDirection != Direction.Up => Direction.Down,
                ConsoleKey.LeftArrow when currentDirection != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when currentDirection != Direction.Left => Direction.Right,
                _ => currentDirection
            };

            return currentDirection;
        }
        
        static void Main(string[] args)
        {
            Console.Title = "SnakeGame";
            Console.SetWindowSize(windowWidth, windowHeight);
            Console.SetBufferSize(windowWidth, windowHeight);
            Console.CursorVisible = false;

            
            StartScreen();           
            Console.ReadLine();
            Console.Clear();

            DrawWalls();
            Snake snake = new Snake(30, 15);

            Direction currentMovement = Direction.Up;
            int lagMs = 0;
            var sw = new Stopwatch();

            while (true)
            {
                sw.Restart();
                Direction oldMovement = currentMovement;

                while (sw.ElapsedMilliseconds <= FrameMilliseconds - lagMs)
                {
                    if (currentMovement == oldMovement)
                        currentMovement = ReadMovement(currentMovement);
                }

                sw.Restart();
                snake.Move(currentMovement);
                
                if (snake.Head.X == windowWidth - 1
                    || snake.Head.X == 0
                    || snake.Head.Y == windowHeight - 1
                    || snake.Head.Y == 0
                    || snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y))
                    break;

                lagMs = (int)sw.ElapsedMilliseconds;
            }
            Console.ReadLine();
        }
    }
}