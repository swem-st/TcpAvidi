using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client
{
    class Program
    {
        const string ip = "127.0.0.1";
        const int port = 8888;
        static void Main(string[] args)
        {
            TcpClient client = null;
            try
            {
                client = new TcpClient(ip, port);
                NetworkStream stream = client.GetStream();
                while (true)
                {
                    string message = DateTime.Now + ": object get!";

                    byte[] data = Encoding.Unicode.GetBytes(message);
                    //send message
                    stream.Write(data, 0, data.Length);

                    //receive answer
                    data = new byte[1024]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        Thread.Sleep(1000);

                    }
                    while (stream.DataAvailable);

                    message = builder.ToString();
                    Console.WriteLine("Сервер: {0}", message);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
            }

        }
    }
}
