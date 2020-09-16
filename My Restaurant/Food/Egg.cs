using System;
namespace My_Restaurant.Food
{
    sealed class Egg : CookedFood ,IDisposable
    {
        private int? quality;
        public Egg(int quantity)
        {
            Random randQuality = new Random();
            quality = randQuality.Next(101);
            Quantitiy = quantity;
            
        }
        public int? Quality
        {
            get => quality;
        }
        private void Crack()
        {
        }
        private void DiscardShell()
        {
        }
        public override void Cook()
        {
            for (int i = 0; i < Quantitiy; i++)
            {
                Crack();
            }
            
        }

        public void Dispose()
        {
            DiscardShell();
        }

        public override IMenuItem Obtain()
        {
            return this;
        }

        public override IMenuItem Serve()
        {
            return this;
        }
    }
}
