using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundry_System
{
    public class Order
    {
        public int ID { get; set; }
        public int Costumer_ID { get; set; }
        public int Clothes { get; set; }
        public int total { get; set; }
        public int paid { get; set; }
    }
}
