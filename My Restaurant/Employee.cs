using System;
namespace My_Restaurant
{
    class Employee
    {
        private static int numberOfRequests = 0;
        private static bool isItNewReq;
        private object order;
        public object NewRequest(int quantity, string menuItem)
        {
            //for each new request
            numberOfRequests++;
            //for each request one food preperation
            isItNewReq = true;

            ChickenOrder chickenOrder = new ChickenOrder(quantity);
            EggOrder eggOrder = new EggOrder(quantity);

            if (menuItem == "Egg")
            {
                //for the wrong order
                if (numberOfRequests % 3 == 0)
                    order = chickenOrder;
                else
                    order = eggOrder;

            }
            else
            {
                if (numberOfRequests % 3 == 0)
                    order = eggOrder;
                else
                    order = chickenOrder;

            }
            return order;

        }
        public object CopyRequest()
        {
            isItNewReq = true;
            if (order is EggOrder eggOrder)
                return eggOrder.Clone();
            return new ChickenOrder(((ChickenOrder)order).GetQuantity());
        }
        public string Inspect(object menuItem)
        {
            if (menuItem is EggOrder eggOrder)
            {
                return eggOrder.GetQuality() + "";
            }
            return "Its chicken ,no inspection is required";

        }
        public string PrepareFood(object menuItem)
        {
            //checking if prepareFood called twice in a row
            if (!isItNewReq)
                throw new Exception("no Order to prepare!");
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
            int numOfRottenEggs = 0;
            for (int i = 0; i < eggOrder.GetQuantity(); i++)
            {
                try
                {
                    eggOrder.Crack();

                }
                catch (Exception)
                {
                    numOfRottenEggs++;

                }
                finally
                {
                    eggOrder.DiscardShell();
                }

            }
            eggOrder.Cook();
            return "Cooking has been comleted!.Number of rotten eggs: " + numOfRottenEggs;
        }

    }
}
