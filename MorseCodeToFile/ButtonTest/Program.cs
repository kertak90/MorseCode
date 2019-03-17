using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ButtonTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start=DateTime.Now;
            DateTime finish;
            bool flag1=false;
            do
            {
                ReadKey();
                if(ReadKey().Key == ConsoleKey.Spacebar)
                {
                    start = DateTime.Now;
                    flag1 = true;
                }
                if(ReadKey().Key!= ConsoleKey.Spacebar)
                {
                    flag1 = false;
                    WriteLine(DateTime.Now-start);
                }
                
            } while(true);
        }
    }
}
