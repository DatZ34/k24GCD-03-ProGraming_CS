
using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region ArrayList
            ArrayList list01 = new ArrayList();
            list01.Add(1);
            list01.Add(2);
            list01.Add(3);
            list01.Add(4);
            list01.Add(5);
            list01.Add(6);
            list01.Add(7);
            list01.Insert(0, 10);
            list01.RemoveAt(3);
            for(int i = 0;i < list01.Count; i++)
            {
                Console.WriteLine($"Item {i} : "+list01[i]);
            }
            Console.WriteLine($"list01 count : {list01.Count}");    
            ArrayList list02 = new ArrayList();
            list02.Add("a");
            list02.Add("b");
            list02.Add("c");
            list02[2] = 100;
            list02.Remove("a");
            list02.Remove("b");
            list01.InsertRange (2 , list02);

            list02.Clear();
            list01.RemoveRange(7, 1);
            Console.WriteLine($"list01 count : {list01.Count}");
            #endregion
            #region HashTable
            Hashtable has01 = new Hashtable(); 
            has01.Add("a", 1);
            has01.Add("b", 2);
            has01.Add("c", 3);
            has01.Add ("d", 4);
            has01.Add("e", 5);
            has01.Remove("a");
            bool haskey = has01.ContainsKey("a");
            if (haskey)
            {
                has01.Remove ("a");
            }
            bool hasValue = has01.ContainsValue(5);

            foreach (DictionaryEntry item in has01)
            {
                Console.WriteLine($"key: {item.Key}/ value : {item.Value}");
                
            }
            Console.WriteLine("=====Keys=====");
            foreach (var key in has01.Keys)
            {
                Console.WriteLine(key);
            }
            Console.WriteLine("=====Values=====");
            foreach (var value in has01.Values)
            {
                Console.WriteLine (value);
            }
            Hashtable has02 = (Hashtable)has01.Clone();

            #endregion
            #region stack
            Stack st01 = new Stack();
            st01.Push("a");
            st01.Push("b");
            st01.Push("c");
            st01.Push("d");

            var a = st01.Pop();
            var b = st01.Peek();

            bool st1 = st01.Contains(2);
            #endregion
            #region stack
            Queue Qe1 = new Queue();
            Qe1.Enqueue("a");
            Qe1.Enqueue("b");
            Qe1.Enqueue("c");
            Qe1.Enqueue("d");


            var d = Qe1.Peek();
            var c = Qe1.Dequeue();

            Console.WriteLine(d);
            bool q1 = Qe1.Contains(2);
            #endregion
            #region bai tap 1
            List<int> n = new List<int>();
            RandomNum(50,n);

            n.Sort();
            Console.WriteLine("Random number");
            foreach(var num in n)
            {
                Console.WriteLine(num);
            }
            #endregion
            Console.ReadLine();

        }
        public static void RandomNum(int num, List<int> n)
        {
            Console.WriteLine("value: " + num);
            Random random = new Random((int) DateTime.Now.Ticks);
            for(int i = 0; i < num; i++)
            {
                int value = random.Next(100);
                
                n.Add(value);
            }
            
        }
    }
}
