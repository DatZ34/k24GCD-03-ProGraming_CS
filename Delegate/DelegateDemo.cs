using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    internal class DelegateDemo
    {
        public delegate double Temperature(double temp);
        public static double FahrenheiToCelsius(double temp)
        {
            return ((temp - 32) / 9) * 5;
        }
    }
}
