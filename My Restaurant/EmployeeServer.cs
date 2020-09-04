using System;

namespace My_Restaurant
{

    class EmployeeServer
    {
        //array of menuItemMainCourse and MenuItemBeverage array
        private object[][] customersOrders;
        private string[] resultOfOrdersToServe;

        private int ordersCount;
        private const int MAX_ORDERS = 8;
        private bool isTherNewReq, anythingToServe;
        public EmployeeServer()
        {
            customersOrders = new object[MAX_ORDERS][];
            resultOfOrdersToServe = new string[MAX_ORDERS];
        }

        public string RecieveRequest(int eggQuantity, int chickenQuantity, MenuItemBeverage drink)
        {
            isTherNewReq = true;

            if (ordersCount >= MAX_ORDERS)
            {
                throw new Exception($"Sorry i can remember only {MAX_ORDERS} orders");
            }

            //array of menuItemMainCourse and +1 MenuItemBeverage array
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
        public string SendReqToCook(EmployeeCook employeeCook)
        {
            if (!isTherNewReq)
                throw new Exception("NO REQUEST TO SEND TO COOK");

            //TODO: You should call SubmitRequest method only one time for Egg and one for Chicken. Call PrepareFood method right after each SubmitRequest method*
            for (int i = 0; i < ordersCount; i++)
            {
                int chickenCount = 0;
                int eggCount = 0;
                string drinkResult = null;
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
                        drinkResult = prepareDrink((MenuItemBeverage)order[j]).ToString();
                }
                //egg
                employeeCook.SubmitRequest(eggCount, MenuItemMainCourse.Egg);
                string eggResult = employeeCook.PrepareFood();
                //chicken
                employeeCook.SubmitRequest(chickenCount, MenuItemMainCourse.Chicken);
                string chickenResult = employeeCook.PrepareFood();

                string orderResult = $"Customer {i+1} is server: {eggResult} , {chickenResult} and {drinkResult}";
                resultOfOrdersToServe[i] = orderResult;
            }
            isTherNewReq = false;
            anythingToServe = true;
            return "Cooking! ...";
        }
        public string[] Serve()
        {
            if (!anythingToServe)
                throw new Exception("Nothing to Serve");
            anythingToServe = false;
            ordersCount = 0;
            return resultOfOrdersToServe;
        }
        private MenuItemBeverage prepareDrink(MenuItemBeverage beverage)
        {
            return beverage;
        }

    }
}
