using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Communication;
using SocketServer;

namespace Program
{
    class ServiceProgram
    {
        static void Main(string[] args)
        {
            List<Order> orders=DbHandler.GetOrders();
            foreach(Order o in orders)
            {
                o.Print();
            }
            
            try
            {
                int port = 50000;
                AsyncService service = new AsyncService(port);
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