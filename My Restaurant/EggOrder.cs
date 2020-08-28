using System;
namespace My_Restaurant
{
    class EggOrder
    {
        private static int numberOfInstances = 0;
        private int quantity;
        private int? quality;
        public EggOrder(int quantity)
        {
            if (numberOfInstances % 2 == 0)
                quality = null;
            
            Random randQuality = new Random();
            quality = randQuality.Next(101);

            this.quantity = quantity;

            numberOfInstances++;
        }
        public int? GetQuality()
        {
            return quality;
        }
        public int GetQuantity()
        {
            return quantity;
        }
        public void Crack()
        {
            if (GetQuality() < 25)
                throw new Exception("not comsumable ,quality below 25 ");
        }
        public void DiscardShell()
        {
        }
        public void Cook()
        {
        }
        public EggOrder Clone()
        {
            EggOrder cloneEggOrder = new EggOrder(quantity);
            cloneEggOrder.quality = quality;
            return cloneEggOrder;
        }
    }
}
