using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Library.Board board = new Library.Board();
            board.SetAlive(2, 3);
            board.SetAlive(2, 1);
            board.SetAlive(3, 2);
            board.SetAlive(1, 2);

            board.SetAlive(5, 6);
            board.SetAlive(5, 5);
            board.SetAlive(6, 5);

            while (true)
            {
                Console.WriteLine(board.Display());
                Console.ReadKey();
                board.NextState();
                Console.Clear();
            }
        }
    }
}
