using System;
namespace My_Restaurant
{
    class EggOrder:Order
    {
        private int? quality;
        public EggOrder(int quantity) :base(quantity)
        {
            Random randQuality = new Random();
            quality = randQuality.Next(101);
        }
        public int? GetQuality()
        {
            return quality;
        }
        public void Crack()
        {
        }
        public void DiscardShell()
        {
        }
        public override void Cook()
        {
        }

    }
}
