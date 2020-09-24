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
            isTherNewReq = true;
            return $"Recieved from customer({ordersCount}) {customerName} :  {eggQuantity} egg, {chickenQuantity} chicken and {drink}";
        }

        public string NotifyToCook()
        {
            if (!isTherNewReq)
                throw new Exception("NO REQUEST TO SEND TO COOK");

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

            //TODO: Refactor this code to be smaller
            int TeaCount = tableOfRequests.GetOrdersByType<Tea>().Count;
            int PepsiCount = tableOfRequests.GetOrdersByType<Pepsi>().Count;
            int CocaColaCount = tableOfRequests.GetOrdersByType<CocaCola>().Count;

            if (TeaCount != 0)
                resultList.Add($"Served total {TeaCount} Tea");

            if (PepsiCount!= 0)
                resultList.Add($"Served total {PepsiCount} Pepsi");

            if (CocaColaCount != 0)
                resultList.Add($"Served total {CocaColaCount} CocaCola");

            foreach (string customerName in tableOfRequests.CustomerNameList)
            {
                List<IMenuItem> orders = tableOfRequests[customerName];

                StringBuilder result = new StringBuilder($"customer {customerName} is served: ");
                foreach (IMenuItem order in orders)
                    if (order is CookedFood)
                        result.Append(order + " ");
                resultList.Add(result.ToString());
            }

            anythingToServe = false;
            ordersCount = 0;
            tableOfRequests.Clear();
            return resultList;
        }
        public void CanServe()
        {
            anythingToServe = true;
        }

    }
}
