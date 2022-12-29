using System.Media;

namespace Rolling_Ball
{
    public partial class Form1 : Form
    {

        int rolling;
        int rollingValue = 8;
        int boxSpeed = 10;
        int score = 0;
        int highScore = 0;
        bool gameOver = false;
        Random random = new Random();


        public Form1()
        {
            InitializeComponent();
            restartGame();
        }

        private void RollingTimer(object sender, EventArgs e)
        {
            lblScore.Text = "Score: " + score;
            lblhighScore.Text = "High Score: " + highScore;
            ball.Top += rolling;

            // when the ball land on the platforms.
            if (ball.Top > 184)
            {
                rolling = 0;
                ball.Top = 184;
                ball.Image = Properties.Resources.ball_down0;
            }
            else if (ball.Top < 42)
            {
                rolling = 0;
                ball.Top = 42;
                ball.Image = Properties.Resources.ball_up0;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "box")
                {
                    x.Left -= boxSpeed;

                    if (x.Left < -100)
                    {
                        x.Left = random.Next(1500, 2500);
                        score += 1;
                    }

                    if (x.Bounds.IntersectsWith(ball.Bounds))
                    {
                        Rollingtimer.Stop();
                        lblScore.Text += "   GAME OVER...!!  PRESS ENTER TO RESTART...!";
                        gameOver = true;

                        // set the high score

                        if (score > highScore)
                        {
                            highScore = score;
                        }

                    }
                }
            }



        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Space)
            {
                if (ball.Top == 184)
                {
                    ball.Top -= 10;
                    rolling = -rollingValue;
                }
                else if (ball.Top == 42)
                {
                    ball.Top += 10;
                    rolling = rollingValue;
                }
            }

            if (e.KeyCode == Keys.Enter && gameOver == true)
            {
                restartGame();
            }

        }
        private void restartGame()
        {
            lblScore.Parent = pictureBox1;
            lblhighScore.Parent = pictureBox2;
            lblhighScore.Top = 0;
            ball.Location = new Point(120, 122);
            ball.Image = Properties.Resources.ball_down0;
            score = 0;
            rollingValue = 8;
            rolling = rollingValue;
            boxSpeed = 10;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "box")
                {
                    x.Left = random.Next(1500, 2500);
                }
            }

            Rollingtimer.Start();
        }
    }
}