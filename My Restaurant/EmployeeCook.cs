using System;
namespace My_Restaurant
{
    class EmployeeCook
    {
        //TODO: SubmitRequest method should not return order. Its return type should be void. 
        private Order order;
        public Order SubmitRequest(int quantity, MenuItemMainCourse menuItem)
        {
            if (menuItem != MenuItemMainCourse.Egg && menuItem != MenuItemMainCourse.Chicken)
                throw new ArgumentException("No such MenuItem yet");

            if (menuItem == MenuItemMainCourse.Egg)
                order = new EggOrder(quantity);
            else
                order = new ChickenOrder(quantity);
            return order;
        }

        //TODO: Please use this.order as a order to prepare, even you can remove parameters from this method
        public string PrepareFood(params Order[] orders)
        {
            foreach (Order order in orders)
                order.Cook();

            return "Cooking has been comleted!";
        }

    }
}
