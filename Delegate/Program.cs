using System.Text;
using static Delegate.DelegateDemo;

namespace Delegate
{
    internal class Program
    {
        delegate void MyDelegate();
        delegate void MyDelegate2(string s);

        delegate int MyDelegate3(string s);
        delegate float Tinh2Thamso(float x, float y);

        delegate void DelegateXinChao(string s);

        public delegate void Logger();
        static void Main(string[] args)
        {
            
            Console.OutputEncoding = Encoding.UTF8;
            Temperature tempConversion = new Temperature(FahrenheiToCelsius);
            double tempF = 96;
            double tempC = tempConversion(tempF);

            Console.WriteLine($" Temperature in Fahrenheit = {tempF}");
            Console.WriteLine("Temperature in Celsius = {0:F}", tempC);

            Tinh2Thamso myTT = Sum;

            float tong = myTT(5, 6);
            Console.WriteLine("tổng = {0}", tong);

            VD04();

            DelegateXinChao btn01 = delegate (string s)
            {
                Console.WriteLine("BTN1: " + s);
            };
            DelegateXinChao btn02 = (string s) => Console.WriteLine("LamDa: " + s);

            btn01?.Invoke("a");
            btn02?.Invoke("b");

            Logger logger = LogSystem.LogToFile;
            logger += LogSystem.LogToConsole;
            logger();

            Eventscs objectEvent = new Eventscs();

            objectEvent.Print += () => Console.WriteLine("Sự kiện đã được sử lý");

            objectEvent.Show();
           
            int a = 10;
            int b = 3;
            Action increment = delegate { a++; };
            increment();
            increment();
            swap(ref a,ref b);

            Console.WriteLine("a = {0} b = {1}", a, b);

            Console.WriteLine("====Func====");
            Func<double, double, double> myFuntionc01 = delegate (double a, double b)
            {
                return a * b;
            };
            Console.WriteLine("Func tích = " + myFuntionc01?.Invoke(5, 20));
            Func<int, double, double> myFuntionc02 = delegate (int a, double b)
            {
                return a / b;

            };
            Console.WriteLine("Func thương = " + myFuntionc02?.Invoke(5, 10));
            Func<int, int, int> myFunc03 = (a, b) => a * b;
            int result = myFunc03(4, 6);
            Console.WriteLine("Tích = " + result);

            // Action
            Console.WriteLine("====Action====");
            Action<string> myAct01 = delegate (string s)
            {
                Console.WriteLine("Ano hello " + s);
            };
            myAct01?.Invoke("bob");

            Action<int> myAct02 = delegate (int a)
            {
                Console.WriteLine("hello " + a);
            };
            myAct02?.Invoke(3);

            Action<double, double> myAct03 = delegate (double a, double b)
            {
                Console.WriteLine($"Tích của {a} x {b} = {a * b}");
            };
            myAct03?.Invoke(30, 2);
            Action<string> myAct04 = message => Console.WriteLine("Tin nhắn: " + message);
            myAct04?.Invoke("Chào bạn");
            Console.WriteLine("====Predicate====");

            Predicate<int> isEven = delegate (int number)
            {
                return number % 2 == 0;
            };
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> evens = numbers.FindAll(isEven);

            foreach(int n in evens)
            {
                Console.WriteLine(n);
            }
        }
        static float Sum(float x, float y)
        {
            return x + y;
        }
        static void VD04_ShowTenCallbacl (MyDelegate2 callback)
        {
            Console.WriteLine("Nhập tên của bạn ");
            string name = Console.ReadLine();
            callback(name);
        }
        static void VD04()
        {
            MyDelegate2 ShowStringDelegate = new MyDelegate2(ShowString);
            ShowStringDelegate("da");
        }
        static void ShowString(string s)
        {
            Console.WriteLine("[ShowString] Your string: " + s);
            
        }

        public static void chaoban(string name)
        {
            Console.WriteLine("Xin chao " + name);
        }
        public static void swap(ref int a,ref int b)
        {
            int temp = a;
            a= b;
            b= temp;
        }
        
    }
    class LogSystem
    {
        public static void LogToFile() => Console.WriteLine("Ghi vào file");
        public static void LogToConsole() => Console.WriteLine("Hiển thị console");
    }
}
