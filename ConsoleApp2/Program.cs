using System;
using System.Text;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                Console.WriteLine("Nhập x: ");
                string inputX = Console.ReadLine();
                Console.WriteLine("Nhập y: ");
                string inputY = Console.ReadLine();
                if(!double.TryParse(inputX, out double x) || !double.TryParse(inputY, out double y))
                {
                    throw new MyException.MyExceptionFormatException("Lỗi format");
                }
                Caculate(x, y);
                
            }catch (Exception ex)
            {
                Console.WriteLine($"Exception :  + {ex.Message}");
            }
        }

        public static void Caculate(double x , double y)
        {
            try
            {
                string path = "input.txt";
                double a = (3 * x + 2 * y);
                double b = (2 * x - y);
                double c = a / b;
                if (double.IsInfinity(c))
                {
                    throw new MyException.MyExceptionDivideByZero("không thê chia cho 0");
                }
                if(c == 0)
                {
                    throw new MyException.NotNegativeException("không thể sprt(0)");
                }
                double h = Math.Sqrt(a / b);
                Console.WriteLine(h);
                SaveFile(path, h);
                double? s = ReadFile(path);
                Console.WriteLine(s);
            }
            finally
            {
                Console.WriteLine("hết");
            }
        }
        public static double? TinhThuong(double a, double b)
        {
            try
            {
                return a / b;
                throw new MyException.MyExceptionDivideByZero("Không thẻ chia cho 0");
            }catch(Exception e)
            {
                Console.WriteLine($"Exception :  + {e.Message}");
                return null;
            }
        }
        public static void SaveFile(string path, double result)
        {
            File.WriteAllText(path, result.ToString());
            Console.WriteLine("Tệp đã lưu thành công");
        }
        public static double? ReadFile(string path)
        {
            if(File.Exists(path))
            {
                string content = File.ReadAllText(path);
                if(double.TryParse(content, out double result))
                {
                    return result;
                }
            }
            return null;
        }

        public static void CauseArrayException(int[] ar)
        {
            try
            {
                for (int i = 0; i <= ar.Length; i++)
                {
                    Console.WriteLine($"Array[{i}] = {ar[i]}");
                    if( ar[i] == null)
                    {
                        throw new MyException.MyExceptionIndexOutOfRangeException("lỗi");
                    }
                }
                
            }catch(IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Vượt qua mảng : {ex.Message}");
            }
        }
        public static void CauseFormatException()
        {

            Console.WriteLine("Nhap a,b");
            double a, b;
            int d;

            a = Convert.ToDouble(Console.ReadLine());

            b = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Chon so");

            menu();
            d = Convert.ToInt32((Console.ReadLine()));
            switch (d)
            {
                case 1:
                    tich(a, b);
                    break;
                case 2:
                    thuong(a, b);
                    break;
                case 3:
                    hieu(a, b);
                    break;
                case 4:
                    tong(a, b);
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
            try
            {
                double c = (a / b);
                if (double.IsInfinity(c))
                {
                    throw new MyException.MyExceptionDivideByZero("Không thể chia cho 0");
                }
                Console.WriteLine(c);
                
                
            }catch(DivideByZeroException e)
            {
                Console.WriteLine($"DivideByZeroException : {e.Message}/ {e.StackTrace} ");
            }
            catch (FormatException e)
            {
                Console.WriteLine($"FormatException : {e.Message}/ {e.StackTrace} ");
            }

            catch (ArithmeticException e)
            {
                Console.WriteLine($"ArithmeticException : {e.Message}/ {e.StackTrace} ");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception : {e.Message}/ {e.StackTrace} ");
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
    public static class MyException
    {
        public class MyExceptionDivideByZero : DivideByZeroException
        {
            public MyExceptionDivideByZero(string ms) : base(ms) { }
        }
        public class MyExceptionIndexOutOfRangeException : Exception
        {
            public MyExceptionIndexOutOfRangeException(string ms) : base(ms) { }
        }
        public class MyExceptionFormatException : FormatException
        {
            public MyExceptionFormatException(string ms) : base(ms) { }
        }
        public class NotNegativeException : Exception
        {
            public NotNegativeException(string ms) : base(ms) { }
        }
    }
}
