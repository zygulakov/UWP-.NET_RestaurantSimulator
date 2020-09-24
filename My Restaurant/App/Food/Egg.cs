﻿using System;
namespace App.My_Restaurant.Food
{
    sealed class Egg : CookedFood, IDisposable
    {
        public Egg(int quantity)
        {
            Random randQuality = new Random();
            Quality = randQuality.Next(101);
            Quantitiy = quantity;

        }
        public static int? Quality
        {
            get;
            private set;
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

        //TODO: Where this method will be called?
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
