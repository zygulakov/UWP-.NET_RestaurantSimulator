using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.My_Restaurant.Food
{
    interface IMenuItem
    { 
        IMenuItem Obtain();
        IMenuItem Serve();
    }
}
