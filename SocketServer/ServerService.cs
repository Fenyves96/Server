using Communication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SocketServer
{
    public class AsyncService
    {
        private IPAddress ipAddress;
        private int port;
        public AsyncService(int port)
        {
            this.port = port;
            
            this.ipAddress = IPAddress.Parse("127.0.0.1");
            if (this.ipAddress == null)
                throw new Exception("No IPv4 address for server");
        }
        public async void Run()
        {
            TcpListener listener = new TcpListener(this.ipAddress, this.port);
            listener.Start();
            Console.Write("Test socket service is now running");
            Console.WriteLine(" " + this.ipAddress + " on port " + this.port);
            Console.WriteLine("Hit <enter> to stop service\n");
            while (true)
            {
                try
                {
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();
                    Task t = Process(tcpClient);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private async Task Process(TcpClient tcpClient)
        {
            string clientEndPoint = tcpClient.Client.RemoteEndPoint.ToString();  //ez adja vissza a kliens ip címét
            Console.WriteLine("Received connection request from " + clientEndPoint);
            try
            {
                //üzenet fogadása és válasz küldése
                NetworkStream networkStream = tcpClient.GetStream();
                StreamReader reader = new StreamReader(networkStream);
                StreamWriter writer = new StreamWriter(networkStream);
                writer.AutoFlush = true;
                
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                while (true)
                {
                    string requestStr = await reader.ReadLineAsync();
                    if (requestStr != null)
                    {

                        //CommObject request = serializer.Deserialize<CommObject>(requestStr);
                        Console.WriteLine(requestStr);
                    
                        Order request = serializer.Deserialize<Order>(requestStr);
                        request.Print();

                        Console.WriteLine(request.ID.ToString());
                        //Console.WriteLine(request.DateIn);
                        Console.WriteLine("Received service request from: " + request.ToString());
                        Order response = Response(request);
            
                        Console.WriteLine("Computed response is: " + response + "\n");
                        await writer.WriteLineAsync(serializer.Serialize(response));
                    }
                    else
                    {
                        Console.WriteLine("Connection closed, client: " + clientEndPoint);
                        break; // Client closed connection
                    }
                }
                tcpClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (tcpClient.Connected)
                {                    
                    tcpClient.Close();
                }
                Console.WriteLine("Connection closed, client: " + clientEndPoint);
            }
        }
        private static Order Response(Order request)
        {
            //Próbáljuk meg elmenteni adatbázisba
            return request;
        }
    }
}
