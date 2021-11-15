using Server.Data;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class Program
    {
        const int port = 8888;
        const string ip = "127.0.0.1";

        static TcpListener listener;
        static void Main(string[] args)
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse(ip), port);
                listener.Start();
                Console.WriteLine("Waiting for connection...");

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(client);
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
              
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
            }
            #region
            //var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            //var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //tcpSocket.Bind(tcpEndPoint);
            //tcpSocket.Listen(5);
            //while (true)
            //{
            //    var listener = tcpSocket.Accept();
            //    var buffer = new byte[256];
            //    var bytes = 0;
            //    var data = new StringBuilder();

            //    do
            //    {
            //        bytes = listener.Receive(buffer);
            //        data.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
            //    }
            //    while (listener.Available > 0);

            //    Console.WriteLine(data);

            //    listener.Send(Encoding.UTF8.GetBytes("Succes"));
            //    Console.WriteLine("Server ready. Press any key to shutdown server.");
            //    Console.ReadKey(true);
            //    listener.Shutdown(SocketShutdown.Both);
            //    listener.Close();
            #endregion
        }
    }
}
