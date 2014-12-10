using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTest.cs
{
    class Drawer
    {
        private Graphics ctx;
        private bool hasCtx = false;
        private SolidBrush brush;
        private int size;
        private int bHeight;
        private int height;
        private int width;
        private Panel canvas;

        public Drawer(int boardHeight)
        {
            bHeight = boardHeight;
            brush = new SolidBrush(Color.White);

        }

        public void setCtx(Panel canvasEl)
        {
            canvas = canvasEl;
            ctx = canvas.CreateGraphics();

            height = canvas.Height;
            width = canvas.Width;

            size = height / bHeight;
            hasCtx = true;

            drawRect(Color.Gray, 0, 0, width, height);
        }
        public void removeCtx()
        {
            hasCtx = false;
        }

        public void clearScreen()
        {
            drawRect(Color.FromArgb(10, 0, 0, 0), 0, 0, bHeight * size, bHeight * size);
        }
        public void drawPlayer(Player player)
        {
            foreach (Tile tile in player.getBody())
            {
                drawRect(Color.White, tile.x * size, tile.y * size, size, size);
            }

            Tile head = player.getHead();
            drawRect(Color.Yellow, head.x * size, head.y * size, size, size);

        }

        public void drawMeal(Tile meal)
        {
            drawRect(Color.Green, meal.x * size, meal.y * size, size, size);
        }

        private void drawRect(Color color, int x, int y, int w, int h)
        {
            brush = new SolidBrush(color);

            try {
                if (hasCtx) ctx.FillRectangle(brush, x, y, w, h);
            } catch(InvalidOperationException e){

            }
        }
    }
}
