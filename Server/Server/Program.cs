using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress address = IPAddress.Parse(Server.HOST);
            // AddressFamily - все адреса сокета (в нашем случае InterNetwork)
            // SocketType.Stream - TCP-подключение (указывает на использование InterNetwork - IPv4 в AddressFamily)
            Server.ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind() - для установки локальной точки прослушивания входящих подключений
            Server.ServerSocket.Bind(new IPEndPoint(address, Server.PORT));
            // после установки Bind обзательно надо начать слушать получаемые подключения
            Server.ServerSocket.Listen(100);

            Console.WriteLine($"Сервер запущен на {Server.HOST}:{Server.PORT}");
            Console.WriteLine("Ожидаются входящие подключения...");

            // бесконечный цикл на прием n-го количества подключений пользователей
            while(Server.Work)
            {
                // для входящего подключения пользователя чата
                // этот Socket будет Send() и Receive() данные
                Socket handle = Server.ServerSocket.Accept(); 
                Console.WriteLine($"Новое подключение: {handle.RemoteEndPoint.ToString()}");

                // объект нового класса user с собственным сокетом подключения
                new User(handle);
            }
            Console.WriteLine("Сервер отключен...");
        }
    }
}
