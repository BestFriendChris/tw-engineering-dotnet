using System;

namespace VideoWorld.Models
{
    public class Customer
    {
        private Cart _cart = new Cart();

        public Cart Cart
        {
            get { return  _cart; }
        }
    }
}