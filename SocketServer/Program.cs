using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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


                //DeliveryNote d = new DeliveryNote();
                //d.foremanid = 1;
                //d.orderid = 4;
                //d.success = true;
                //DbHandler.addDeliveryNote(d);
                //
                //foreach (Order o in orders) {
                //    Console.WriteLine(o.Terminal);
                //}
                //List<DeliveryNote> notes = DbHandler.GetDeliveryNotes();
                //foreach(DeliveryNote d in notes)
                //{
                //    Console.WriteLine(d.orderid);
                //}

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