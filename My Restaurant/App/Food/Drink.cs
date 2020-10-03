using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.My_Restaurant.Food
{
    abstract class Drink : IMenuItem
    {
        public int Quantity
        {
            get;
            set;
        }
        public virtual IMenuItem Obtain() { return this; }
        public virtual IMenuItem Serve() { return this; }
        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}
