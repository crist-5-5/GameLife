using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Board
    {
        private List<bool> cells = new List<bool>();

        private int Coords(int x, int y)
        {
            return (x + y * 20);
        }

        public Board()
        {
            for (int i = 0; i < 20*20; i++)
            {
                cells.Add(false);
            }
        }
        

        public void SetAlive(int v1, int v2)
        {
            cells[Coords(v1, v2)] = true;
        }
        public void SetDead(int v1, int v2)
        {
            cells[Coords(v1, v2)] = false;
        }

        public string Display()
        {
            StringBuilder S = new StringBuilder();
            int count = 0;
            foreach (var item in cells)
            {
                S.Append(item ? "o" : "x");
                count++;
                if (count >= 20)
                {
                    S.Append("\r\n");
                    count = 0;
                }
                
            }
            return S.ToString();
        }

        public int GetNeighbours(int xx, int yy)
        {
            int count = 0;
            foreach (int x in Enumerable.Range(xx-1, 3))
            {
                foreach (int y in Enumerable.Range(yy-1, 3))
                {
                    if (IsAlive(x, y))
                        count++;
                }
            }
            if (IsAlive(xx, yy))
                count--;
            return count;
        }

        public bool IsAlive(int x, int y)
        {
            if (Coords(x, y) < 0)
                return false;
            if (Coords(x, y) >= 20*20)
                return false;
            return cells[Coords(x, y)];
        }

        public void NextState()
        {
            List<bool> newCells = new List<bool>();
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    bool newState = IsAlive(x, y);
                    if (newState)
                    {
                        if (GetNeighbours(x, y) < 2)
                            newState = false;
                        if (GetNeighbours(x, y) == 2 || GetNeighbours(x, y) == 3)
                            newState = true;
                        if (GetNeighbours(x, y) > 3)
                            newState = false;
                    }
                    else
                        if (GetNeighbours(x, y) == 3)
                            newState = true;
                    newCells.Add(newState);
                }
            }
            cells = newCells;
        }
    }
}
