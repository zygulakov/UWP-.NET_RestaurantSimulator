
namespace My_Restaurant.Food
{
    sealed class Chicken : CookedFood
    {
        public Chicken(int quantity)
        {
            Quantitiy = quantity;
        }
        private void CutUp()
        {
        }
        public override void Cook()
        {
            for (int i = 0; i < Quantitiy; i++)
            {
                CutUp();
            }
        }

        public override void Request()
        {
        }

        public override void Obtain()
        {
        }

        public override void Serve()
        {
        }
    }
}
