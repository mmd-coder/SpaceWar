using System;
using System.Drawing;
using System.Windows.Forms;

public class TransparentImageControl : Control
{
    public Image MyImage { get; set; }

    public TransparentImageControl()
    {
        this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        this.BackColor = Color.Transparent;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (MyImage != null)
        {
            e.Graphics.DrawImage(MyImage, 0, 0, MyImage.Width, MyImage.Height);
        }
    }
}
