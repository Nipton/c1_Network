using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Chat
    {
        readonly IPEndPoint localEndPoint;
        readonly IPEndPoint remoteEndPoint;
        Message message = new Message();
        public Chat(IPEndPoint localEndPoint, IPEndPoint remoteEndPoint)
        {
            this.localEndPoint = localEndPoint;
            this.remoteEndPoint = remoteEndPoint;
        }
        
        public async Task SendMessageAsync()
        {
            Console.WriteLine("Введите имя:");
            message.Name = Console.ReadLine()!;
            using UdpClient udpClient = new();
            Console.WriteLine("Введите сообщение:");
            while (true)
            {
                try
                {
                    message.Text = Console.ReadLine();
                    message.Date = DateTime.Now;
                    string messegeToSend = message.ToJson();
                    byte[] data = Encoding.UTF8.GetBytes(messegeToSend);
                    await udpClient.SendAsync(data, remoteEndPoint);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public async Task ReceiveMessageAsync()
        {
            using UdpClient udpClient = new(localEndPoint);
            while (true)
            {
                try
                {
                    var result = await udpClient.ReceiveAsync();
                    var message = Message.FromJson(Encoding.UTF8.GetString(result.Buffer));
                    Console.WriteLine(message);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }           
        }
    }
}
