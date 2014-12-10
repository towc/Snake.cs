using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest.cs
{
    class Board
    {
        private int width, height;
        private int[,] matrix;
        public Tile meal;

        public Board(int nWidth, int nHeight)
        {
            width = nWidth;
            height = nHeight;
            matrix = new int[width, height];

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
            matrix[y, x] = value;
        }
        public int get(int x, int y)
        {
            if (x >= 0 && x < width && y >= 0 && y < height) return matrix[x, y];
            else return 2;
        }

        public void changeMeal()
        {
            meal = getRandom();
        }

        public Tile getRandom()
        {
            Random rand = new Random();
            return new Tile(rand.Next(width), rand.Next(height));
        }
    }
}
