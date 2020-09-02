
namespace My_Restaurant
{
    class ChickenOrder : Order
    {
        public ChickenOrder(int quantity) : base(quantity)
        {
        }
        private void CutUp()
        {
        }
        public override void Cook()
        {
            for (int i = 0; i < GetQuantity(); i++)
            {
                CutUp();
            }
        }
        public override string ToString()
        {
            return MenuItemMainCourse.Chicken.ToString();
        }
    }
}
