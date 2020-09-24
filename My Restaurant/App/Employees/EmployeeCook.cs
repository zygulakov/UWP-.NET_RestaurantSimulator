using App.My_Restaurant.Table;
using App.My_Restaurant.Food;
using System.Collections.Generic;

namespace App.My_Restaurant.Employees
{ 
    class EmployeeCook
    {
        public delegate void ProcessedDelegate();
        public event ProcessedDelegate ProcessedEvent;
        
        public string Process(TableRequests tableRequests)
        {   
                List<IMenuItem> orders = tableRequests.GetOrdersByType<CookedFood>();
                foreach (IMenuItem order in orders)
                {
                    ((CookedFood)order.Obtain()).Cook();
                }
            //fires processed event
            ProcessedEvent.Invoke();
            return "Got the orders , starting to cook...";
        }
        
    }
}
