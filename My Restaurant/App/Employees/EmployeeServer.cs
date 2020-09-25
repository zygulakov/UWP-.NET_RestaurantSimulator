using App.My_Restaurant.Food;
using App.My_Restaurant.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.My_Restaurant.Employees
{

    class EmployeeServer
    {
        private int ordersCount;
        private readonly int MAX_ORDERS;
        private bool isTherNewReq, anythingToServe;
        private TableRequests tableOfRequests;
        public delegate string ReadyDelegate(TableRequests tableOfReq);
        public event ReadyDelegate Ready;
        public EmployeeServer()
        {
            MAX_ORDERS = 8;
            tableOfRequests = new TableRequests(MAX_ORDERS);
        }

        public string RecieveRequest(int eggQuantity, int chickenQuantity, Drink drink, string customerName)
        {

            if (ordersCount >= MAX_ORDERS)
                throw new Exception($"Sorry i can remember only {MAX_ORDERS} orders");
            

            if (eggQuantity < 0 || chickenQuantity < 0)
                throw new ArgumentException("order cant be smaller then 0");


            
            tableOfRequests.AddFood<Egg>(eggQuantity, customerName);

            tableOfRequests.AddFood<Chicken>(chickenQuantity, customerName);
            
            tableOfRequests.AddDrink(drink, customerName);

            ordersCount = tableOfRequests.CustomerNameList.Count;
            anythingToServe = true;
            isTherNewReq = true;
            return $"Recieved from customer({ordersCount}) {customerName} :  {eggQuantity} egg, {chickenQuantity} chicken and {drink}";
        }

        public string NotifyToCook()
        {
            if (!isTherNewReq)
                throw new Exception("No request to send to Chef");

            isTherNewReq = false;
            // if there is Cooker then cook and return result;
            if (Ready != null)
                return Ready(tableOfRequests);
            else
                throw new Exception("No chef to cook");
        }

        public List<string> Serve()
        {
            List<string> resultList = new List<string>();
            if (!anythingToServe)
                throw new Exception("Nothing to Serve");

            //TableRequests enumeration serves drinks first 
            //drinks added to resulList to show in dialog box(serving first*)
            foreach (IMenuItem order in tableOfRequests)
                order.Serve();

            //TODO: Refactor this code to be smaller ***
              
            foreach (string customerName in tableOfRequests.CustomerNameList)
            {
                List<IMenuItem> orders = tableOfRequests[customerName];
                string result = printOrders(orders);
                resultList.Add($"customer {customerName} served: "+result);
            }

            anythingToServe = false;
            ordersCount = 0;
            tableOfRequests.Clear();
            return resultList;
        }
        private string printOrders(List<IMenuItem> orders)
        {
            StringBuilder result = new StringBuilder();

            IMenuItem egg = orders.Find(o => o is Egg);
            IMenuItem chicken = orders.Find(o => o is Chicken);

            List<IMenuItem> Drinks = orders.FindAll(o => o is Drink).FindAll(o=>!(o is NoDrink));
            
            result.Append($"{egg.Quantity} {egg} {chicken.Quantity} {chicken} ");
            foreach(IMenuItem drink in Drinks)
                result.Append($"{drink.Quantity} {drink} ");
            
            return result.ToString();
        }
        

    }
}
