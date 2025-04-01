namespace SpaceWar
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = (this.ClientSize.Height) - 145;

            pictureBox1.Anchor = AnchorStyles.None;
        }
    }
}
