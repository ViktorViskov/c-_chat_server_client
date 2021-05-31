// 
// libs
// 
using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Collections.Generic;

namespace chat_server.connection
{
    // class connection
    class Connection
    {
        // 
        // variables
        // 
        public TcpListener listener;
        List<NetworkStream> clients = new List<NetworkStream> { };

        // 
        // constructor
        // 
        public Connection()
        {
            // init connection
            Init();

            // print Info
            Info();

            // await for client connetion
            ConnectClient();

        }

        //
        // Methods
        // 


        // init connection
        private void Init()
        {
            // base connection
            IPAddress ip = IPAddress.Any;
            int port = 8182;
            IPEndPoint endPoint = new IPEndPoint(ip, port);


            // bindingh with pc interface and socket
            listener = new TcpListener(endPoint);
            listener.Start();
        }

        // async connections
        private async void ConnectClient()
        {
            // list with clients

            while (true)
            {
                // get connection
                TcpClient client = await listener.AcceptTcpClientAsync();

                // print connection status
                Console.WriteLine($"{client.Client.RemoteEndPoint.ToString().Split(":")[0]} connected");

                // open stream
                NetworkStream stream = client.GetStream();

                // add to list
                clients.Add(stream);

                // redirect messages
                Redirect(stream);
            }

        }

        // method for redirecting data
        private async void Redirect(NetworkStream stream)
        {
            // main loop
            while (true)
            {
                // try exeprion if host clouse connection
                try
                {
                    // buffer for receive data
                    byte[] buffer = new byte[1500];

                    // get data and canculate bytes
                    int numberOfBytes = await stream.ReadAsync(buffer, 0, 1500);

                    // resend message
                    foreach (NetworkStream someClient in clients)
                    {
                        // dont send message back for owner
                        if (someClient != stream)
                        {
                            someClient.Write(Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(buffer, 0, numberOfBytes)));
                        }
                    }
                }

                // if connection was cloused remove client from clients list 
                catch (System.IO.IOException)
                {
                    // print connection status
                    Console.WriteLine($"{stream.Socket.RemoteEndPoint.ToString().Split(":")[0]} disconnected");

                    // delete client
                    clients.Remove(stream);

                    // stop loop
                    break;
                }
            }
        }

        // connection info
        private void Info()
        {
            // print pc interface adresses
            Console.WriteLine($"PC {Dns.GetHostName()} intefaces");
            foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                Console.WriteLine(address.ToString());
            }

            // print server connection info
            Console.WriteLine($"Server started on {listener.LocalEndpoint}");

        }
    }
}