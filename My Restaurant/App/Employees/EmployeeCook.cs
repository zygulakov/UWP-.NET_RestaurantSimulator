using App.My_Restaurant.Table;
using App.My_Restaurant.Food;
using System.Collections.Generic;

namespace App.My_Restaurant.Employees
{
    class EmployeeCook
    {
        public delegate void ProcessedDelegate();
        public event ProcessedDelegate Processed;

        public string Process(TableRequests tableRequests)
        {
            //TODO: orders count is always 0 here. Please fix this.
            List<IMenuItem> orders = tableRequests.GetOrdersByType<CookedFood>();
            foreach (IMenuItem order in orders)
            {
                ((CookedFood)order.Obtain()).Cook();
            }
            //fires processed event
            Processed.Invoke();
            return "Got the orders , starting to cook...";
        }

    }
}
