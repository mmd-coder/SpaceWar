using System;

public class Class1
{
	public Class1()
	{

        private void fire()
    {
        SoundPlayer player = new SoundPlayer("C:\\Users\\MAC\\Downloads\\exl.wav");
        player.Play();
    }

    private void loseheart()
    {
        SoundPlayer player = new SoundPlayer("C:\\Users\\MAC\\Downloads\\heart.wav");
        player.Play();
    }

    private void healthgain()
    {
        SoundPlayer player = new SoundPlayer("C:\\Users\\MAC\\Downloads\\heartt.wav");
        player.Play();
    }

    private void UpdateScore(int points)
    {
        currentScore = int.Parse(lbl_score.Text) + points;
        lbl_score.Text = currentScore.ToString("D4");
    }
    private void LoadHighScore()
    {
        string filePath = "scorehigh.txt";
        if (File.Exists(filePath))
        {
            string fileContent = File.ReadAllText(filePath);
            highscore = int.TryParse(fileContent, out int result) ? result : 0;
        }
        else
        {
            highscore = 0;
        }
        lbl_highscore.Text = $"High score : {highscore.ToString("D4")}";
    }
    private void SaveHighScore()
    {
        string filePath = "scorehigh.txt";
        if (currentScore > highscore)
        {
            highscore = currentScore;
            File.WriteAllText(filePath, highscore.ToString());
        }
    }

    private void btn_restart_Click(object sender, EventArgs e)
    {
        SaveHighScore();
        currentScore = 0;
        lbl_score.Text = currentScore.ToString("D4");
        LoadHighScore();
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
        {
            Controls.Remove(enemyList[j]);
            enemyList.RemoveAt(j);
        }
    }
}
}
