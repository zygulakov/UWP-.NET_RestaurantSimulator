using My_Restaurant.Food;
using System;

namespace My_Restaurant
{

    class EmployeeServer
    {
        private int ordersCount;
        private const int MAX_ORDERS = 8;
        private bool isTherNewReq, anythingToServe;
        private TableRequests tableOfRequests;
        private int? eggQuality;
        public EmployeeServer()
        {
            tableOfRequests = new TableRequests(MAX_ORDERS);
        }

        public string RecieveRequest(int eggQuantity, int chickenQuantity, Drink drink)
        {

            if (ordersCount >= MAX_ORDERS)
                throw new Exception($"Sorry i can remember only {MAX_ORDERS} orders");

            if (eggQuantity < 0 || chickenQuantity < 0)
                throw new ArgumentException("order cant be smaller then 0");

            isTherNewReq = true;
            Egg egg = new Egg(eggQuantity);
            tableOfRequests.Add(ordersCount, egg);
            eggQuality = egg.Quality;


            Chicken chicken = new Chicken(chickenQuantity);
            tableOfRequests.Add(ordersCount, chicken);

            tableOfRequests.Add(ordersCount, drink);

            ordersCount++;
            return $"Recieved from customer {ordersCount} :  {eggQuantity} egg, {chickenQuantity} chicken and {drink}";
        }

        public string SendReqToCook(EmployeeCook employeeCook)
        {
            if (!isTherNewReq)
                throw new Exception("NO REQUEST TO SEND TO COOK");

            employeeCook.Process(tableOfRequests);
            isTherNewReq = false;
            anythingToServe = true;
            return "Cooking! ...";
        }

        public string[] Serve()
        {
            string[] resultOfServing = new string[ordersCount];
            if (!anythingToServe)
                throw new Exception("Nothing to Serve");

            for (int i = 0; i < ordersCount; i++)
            {
                IMenuItem[] orders = tableOfRequests[i];

                Chicken chicken = null;
                Egg egg = null;
                Drink drink = null;
                foreach (IMenuItem order in orders)
                {
                    if (order is Chicken)
                        chicken = (Chicken)order.Serve();
                    if (order is Egg)
                        egg = (Egg)order.Serve();
                    if (order is Drink)
                        drink = (Drink)order.Serve();
                }

                resultOfServing[i] = $"customer {i} is served: {chicken.Quantitiy} chicken, {egg.Quantitiy} egg, {drink}";

            }
            anythingToServe = false;
            ordersCount = 0;
            return resultOfServing;
        }

        public int? GetEggQuality()
        {
            return eggQuality;
        }
    }
}
