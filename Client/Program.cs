using System.Net;

namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Введите локальный порт: ");
            if (!ushort.TryParse(Console.ReadLine(), out var localPort))
            {
                Console.WriteLine("Неверный ввод.");
                return;
            }
            Console.Write("Введите удаленный порт: ");
            if (!ushort.TryParse(Console.ReadLine(), out var remotePort))
            {
                Console.WriteLine("Неверный ввод.");
                return;
            }
            Console.Clear();
            IPEndPoint localPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), localPort);
            IPEndPoint remotePoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), remotePort);
            Chat chat = new Chat(localPoint, remotePoint);
            try
            {
                Task.Run(chat.ReceiveMessageAsync);
                await chat.SendMessageAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}