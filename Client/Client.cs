﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class Client
    {
        private Socket socket;
        private IPEndPoint ipPoint;

        public Client(string ip,int port)
        {
            socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            ipPoint = new IPEndPoint(IPAddress.Parse(ip),port);
        }
        public void Connect() {
            socket.Connect(ipPoint);
        }
        public void Send(List<byte> data) {
            socket.Send(data.ToArray());
        }
        public List<byte> Get() {
            List<byte> data = new List<byte>();
            int bytes = 0;
            byte[] array = new byte[255];

            do
            {
                bytes = socket.Receive(array, array.Length, 0);
                for(int i=0;i<bytes;i++)
                {
                    data.Add(array[i]);
                }


            } while (socket.Available > 0);


            return data;
        }
        public void Close()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }


        public static string FromBytesToString(List<byte> bytes) {
            return Encoding.Unicode.GetString(bytes.ToArray());
        }
        public static List<byte> FromStringToBytes(string str)
        {
            return Encoding.Unicode.GetBytes(str).ToList();
        }


    }
}
