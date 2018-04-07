using System;
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
            Order o = new Order(3, "2018-04-05", "2018-05-06", 4, false, "ads");
            DbHandler.addOrder(o);

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