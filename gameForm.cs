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
        public delegate void setScoreDelegate(int value);
        public setScoreDelegate scoreDelegate;
        public delegate void setTimeDelegate(int value);
        public setTimeDelegate timeDelegate;

        private GameTest.cs.Game game;
        private Thread GameThread;

        public void setScore(int value)
        {
            LScore.Text = value + " ";
        }
        public void setTime(int value)
        {
            LTime.Text = value + "s";
        }
        public GameForm()
        {
            InitializeComponent();
            scoreDelegate = new setScoreDelegate(setScore);
            timeDelegate = new setTimeDelegate(setTime);

            game = new GameTest.cs.Game(this);
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
            game.setKey(e, true);
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            game.setKey(e, false);
        }

        private void BTReset_Click(object sender, EventArgs e)
        {
            game.reset();
        }
    }
}