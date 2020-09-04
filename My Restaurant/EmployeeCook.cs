using System;

namespace My_Restaurant
{
    class EmployeeCook
    {
        //TODO: SubmitRequest method should not return order. Its return type should be void. *
        private Order order;
        private int? eggQuality;
        private bool isNewOrder;
        public void SubmitRequest(int quantity, MenuItemMainCourse menuItem)
        {
            if (menuItem != MenuItemMainCourse.Egg && menuItem != MenuItemMainCourse.Chicken)
                throw new ArgumentException("No such MenuItem yet");

            if (menuItem == MenuItemMainCourse.Egg)
            {                
                order = new EggOrder(quantity);
                eggQuality = ((EggOrder)order).GetQuality();
            }
            else
                order = new ChickenOrder(quantity);
            isNewOrder = true;
        }

        //TODO: Please use this.order as a order to prepare, even you can remove parameters from this method *
        public string PrepareFood()
        {
            if (!isNewOrder)
                throw new Exception("no order to cook");
            isNewOrder = false;
            order.Cook();
            return $"{order.GetQuantity()} {order.ToString()}";
        }
        public int? GetEggQuality()
        {
            return eggQuality;

        }
    }
}
