using My_Restaurant.Food;
using System;

namespace My_Restaurant
{
    class EmployeeCook
    {
        private int? eggQuality;
        public void Process(TableRequests tableRequests)
        {
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
        public int? GetEggQuality()
        {
            return eggQuality;

        }
    }
}
