using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication9
{
    public partial class Ip : Form
    {
        Menu Mnu;
        public Ip(Menu Menu_)
        {
            InitializeComponent();
            Mnu = Menu_;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Mnu.Close();
            StartClient(textBox1.Text);
        }

        static void StartClient(object Ip)
        {
            new ClientServer(80, (string)Ip);
        }

        private void Ip_Load(object sender, EventArgs e)
        {

        }
    }
}
