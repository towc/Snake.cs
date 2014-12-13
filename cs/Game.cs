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
    class Game
    {
        private GameForm form;

        private int width = 60;
        private int height = 60;

        private int frame;
        private bool initialized = false;
        private bool running = true;
        public bool wantsToStart = false;

        private Drawer drawer;
        private Board board;
        private Player player;
        private Control controls = new Control();

        private Tile[] directions = {new Tile(-1, 0), new Tile(0, 1), new Tile(1, 0), new Tile(0, -1)};

        public Game(GameForm GForm)
        {
            form = GForm;
        }

        public void start()
        {
            if (initialized)
            {
                running = true;
                wantsToStart = false;
                frame = 0;

                board = new Board(width, height);
                player = new Player(width / 2, height / 2);
                startTimer();
            }
            else
            {
                drawer = new Drawer(height);
                drawer.setCtx(form.canvas);

                initialized = true;

                start();
            }
        }

        private void update()
        {
            ++frame;

            if(frame % 10 == 0)
            {

                if (controls.Left && player.direction != 2) player.direction = 0;
                if (controls.Down && player.direction != 3) player.direction = 1;
                if (controls.Right && player.direction != 0) player.direction = 2;
                if (controls.Up && player.direction != 1) player.direction = 3;

                Tile dir = directions[player.direction];
                tryMoving(dir.x, dir.y);
            }
        }

        private void render()
        {
            drawer.clearScreen();

            drawer.drawMeal(board.meal);
            drawer.drawPlayer(player);

            form.Invoke(form.scoreDelegate, player.getBody().Count);
            form.Invoke(form.timeDelegate, (int)timeFromStart.TotalSeconds);
        }
        public void reset()
        {
            if (!running) start();
            else
            {
                running = false;
                wantsToStart = true;
            }
        }

        private void tryMoving(int sx, int sy)
        {
            Tile head = player.getHead();

            int updatedX = head.x + sx;
            int updatedY = head.y + sy;

            int cell = board.get(updatedX, updatedY);
            if (cell == 0)
            {
                running = player.setPos(updatedX, updatedY);
            }
            else if (cell == 2)
            {
                if (updatedX < 0) updatedX += width;
                if (updatedY < 0) updatedY += height;
                running = player.setPos(updatedX % width, updatedY % height);
            }

            if (updatedX == board.meal.x && updatedY == board.meal.y)
            {
                board.changeMeal();

                player.insertTile(player.getBody()[0], 0);
            }
        }

        //
        // Timer
        //
        private DateTime startTime;
        private TimeSpan timeFromStart;

        private DateTime lastTime;
        private TimeSpan elapsedTime;
        private static TimeSpan nullTime = new TimeSpan();
        private TimeSpan updateTime = new TimeSpan(0, 0, 0, 0, 4);

        private void tick(){
            while(running)
            {
                DateTime now = DateTime.Now;
                elapsedTime += (now - lastTime);

                timeFromStart = now - startTime;

                while(elapsedTime - updateTime >= nullTime){
                    elapsedTime -= updateTime;

                    update();
                }

                render();

                lastTime = now;
            }
            if (wantsToStart)
            {
                start();
            }
        }

        private void startTimer()
        {
            startTime = DateTime.Now;

            lastTime = DateTime.Now;
            elapsedTime = new TimeSpan();

            tick();
        }

        //
        //Drawer stuff
        //
        public void removeDrawerCtx()
        {
            drawer.removeCtx();
        }

        //
        // control stuff
        //

        public void setKey(KeyEventArgs e, bool value)
        {
            switch (e.KeyCode)
            {
                case Keys.Up: controls.Up = value; break;
                case Keys.Left: controls.Left = value; break;
                case Keys.Down: controls.Down = value; break;
                case Keys.Right: controls.Right = value; break;
            }
        }
    }
}
