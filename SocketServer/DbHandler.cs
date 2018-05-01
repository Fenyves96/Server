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
                SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\kovac\OneDrive\Documents\GitHub\Server\SocketServer\DB_Storage.mdf; Integrated Security = True");
                con.Open();
                int confirmed = 0;
                if (order.Confirmed)
                    confirmed = 1;
                int cooled = 0;
                if (order.Cooled)
                    cooled = 1;

                string dateIn = order.DateIn.ToLocalTime().ToString("yyyy-MM-dd");
                string dateOut = order.DateOut.ToLocalTime().ToString("yyyy-MM-dd");
                int customerID = order.CustomerID;
                int dispatcherID = order.DispatcherID;
                string orderTime = order.OrderTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                int quantity = order.PalletQuantity;
                int terminal = order.Terminal;
                string comment = order.Comment;
                SqlCommand cmd = new SqlCommand
                    (
                    "INSERT INTO Orders" +
                    "(CustomerID, DispatcherID, DateIn," +
                    "DateOut, PalletQuantity, Cooled, Confirmed," +
                    " Terminal, OrderTime, Comment)"+ "VALUES("+customerID+","+dispatcherID+",'"+dateIn+"','"+dateOut+"',"+quantity+","+cooled+","+confirmed+","+terminal+",'"+orderTime+"','"+comment+"')"
                    );
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
              
            }
            catch (Exception e) {
                if (e.GetHashCode() == 43527150)
                {
                    Console.WriteLine("There is an order with this ID.");
                    Console.WriteLine(e.Message);
                }
                else
                    Console.WriteLine(e.Message);
            }
        }

        public static List<Order> GetOrders()
        {
            List<Order> orders=new List<Order>();
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\kovac\OneDrive\Documents\GitHub\Server\SocketServer\DB_Storage.mdf; Integrated Security = True");
            con.Open();
            SqlCommand cmd = new SqlCommand
                    ("select * from Orders");
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ID = int.Parse(dt.Rows[i]["OrderID"].ToString());
                string comment = dt.Rows[i]["Comment"].ToString();
                string dateIn = dt.Rows[i]["DateIn"].ToString();
                string dateOut = dt.Rows[i]["DateOut"].ToString();
                int customerID = int.Parse(dt.Rows[i]["CustomerID"].ToString());
                int dispatcherID = int.Parse(dt.Rows[i]["DispatcherID"].ToString());
                string orderTime = dt.Rows[i]["Comment"].ToString();
                int quantity = int.Parse(dt.Rows[i]["PalletQuantity"].ToString());
                int terminal = int.Parse(dt.Rows[i]["Terminal"].ToString());
                bool cooled = false;
                string cooledstring = dt.Rows[i]["Cooled"].ToString();
                if (cooledstring.Equals("True"))
                {
                    cooled = true;
                }
                 
                Order o = new Order(ID,customerID,dateIn,dateOut,quantity,cooled,comment);
                orders.Add(o);
            }
            
            return orders;
        }
        //TODO:Ezt még át kell írni
        internal static List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\kovac\OneDrive\Documents\GitHub\Server\SocketServer\DB_Storage.mdf; Integrated Security = True");
            con.Open();
            SqlCommand cmd = new SqlCommand
                    ("select * from Customers");
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ID = int.Parse(dt.Rows[i]["CustomerID"].ToString());
                string name = dt.Rows[i]["Name"].ToString();
                Customer c = new Customer(ID, name);
                customers.Add(c);
            }

            return customers;
        }

        internal static List<DeliveryNote> GetDeliveryNotes()
        {
            List<DeliveryNote> deliverynotes = new List<DeliveryNote>();
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\kovac\OneDrive\Documents\GitHub\Server\SocketServer\DB_Storage.mdf; Integrated Security = True");
            con.Open();
            SqlCommand cmd = new SqlCommand
                    ("select * from DeliveryNotes");
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ID = int.Parse(dt.Rows[i]["Id"].ToString());
                int foremanid = int.Parse(dt.Rows[i]["ForemanID"].ToString());
                int orderid = int.Parse(dt.Rows[i]["OrderID"].ToString());
                bool success = bool.Parse(dt.Rows[i]["Success"].ToString());
                DeliveryNote dn = new DeliveryNote(ID, success, foremanid, orderid);
                deliverynotes.Add(dn);
            }

            return deliverynotes;
        }

        public static void addDeliveryNote(DeliveryNote deliverynote)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\kovac\OneDrive\Documents\GitHub\Server\SocketServer\DB_Storage.mdf; Integrated Security = True");
                con.Open();
                int orderID = deliverynote.orderid;
                int foremanID = deliverynote.foremanid;
                bool success = deliverynote.success;
                SqlCommand cmd = new SqlCommand
                    (
                    "INSERT INTO DeliveryNotes" +
                    "(OrderID, ForemanID, Success," + "VALUES(" + orderID + "," + foremanID + "," + success + ",)"
                    );
                    /*"DateOut, PalletQuantity, Cooled, Confirmed," +
                    " Terminal, OrderTime, Comment)" + "VALUES(" + customerID + "," + dispatcherID + ",'" + dateIn + "','" + dateOut + "'," + quantity + "," + cooled + "," + confirmed + "," + terminal + ",'" + orderTime + "','" + comment + "')"
                    );*/
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

            }
            catch (Exception e)
            {
                if (e.GetHashCode() == 43527150)
                {
                    Console.WriteLine("There is a delivery note with this ID.");
                    Console.WriteLine(e.Message);
                }
                else
                    Console.WriteLine(e.Message);
            }
        }
    }
}
