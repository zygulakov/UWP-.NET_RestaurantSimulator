using System;
namespace My_Restaurant
{
    class EmployeeCook
    {
        private Order order;
        //TODO: If you rename it to SubmitRequest() it would be nice *
        public Order SubmitRequest(int quantity, MenuItemMainCourse menuItem)
        {
            if (menuItem != MenuItemMainCourse.Egg && menuItem != MenuItemMainCourse.Chicken)
                throw new ArgumentException("No such MenuItem yet");
            //TODO: You don't need to create both Chicken and Egg instances. It should be one of them. *

            if (menuItem == MenuItemMainCourse.Egg)
                order = new EggOrder(quantity);
            else
                order = new ChickenOrder(quantity);
            return order;
        }
        public string PrepareFood(params Order[] orders)
        {
            //TODO: You can call Cook() method only one time at the end. Also how we can refactor the method so that it will be simple?*

            foreach (Order order in orders)
                order.Cook();

            return "Cooking has been comleted!";
        }

    }
}
