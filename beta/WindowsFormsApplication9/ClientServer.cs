using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication9.Properties;

namespace WindowsFormsApplication9
{
    class ClientServer
    {
        public TcpClient Client;
        public string nameClient = string.Empty;
        delegate void Del(int ChessJ, int ChessI, int PoleJ, int PoleI, int method);

        public ClientServer(TcpClient Client_)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Client = Client_;
            nameClient = "clientServer1";
            string msgReaded = string.Empty;
            string msgWrite = string.Empty;
            bool game = false;
            Thread ThreadForm;

            while (Client.Connected)
            {
                if (!game)
                {
                    game = true;
                    ThreadForm = new Thread(new ParameterizedThreadStart(StartForm));
                    ThreadForm.Start(nameClient);
                }
                else if (true)
                {                    
                    int[] intStroke = ReadStroke();
                    /*Del stroke = new Del(delegateStroke);
                    stroke.Invoke(intStroke[0], intStroke[1], intStroke[2], intStroke[3], intStroke[4]);*/
                    Graphics.Stroke(intStroke[0], intStroke[1], intStroke[2], intStroke[3], intStroke[4]);
                    Graphics.ChessActive = null;
                    Graphics.isStroke = true;
                }
            }
        }// конец конструктора 1

        public ClientServer(int Port, string Ip)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            TcpClient Client_ = new TcpClient(Ip, Port);
            Client = Client_;
            nameClient = "clientServer2";
            string msgReaded = string.Empty;
            string msgWrite = string.Empty;
            bool game = false;
            Thread ThreadForm;

            while (Client.Connected)
            {
                if (!game)
                {
                    game = true;
                    ThreadForm = new Thread(new ParameterizedThreadStart(StartForm));
                    ThreadForm.Start(nameClient);
                }
                else if (true)
                {
                    int[] intStroke = ReadStroke();
                    Graphics.Stroke(intStroke[0], intStroke[1], intStroke[2], intStroke[3], intStroke[4]);
                    Graphics.ChessActive = null;
                    Graphics.isStroke = true;
                }
            }
        }// конец конструктора 2

        private static void delegateStroke(int ChessJ, int ChessI, int PoleJ, int PoleI, int method)
        {
            Graphics.Stroke(ChessJ, ChessI, PoleJ, PoleI, method);
        }

        private void StartForm(object name)
        {
            PictureBox Pole;
            Graphics ChessGrp;

            Form Game = new Form();
            Game.Text = (string)name;
            Game.ClientSize = new System.Drawing.Size(560, 560);
            ChessGrp = new Graphics(Game, this);
            Pole = new PictureBox();
            Pole.Image = new Bitmap(Resources.pole);
            Pole.Location = new Point(0, 0);
            Pole.SizeMode = PictureBoxSizeMode.AutoSize;
            Game.Controls.Add(Pole);
            Game.ShowDialog();
        }

        public string Read()
        {
            string msgReaded = string.Empty;
            byte[] data = new byte[256];
            Int32 bytes = Client.GetStream().Read(data, 0, data.Length);
            msgReaded = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

            return msgReaded;
        }

        public void Write(string msgWrite)
        {
            byte[] BufferTo = Encoding.ASCII.GetBytes(msgWrite);
            Client.GetStream().Write(BufferTo, 0, BufferTo.Length);
        }

        public void WriteStroke(int ChessI, int ChessJ, int PoleI, int PoleJ, int method)
        {
            string msgWrite = ChessI.ToString() + "," + ChessJ.ToString() + "," + PoleI.ToString() + "," + PoleJ.ToString() + "," + method.ToString();
            byte[] BufferTo = Encoding.ASCII.GetBytes(msgWrite);
            Client.GetStream().Write(BufferTo, 0, BufferTo.Length);
        }

        public int[] ReadStroke()
        {
            int[] intStroke = new int[5];
            string msgReaded = string.Empty;
            byte[] data = new byte[256];
            Int32 bytes = Client.GetStream().Read(data, 0, data.Length);
            msgReaded = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            string[] strStroke = msgReaded.Split(',');
            for (int i = 0; i <= 4; i++)
            {
                intStroke[i] = Convert.ToInt32(strStroke[i]);
            }

            return intStroke;
        }
    }// конец класса
}
