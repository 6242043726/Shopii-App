using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Shopii
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleMode(IntPtr handle, out int mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int handle);
        static void Main(string[] args)
        {
            var handle = GetStdHandle(-11);
            int mode;
            GetConsoleMode(handle, out mode);
            SetConsoleMode(handle, mode | 0x4);

            Console.Write("\x1b[38;2;" + 255 + ";" + 160 + ";" + 0 + "m");
            //category: 1.Fashion 2.Electronic 3.Home
            ArrayList goods = new ArrayList();
            goods.Add(new Goods("T-shirt", 189, 1)); 
            goods.Add(new Goods("Shorts ", 99, 1));
            goods.Add(new Goods("Jacket ", 359, 1));
            goods.Add(new Goods("Power Bank", 469, 2));
            goods.Add(new Goods("Speaker", 259, 2));
            goods.Add(new Goods("Air Fryer", 1290, 2));
            goods.Add(new Goods("Table  ", 569, 3));
            goods.Add(new Goods("Closet ", 1599, 3));
            goods.Add(new Goods("Bed    ", 2490, 3));
            //status: 0 = old, 1 = new
            SortedList sale = new SortedList();
            sale.Add("094", new Sale(new Member("Than", "094", 200, 0)));
            sale.Add("082", new Sale(new Member("Mina", "082", 600, 0)));

            string pn, n;
            char more, c;
            int ch, g, q;
            int qc1 = 0, qc2 = 0, qc3 = 0;
            double netsale = 0;
            double netnewP = 0;
            double netorder = 0;

            Console.WriteLine(" ______________________________________ ");
            Console.WriteLine("|  >>>  Welcome to 'Shopping App' <<<  |");
            Console.WriteLine("|                                      |" +
                "\n|░██████╗██╗░░██╗░█████╗░██████╗░██╗██╗|" +
                "\n|██╔════╝██║░░██║██╔══██╗██╔══██╗██║██║|" +
                "\n|╚█████╗░███████║██║░░██║██████╔╝██║██║|" +
                "\n|░╚═══██╗██╔══██║██║░░██║██╔═══╝░██║██║|" +
                "\n|██████╔╝██║░░██║╚█████╔╝██║░░░░░██║██║|" +
                "\n|╚═════╝░╚═╝░░╚═╝░╚════╝░╚═╝░░░░░╚═╝╚═╝|");
            Console.WriteLine("|______________________________________|");
            Console.Write("\x1b[38;2;" + 51 + ";" + 255 + ";" + 255 + "m");
            Console.WriteLine("\n >> Menu <<");
            Console.Write("\x1b[38;2;" + 255 + ";" + 255 + ";" + 255 + "m");
            Console.Write("1.Shopping\n2.Purchase\n3.Member Register\n4.Exit\nOption: ");
            ch = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            do
            {
                if (ch == 1) //ซื้อ
                {
                    Console.Write("\x1b[38;2;" + 51 + ";" + 255 + ";" + 255 + "m");
                    Console.WriteLine(" < Menu: Shopping >");
                    Console.ResetColor();
                    Console.Write("Login (phone number): ");
                    pn = Console.ReadLine();
                    try
                    {
                        Console.WriteLine("Customer name: " + ((Sale)sale[pn]).Name);
                        Console.WriteLine("Current Coins: " + Math.Round(((Sale)sale[pn]).Coins, 2));
                        do
                        {
                            Console.WriteLine("");
                            for (int i = 0; i < goods.Count; i++)
                                Console.WriteLine((i + 1) + ". " + ((Goods)goods[i]).Name + "\t" + ((Goods)goods[i]).Price);
                            Console.Write("Your selection: ");
                            g = Convert.ToInt32(Console.ReadLine());
                            while (g > goods.Count || g < 1)
                            {
                                Console.WriteLine("* Not have this Item! *", Console.ForegroundColor = ConsoleColor.Red);
                                Console.ResetColor();
                                Console.Write("Your selection: ");
                                g = Convert.ToInt32(Console.ReadLine());
                            }
                            Console.Write("How many: ");
                            q = Convert.ToInt32(Console.ReadLine());
                            ((Sale)sale[pn]).recordSaleGoods(q, ((Goods)goods[g - 1]), g);
                            ((Sale)sale[pn]).currentSale();
                            Console.Write("Buy more? (y/n): ");
                            more = Convert.ToChar(Console.ReadLine());
                        } while (more == 'y');
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("*** Not member ***", Console.ForegroundColor = ConsoleColor.Red);
                        Console.ResetColor();
                    }
                }
                if (ch == 2)//จ่ายเงิน
                {
                    Console.Write("\x1b[38;2;" + 51 + ";" + 255 + ";" + 255 + "m");
                    Console.WriteLine(" < Menu: Purchase >");
                    Console.ResetColor();
                    Console.Write("Login (phone number): ");
                    pn = Console.ReadLine();
                    try
                    {
                        Console.WriteLine("Customer name: " + ((Sale)sale[pn]).Name);
                        Console.WriteLine("Current Coins: " + Math.Round(((Sale)sale[pn]).Coins, 2));
                        ((Sale)sale[pn]).currentSale();
                        Console.Write("Confirm? (y/n): ");
                        c = Convert.ToChar(Console.ReadLine());
                        if (c == 'y')
                        {
                            Console.Write($"Amount of coins that want to use (Max:{((Sale)sale[pn]).calculateUcoins()}): ");
                            int uc = Math.Abs(Convert.ToInt32(Console.ReadLine()));
                            ((Sale)sale[pn]).Purchase(uc);
                            netsale += ((Sale)sale[pn]).Sumprice;
                            ((Sale)sale[pn]).PrintReciept();
                            ((Sale)sale[pn]).recordCategory();
                            qc1 += ((Sale)sale[pn]).QC1;
                            qc2 += ((Sale)sale[pn]).QC2;
                            qc3 += ((Sale)sale[pn]).QC3;
                            if (((Sale)sale[pn]).Sumprice > 0) netorder += 1;
                            ((Sale)sale[pn]).DelSale();
                        }
                        else if (c == 'n')
                        {
                            Console.Write("Do u want to cancel all goods in basket? (y/n): ");
                            c = Convert.ToChar(Console.ReadLine());
                            if (c == 'y')
                            {
                                ((Sale)sale[pn]).DelSale();
                                Console.WriteLine("* All goods cleared *", Console.ForegroundColor = ConsoleColor.Green);
                                Console.ResetColor();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("*** Not member ***", Console.ForegroundColor = ConsoleColor.Red);
                        Console.ResetColor();
                    }
                }
                if (ch == 3)//สมัคร
                {
                    Console.Write("\x1b[38;2;" + 51 + ";" + 255 + ";" + 255 + "m");
                    Console.WriteLine(" < Menu: Register >");
                    Console.ResetColor();
                    Console.Write("input phone number: ");
                    pn = Console.ReadLine();
                    if (!sale.Contains(pn))
                    {
                        Console.Write("Name: ");
                        n = Console.ReadLine();
                        Sale s = new Sale(new Member(n, pn, 0, 1));
                        sale.Add(pn, s);
                        Console.WriteLine("* Successfully registered *", Console.ForegroundColor = ConsoleColor.Green);
                        Console.ResetColor();
                        netnewP += 1;
                    }else
                    Console.WriteLine("*Already a member!*", Console.ForegroundColor = ConsoleColor.Red);
                    Console.ResetColor();
                }
                Console.Write("\x1b[38;2;" + 51 + ";" + 255 + ";" + 255 + "m");
                Console.WriteLine("\n >> Menu <<");
                Console.Write("\x1b[38;2;" + 255 + ";" + 255 + ";" + 255 + "m");
                Console.Write("1.Shopping\n2.Purchase\n3.Member Register\n4.Exit\nOption: ");
                ch = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            } while (ch != 4); //สรุปยอด
            Console.Write("\x1b[38;2;" + 57 + ";" + 255 + ";" + 20 + "m");
            Console.WriteLine(" _______________________________________" +
                "\n|                                       |" +
                "\n|  █▀ █░█ █▀▄▀█ █▀▄▀█ ▄▀█ █▀█ █▀█ █▄█   |" +
                "\n|  ▄█ █▄█ █░▀░█ █░▀░█ █▀█ █▀▄ █▀▄ ░█░   |");
            Console.WriteLine("|                                       |");
            Console.WriteLine("|\tFashion:\t" + qc1 + "\t\t|\n|\tElectronic:\t" + qc2 + "\t\t|\n|\tHome&Living:\t" + qc3+"\t\t|");
            Console.WriteLine("|\tOverall Sale:\t" + netsale + " baht  \t|");
            Console.WriteLine("|\tOrder Amount:\t" + netorder + "\t\t|");
            Console.WriteLine("|\tNew member:\t" + netnewP + " people\t|");
            Console.WriteLine("|_______________________________________|");
            Console.ResetColor();
        }
    }
}
