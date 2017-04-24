using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication9
{
    public partial class Form1 : Form
    {
        private PictureBox Pole;
        private Graphics ChessGrp;
        private static int active;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ChessGrp = new Graphics(this);

            Pole = new PictureBox();
            Pole.Image = new Bitmap(@"H:\Users\denis\Documents\Visual Studio 2015\Projects\kurs\WindowsFormsApplication9\bin\Debug\image\pole.jpg");
            Pole.Location = new Point(0, 0);
            Pole.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Controls.Add(Pole);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
