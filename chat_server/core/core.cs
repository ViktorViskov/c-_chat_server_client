// 
// libs
// 
using System;
using chat_server.connection;

namespace chat_server.core
{
    // main class
    class Core
    {
        // 
        // variables
        // 
        Connection connection;


        // 
        // constructor
        // 
        public Core()
        {
            // print welcome message
            Message();

            // start main code
            Start();

            // start controll
            Controll();
        }
        // 
        // methods
        // 

        // method for print message
        private void Message()
        {
            Console.Clear();
            Console.WriteLine("Chat server");
            Console.WriteLine("Press ESC to exit from app");
        }

        // method for start main code
        private void Start()
        {
            // create connection
            connection = new Connection();
        }

        // method for controlling
        private void Controll()
        {
            while (true)
            {
                // break loop if pressed esc
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

    }
}


// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
