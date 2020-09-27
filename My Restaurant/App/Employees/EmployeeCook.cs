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
            List<IMenuItem> orders = tableRequests.GetOrdersByType<CookedFood>();
            foreach (IMenuItem order in orders)
            {
                if (order is Egg egg)
                    using (egg)
                        ((CookedFood)egg.Obtain()).Cook();
                else
                    ((CookedFood)order.Obtain()).Cook();
            }
            //fires processed event
            Processed.Invoke();
            return "Cooking ...\n cooked!";
            
        }

    }
}
