using System;
namespace My_Restaurant
{
    //TODO: Please use private, protected for some methods
    class EggOrder : Order
    {
        private int? quality;
        public EggOrder(int quantity) : base(quantity)
        {
            Random randQuality = new Random();
            quality = randQuality.Next(101);
        }
        //TODO: You should use this GetQuality method
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
