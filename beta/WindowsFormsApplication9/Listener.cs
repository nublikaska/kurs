using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApplication9
{
    class Listener
    {
        TcpListener listener;

        public Listener(int Port)
        {
            listener = new TcpListener(IPAddress.Any, Port);
            listener.Start();

            while (true)
            {
                TcpClient Client = listener.AcceptTcpClient();
                ServerStart(Client);
                listener.Stop();
            }
        }
        private static void ServerStart(object Client)
        {
            new ClientServer((TcpClient)Client);
        }
        
    }
}
