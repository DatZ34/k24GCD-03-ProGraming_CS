using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    public delegate void Display();
    internal class Eventscs
    {
        public event Display Print;

        public void Show()
        {
            Console.WriteLine("This is an event drivent program");
            Print?.Invoke();
        }
    }
}
