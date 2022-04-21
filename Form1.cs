using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappING_bird
{
    public partial class FlappyBird : Form
    {
        int pipeSpeed = 8;
        int gravity = 15;
        int score = 0;

        public FlappyBird()
        {
            InitializeComponent();
        }

        //movement w/ time function
        private void gameTimerEvent(object sender, EventArgs e)
        {
            //initialize gravity
            bird.Top += gravity;
            //moving pipes
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            //scoreText.Text = score.ToString();
            scoreText.Text = "Score: " + score.ToString();
            Random rnd = new Random(); //rng generator
            int pipeBottom_rng = 0;
            int pipeTop_rng = 0;

            //resetting pipes when they scroll off screen (static positioning check)
            if (pipeBottom.Left < -120)
            {
                //generating height position of pipe on reset
                pipeBottom_rng = rnd.Next(200, 450);
                pipeBottom.Top = pipeBottom_rng;

                pipeBottom.Left = 800;
                score++;
            }
            if (pipeTop.Left < -150)
            {
                pipeTop_rng = rnd.Next(-350, -100);
                pipeTop.Top = pipeTop_rng;

                pipeTop.Left = 850;
                score++;
            }

            //bird collides with either pipe, the bottom, or the top
            if (bird.Bounds.IntersectsWith(pipeBottom.Bounds) || 
                bird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                bird.Bounds.IntersectsWith(ground.Bounds) ||
                bird.Top < -25)
            {
                endGame(); //ends the game
            }

            //speed up every 5 points
            if((score >= 5) && (score % 5) == 0)
            {
                pipeSpeed += 5;
                score += 1;
            }

        }

        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            //if space is pressed down
            if (e.KeyCode == Keys.Space)
            {
                gravity = -15;
            }
        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 15;
            }
        }

        private void endGame()
        {
            gameTimer.Stop();
            scoreText.Text += " Game Over!";
        }
    }
}
