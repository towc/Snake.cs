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
using System.Windows.Input;

namespace GameTest.cs
{
    class Game
    {
        private int w = 60;
        private int h = 60;

        private int frame;

        private Drawer drawer;
        private Board board;
        private Player player;
        private Control controls = new Control();

        private Tile[] directions = {new Tile(-1, 0), new Tile(0, -1), new Tile(1, 0), new Tile(0, 1)};
        private int direction = 0;

        public void start()
        {
            frame = 0;

            drawer = new Drawer(h);
            board = new Board(w, h);
            player = new Player(w / 2, h / 2);
            startTimer();
        }

        private void update()
        {
            ++frame;

            if(frame % 10 == 0)
            {
                if (controls.getKey(0) && direction != 2) direction = 0;
                if (controls.getKey(1) && direction != 3) direction = 1;
                if (controls.getKey(2) && direction != 0) direction = 2;
                if (controls.getKey(3) && direction != 1) direction = 3;

                Tile dir = directions[direction];
                tryMoving(dir.x, dir.y);
            }
        }

        private void render()
        {
            drawer.clearScreen();

            drawer.drawMeal(board.meal);
            drawer.drawPlayer(player);
        }

        private void tryMoving(int sx, int sy)
        {
            Tile head = player.getHead();

            int updatedX = head.x + sx;
            int updatedY = head.y + sy;

            int cell = board.get(updatedX, updatedY);
            if (cell == 0)
            {
                player.setPos(updatedX, updatedY);
            }
            else if (cell == 2)
            {
                if (updatedX < 0) updatedX += w;
                if (updatedY < 0) updatedY += h;
                player.setPos(updatedX % w, updatedY % h);
            }

            if (updatedX == board.meal.x && updatedY == board.meal.y)
            {
                board.changeMeal();

                player.addTile(player.getBody()[0]);
            }
        }

        //
        // Timer
        //
        private DateTime lastTime;
        private TimeSpan elapsedTime;
        private static TimeSpan nullTime = new TimeSpan();
        private TimeSpan updateTime = new TimeSpan(0, 0, 0, 0, 4);

        private void tick(){
            while(true)
            {
                DateTime now = DateTime.Now;
                elapsedTime += (now - lastTime);

                while(elapsedTime - updateTime >= nullTime){
                    elapsedTime -= updateTime;

                    update();
                }

                render();

                lastTime = now;
            }
        }

        private void startTimer()
        {
            lastTime = DateTime.Now;
            elapsedTime = new TimeSpan();

            tick();
        }

        //
        //Drawer stuff
        //

        public void setDrawerCtx(Panel canvas)
        {
            drawer.setCtx(canvas);
        }
        public void removeDrawerCtx()
        {
            drawer.removeCtx();
        }

        //
        // control stuff
        //

        public void setKey(int index, bool value)
        {
            controls.setKey(index, value);
        }
    }
}
