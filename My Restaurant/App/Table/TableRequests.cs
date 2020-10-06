using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.My_Restaurant.Food;

namespace App.My_Restaurant.Table
{
    class TableRequests : Dictionary<string, List<IMenuItem>>
    {
        private static int _tableNumber;
        public int TableNumber { get; private set; }
        public TableRequests()
        {
            //simulating 8 tables;
            if (_tableNumber > 8)
                _tableNumber = 0;
            TableNumber = ++_tableNumber;
        }

        public void Add<T>(int amountOfOrder, string nameOfCustomer)
        {
             if (nameOfCustomer == null)
                throw new ArgumentNullException("item cant be null");
            if (amountOfOrder < 0)
                throw new ArgumentOutOfRangeException("amount of MenuItem cannot be smaller then 0");

            IMenuItem item = getObjectOfType(typeof(T), amountOfOrder);
            addToOrderList(item, nameOfCustomer);
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
            //TODO: Can you test using Activator.CreateInstance(...) to create object? So, it should replace all the 'if' conditions***
            
            return (IMenuItem)Activator.CreateInstance(t, quantity);
        }

    }
}

