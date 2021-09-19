using System;
using System.Linq;
using System.Text;
using TTTLogic;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1",8000);
            server.Start();

            GameUnit turn = GameUnit.X;


            Console.WriteLine("Server started!");


            GameLogic game = new GameLogic();


            server.Send(Server.FromStringToBytes(game.ToString()));
            while (game.CheckStatus()==GameStatus.PLAY) {
                Console.Clear();
                Console.WriteLine(game.ToString());

                string[] values = new string[255];
                do
                {
                Console.WriteLine("Enter Y and X:");
                values = Console.ReadLine().Split(" ");
                } while (!game.SetUnit(int.Parse(values[0]) - 1, int.Parse(values[1]) - 1));

                server.Send(Server.FromStringToBytes(game.ToString()));

                string[] client = new string[255];

                client = Server.FromBytesToString(server.Get()).Split(" ");
                while (!game.SetUnit(int.Parse(client[0]) - 1, int.Parse(client[1]) - 1))
                {
                    server.Send(Server.FromStringToBytes(game.ToString()));

                    client = Server.FromBytesToString(server.Get()).Split(" ");
                } 


            }

            string gameinfo = string.Empty;
            switch (game.CheckStatus()) {
                case GameStatus.XWIN:
                    gameinfo = "X Win!";
                    break;
                case GameStatus.OWIN:
                    gameinfo = "O Win!";

                    break;
                case GameStatus.DRAW:
                    gameinfo = "Draw!";

                    break;
            }
            Console.WriteLine(gameinfo);
            server.Send(Server.FromStringToBytes(gameinfo));

            server.Close();
            Console.ReadLine();
        }
    }
}
