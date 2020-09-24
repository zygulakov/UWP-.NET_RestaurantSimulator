

namespace App.My_Restaurant.Food
{
    abstract class CookedFood : IMenuItem
    {
        public int Quantitiy
        {
            get;
            set;
        }
        public abstract void Cook();
        public abstract IMenuItem Obtain();
        public abstract IMenuItem Serve();

        public override string ToString()
        {
            return $"{this.Quantitiy} {this.GetType().Name}";
        }
    }
    

}
