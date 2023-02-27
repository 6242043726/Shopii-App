using System;
using System.Collections.Generic;
using System.Text;

namespace Shopii
{
    class Goods
    {
        private string name;
        private double price;
        private int category;
        public Goods(string n, double p, int c)
        {
            name = n;
            price = p;
            category = c;
        }
        public string Name { get { return name; } }
        public double Price { get { return price; } }
        public int Category { get { return category; } }
    }
}
