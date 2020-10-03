

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
        //TODO: Implementation of the Obtain and Serve methods are the same. So it will be better not overriding them in chicken and egg classes. Instead of that you can implement the in this class.***
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
