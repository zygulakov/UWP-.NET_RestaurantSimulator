using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Identity.Core;
using Windows.Security.Credentials.UI;
using Windows.UI.Xaml;

namespace My_Restaurant
{

    class EmployeeServer
    {
        private EmployeeCook employeeCook;
        private MenuItem[][] customersOrders;
        private int ordersCount;
        private const int MAX_ORDERS = 8;
        private bool isTherNewReq;
        public EmployeeServer()
        {
            employeeCook = new EmployeeCook();
            customersOrders = new MenuItem[MAX_ORDERS][];
        }
        public string RecieveRequest(int eggQuantity, int chickenQuantity, MenuItem drink)
        {
            isTherNewReq = true;

            if (ordersCount >= MAX_ORDERS)
            {
                throw new Exception($"Sorry i can remember only {MAX_ORDERS} orders");
            }

            //adding to the list. "+1" is for drink
            MenuItem[] order = new MenuItem[eggQuantity + chickenQuantity + 1];

            for (int i = 0; i < eggQuantity; i++)
                order[i] = MenuItem.Egg;

            //starting from last element 
            for (int i = eggQuantity; i < order.Length - 1; i++)
                order[i] = MenuItem.Chicken;

            //adding drink to the as last element of the array
            order[order.Length - 1] = drink;
            customersOrders[ordersCount] = order;
            ordersCount++;
            return $"Recieved from customer {ordersCount - 1} :  {eggQuantity} egg, {chickenQuantity} chicken and {drink}";
        }
        public string[] SendReqToCook()
        {
            if (!isTherNewReq)
                throw new Exception("NO REQUEST TO SEND TO COOK");
            string[] result = new string[ordersCount];

            for (int i = 0; i < ordersCount; i++)
            {
                int chickenCount = 0;
                int eggCount = 0;
                MenuItem[] items = customersOrders[i];

                //for each items in the each order except(drink)
                foreach (MenuItem item in items)
                {
                    if (item.Equals(MenuItem.Chicken) || item.Equals(MenuItem.Egg))
                    {
                        if (item.Equals(MenuItem.Egg))
                            eggCount++;
                        else
                            chickenCount++;
                    }
                }
                //egg
                Order preparedOrder = employeeCook.NewRequest(eggCount, MenuItem.Egg);
                employeeCook.PrepareFood(preparedOrder);
                //chicken
                preparedOrder = employeeCook.NewRequest(chickenCount, MenuItem.Chicken);
                employeeCook.PrepareFood(preparedOrder);

                result[i] = $"Customer {i} is served {chickenCount} {MenuItem.Chicken}," +
                    $" {eggCount} {MenuItem.Egg},{items[items.Length - 1]}";

            }
            //moving to other table
            ordersCount = 0;
            isTherNewReq = false;

            return result;
        }

    }
}
