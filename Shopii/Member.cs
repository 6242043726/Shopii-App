using System;
using System.Collections.Generic;
using System.Text;

namespace Shopii
{
    class Member
    {
        private string name;
        private string phone;
        private double coins;
        private int status;
        
        public Member(string n, string pn, double c, int st)
        {
            name = n;
            phone = pn;
            coins = c;
            status = st;
        }
        public string Name { get { return name; } }
        public double Coins { get { return coins; } }
        public int Status { get { return status; } }
        public void DeductCoins(double uc)
        {
            coins -= uc;
        }
        public void RecieveCoins(double rc)
        {
            coins += rc;
        }
        public void reStatus()
        {
            status = 0;
        }
    }
}
