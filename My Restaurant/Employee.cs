using System;
namespace My_Restaurant
{
    class Employee
    {
        private static int numberOfInstances = 0;
        private static  bool isItNewReq;
        private object lastOrder;
        private object currentOrder;
        public Employee()
        {
            numberOfInstances++;
        }
        public object NewRequest(int quantity,object menuItem)
        {
            //for each request one food preperation
            isItNewReq = true;
            //wrong order of chicken
            switch (menuItem)
            {
                case ChickenOrder chickenOrder:
                    lastOrder = currentOrder;
                    //wrong order
                    if(numberOfInstances%3==0)
                    {
                        EggOrder wrongEggOrder = new EggOrder(quantity);
                        currentOrder = wrongEggOrder;
                    }else
                    {
                        currentOrder = chickenOrder;
                    }
                    return currentOrder;
                case EggOrder eggOrder:
                     lastOrder = currentOrder;
                    //wrong order
                    if (numberOfInstances % 3 == 0)
                    {
                        ChickenOrder wrongChickenOrder = new ChickenOrder(quantity);
                        currentOrder = wrongChickenOrder;
                    }
                    else
                    {
                        currentOrder = eggOrder;
                    }
                    return currentOrder;
                default:
                    throw new Exception("wrong menuItem");
                    
            }
            
            
 
        }
        public object CopyRequest()
        {
            if (lastOrder == null)
                throw new Exception("there is no last order to copy");
            isItNewReq = true;
            return lastOrder;
        }
        public string Inspect(object menuItem)
        {
            if(menuItem is EggOrder eggOrder)
            {
                return eggOrder.GetQuality()+"";
            }
            return "no inspection is required";

        }
        public string PrepareFood(object menuItem)
        {
            //checking if prepareFood called twice in a row
            if (!isItNewReq)
                throw new Exception("no Order to prepare!");
            isItNewReq = false;
            //for chicken
            if(menuItem is ChickenOrder chickenOrder)
            {
                for(int i = 0; i<chickenOrder.GetQuantity(); i++)
                {
                    chickenOrder.CutUp();
                }
                chickenOrder.Cook();
                return "Cooking has been completed!";
            }
            //for egg
            EggOrder eggOrder = (EggOrder)menuItem;
            int numOfRottenEggs = 0;
            for(int i = 0; i < eggOrder.GetQuantity(); i++)
            {
                try
                {
                eggOrder.Crack();

                }catch(Exception)
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
