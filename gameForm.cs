using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTest
{
    public partial class GameForm : Form
    {
        private GameTest.cs.Game game;
        private Thread GameThread;
        public GameForm()
        {
            InitializeComponent();

            game = new GameTest.cs.Game();
            GameThread = new Thread(new ThreadStart(game.start));
            GameThread.Start();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            game.setDrawerCtx(canvas);
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GameThread.Abort();

            game.removeDrawerCtx();
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue <= 40 && e.KeyValue >= 37) game.setKey(e.KeyValue - 37, true);
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue <= 40 && e.KeyValue >= 37) game.setKey(e.KeyValue - 37, false);
        }
    }
}