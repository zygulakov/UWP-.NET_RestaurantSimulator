using App.My_Restaurant.Table;
using App.My_Restaurant.Food;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace App.My_Restaurant.Employees
{
    class EmployeeCook
    {
        public static SemaphoreSlim semSlim = new SemaphoreSlim(1);
        //async method
        public bool IsAvailable { get; private set; } = true;
        public async Task<string> ProcessAsync(TableRequests tableRequests)
        {
            IsAvailable = false;
            await Task.Run(() =>
            {
                semSlim.WaitAsync();
                List<IMenuItem> orders = tableRequests.Get<CookedFood>();
                //TODO: Can we use Paraller.Foreach(...) here? So it will be done faster.***
                Parallel.ForEach(orders, (order) =>
                {
                    if (order is Egg egg)
                        using (egg)
                            ((CookedFood)egg.Obtain()).Cook();
                    else
                        ((CookedFood)order.Obtain()).Cook();
                });
                Thread.Sleep(2000);

                semSlim.Release();
            });
            IsAvailable = true;
            return "Cooked!";
        }

    }
}
