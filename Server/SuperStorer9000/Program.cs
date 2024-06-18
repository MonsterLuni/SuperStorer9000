using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        TcpListener server = null;
        try
        {
            int port = 5555;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            server = new TcpListener(localAddr, port);
            server.Start();

            Console.WriteLine("Warten auf eine Verbindung... ");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.Start();
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
        finally
        {
            if (server != null)
                server.Stop();
        }

        Console.WriteLine("\nDrücken Sie eine beliebige Taste zum Fortsetzen...");
        Console.Read();
    }
    private static void HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        Byte[] bytes = new Byte[256];
        String data = null;

        try
        {
            int i;
            int before = 0;
            while ((i = stream.Read(bytes, 0, bytes.Length - before)) != 0)
            {
                data = Encoding.ASCII.GetString(bytes, 0, i);
                string name = data.Split(" ")[0];
                Console.WriteLine(name + " ist dem Server beigetreten!");
                before = bytes.Length;
                //Boolean writing = true;
                /*while (writing)
                {
                    Console.WriteLine("Nachricht für den Client: ");
                    byte[] msg = Encoding.ASCII.GetBytes(Console.ReadLine());
                    Console.Clear();
                    Console.WriteLine("Data von " + data);
                    stream.Write(msg, 0, msg.Length);
                }*/
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: {0}", e.Message);
        }
        finally
        {
            client.Close();
        }
    }
}
