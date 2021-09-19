using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class Server
    {
        private Socket socket;
        private Socket handler;
        private IPEndPoint ipPoint;
        
        //private string ip { get; }
        //private int port { get; }

        public Server(string ip,int port)
        {
            socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            ipPoint = new IPEndPoint(IPAddress.Parse(ip),port);
        }

        public void Start() {
            socket.Bind(ipPoint);
            socket.Listen(10);
            handler = socket.Accept();
        }

        public void Send(List<byte> data) {
            handler.Send(data.ToArray());
        }
        public List<byte> Get() {

            List<byte> data = new List<byte>();
            int bytes = 0;
            byte[] array = new byte[255]; 

            do
            {
                bytes = handler.Receive(array, array.Length, 0);
                for (int i = 0; i < bytes; i++)
                {
                    data.Add(array[i]);
                }


            } while (handler.Available>0);


            return data;
        }
        public void Close() {
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }

        public static string FromBytesToString(List<byte> bytes)
        {
            return Encoding.Unicode.GetString(bytes.ToArray());
        }
        public static List<byte> FromStringToBytes(string str)
        {
            return Encoding.Unicode.GetBytes(str).ToList();
        }
    }
}
