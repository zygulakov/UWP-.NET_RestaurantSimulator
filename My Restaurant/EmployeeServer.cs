using System;

namespace My_Restaurant
{

    class EmployeeServer
    {
        private EmployeeCook employeeCook;

        private object[][] customersOrders;
        private Order[] ordersToServe;
        private MenuItemBeverage[] beverages;

        private int ordersCount;
        private const int MAX_ORDERS = 8;
        private bool isTherNewReq;
        public EmployeeServer()
        {
            employeeCook = new EmployeeCook();
            customersOrders = new object[MAX_ORDERS][];
            //for evert customer 2 menu item
            ordersToServe = new Order[MAX_ORDERS * 2];
            beverages = new MenuItemBeverage[MAX_ORDERS];
        }
        public string RecieveRequest(int eggQuantity, int chickenQuantity, MenuItemBeverage drink)
        {
            isTherNewReq = true;

            if (ordersCount >= MAX_ORDERS)
            {
                throw new Exception($"Sorry i can remember only {MAX_ORDERS} orders");
            }

            //array of order
            object[] order = new object[eggQuantity + chickenQuantity + 1];

            //adding eggs
            for (int i = 0; i < eggQuantity; i++)
            {
                order[i] = MenuItemMainCourse.Egg;
            }


            //adding chicken after the eggs till last element
            for (int i = eggQuantity; i < order.Length - 1; i++)
            {
                order[i] = MenuItemMainCourse.Chicken;
            }

            //last element is drink
            order[order.Length - 1] = drink;
            //adding order to the array of orders
            customersOrders[ordersCount] = order;
            ordersCount++;
            return $"Recieved from customer {ordersCount} :  {eggQuantity} egg, {chickenQuantity} chicken and {drink}";
        }
        public string SendReqToCook()
        {  //TODO: Create Serve() method in this class and move some code to that Serve method*
            if (!isTherNewReq)
                throw new Exception("NO REQUEST TO SEND TO COOK");

            for (int i = 0 ; i < ordersCount; i++)
            {
                int chickenCount = 0;
                int eggCount = 0;
                //getting each order from array of orders
                object[] order = customersOrders[i];

                //for each items in  order 
                for (int j = 0; j < order.Length; j++)
                {
                    //counting number of chicken and egg 
                    if (order[j] is MenuItemMainCourse item)
                    {
                        if (item.Equals(MenuItemMainCourse.Chicken))
                            chickenCount++;
                        if (item.Equals(MenuItemMainCourse.Egg))
                            eggCount++;
                    }
                    else
                        beverages[i] = prepareDrink((MenuItemBeverage)order[j]);
                }
                //egg
                //from 0 till number of orders
                ordersToServe[i] = employeeCook.SubmitRequest(eggCount, MenuItemMainCourse.Egg);
                //chicken
                //starting from where egg orders end
                ordersToServe[ordersCount+i] = employeeCook.SubmitRequest(chickenCount, MenuItemMainCourse.Chicken);




            }
            isTherNewReq = false;
            return "request has been sent! please wait!";
        }
        public string[] Serve()
        {
            string[] result = new string[ordersToServe.Length];

            for (int i = 0; i < ordersCount; i++)
            {
                Order egg = (Order)ordersToServe[i];
                Order chicken = (Order)ordersToServe[ordersCount+i];
                employeeCook.PrepareFood(egg);
                employeeCook.PrepareFood(chicken);

                result[i] = $"Customer {i + 1} is served: {egg.GetQuantity()} {egg} , " +
                    $"{chicken.GetQuantity()} {chicken} , {beverages[i]}";

            }
            //moving to other table
            ordersCount = 0;
            return result;
        }
        private MenuItemBeverage prepareDrink(MenuItemBeverage beverage)
        {
            return beverage;
        }

    }
}
