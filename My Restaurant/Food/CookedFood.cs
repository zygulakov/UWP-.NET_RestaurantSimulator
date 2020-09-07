

namespace My_Restaurant.Food
{
    abstract class CookedFood : MenuItem
    {
        
        public int Quantitiy
        {
            get;
            set;
        }
        public abstract void Cook();
        public abstract void Obtain();
        public abstract void Request();
        public abstract void Serve();
    }

}
