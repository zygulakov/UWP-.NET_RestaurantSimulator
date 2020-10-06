using App.My_Restaurant.Food;
using App.My_Restaurant.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.My_Restaurant.Employees
{
    class EmployeeServer
    {
        private int ordersCount;
        private readonly int MAX_ORDERS;
        public EmployeeServer()
        {
            MAX_ORDERS = 8;
            tableOfRequests = new TableRequests(MAX_ORDERS);
        }
        public TableRequests tableOfRequests { get; private set; }
        public string RecieveRequest(int eggQuantity, int chickenQuantity, string drink, string customerName)
        {
            if (ordersCount >= MAX_ORDERS)
                throw new Exception($"Sorry i can remember only {MAX_ORDERS} orders");
            if (eggQuantity < 0 || chickenQuantity < 0)
                throw new ArgumentException("order cant be smaller then 0");

            tableOfRequests.Add<Egg>(eggQuantity, customerName);
            tableOfRequests.Add<Chicken>(chickenQuantity, customerName);

            switch (drink)
            {
                case "CocaCola":
                    tableOfRequests.Add<CocaCola>(1, customerName);
                    break;
                case "Pepsi":
                    tableOfRequests.Add<Pepsi>(1, customerName);
                    break;
                case "Tea":
                    tableOfRequests.Add<Tea>(1, customerName);
                    break;
                default:
                    tableOfRequests.Add<NoDrink>(1, customerName);
                    break;
            }
            ordersCount = tableOfRequests.Count;
            return $"Recieved from customer({ordersCount}) {customerName} :  {eggQuantity} egg, {chickenQuantity} chicken and {drink}";
        }

        //async method of Serve
        public async Task<List<string>> ServeAsync()
        {
            //TODO: You should use LINQ as much as possible. Please use linq instead of all 'foreach'***
            return await Task.Run(() =>
             {
                 lock (this)
                 {
                     if (ordersCount<=0)
                         throw new Exception("All Done!!!");

                     //sorted first drinks then food
                     IEnumerable<IMenuItem> drinksAndFood = from orders in tableOfRequests.Values
                                                            from order in orders
                                                            orderby order is Drink descending
                                                            select order;
                     drinksAndFood.ToList().ForEach(order => order.Serve());

                     //TODO: ResultList var will be removed when you replace this foreach with linq***
                     //ordered by name
                     List<string> servingResults = new List<string>();
                     tableOfRequests.ToList().OrderBy(i => i.Key).ToList().ForEach(orders =>
                     {
                         string name = orders.Key;
                         List<IMenuItem> customerOrders = orders.Value;
                         StringBuilder resultTemplate = new StringBuilder();
                         IMenuItem egg = customerOrders.Find(o => o is Egg);
                         IMenuItem chicken = customerOrders.Find(o => o is Chicken);
                         List<IMenuItem> customerDrinks = customerOrders.FindAll(o => o is Drink).FindAll(o => !(o is NoDrink));
                         resultTemplate.Append($"{name} is served: {egg.Quantity} {egg} {chicken.Quantity} {chicken} {string.Join(",", customerDrinks) }");
                         servingResults.Add(resultTemplate.ToString());
                     });

                     ordersCount = 0;
                     tableOfRequests.Clear();
                     Thread.Sleep(2000);
                     return servingResults;
                 }
             });

        }

        //TODO: this method will be removed(or merged with the previous method) when you replace 'foreach' with linq in ServeAsync method***

    }
}
