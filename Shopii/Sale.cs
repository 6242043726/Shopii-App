using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Shopii
{
    class Sale
    {
        private double sumprice;
        private double CoinUsed;
        private double coinsR;
        private double discount;
        private int qc1;
        private int qc2;
        private int qc3;
        private Member member;
        private SortedList salegoods = new SortedList();
        public Sale(Member m)
        {
            member = m;
            sumprice = 0;
            CoinUsed = 0;
        }
        public double Coins { get { return member.Coins; } }
        public string Name { get { return member.Name; } }
        public double Sumprice { get { return sumprice; } }
        public int QC1 { get { return qc1; } }
        public int QC2 { get { return qc2; } }
        public int QC3 { get { return qc3; } }
        public void recordSaleGoods(int q, Goods g, int si)
        {

            if (salegoods.Contains(si))
            {
                ((SaleGoods)salegoods[si]).changeQuantity(q);
                if (((SaleGoods)salegoods[si]).Quantity <= 0) salegoods.Remove(si);
            }
            else if (q > 0)
            {
                SaleGoods c = new SaleGoods(q, g);
                salegoods.Add(si, c);
            }
        }

        public void currentSale()
        {
            sumprice = 0;
            Console.WriteLine(" _______________________________");
            Console.WriteLine("| Description\tAmount\tPrice\t|");
            for (int i = 0; i < salegoods.Count; i++)
            {
                ((SaleGoods)salegoods.GetByIndex(i)).calculateTotalprice();
                Console.WriteLine("| -" + ((SaleGoods)salegoods.GetByIndex(i)).Name + "\t" + ((SaleGoods)salegoods.GetByIndex(i)).Quantity + "\t" + ((SaleGoods)salegoods.GetByIndex(i)).Totalprice+"\t|");
                sumprice += ((SaleGoods)salegoods.GetByIndex(i)).Totalprice;
            }
            Console.WriteLine("|\t\t Total: " + sumprice + "\t|");
            Console.WriteLine("|_______________________________|");
        }

        public void DelSale()
        {
            int j = salegoods.Count;
            for (int i = 0; i < j; i++)
            {
                salegoods.RemoveAt(0);
            }
            sumprice = 0;
        }
        public void recordCategory()
        {
            qc1 = 0; qc2 = 0; qc3 = 0;
            for (int i = 0; i < salegoods.Count; i++)
            {
                if (((SaleGoods)salegoods.GetByIndex(i)).Category == 1) qc1 += ((SaleGoods)salegoods.GetByIndex(i)).Quantity;
                else if (((SaleGoods)salegoods.GetByIndex(i)).Category == 2) qc2 += ((SaleGoods)salegoods.GetByIndex(i)).Quantity;
                else if (((SaleGoods)salegoods.GetByIndex(i)).Category == 3) qc3 += ((SaleGoods)salegoods.GetByIndex(i)).Quantity;
            }
        }
        public double calculateUcoins()
        {
            double uc = sumprice / 4;
            if (uc > member.Coins) uc = member.Coins;
            if (uc > 500) uc = 500;
            double MaxC = Math.Round(uc, 0, MidpointRounding.ToNegativeInfinity);
            return MaxC;
        }

        public void Purchase(double uc)
        {
            discount = 0;
            coinsR = 0;
            if (sumprice >= 3000)
            {
                discount = Math.Round(sumprice * 30 / 100,0);
                if (discount > 1000) discount = 1000;
            }
            else if (sumprice >= 1500)
            {
                coinsR = sumprice * 20 / 100;
                if (coinsR > 500) coinsR = 500;
            }
            else if (sumprice >= 500)
            {
                coinsR = sumprice * 10 / 100;
                if (coinsR > 100) coinsR = 100;
            }
            if (member.Status == 1)
            {
                int d = 0;
                if (sumprice > 200) d = 100;
                discount += d;
                member.reStatus();
            }
            sumprice -= discount;
            if (uc > member.Coins || uc > 500 || uc > sumprice / 4)
            {
                uc = 0;
                Console.WriteLine("* Cannot use coins *");
            }
            sumprice -= uc;
            member.DeductCoins(uc);
            CoinUsed = uc;
            coinsR += Math.Round(sumprice / 100, 0, MidpointRounding.ToNegativeInfinity);
            member.RecieveCoins(coinsR);
        }
        public void PrintReciept()
        {
            Console.WriteLine(" _______________________________");
            Console.WriteLine("|        << Receipt >>          |");
            Console.WriteLine("| Discount:\t\t" + discount + "\t|\n| Coins Used:\t\t" + CoinUsed+"\t|");
            Console.WriteLine("| Net Total:\t\t" + sumprice + "\t|");
            Console.WriteLine("|\t\t\t\t|\n| Recieve Coins:\t" + coinsR + "\t|\n| Balanced Coins:\t" + Math.Round(member.Coins,2) + "\t|");
            Console.WriteLine("|_______________________________|");
        }
    }
}
