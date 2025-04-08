namespace SpaceWar
{
    partial class Game
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            player = new PictureBox();
            enemy_1 = new PictureBox();
            lbl_score = new Label();
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemy_1).BeginInit();
            SuspendLayout();
            // 
            // player
            // 
            player.BackColor = Color.Transparent;
            player.BackgroundImageLayout = ImageLayout.None;
            player.Image = Properties.Resources.fighterplane;
            player.Location = new Point(935, 600);
            player.Name = "player";
            player.Size = new Size(132, 136);
            player.SizeMode = PictureBoxSizeMode.Zoom;
            player.TabIndex = 0;
            player.TabStop = false;
            // 
            // enemy_1
            // 
            enemy_1.BackColor = Color.Transparent;
            enemy_1.Image = Properties.Resources.alien;
            enemy_1.Location = new Point(788, 431);
            enemy_1.Name = "enemy_1";
            enemy_1.Size = new Size(81, 86);
            enemy_1.SizeMode = PictureBoxSizeMode.Zoom;
            enemy_1.TabIndex = 1;
            enemy_1.TabStop = false;
            enemy_1.Visible = false;
            // 
            // lbl_score
            // 
            lbl_score.AutoSize = true;
            lbl_score.Font = new Font("Brush Script MT", 25.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lbl_score.Location = new Point(12, 9);
            lbl_score.Name = "lbl_score";
            lbl_score.Size = new Size(98, 51);
            lbl_score.TabIndex = 2;
            lbl_score.Text = "0000";
            // 
            // Game
            // 
            AutoScaleDimensions = new SizeF(9F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 255, 255);
            ClientSize = new Size(1354, 748);
            Controls.Add(enemy_1);
            Controls.Add(lbl_score);
            Controls.Add(player);
            Name = "Game";
            Text = "Space War Game";
            Load += Game_Load;
            ((System.ComponentModel.ISupportInitialize)player).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemy_1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox player;
        private PictureBox enemy_1;
        private Label lbl_score;
    }
}
