using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    [Serializable]
    public class OrderConatiner
    {
        public List<Order> orders { get; set; }
        public OrderConatiner(List<Order>orders)
        {
            this.orders = orders;
        }
        public void AddOrder(Order o)
        {
            orders.Add(o);
        }

    }
}
