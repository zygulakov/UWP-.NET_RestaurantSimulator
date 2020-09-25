using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.My_Restaurant.Food
{
    interface IMenuItem
    {
        int Quantity
        {
            get;
            set;
        }
        IMenuItem Obtain();
        IMenuItem Serve();
    }
}
