using System;
using Chess;

namespace DemoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            string error;
            while (true)
            {
                Console.WriteLine("EatenWhite: " + game.EatenWhite);
                Console.WriteLine("EatenBlack: " + game.EatenBlack + "\n");
                for (int y = 0; y < 8; y++)
                {
                    Console.Write(y + 1 + "  ");
                    for (int x = 0; x < 8; x++)
                    {
                        Console.Write("{0} ", game.Placement[y, x].ToString());
                    }
                    Console.WriteLine();
                }
                
                Console.WriteLine("\n   A B C D E F G H\n");

                error = game.Move(Console.ReadLine());

                if (error != "OK")
                {
                    Console.WriteLine(error);
                    Console.ReadLine();
                }
                Console.Clear();
            }
        }
    }
}
