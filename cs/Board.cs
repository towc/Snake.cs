using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest.cs
{
    class Board
    {
        private int w, h;
        private int[,] array;
        public Tile meal;

        public Board(int width, int height)
        {
            w = width;
            h = height;
            array = new int[width, height];

            changeMeal();
        }

        public void setTiles(Tile[] tiles)
        {
            foreach (Tile tile in tiles)
            {
                set(tile.x, tile.y, 1);
            }
        }
        public void set(int x, int y, int value)
        {
            array[y, x] = value;
        }
        public int get(int x, int y)
        {
            if (x >= 0 && x < w && y >= 0 && y < h) return array[x, y];
            else return 2;
        }

        public void changeMeal()
        {
            meal = getRandom();
        }

        public Tile getRandom()
        {
            Random rand = new Random();
            return new Tile(rand.Next(w), rand.Next(h));
        }
    }
}
