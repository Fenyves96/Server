using Communication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    class DbHandler
    {
        //Order hozzáadása az adatbázishoz
        public static void addOrder(Order order)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Suli\egyetem\6. félév\Rendszerfejlesztés\Programok\Server\SocketServer\DB_Storage.mdf;Integrated Security=True");
                con.Open();
                int confirmed = 0;
                if (order.Confirmed)
                    confirmed = 1;
                int cooled = 0;
                if (order.Cooled)
                    cooled = 1;

                string dateIn = order.DateIn.ToString("yyyy-MM-dd");
                string dateOut = order.DateOut.ToString("yyyy-MM-dd");
                int id = order.ID;
                int customerID = order.CustomerID;
                int dispatcherID = order.DispatcherID;
                string orderTime = order.OrderTime.ToString("yyyy-MM-dd HH:mm:ss");
                int quantity = order.PalletQuantity;
                int terminal = order.Terminal;
                string comment = order.Comment;
                SqlCommand cmd = new SqlCommand
                    (
                    "INSERT INTO Orders" +
                    "(OrderID, CustomerID, DispatcherID, DateIn," +
                    "DateOut, PalletQuantity, Cooled, Confirmed," +
                    " Terminal, OrderTime, Comment)"+ "VALUES("+order.ID+","+customerID+","+dispatcherID+",'"+dateIn+"','"+dateOut+"',"+quantity+","+cooled+","+confirmed+","+terminal+",'"+orderTime+"','"+comment+"')"
                    );
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Console.WriteLine(dt.Rows[i]["Comment"].ToString());
                }
            }
            catch (Exception e) {
                if(e.GetHashCode()== 43527150)
                    Console.WriteLine("There is an order with this ID.");
            }
        }
    }
}
