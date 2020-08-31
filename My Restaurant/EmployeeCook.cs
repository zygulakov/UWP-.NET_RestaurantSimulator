using System;
namespace My_Restaurant
{
    class EmployeeCook
    {
        private static bool isItNewReq;
        private Order order;
        public Order NewRequest(int quantity, MenuItem menuItem)
        {
            if (menuItem != MenuItem.Egg && menuItem != MenuItem.Chicken)
                throw new ArgumentException("No such MenuItem");
            isItNewReq = true;

            ChickenOrder chickenOrder = new ChickenOrder(quantity);
            EggOrder eggOrder = new EggOrder(quantity);

            if (menuItem == MenuItem.Egg)
                order = eggOrder;
            else
                order = chickenOrder;
            return order;
        }
        public string PrepareFood(Order menuItem)
        {
            //checking if prepareFood called twice in a row
            if (!isItNewReq)
                throw new Exception("no Order to prepare!");

            // after food prepared , there must be new request
            isItNewReq = false;
            //for chicken
            if (menuItem is ChickenOrder chickenOrder)
            {
                for (int i = 0; i < chickenOrder.GetQuantity(); i++)
                {
                    chickenOrder.CutUp();
                }
                chickenOrder.Cook();
                return "Cooking has been completed!";
            }
            //for egg
            EggOrder eggOrder = (EggOrder)menuItem;
            for (int i = 0; i < eggOrder.GetQuantity(); i++)
            {
                    eggOrder.Crack();
                    eggOrder.DiscardShell();
            }
            eggOrder.Cook();
            return "Cooking has been comleted!";
        }

    }
}
