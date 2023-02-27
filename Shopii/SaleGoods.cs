using System;
using System.Collections.Generic;
using System.Text;

namespace Shopii
{
    class SaleGoods
    {
        private int quantity;
        private double totalprice;
        private Goods goods;
        public SaleGoods(int q, Goods g)
        {
            quantity = q;
            goods = g;
            totalprice = 0;
        }
        public int Quantity { get { return quantity; } }
        public string Name { get { return goods.Name; } }
        public double Totalprice { get { return totalprice; } }
        public int Category { get { return goods.Category; } }
        public void changeQuantity(int q)
        {
            quantity = q;
        }
        public double calculateTotalprice()
        {
            return totalprice = quantity * goods.Price;
        }
    }
}
