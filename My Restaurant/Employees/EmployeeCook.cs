using My_Restaurant.Food;
using System;

namespace My_Restaurant
{
    class EmployeeCook
    {
        public static int? EggQuality
        {
            get;
            private set;
        }
        public void Process(TableRequests tableRequests)
        {
            //TODO: You can make the method as sample by using typeof(CookedFood) and only one foreach. You can create static property for egg quality.
            //Chicken*******
            IMenuItem[] orders = tableRequests[typeof(CookedFood)];
            foreach (IMenuItem order in orders)
            {
                if (order is Egg egg)
                    EggQuality = egg.Quality;
                ((CookedFood)order.Obtain()).Cook();
            }
        }

        //TODO: This method is not used. It should be used or removed.******

    }
}
