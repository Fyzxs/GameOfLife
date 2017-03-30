using System;
using GameOfLife;

namespace DisplayLife
{
    internal class Program
    {
        static int width = 10;
        static int height = 10;

        public static void Main(string[] args)
        {
            Console.WriteLine("Hit Enter to Continue");
            Console.ReadLine();
            Cell[,] cells = new Cell[height,width];
            Random random = new Random();
            for (int vIndex = 0; vIndex < height; vIndex++)
            {
                for (int hIndex = 0; hIndex < width; hIndex++)
                {
                    cells[vIndex,hIndex] = new Cell(random.Next(100)%2==0 ? PopulationControl.Alive : PopulationControl.Dead);
                    Console.Write(cells[vIndex, hIndex].IsAlive() ? "X" : " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Hit Enter To Step");
            while (Console.ReadLine() != "q")
            {
                Console.Clear();
                Update(cells);
                Display(cells);
                Console.WriteLine("Hit Enter To Step");
            }
        }

        private static void Display(Cell[,] cells)
        {
            for (int vIndex = 0; vIndex < height; vIndex++)
            {
                for (int hIndex = 0; hIndex < width; hIndex++)
                {
                    Console.Write(cells[vIndex, hIndex].IsAlive() ? "X" : " ");
                }
                Console.WriteLine();
            }
        }

        private static void Update(Cell[,] cells)
        {
            PopulationControl pc = new PopulationControl();
            for (int vIndex = 0; vIndex < height; vIndex++)
            {
                for (int hIndex = 0; hIndex < width; hIndex++)
                {
                    pc.UpdateState(cells[vIndex, hIndex], CalculateLivingNeighbors(cells, vIndex, hIndex));
                }
            }
            for (int vIndex = 0; vIndex < height; vIndex++)
            {
                for (int hIndex = 0; hIndex < width; hIndex++)
                {
                    cells[vIndex, hIndex].Update();
                }
            }
        }

        private static int CalculateLivingNeighbors(Cell[,] cells, int vIndex, int hIndex)
        {
            int livingCtr = 0;
            try
            {
                livingCtr += cells[vIndex - 1, hIndex - 1].IsAlive() ? 1 : 0;
            }
            catch
            {
                // ignored
            }
            try
            {
                livingCtr += cells[vIndex - 1, hIndex].IsAlive() ? 1 : 0;
            }
            catch
            {
                // ignored
            }
            try
            {
                livingCtr += cells[vIndex - 1, hIndex + 1].IsAlive() ? 1 : 0;
            }
            catch
            {
                // ignored
            }

            try
            {
                livingCtr += cells[vIndex, hIndex - 1].IsAlive() ? 1 : 0;
            }
            catch
            {
                // ignored
            }
            try
            {
                //livingCtr += cells[vIndex, hIndex].IsAlive() ? 1 : 0;
            }
            catch
            {
                // ignored
            }
            try
            {
                livingCtr += cells[vIndex, hIndex + 1].IsAlive() ? 1 : 0;
            }
            catch
            {
                // ignored
            }
            try
            {
                livingCtr += cells[vIndex + 1, hIndex - 1].IsAlive() ? 1 : 0;
            }
            catch
            {
                // ignored
            }
            try
            {
                livingCtr += cells[vIndex + 1, hIndex].IsAlive() ? 1 : 0;
            }
            catch
            {
                // ignored
            }
            try
            {
                livingCtr += cells[vIndex + 1, hIndex + 1].IsAlive() ? 1 : 0;
            }
            catch
            {
                // ignored
            }

            return livingCtr;
        }
    }
}