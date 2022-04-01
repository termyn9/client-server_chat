using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class User
    {
        private Thread _userThread; // поток пользователя
        private string _userName;
        private bool AuthSuccess = false; // поле успешной авторизации 
        private Socket _userHandle; // сокет пользовательского подключения

        public string Username
        {
            get { return _userName; }
        }
        public User(Socket handle)
        {
            _userHandle = handle; // сокет пользователя
            _userThread = new Thread(listener); // поток пользователя
            _userThread.IsBackground = true; // переводим наш поток в фоновой 
            _userThread.Start(); // запуск потока
        }
        // метод прослушивания входящих сообщений, дальнейшая обработка полученного сообщения через сокет
        private void listener()
        {
            try
            {
                while (_userHandle.Connected) // пока сокет подключен к удаленному хосту (bool)
                {
                    // === Получаем данные для дальнейшей отправки пользователю === //
                    byte[] buffer = new byte[2048]; // буфер обмена
                    int bytesReceive = _userHandle.Receive(buffer); // получение данных из буфера -> их количество

                    // === Операции отправки Send() описаны через отдельный метод с всевозможными условиями === //
                    handleCommand(Encoding.Unicode.GetString(buffer, 0, bytesReceive)); // обработка входящей строки (byte -> string) сокета
                }
            }
            catch { Server.EndUser(this); }
        }

        // ПРИВАТНЫЙ метод дополнительных проверок имени пользователя
        private bool setName(string Name)
        {
            //Тут можно добавить различные проверки
            _userName = Name;
            Server.NewUser(this);
            AuthSuccess = true;
            return true;
        }

        // мето обработки сокетом входящей строки (команды)
        private void handleCommand(string cmd)
        {
            try
            {
                string[] commands = cmd.Split('#'); // разделяем входную строчку данных на массив подстрок
                int countCommands = commands.Length;
                for (int i = 0; i < countCommands; i++) // пробегаемся по всем отдельным словам в массиве
                {
                    string currentCommand = commands[i];
                    if (string.IsNullOrEmpty(currentCommand))
                        continue;
                    if (!AuthSuccess) // просто дополнительный параметр успешной авторизации
                    {
                        // если в потоке имеется со стороны клиента отправленные данные #setname|{nickName}
                        if (currentCommand.Contains("setname")) // "setname|UserName"
                        {
                            // !!! Здесь же вызываем событие setName -> Server.NewUser(this) -> EVENT UserConnected!!! // 
                            if (setName(currentCommand.Split('|')[1])) // UserName (введенное имя)
                                Send("#setnamesuccess"); // посылаем в соединение команду 
                            else
                                Send("#setnamefailed");
                        }
                        continue;
                    }

                    //
                    if(currentCommand.Contains("yy"))
                    {
                        string id = currentCommand.Split('|')[1];
                        Server.FileD file = Server.GetFileByID(int.Parse(id));
                        if (file.ID == 0)
                        {
                            SendMessage("Ошибка при передаче файла...", "1");
                            continue;
                        }
                        Send(file.fileBuffer);
                        Server.Files.Remove(file);
                    }

                    // передача обычного сообщения в чате
                    if (currentCommand.Contains("message"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        Server.SendGlobalMessage($"[{_userName}]: {Arguments[1]}","Black");

                        continue;
                    }
                    if(currentCommand.Contains("endsession"))
                    {
                        Server.EndUser(this);
                        return;
                    }

                    // обработка отправки файла
                    if(currentCommand.Contains("sendfileto"))
                    {
                        string[] Arguments = currentCommand.Split('|');

                        // Параметры файла
                        string TargetName = Arguments[1];
                        int FileSize = int.Parse(Arguments[2]);
                        string FileName = Arguments[3];
                        byte[] fileBuffer = new byte[FileSize];

                        _userHandle.Receive(fileBuffer); // возвращаем данные из сокета от объекта

                        User targetUser = Server.GetUser(TargetName); // попытка получить пользователя для отправки
                        if(targetUser == null)
                        {
                            SendMessage($"Пользователь {FileName} не найден!","Black");
                            continue;
                        }

                        // Инициализация файла (через структуру FileD) 
                        Server.FileD newFile = new Server.FileD()
                        {
                            ID = Server.Files.Count+1,
                            FileName = FileName,
                            From = Username,
                            fileBuffer = fileBuffer,
                            FileSize = fileBuffer.Length
                        };
                        Server.Files.Add(newFile); // добавляем файл в список
                        targetUser.SendFile(newFile,targetUser); // отправка файла пользователю 
                    }

                    //  отправка личного сообщения
                    if(currentCommand.Contains("private"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        string TargetName = Arguments[1];
                        string Content = Arguments[2];
                        User targetUser = Server.GetUser(TargetName);
                        if(targetUser == null)
                        {
                            SendMessage($"Пользователь {TargetName} не найден!","Red");
                            continue;
                        }
                        SendMessage($"-[Отправлено][{TargetName}]: {Content}","Green");
                        targetUser.SendMessage($"-[Получено][{Username}]: {Content}", "Green");
                        continue;
                    }

                }

            }
            catch (Exception exp) { Console.WriteLine("Error with handleCommand: " + exp.Message); }
        }

        // метод отправки сообщения с контентом (+ цвет)
        public void SendMessage(string content, string clr)
        {
            Send($"#msg|{content}|{clr}");
        }

        // непосредственный метод отправки файла
        public void SendFile(Server.FileD fd, User To)
        {
            byte[] answerBuffer = new byte[48];
            Console.WriteLine($"Sending {fd.FileName} from {fd.From} to {To.Username}");
            To.Send($"#gfile|{fd.FileName}|{fd.From}|{fd.fileBuffer.Length}|{fd.ID}");
        }

        // === 2 метода отправвки непосредственно в сокет === //
        // входные данные - байты
        public void Send(byte[] buffer)
        {
            try
            {
                _userHandle.Send(buffer);
            }
            catch { }
        }
        // входные данные - строка 
        public void Send(string Buffer)
        {
            try
            {
                _userHandle.Send(Encoding.Unicode.GetBytes(Buffer));
            }
            catch { }
        }

        // закрыть сокет
        public void End()
        {
            try
            {
                _userHandle.Close();
            }
            catch { }
        }
    }
}
