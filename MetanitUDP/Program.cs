using System.Net.Sockets;
using System.Net;
using System.Text;

namespace MetanitUDP
{
    internal class Program
    {
        static bool end = false;
        static void Main(string[] args)
        {
            Sender();
            while(!end)
            {
                Thread.Sleep(100);
            }
        }
        static async void Sender()
        {
            using var updClient = new UdpClient(8001);
            var brodcastAddress = IPAddress.Parse("235.5.5.11");
            updClient.JoinMulticastGroup(brodcastAddress);
            Console.WriteLine("Начало прослушивания сообщений");
            while (true)
            {
                var result = await updClient.ReceiveAsync();
                string message = Encoding.UTF8.GetString(result.Buffer);
                if (message == "END") break;
                Console.Write(message);
            }
            updClient.DropMulticastGroup(brodcastAddress);
            Console.WriteLine("UDP-client закончил работу");
            end = true;
        }
    }
}