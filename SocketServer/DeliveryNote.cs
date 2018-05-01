using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    class DeliveryNote
    {
        private static int currentID;
        public bool success;
        public int foremanid;
        public int orderid;
        public int ID { get; set; }

        public DeliveryNote(int ID, bool success, int foremanid, int orderid)
        {
            ID = getNextID();
            this.success = false;
            this.foremanid = 1;
            this.orderid = 1;
        }

        protected int getNextID()
        {
            return ++currentID;
        }

        public void Print()
        {

        }

    }
}
