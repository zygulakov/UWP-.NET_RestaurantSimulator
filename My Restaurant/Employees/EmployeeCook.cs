using My_Restaurant.Food;
using System;

namespace My_Restaurant
{
    class EmployeeCook
    {
        public void Process(TableRequests tableRequests)
        {            
            IMenuItem[] orders = tableRequests[typeof(CookedFood)];
            foreach (IMenuItem order in orders)
            {               
                ((CookedFood)order.Obtain()).Cook();
            }
        }
    }
}
