// 
// Libs
// 
using System;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace engine.connection
{
    // class for creating connection
    class Connection
    {
        // 
        // Variables
        // 
        NetworkStream stream;


        // 
        // Constructor
        // 
        public Connection(string ip_addr, int port)
        {
            // Open connection
            Open(ip_addr, port);

            // start receiver
            Receive();
        }


        // 
        // Methods
        // 

        // method for open connection
        private void Open(string ip_addr, int port)
        {
            // connection preinitialization
            IPAddress ip = IPAddress.Parse(ip_addr);
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            // connection initialization
            TcpClient client = new TcpClient();
            client.Connect(endPoint);

            // open stream
            stream = client.GetStream();
        }

        // method for receive messages and print
        private async void Receive()
        {
            // loop for reading data from server
            while (true)
            {
                // try exeprion if host clouse connection
                try
                {
                    // buffer for receive data
                    byte[] buffer = new byte[1500];

                    // get data and canculate bytes
                    int numberOfBytes = await stream.ReadAsync(buffer, 0, 1500);

                    // print message
                    Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, numberOfBytes));
                }
                // if connection was cloused remove client from clients list 
                catch (System.IO.IOException)
                {
                    // print message
                    Console.WriteLine("\nRemote server closed the connection...");
                    Console.ReadKey();

                    // exit from app
                    System.Environment.Exit(0);
                }
            }
        }

        // method for send message
        public void Send(string message)
        {
            // send bytes
            stream.Write(Encoding.UTF8.GetBytes(message));
        }

    }
}


