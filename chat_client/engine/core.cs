// 
// Libs
// 
using System;
using engine.connection;

namespace engine.core
{
    // core class
    class Core
    {
        // 
        // Variables
        // 
        string userName;
        string ip;
        int port;
        Connection con;


        // 
        // Constructor
        // 
        public Core()
        {
            // getting info about connection
            GetConnectionInfo();

            try
            {
                // open connection
                con = new Connection(ip, port);
                
                // clean screen
                Console.Clear();
                Console.WriteLine($"Connected to {ip}:{port}");

                // start messaging
                StartMessaging();
            }

            // print error
            catch
            {
                Console.WriteLine("Check inputed data and try again.");
            }

        }


        // 
        // Methods
        // 

        // method for get data connection data from user
        private void GetConnectionInfo()
        {
            // clean screen
            Console.Clear();

            // messages
            Console.WriteLine("Hello in chat client");
            Console.WriteLine("Type servers ip address and port");

            // getting data
            Console.Write("Your name: ");
            userName = Console.ReadLine();
            Console.Write("IP address: ");
            ip = Console.ReadLine();
            Console.Write("Port: ");
            port = int.Parse(Console.ReadLine());

        }

        // method for starting messaging
        private void StartMessaging()
        {
            // main loop
            while (true)
            {
                con.Send($"{userName}: {Console.ReadLine()}");
            }
        }
    }
}
