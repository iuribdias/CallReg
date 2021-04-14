using System;
using System.Linq;

namespace CallReg
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Call Reg \n");
            Console.WriteLine("GiraKuza");
            ConsoleKeyInfo firstKey = Console.ReadKey();
            if(firstKey.Key == ConsoleKey.UpArrow)
            {

            }
            else
            {
                string oi = string.Concat(firstKey.Key.ToString(), Console.ReadLine());
                Console.WriteLine(oi + " written");
                Console.ReadLine();
            }
        }
    }
}
