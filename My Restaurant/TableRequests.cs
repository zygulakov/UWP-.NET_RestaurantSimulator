using My_Restaurant.Food;
using System;
using Windows.Storage;

namespace My_Restaurant
{
    class TableRequests
    {

        private MenuItem[][] items;
        private readonly int MAX_CUSTOMER_QUANTITY;

        public TableRequests(int maxCustomerQuantity)
        {
            MAX_CUSTOMER_QUANTITY = maxCustomerQuantity;
            items = new MenuItem[MAX_CUSTOMER_QUANTITY][];
        }
        public void Add(int customerNumber, MenuItem item)
        {
            if (customerNumber > items.Length || customerNumber < 0)
                throw new ArgumentException($"customer with number : {customerNumber} is invalid");
            if (item == null)
                throw new ArgumentNullException("item cant be null");

            if (isArrayContainsCustomer(customerNumber))
            {
                MenuItem[] oldOrders = items[customerNumber];
                //new array with extra 1 space for our item;
                MenuItem[] newOrders = copyArrayToNewCustomSizeArray(oldOrders, oldOrders.Length + 1);
                //adding item as last element;
                newOrders[newOrders.Length - 1] = item;
                //putting back newOrders array to customers number place
                items[customerNumber] = newOrders;


            }
            else
            {
                int lastIndex = getLastCustomerIndex(items);
                //adding new array of item after last element
                items[lastIndex + 1] = new MenuItem[] { item };
            }



        }
        //indexers
        public MenuItem[] this[Type type]
        {
            get
            {
                int numberOfSameType = 0;
                // counting number of same types
                for (int i = 0; i < items.GetLength(0); i++)
                {
                    MenuItem[] orders = items[i];
                    if(orders !=null)
                    {
                        for (int j = 0; j < orders.Length; j++)
                        {
                            MenuItem order = orders[j];
                            if(order != null)
                            {
                                if (order.GetType() == type)
                                    numberOfSameType++;
                            }
                        }
                    }

                }

                //new array
                MenuItem[] sameItems = new MenuItem[numberOfSameType];

                //adding all same types to the new array
                for (int i = 0; i < items.GetLength(0); i++)
                {
                    MenuItem[] orders = items[i];
                    if (orders != null)
                    {
                        for (int j = 0; j < orders.Length; j++)
                        {
                            MenuItem order = orders[j];
                            if (order != null)
                            {
                                if (order.GetType() == type)
                                    sameItems[--numberOfSameType] = order;
                            }
                        }
                    }

                }
                return sameItems;
            }

        }

        public MenuItem[] this[int customer]
        {
            get
            {
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
        private int getLastCustomerIndex(MenuItem[][] items)
        {
            int lastIndex = -1;
            for (int i = 0; i < items.GetLength(0); i++)
            {
                MenuItem[] orders = items[i];
                if (orders != null)
                    lastIndex = i;
            }

            return lastIndex;
        }
        private MenuItem[] copyArrayToNewCustomSizeArray(MenuItem[] arrayToCopy, int newArraySize)
        {
            MenuItem[] newArray = new MenuItem[newArraySize];
            for (int i = 0; i < arrayToCopy.Length; i++)
            {
                newArray[i] = arrayToCopy[i];
            }
            return newArray;
        }

    }
}

