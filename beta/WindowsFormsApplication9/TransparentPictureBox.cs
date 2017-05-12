using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication9
{
    public class TransparentPictureBox : PictureBox
    {
        public string chs = "pole";
        public bool active = false;
        public int j;
        public int i;

        /*Boolean parent_refreshed = false;

        public TransparentPictureBox()
        {
            this.BackColor = Color.Transparent;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(this.Image);
            e.Graphics.DrawImage(bmp, new Point(0, 0));

            int x;
            int y;

            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            Color mask = Color.FromArgb(0x00000000);
            for (x = 0; x <= bmp.Width - 1; x++)
            {
                for (y = 0; y <= bmp.Height - 1; y++)
                {
                    if (!bmp.GetPixel(x, y).Equals(mask))
                    {
                        gp.AddRectangle(new Rectangle(x, y, 1, 1));
                    }
                }
            }

            bmp.Dispose();
            this.Region = new System.Drawing.Region(gp);
            if (!parent_refreshed)
            {
                this.Parent.Invalidate();
                parent_refreshed = true;
            }
        }*/
    }
}
