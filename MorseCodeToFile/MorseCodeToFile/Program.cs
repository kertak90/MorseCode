using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MorseCodeToFile;


namespace MorseCodeToFile
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Morse a = new Morse("file.txt");
            a.ShowText1();
            a.TextConvertToMorse();
            a.ShowText2();
            a.WriteToFile("file2.txt");

            Morse b = new Morse("file2.txt");
            b.ShowText1();
            b.MorseConvertToText();

            b.ShowText2();
            b.WriteToFile("file3.txt");
            a.MorseBeep2();
            Console.ReadLine();
        }
    }
}
