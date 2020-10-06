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
        private readonly int MAX_ORDERS;
        public EmployeeServer(int maxOrders)
        {
            MAX_ORDERS = maxOrders;
        }
        public TableRequests TableOfRequests
        {
            get;
            set;
        }
        public string RecieveRequest(int eggQuantity, int chickenQuantity, string drink, string customerName)
        {
            if (TableOfRequests.Count >= MAX_ORDERS)
                throw new Exception($"Sorry i can remember only {MAX_ORDERS} orders");
            if (eggQuantity < 0 || chickenQuantity < 0)
                throw new ArgumentException("order cant be smaller then 0");

            TableOfRequests.Add<Egg>(eggQuantity, customerName);
            TableOfRequests.Add<Chicken>(chickenQuantity, customerName);

            switch (drink)
            {
                case "CocaCola":
                    TableOfRequests.Add<CocaCola>(1, customerName);
                    break;
                case "Pepsi":
                    TableOfRequests.Add<Pepsi>(1, customerName);
                    break;
                case "Tea":
                    TableOfRequests.Add<Tea>(1, customerName);
                    break;
                default:
                    TableOfRequests.Add<NoDrink>(1, customerName);
                    break;
            }
            return $"Table: {TableOfRequests.TableNumber} Recieved from customer({TableOfRequests.Count}) {customerName} :  {eggQuantity} egg, {chickenQuantity} chicken and {drink}";
        }

        //async method of Serve
        public async Task<List<string>> ServeAsync(TableRequests tableOfRequests)
        {
            return await Task.Run(() =>
             {
                 lock (this)
                 {
                     //sorted first drinks then food
                     IEnumerable<IMenuItem> drinksAndFood = from orders in tableOfRequests.Values
                                                            from order in orders
                                                            orderby order is Drink descending
                                                            select order;
                     drinksAndFood.ToList().ForEach(order => order.Serve());

                     //ordered by name
                     List<string> servingResults = new List<string>();
                     servingResults.Add($"Served Table:{tableOfRequests.TableNumber}");
                     tableOfRequests.ToList().OrderBy(i => i.Key).ToList().ForEach(orders =>
                     {
                         string name = orders.Key;
                         List<IMenuItem> customerOrders = orders.Value;
                         StringBuilder resultTemplate = new StringBuilder();
                         IMenuItem egg = customerOrders.Find(o => o is Egg);
                         IMenuItem chicken = customerOrders.Find(o => o is Chicken);
                         List<IMenuItem> customerDrinks = customerOrders.FindAll(o => o is Drink).FindAll(o => !(o is NoDrink));
                         resultTemplate.Append($"{name} is served: {egg.Quantity} {egg} {chicken.Quantity} {chicken}");
                         customerDrinks.ForEach(drink => resultTemplate.Append($" { drink.Quantity} {drink}"));
                         servingResults.Add(resultTemplate.ToString());
                     });
                     Thread.Sleep(2000);
                     return servingResults;
                 }
             });

        }
    }
}
