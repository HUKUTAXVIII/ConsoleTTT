using System;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client("127.0.0.1",8000);
            client.Connect();
            Console.WriteLine("Client connected!");

            Console.WriteLine(Client.FromBytesToString(client.Get()));
            while (true) {
                var info = Client.FromBytesToString(client.Get());
                if (info == "X Win!" || info == "O Win!" || info == "Draw!")
                {
                    Console.WriteLine(info);
                    break;
                }
                Console.Clear();
                Console.WriteLine(info);
                Console.WriteLine("Enter X and Y:");
                client.Send(Client.FromStringToBytes(Console.ReadLine()));
            }
            
            Console.WriteLine(Client.FromBytesToString(client.Get()));
            client.Close();
            Console.ReadLine();
        }
    }
}
