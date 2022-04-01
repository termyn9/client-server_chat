using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public static class Server
    {
        // поля инициализации сокета
        public static Socket ServerSocket;
        public const string HOST = "127.0.0.1";
        public const int PORT = 2222;
        public static bool Work = true;

        public static List<User> UserList = new List<User>();
        
        public static List<FileD> Files = new List<FileD>();
        public struct FileD
        {
            public int ID;
            public string FileName;
            public string From;
            public int FileSize;
            public byte[] fileBuffer;
        }

        public static int CountUsers = 0;
        public delegate void UserEvent(string Name); // делегат события UserEvent (действия, связанные с пользователем)
        
        // === СОБЫТИЯ, представляющие делегат === //
        // событие подключения пользователя
        public static event UserEvent UserConnected = (Username) =>  
        {
            Console.WriteLine($"Пользователь {Username} подключился.");
            CountUsers++;
            SendGlobalMessage($"Пользователь {Username} подключился к чату.", "Black");
            SendUserList();
        };

        public static event UserEvent UserDisconnected = (Username) =>
        {
            Console.WriteLine($"Пользователь {Username} отключился.");
            CountUsers--;
            SendGlobalMessage($"Пользователь {Username} отключился от чата.","Black");
            SendUserList();
        };

        // подключение клиента в чат
        public static void NewUser(User user)
        {
            if (UserList.Contains(user))
                return;
            UserList.Add(user);
            UserConnected(user.Username); // "Пользователь {username} подключен"
        }
        // отключение клиента из чата
        public static void EndUser(User usr)
        {
            if (!UserList.Contains(usr))
                return;
            UserList.Remove(usr);
            usr.End(); // ОБЯЗАТЕЛЬНО закрываем сокет подключения для этого пользователя
            UserDisconnected(usr.Username); // "Пользователь {username} отключен"
        }

        // метод для обращения к определенному пользователю (личное сообщение и передача файла)
        public static User GetUser(string Name)
        {
            for(int i = 0; i < CountUsers;i++)
            {
                if (UserList[i].Username == Name)
                    return UserList[i];
            }
            return null;
        }
        // метод рассылки в чат
        public static void SendUserList()
        {
            string userList = "#userlist|";

            for(int i = 0; i < CountUsers;i++)
            {
                userList += UserList[i].Username + ", ";
            }
            SendAllUsers(userList);
        }
        // метод отправки личного сообщения
        public static void SendGlobalMessage(string content,string clr)
        {
            for(int i = 0;i < CountUsers;i++)
            {
                UserList[i].SendMessage(content, clr);
            }
        }

        // отправка данных (строка) - чат
        public static void SendAllUsers(string data)
        {
            for (int i = 0; i < CountUsers; i++)
            {
                UserList[i].Send(data);
            }
        }
        // отправка данных (байты) - сеть
        public static void SendAllUsers(byte[] data)
        {
            for (int i = 0; i < CountUsers; i++)
            {
                UserList[i].Send(data);
            }
        }

        // работа с файлом по ID
        public static FileD GetFileByID(int ID)
        {
            int countFiles = Files.Count;
            for (int i = 0; i < countFiles; i++)
            {
                if (Files[i].ID == ID)
                    return Files[i];
            }
            return new FileD() { ID = 0 };
        }

    }
}
