using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.My_Restaurant.Food;

namespace App.My_Restaurant.Table
{
    //TODO: This class needs refactoring. Dictionary also implemented IEnumerable. If inherit this class from Dictionary<string, List<IMenuItem>> instead of IEnumerable<IMenuItem> it should be small class.***
    class TableRequests : Dictionary<string, List<IMenuItem>>
    {
        private readonly int MAX_CUSTOMER_QUANTITY;
        private int orderCount;
        public TableRequests(int maxCustomerQuantity)
        {
            MAX_CUSTOMER_QUANTITY = maxCustomerQuantity;
        }
        //TODO: You should not seperate methods for food and drink. Let's create just Add<T> method for IMenuItem type***
        public void Add<T>(int amountOfOrder, string nameOfCustomer)
        {
            if (orderCount > MAX_CUSTOMER_QUANTITY)
                throw new ArgumentOutOfRangeException($"reached max amount of orders {MAX_CUSTOMER_QUANTITY}");
            if (nameOfCustomer == null)
                throw new ArgumentNullException("item cant be null");
            if (amountOfOrder < 0)
                throw new ArgumentOutOfRangeException("amount of MenuItem cannot be smaller then 0");

            IMenuItem item = getObjectOfType(typeof(T), amountOfOrder);
            addToOrderList(item, nameOfCustomer);
            orderCount = this.Count;
        }

        public List<IMenuItem> Get<T>()
        {
            IEnumerable<IMenuItem> sameOrders = from orders in this.Values
                                                from order in orders
                                                where (order is T)
                                                select order;
            return sameOrders.ToList();
        }
        private void addToOrderList(IMenuItem newOrder, string name)
        {
            if (ContainsKey(name))
            {
                List<IMenuItem> customerOrders = this[name];
                IMenuItem order = customerOrders.Find((o) => o.GetType() == newOrder.GetType());
                if (order != null)
                    order.Quantity += newOrder.Quantity;
                else
                    customerOrders.Add(newOrder);
            }
            else
                this.Add(name, new List<IMenuItem>() { newOrder });
        }
        private IMenuItem getObjectOfType(Type t, int quantity)
        {
            if (t == typeof(Egg))
                return new Egg(quantity);
            if (t == typeof(Chicken))
                return new Chicken(quantity);

            if (t == typeof(CocaCola))
                return new CocaCola(quantity);
            if (t == typeof(Pepsi))
                return new Pepsi(quantity);
            if (t == typeof(Tea))
                return new Tea(quantity);
            if (t == typeof(NoDrink))
                return new NoDrink();

            throw new ArgumentException("No such type found");
        }

    }
}

