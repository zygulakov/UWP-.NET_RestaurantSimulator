using My_Restaurant.Food;
using System;
using Windows.Storage;

namespace My_Restaurant
{
    class TableRequests
    {

        private IMenuItem[][] items;
        private readonly int MAX_CUSTOMER_QUANTITY;

        public TableRequests(int maxCustomerQuantity)
        {
            MAX_CUSTOMER_QUANTITY = maxCustomerQuantity;
            items = new IMenuItem[MAX_CUSTOMER_QUANTITY][];
        }

        public void Add(int customerNumber, IMenuItem item)
        {
            if (customerNumber > items.Length || customerNumber < 0)
                throw new ArgumentException($"customer with number : {customerNumber} is invalid");
            if (item == null)
                throw new ArgumentNullException("item cant be null");

            if (isArrayContainsCustomer(customerNumber))
            {
                IMenuItem[] orders = items[customerNumber];
                //resize array with extra 1 space for our item;
                Array.Resize(ref orders, orders.Length + 1);
                //adding item as last element;
                orders[orders.Length - 1] = item;
                //putting back resized array to customers number place
                items[customerNumber] = orders;
            }
            else
            {
                int lastIndex = getLastCustomerIndex(items);
                //adding new array of item after last element
                items[lastIndex + 1] = new IMenuItem[] { item };
            }
        }

        //indexers
        public IMenuItem[] this[Type type]
        {
            get
            {
                //new array with minimal amount
                IMenuItem[] sameItems = new IMenuItem[0];

                //adding all same types to the new array
                for (int i = 0; i < items.GetLength(0); i++)
                {
                    IMenuItem[] orders = items[i];
                    if (orders != null)
                    {
                        for (int j = 0; j < orders.Length; j++)
                        {
                            IMenuItem order = orders[j];

                            if (order.GetType() == type)
                            {
                                Array.Resize(ref sameItems, sameItems.Length + 1);
                                sameItems[sameItems.Length-1] = order;
                            }

                        }
                    }

                }
                return sameItems;
            }
        }

        public IMenuItem[] this[int customer]
        {
            get
            {
                if (customer >7 || customer < 0)
                    throw new ArgumentException("invalid customer number");
                return items[customer];
            }
        }

        private bool isArrayContainsCustomer(int customerNumber)
        {
            if (customerNumber >= MAX_CUSTOMER_QUANTITY)
                throw new ArgumentException("too much customer for the specified amount");
            return items[customerNumber] != null;
        }

        //returns last index where element is not null for array arg
        private int getLastCustomerIndex(IMenuItem[][] items)
        {
            int lastIndex = -1;
            for (int i = 0; i < items.GetLength(0); i++)
            {
                IMenuItem[] orders = items[i];
                if (orders != null)
                    lastIndex = i;
            }

            return lastIndex;
        }

    }
}

