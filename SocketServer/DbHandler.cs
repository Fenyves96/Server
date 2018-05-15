using Communication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    class DbHandler
    {
        public static string MateFenyvConnectionString= @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\Suli\egyetem\6. félév\Rendszerfejlesztés\MasodikIteracio\Server-master\SocketServer\DB_Storage.mdf; Integrated Security = True";
        //Majd ide írd be a saját connectionöd útvonalát
        public static string KovacsMateConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\kovac\OneDrive\Documents\GitHub\Server\SocketServer\DB_Storage.mdf; Integrated Security = True";

        
        public static void setConfirmedOrder(int id)
        {
            try
            {
                SqlConnection con = new SqlConnection(MateFenyvConnectionString);
                con.Open();

                SqlCommand cmd = new SqlCommand
                        (
                            "UPDATE Orders SET Confirmed=1 Where OrderId=" + id
                        );
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
            }
            catch (Exception e) { Console.Write(e.Message); }
        }
        //Order hozzáadása az adatbázishoz
        public static void addOrder(Order order)
        {
            try
            {
                SqlConnection con = new SqlConnection(MateFenyvConnectionString);
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
                int FirstOP = order.FirstOccupiedPlace;
                int LastOP = order.LastOccupiedPlace;
                SqlCommand cmd = new SqlCommand
                    (
                    "INSERT INTO Orders" +
                    "(CustomerID, DispatcherID, DateIn," +
                    "DateOut, PalletQuantity, Cooled, Confirmed," +
                    " Terminal, OrderTime, Comment,FirstOccupiedPlace,LastOccupiedPlace)" + "VALUES("+customerID+","+dispatcherID+",'"+dateIn+"','"+dateOut+"',"+quantity+","+cooled+","+confirmed+","+terminal+",'"+orderTime+"','"+comment+"',"+FirstOP+","+LastOP+")"
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
                    Console.WriteLine(e.StackTrace);
                }
                else
                    Console.WriteLine(e.Message);
            }
        }

        public static void setOrderTerminal(int id, int terminal)
        {
            try
            {
                SqlConnection con = new SqlConnection(MateFenyvConnectionString);
                con.Open();

                SqlCommand cmd = new SqlCommand
                        (
                            "UPDATE Orders SET Terminal="+terminal+" Where OrderId=" + id
                        );
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
            }
            catch (Exception e) { Console.Write(e.Message); }
        }

        public static List<Order> GetOrders()
        {
            try
            {
                List<Order> orders = new List<Order>();
                SqlConnection con = new SqlConnection(MateFenyvConnectionString);
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
                    bool confirmed = false;
                    string cooledstring = dt.Rows[i]["Cooled"].ToString();
                    int FirstOP = 0;
                    int LastOP = 0;
                    if (dt.Rows[i]["FirstOccupiedPlace"].ToString()!= "" && dt.Rows[i]["LastOccupiedPlace"].ToString() != "")
                    {
                        FirstOP = int.Parse(dt.Rows[i]["FirstOccupiedPlace"].ToString());
                        LastOP = int.Parse(dt.Rows[i]["LastOccupiedPlace"].ToString());
                    }
                   
                    if (cooledstring.Equals("True"))
                    {
                        cooled = true;
                    }
                    string confirmedstring = dt.Rows[i]["Confirmed"].ToString();
                    if (confirmedstring.Equals("True"))
                    {
                        confirmed = true;
                    }

                    Order o = new Order(ID, customerID, dateIn, dateOut, quantity, cooled, confirmed, comment);
                    o.Terminal = terminal;
                    if (FirstOP != null && LastOP != null)
                    {
                        o.FirstOccupiedPlace = FirstOP;
                        o.LastOccupiedPlace = LastOP;
                    }
                    else
                    {
                        o.FirstOccupiedPlace = 0;
                        o.LastOccupiedPlace = 0;
                    }
                    orders.Add(o);
                }
                return orders;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return null; }

            
        }

        public static void SetDeliveryNoteToSuccess(int id)
        {

            try
            {
                SqlConnection con = new SqlConnection(MateFenyvConnectionString);
                con.Open();

                SqlCommand cmd = new SqlCommand
                        (
                            "UPDATE DeliveryNotes SET Success=1 Where Id=" + id
                        );
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
            }
            catch (Exception e) { Console.Write(e.Message); }
        }

        public static void SetOrderOccupiedPlaces(int id, int FirstOP, int LastOP)
        {

            try
            {
                SqlConnection con = new SqlConnection(MateFenyvConnectionString);
                con.Open();

                SqlCommand cmd = new SqlCommand
                        (
                            "UPDATE Orders SET FirstOccupiedPlace="+FirstOP+", LastOccupiedPlace="+LastOP+" Where OrderId=" + id
                        );
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
            }
            catch (Exception e) { Console.Write(e.Message); }
        }

        //TODO:Ezt még át kell írni
        internal static List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            SqlConnection con = new SqlConnection(MateFenyvConnectionString);
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
            SqlConnection con = new SqlConnection(MateFenyvConnectionString);
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
                bool success = false;
                string succesString = dt.Rows[i]["Success"].ToString();;
                if (succesString.Equals("True"))
                {
                    success = true;
                }
                DeliveryNote dn = new DeliveryNote(ID, success, foremanid, orderid);
                deliverynotes.Add(dn);
            }

            return deliverynotes;
        }

        public static void addDeliveryNote(DeliveryNote deliverynote)
        {
            try
            {
                SqlConnection con = new SqlConnection(MateFenyvConnectionString);
                con.Open();
                int orderID = deliverynote.orderid;
                int foremanID = deliverynote.foremanid;
                bool success_bool = deliverynote.success;
                int success;
                if (success_bool)
                    success = 1;
                else
                    success = 0;
         
                SqlCommand cmd = new SqlCommand
                    (
                    "INSERT INTO DeliveryNotes" +
                    "(OrderID, ForemanID, Success) " + "VALUES(" + orderID + "," + foremanID + "," + success + ")"
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
