﻿using App.My_Restaurant.Table;
using App.My_Restaurant.Food;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace App.My_Restaurant.Employees
{
    class EmployeeCook
    {
        public static SemaphoreSlim semSlim = new SemaphoreSlim(1);
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
           
            return $"Cooked!";

        }
        //async method
        public async Task<string> ProcessAsync(TableRequests tableRequests)
        {
            await Task.Run(() =>
            {
                semSlim.WaitAsync();
                List<IMenuItem> orders = tableRequests.GetOrdersByType<CookedFood>();
                    foreach (IMenuItem order in orders)
                    {
                        if (order is Egg egg)
                            using (egg)
                                ((CookedFood)egg.Obtain()).Cook();
                        else
                            ((CookedFood)order.Obtain()).Cook();
                    }
                    Thread.Sleep(2000);
                
                semSlim.Release();
            });
            return "Cooked!";

        }

        
    }
}
