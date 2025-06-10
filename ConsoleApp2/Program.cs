using System;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Nhap a,b");
            double a, b;
            int d;
            a =  Convert.ToDouble(Console.ReadLine());
            b= Convert.ToDouble(Console.ReadLine());
            d = Convert.ToInt32((Console.ReadLine()));
            Console.WriteLine("Chon so");
            menu();
            switch (d)
            {
                case 1:
                    tich(a,b);
                    break;
                case 2:
                    thuong(a,b);
                    break;
                case 3:
                    hieu(a,b);
                    break;
                case 4:
                    tong(a,b);
                    break;
                default:
                    break;
            }
        }
        public static void tich(double a, double b)
        {
            double c = a * b;
            Console.WriteLine(c);
        }
        public static void thuong(double a, double b)
        {
            double c = a / b;
            if(b == 0)
            {
                Console.WriteLine("Ko hop le");
            }
            else
            {
                Console.WriteLine(c);
            }
        }
        public static void hieu(double a, double b)
        {
            double c = a - b;
            Console.WriteLine(c);
        }
        public static void tong(double a, double b)
        {
            double c = a + b;
            Console.WriteLine(c);
        }
        public static void menu()
        {
            Console.WriteLine("1.tich");
            Console.WriteLine("2.thuong");
            Console.WriteLine("3.hieu");
            Console.WriteLine("4.tong");

        }
    }
}
