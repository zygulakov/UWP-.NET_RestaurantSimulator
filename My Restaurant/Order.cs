using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Restaurant
{
    class Order
    {
        private int quantity;
        public Order(int quantity)
        {
            this.quantity = quantity;
        }
        public int GetQuantity()
        {
            return quantity;
        }
        public virtual void Cook()
        {
        }
    }
}
