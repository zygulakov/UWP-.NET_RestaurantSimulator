using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.My_Restaurant.Food;

namespace App.My_Restaurant.Table
{
    //TODO: This class needs refactoring. Dictionary also implemented IEnumerable. If inherit this class from Dictionary<string, List<IMenuItem>> instead of IEnumerable<IMenuItem> it should be small class.
    class TableRequests : IEnumerable<IMenuItem>
    {
        private Dictionary<string, List<IMenuItem>> items;
        private readonly int MAX_CUSTOMER_QUANTITY;
        private int orderCount;

        public TableRequests(int maxCustomerQuantity)
        {
            MAX_CUSTOMER_QUANTITY = maxCustomerQuantity;
            items = new Dictionary<string, List<IMenuItem>>();
        }

        //Properties
        public List<string> CustomerNameList
        {
            get => items.Keys.ToList<string>();
        }

        //TODO: You should not seperate methods for food and drink. Let's create just Add<T> method for IMenuItem type
        public void AddFood<T>(int amountOfOrder, string nameOfCustomer)
        {
            if (orderCount > MAX_CUSTOMER_QUANTITY)
                throw new ArgumentOutOfRangeException($"reached max amount of orders {MAX_CUSTOMER_QUANTITY}");
            if (nameOfCustomer == null)
                throw new ArgumentNullException("item cant be null");
            if (amountOfOrder < 0)
                throw new ArgumentOutOfRangeException("amount of MenuItem cannot be smaller then 0");


            IMenuItem item = getObjectOfType(typeof(T), amountOfOrder);
            addToOrderList(item, nameOfCustomer);

            orderCount = items.Count;
        }

        public void AddDrink(Drink drink, string nameOfCustomer)
        {
            addToOrderList(drink, nameOfCustomer);
        }

        public List<IMenuItem> Get<T>()
        {
            IEnumerable<IMenuItem> sameOrders = from orders in items.Values
                                                from order in orders
                                                where (order is T)
                                                select order;
            return sameOrders.ToList();
        }

        //indexers
        public List<IMenuItem> this[string name]
        {
            get
            {
                if (items.ContainsKey(name))
                    return items[name];
                else
                    throw new ArgumentException($"No such name \"{name}\" in list");

            }
        }
        public IEnumerator<IMenuItem> GetEnumerator()
        {
            IEnumerable<IMenuItem> drinks = from orders in items.Values
                                            from order in orders
                                            where order is Drink
                                            select order;
            foreach (IMenuItem drink in drinks)
                yield return drink;

            IEnumerable<IMenuItem> foods = from orders in items.Values
                                           from order in orders
                                           where order is CookedFood
                                           select order;
            foreach (IMenuItem food in foods)
                yield return food;
        }
        public void Clear() => items.Clear();

        private void addToOrderList(IMenuItem newOrder, string name)
        {
            if (items.ContainsKey(name))
            {
                List<IMenuItem> customerOrders = items[name];
                for (int i = 0; i < customerOrders.Count; i++)
                {
                    IMenuItem order = customerOrders[i];
                    if (order.GetType() == newOrder.GetType())
                    {
                        order.Quantity += newOrder.Quantity;
                        return;
                    }

                }
                customerOrders.Add(newOrder);

            }
            else
                items.Add(name, new List<IMenuItem>() { newOrder });
        }
        private IMenuItem getObjectOfType(Type t, int quantity)
        {
            if (t == typeof(Egg))
                return new Egg(quantity);
            else
            if (t == typeof(Chicken))
                return new Chicken(quantity);

            throw new ArgumentException("No such type found");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

