using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest.cs
{
    class Player
    {
        private Tile head;
        private List<Tile> body = new List<Tile>();

        public int direction = 0;

        public Player(int posX, int posY)
        {
            head = new Tile(posX, posY);

            addTile(posX, posY);
        }

        public void addTile(Tile tile)
        {
            body.Add(tile);
        }
        public void addTile(int posX, int posY)
        {
            body.Add(new Tile(posX, posY));
        }
        public void insertTile(Tile tile, int index)
        {
            body.Insert(index, tile);
        }

        public bool move(int sx, int sy)
        {
            Tile updatedHead = new Tile(head.x + sx, head.y + sy);

            return checkPosition(updatedHead);
        }
        public bool setPos(int posX, int posY)
        {
            Tile updatedHead = new Tile(posX, posY);

            return checkPosition(updatedHead);
        }

        private bool checkPosition(Tile updatedHead)
        {

            body.RemoveAt(0);
            addTile(updatedHead);

            foreach (Tile tile in body)
            {
                if (tile != updatedHead & updatedHead.x == tile.x && updatedHead.y == tile.y) return false;
            }

            head = updatedHead;

            return true;
        }

        public Tile getHead()
        {
            return head;
        }
        public List<Tile> getBody()
        {
            return body;
        }
    }
}
