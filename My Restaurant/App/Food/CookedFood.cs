

namespace App.My_Restaurant.Food
{
    abstract class CookedFood : IMenuItem
    {
        public int Quantity
        {
            get;
            set;
        }
        public abstract void Cook();
        public virtual IMenuItem Obtain()
        {
            return this;
        }
        public virtual IMenuItem Serve()
        {
            return this;
        }
        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
    

}
