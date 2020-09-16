using My_Restaurant.Food;
using System;

namespace My_Restaurant
{
    class EmployeeCook
    {
        private int? eggQuality;
        public void Process(TableRequests tableRequests)
        {
            //TODO: You can make the method as sample by using typeof(CookedFood) and only one foreach. You can create static property for egg quality.
            //Chicken
            MenuItem[] orders = tableRequests[typeof(Chicken)];
            foreach (MenuItem order in orders)
            {
                Chicken chicken = (Chicken)order;
                chicken.Cook();
            }
            //Egg
            orders = tableRequests[typeof(Egg)];
            foreach (MenuItem order in orders)
            {
                using (Egg egg = (Egg) order)
                {
                    egg.Cook();
                    eggQuality = egg.GetQuality;
                }
            }
        }

        //TODO: This method is not used. It should be used or removed.
        public int? GetEggQuality()
        {
            return eggQuality;
        }
    }
}
