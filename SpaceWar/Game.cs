using System.Numerics;
using System.Windows.Forms;

namespace SpaceWar
{
    public partial class Game : Form
    {
        private bool moveLeft;
        private bool moveRight;
        private int moveSpeed;
        private System.Windows.Forms.Timer movetimer;
        private System.Windows.Forms.Timer enemy_sp;
        private Random rand = new Random();
        

        public Game()
        
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.KeyDown += new KeyEventHandler(Game_KeyDown);
            this.KeyUp += new KeyEventHandler(Game_KeyUp);
            moveSpeed = 9; // Initialize moveSpeed here
            movetimer = new System.Windows.Forms.Timer();
            movetimer.Interval = 5;
            movetimer.Tick += new EventHandler(GameTimer_Tick);
            movetimer.Start();
            enemy_sp = new System.Windows.Forms.Timer();
            enemy_sp.Interval = 3000;
            enemy_sp.Tick += new EventHandler(Enemy_Sp_Tick);
            enemy_sp.Start();

        }

        private void Game_Load(object sender, EventArgs e)
        {
            player.Left = (this.ClientSize.Width - player.Width) / 2;
            player.Top = (this.ClientSize.Height) - 145;
            player.BackColor = Color.Transparent;
            enemy_1.BackColor = Color.Transparent;

            player.Anchor = AnchorStyles.None;
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                moveLeft = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                moveRight = true;
            }
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                moveLeft = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                moveRight = false;
            }
        }

        private void add_score(int score)
        {
            int f_score = Int32.Parse(lbl_score.Text) + score;
            int len = 4 - f_score.ToString().Length;
            string final = "";
            for (int i = 0; i < len; i++)
            {
                final += "0";
            }
            lbl_score.Text = final + f_score.ToString();
        }

        private async void MoveEnemy(PictureBox clone)
        {
            while (clone.Top < this.ClientSize.Height)
            {
                clone.Top += 2;
                await Task.Delay(10);
            }


            this.Controls.Remove(clone);
            //add_score(1);
            clone.Dispose();

        }


        private void Enemy_Sp_Tick(object sender, EventArgs e)
        {
            PictureBox clone = new PictureBox();
            clone.Size = enemy_1.Size;
            clone.Image = enemy_1.Image;
            clone.SizeMode = enemy_1.SizeMode;

            add_score(1);
            int x = rand.Next(0, this.ClientSize.Width - clone.Width);
            clone.Location = new Point(x, 0);

            this.Controls.Add(clone);
            player.SendToBack();
            clone.BringToFront();
            MoveEnemy(clone);
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (moveLeft && player.Left > 0)
            {
                player.Left -= moveSpeed;
                
            }
            if (moveRight && player.Right < this.ClientSize.Width)
            {
                player.Left += moveSpeed;
            }
        }
    }
}