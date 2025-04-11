using SpaceWar.Properties;
namespace SpaceWar
{
    public partial class Game : Form
    {
        System.Windows.Forms.Timer enemySpawnTimer = new();
        System.Windows.Forms.Timer difficult = new();
        System.Windows.Forms.Timer gameTimer = new();
        System.Windows.Forms.Timer change_day_night = new();
        List<PictureBox> hearts = new List<PictureBox>();
        List<PictureBox> explosionPool = new();
        List<PictureBox> bulletList = new();
        List<PictureBox> enemyList = new();
        Random random = new();
        bool isShootingAllowed = true;
        int bulletCooldown = 500;
        int playerHealth = 3;
        int enemy_speed = 2;
        int playerSpeed = 10;
        bool isMovingRight;
        bool isMovingLeft;
        int currentScore;
        int num = 1;

        public Game()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;

            gameTimer.Interval = 5;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            enemySpawnTimer.Interval = 2400;
            enemySpawnTimer.Tick += SpawnEnemy;
            enemySpawnTimer.Start();

            difficult.Interval = 5000;
            difficult.Tick += ChangeDifficult;
            difficult.Start();

            change_day_night.Interval = 20000;
            change_day_night.Tick += ChangeDayNight;
            change_day_night.Start();

            InitializeExplosions();
        }

        private void ChangeDifficult(object sender, EventArgs e)
        {
            if (enemySpawnTimer.Interval > 500)
            { enemySpawnTimer.Interval -= 100; }
            if (num % 9 == 0 & enemy_speed < 11)
            { enemy_speed += 1; playerSpeed += 1; }
            num++;
        }

        private void ChangeDayNight(object sender, EventArgs e)
        {
            if (this.BackColor == Color.LightSkyBlue) { this.BackColor = Color.FromArgb(255, 10, 15, 30);
                lbl_score.ForeColor = Color.White; }
            else { this.BackColor = Color.LightSkyBlue; lbl_score.ForeColor = Color.Black; }
        }

        private async void StopExplosionAnimation(PictureBox explosion)
        {
            System.Windows.Forms.Timer timer = new() { Interval = 800};
            timer.Tick += (s, e) =>
            { timer.Stop(); explosion.Visible = false; };
            await Task.Delay(1);
            timer.Start();
        }

        private void InitializeExplosions()
        {
            for (int i = 0; i < 5; i++)
            {
                PictureBox explosion = new()
                {   Size = explode.Size,
                    Image = Resources.explode,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Visible = false
                };
                explosionPool.Add(explosion);
                Controls.Add(explosion);
            }
        }

        private void CreateHearts()
        {
            foreach (var heart in hearts) { Controls.Remove(heart); }
            hearts.Clear();

            for (int i = 0; i < 3; i++)
            {
                PictureBox heart = new PictureBox();
                heart.Image = Properties.Resources.full_heart;
                heart.Size = new Size(40, 40);
                heart.Location = new Point((this.ClientSize.Width - 160) + (i * 50), 15);
                heart.BackColor = Color.Transparent;
                heart.SizeMode = PictureBoxSizeMode.Zoom;

                Controls.Add(heart);
                hearts.Add(heart);
                heart.BringToFront();
            }
        }

        private void ShowExplosion(Point location)
        {
            var explosion = explosionPool.FirstOrDefault(e => !e.Visible) ?? new PictureBox();
            explosion.Location = location;
            explosion.Visible = true;
            StopExplosionAnimation(explosion);
        }

        private async void UpdatePlayerHealth(bool increase)
        {
            if (increase)
            {
                hearts[playerHealth - 1].Image = Properties.Resources.full_heart;
                playerHealth += 1;
            }
            else
            {
                hearts[playerHealth - 1].Image = Properties.Resources.empty_heart;
                await Task.Delay(300);
                hearts[playerHealth - 1].Image = Properties.Resources.full_heart;
                await Task.Delay(300);
                hearts[playerHealth - 1].Image = Properties.Resources.empty_heart;
                playerHealth -= 1;
                if (playerHealth == 0) { GameOver(); return; }
            }
        }

        private void GameOver()
        {
            btn_exit.Visible = true;
            btn_restart.Visible = true;
            lbl_gameover.Visible = true;
            enemySpawnTimer.Stop();
            difficult.Stop();
            change_day_night.Stop();
            enemy_speed = 0;
            for (int i = bulletList.Count - 1; i >= 0; i--)
            {
                Controls.Remove(bulletList[i]); bulletList.RemoveAt(i);
            }
        }

        private void UpdateScore(int points)
        {   currentScore = int.Parse(lbl_score.Text) + points;
            lbl_score.Text = currentScore.ToString("D4");
        }

        private async void MoveEnemy(PictureBox enemy)
        {
            while (enemy.Top < ClientSize.Height)
            {
                enemy.Top += (enemy_speed);
                for (int i = enemyList.Count - 1; i >= 0; i--)
                {
                    var playerBounds = player.Bounds;
                    playerBounds.Y += 10;

                    if (playerBounds.IntersectsWith(enemyList[i].Bounds))
                    {
                        UpdatePlayerHealth(false);
                        player.Image = Resources.fighterplane_50;
                        var resetTimer = new System.Windows.Forms.Timer { Interval = 1000 };
                        resetTimer.Tick += (s, e) =>
                        {
                            player.Image = Resources.fighterplane;
                            resetTimer.Stop();
                        };
                        resetTimer.Start();
                        Controls.Remove(enemyList[i]);
                        enemyList.RemoveAt(i);
                        break;
                    }
                }
                await Task.Delay(1);
            }

            Controls.Remove(enemy);
            enemy.Dispose();
            enemyList.Remove(enemy);
        }

        private void SpawnEnemy(object sender, EventArgs e)
        {
            var enemy = new PictureBox
            {   Size = enemy_1.Size,
                Image = enemy_1.Image,
                SizeMode = enemy_1.SizeMode
            };

            enemyList.Add(enemy);
            UpdateScore(1);
            int xPosition = random.Next(0, ClientSize.Width - enemy.Width);
            enemy.Location = new Point(xPosition, -90);
            Controls.Add(enemy);
            enemy.SendToBack();
            MoveEnemy(enemy);
        }

        private async void GameTimer_Tick(object sender, EventArgs e)
        {
            if (isMovingLeft && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }
            if (isMovingRight && player.Right < ClientSize.Width)
            {
                player.Left += playerSpeed;
            }
        }

        private async void MoveBullet(PictureBox bullet)
        {
            while (bullet.Top > -70)
            {
                bullet.Top -= 3;
                for (int i = bulletList.Count - 1; i >= 0; i--)
                {
                    for (int j = enemyList.Count - 1; j >= 0; j--)
                    {
                        var bulletBounds = bulletList[i].Bounds;
                        bulletBounds.Y += 15;

                        if (bulletBounds.IntersectsWith(enemyList[j].Bounds))
                        {
                            UpdateScore(2);
                            ShowExplosion(new Point(bulletList[i].Location.X - 10, bulletList[i].Location.Y));

                            Controls.Remove(enemyList[j]);
                            Controls.Remove(bulletList[i]);
                            bulletList.RemoveAt(i);
                            enemyList.RemoveAt(j);
                            break;
                        }
                    }
                }
                await Task.Delay(1);
            }

            Controls.Remove(bullet);
            bulletList.Remove(bullet);
            bullet.Dispose();
        }

        private async void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                await Task.Delay(1);
                if (!isShootingAllowed) return;
                var bullet = new PictureBox
                {
                    Size = bullet_1.Size,
                    Image = bullet_1.Image,
                    BackColor = bullet_1.BackColor,
                    SizeMode = bullet_1.SizeMode
                };

                bulletList.Add(bullet);
                bullet.Location = new Point(player.Location.X + 50, player.Location.Y - 60);
                Controls.Add(bullet);
                bullet.BringToFront();

                MoveBullet(bullet);

                isShootingAllowed = false;
                await Task.Delay(bulletCooldown);
                isShootingAllowed = true;
            }

            if (e.KeyCode == Keys.Left) { isMovingLeft = true; }
            else if (e.KeyCode == Keys.Right) { isMovingRight = true; }
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { isMovingLeft = false; }
            else if (e.KeyCode == Keys.Right) { isMovingRight = false; }
        }

        private void Game_Load(object sender, EventArgs e)
        {
            CreateHearts();
            player.Left = (this.ClientSize.Width - player.Width) / 2;
            player.Top = (this.ClientSize.Height) - 145;
            player.Anchor = AnchorStyles.None;

            lbl_gameover.Left = (this.ClientSize.Width - lbl_gameover.Width) / 2;
            lbl_gameover.Top = (this.ClientSize.Height - lbl_gameover.Height) / 2 - 150;

            btn_exit.Top = lbl_gameover.Top + 140;
            btn_exit.Left = lbl_gameover.Left + 70;

            btn_restart.Top = lbl_gameover.Top + 140;
            btn_restart.Left = lbl_gameover.Width + lbl_gameover.Left - 180;
        }

        private void btn_restart_Click(object sender, EventArgs e)
        {
            UpdateScore(currentScore * -1);
            CreateHearts();
            difficult.Start();
            this.Select();
            this.Focus();

            playerHealth = 3;
            playerSpeed = 9;
            
            btn_exit.Visible = false;
            btn_restart.Visible = false;
            lbl_gameover.Visible = false;

            enemy_speed = 2;
            enemySpawnTimer.Interval = 2400;
            enemySpawnTimer.Start();
            change_day_night.Start();

            for (int j = enemyList.Count - 1; j >= 0; j--)
            {   Controls.Remove(enemyList[j]); enemyList.RemoveAt(j); }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        { this.Close(); }
    }
}