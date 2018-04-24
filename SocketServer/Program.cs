using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Communication;
using SocketServer;

namespace Program
{
    class ServerContoller
    {
        static AsyncService service;
        static void Main(string[] args)
        {
            ServerContoller.StartService();
            
        }

        public static void StartService()
        {
            try
            {
                int port = 50000;
                service = new AsyncService(port);
                service.Run();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}