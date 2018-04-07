
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Communication
{
    [Serializable]
    public class Order
    {
        
        public static int NextID = 0;
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public int PalletQuantity { get; set; }
        public bool Cooled { get; set; }
        public bool Confirmed { get; set; }
        public int Terminal { get; set; }
        public DateTime OrderTime { get; set; } //Mikor lett leadva a megrendelés
        public int DispatcherID { get; set; } //Ki dolgozta fel a megrendelést?

        public string Comment;
        public Order() { NextID++;ID = NextID; }
        public Order(Order previousOrder)
        {
            CustomerID = previousOrder.CustomerID;
            ID = previousOrder.ID;
            DateIn = previousOrder.DateIn;
            DateOut = previousOrder.DateOut;
            PalletQuantity = previousOrder.PalletQuantity;
            Cooled = previousOrder.Cooled;
            Comment = previousOrder.Comment;
            OrderTime = previousOrder.OrderTime;
            DispatcherID = previousOrder.DispatcherID;
            Terminal = previousOrder.Terminal;
        }
        public Order(int customerID, string dateIn, string dateOut, int productQuantity, bool cooled, string comment = "")
        {
            CustomerID = customerID;
            NextID++;
            ID = NextID;
            DateIn = DateTime.Parse(dateIn);
            DateOut = DateTime.Parse(dateOut);
            PalletQuantity = productQuantity;
            Cooled = cooled;
            Comment = comment;
            OrderTime = DateTime.Now;
            DispatcherID = 0;
            Terminal = 0;
        }

        public void Print()
        {
            Console.WriteLine("ID: " + ID);
            Console.WriteLine("Tervezett beérkezés: " + DateIn.ToShortDateString());
            Console.WriteLine("Tervezett kivitel: " + DateOut.ToShortDateString());
            Console.WriteLine("Megrendelés leadva: " + OrderTime.ToString());
            Console.WriteLine("Mennyiség: " + PalletQuantity);
            if (Comment != "")
                Console.WriteLine("Megjegyzés: " + Comment);
            if (Cooled)
                Console.WriteLine("Hűtött: igen");
            else
                Console.WriteLine("Hűtött:nem");
            if (Confirmed)
                Console.WriteLine("Jóváhagyott: igen ");
            else
                Console.WriteLine("Jóváhagyott: nem ");

        }

        public override bool Equals(Object o)
        {
            try
            {
                Order order = (Order)o;
                if (order.ID == ID)
                {
                    return true;
                }
            }
            catch (Exception e) { }
            return false;
        }

        public override string ToString()
        {
            return ID.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}