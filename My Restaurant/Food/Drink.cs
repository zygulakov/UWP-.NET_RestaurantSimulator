using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Restaurant.Food
{
    abstract class Drink : MenuItem
    {
        public abstract void Obtain();
        public abstract void Request();
        public abstract void Serve();

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}
