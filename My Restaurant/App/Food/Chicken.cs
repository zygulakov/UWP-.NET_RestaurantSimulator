
namespace App.My_Restaurant.Food
{
    sealed class Chicken : CookedFood
    {
        public Chicken(int quantity)
        {
            Quantity = quantity;
        }
        private void CutUp()
        {
        }
        public override void Cook()
        {
            for (int i = 0; i < Quantity; i++)
            {
                CutUp();
            }
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
