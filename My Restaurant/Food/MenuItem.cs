using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Restaurant.Food
{
    //TODO: It would be better if interface name starts from "I" letter according of naming conventions***
    interface IMenuItem
    {
        //TODO: Why we need Request() method? It was not in requirements and also it's not used.***
        //TODO: Please use Obtain and Serve methods. You can call them when cooking and serving processes orders.****
        IMenuItem Obtain();
        IMenuItem Serve();
    }
}
