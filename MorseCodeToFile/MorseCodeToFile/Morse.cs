using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using NAudio;
using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.CoreAudioApi;

namespace MorseCodeToFile
{
    class Morse
    {
        #region Dictionary morseTable
        Dictionary<char, string> morseTable = new Dictionary<char, string>
        {
            ['A'] = "*-",
            ['B'] = "-***",
            ['C'] = "-*-*",
            ['D'] = "-**",
            ['E'] = "*",
            ['F'] = "**-*",
            ['G'] = "--*",
            ['H'] = "****",
            ['I'] = "**",
            ['J'] = "*---",
            ['K'] = "-*-",
            ['L'] = "*-**",
            ['M'] = "--",
            ['N'] = "-*",
            ['O'] = "---",
            ['P'] = "*--*",
            ['Q'] = "--*-",
            ['R'] = "*-*",
            ['S'] = "***",
            ['T'] = "-",
            ['U'] = "**-",
            ['V'] = "***-",
            ['W'] = "*--",
            ['X'] = "-**-",
            ['Y'] = "-*--",
            ['Z'] = "--**",

            ['a'] = "*-",
            ['b'] = "-***",
            ['c'] = "-*-*",
            ['d'] = "-**",
            ['e'] = "*",
            ['f'] = "**-*",
            ['g'] = "--*",
            ['h'] = "****",
            ['i'] = "**",
            ['j'] = "*---",
            ['k'] = "-*-",
            ['l'] = "*-**",
            ['m'] = "--",
            ['n'] = "-*",
            ['o'] = "---",
            ['p'] = "*--*",
            ['q'] = "--*-",
            ['r'] = "*-*",
            ['s'] = "***",
            ['t'] = "-",
            ['u'] = "**-",
            ['v'] = "***-",
            ['w'] = "*--",
            ['x'] = "-**-",
            ['y'] = "-*--",
            ['z'] = "--**",

            [','] = "*-*-*-",
            ['.'] = "******",
            ['?'] = "**--**",
            ['!'] = "--**--",
            ['/'] = "-**-*",
            ['-'] = "-****-",
            ['"'] = "*-**-*",
            [';'] = "-*-*-*",
            [':'] = "---***"
        };
        #endregion

        #region Dictionary AlphabetTable
        Dictionary<string, char> AlphabetTable = new Dictionary<string, char>
        {
            ["*-"] = 'A',
            ["-***"] = 'B',
            ["-*-*"] = 'C',
            ["-**"] = 'D',
            ["*"] = 'E',
            ["**-*"] = 'F',
            ["--*"] = 'G',
            ["****"] = 'H',
            ["**"] = 'I',
            ["*---"] = 'J',
            ["-*-"] = 'K',
            ["*-**"] = 'L',
            ["--"] = 'M',
            ["-*"] = 'N',
            ["---"] = 'O',
            ["*--*"] = 'P',
            ["--*-"] = 'Q',
            ["*-*"] = 'R',
            ["***"] = 'S',
            ["-"] = 'T',
            ["**-"] = 'U',
            ["***-"] = 'V',
            ["*--"] = 'W',
            ["-**-"] = 'X',
            ["-*--"] = 'Y',
            ["--**"] = 'Z',
            
            ["*-*-*-"] = ',',
            ["******"] = '.',
            ["**--**"] = '?',
            ["--**--"] = '!',
            ["-**-*"] = '/',
            ["-****-"] = '-',
            ["*-**-*"] = '"',
            ["-*-*-*"] = ';',
            ["---***"] = ':'
        };
        #endregion

        #region Wave
        //WaveIn waveIn;          //объект класса (WaveIn - потока для записи)
        //WaveFileWriter writer;
        //string outputFilename = "file.wav";
        //void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        //{
        //    writer.WriteData();
        //}
        #endregion


        public string[] Text1;
        public string[] Text2;
        string msg;
        string fileadress;
        DateTime PushTime;
        DateTime UpTime;
        public Morse()                                  //Конструктор класса, пока пустой
        {
           
        }
        public Morse(string FileAdress)
        {
            Text1 = File.ReadAllLines(FileAdress);      //В Text1 хранится текст до преобразования
            fileadress = FileAdress;
        }
        public void TextConvertToMorse()                //Преобразование текста в морзе согласно таблице морзе
        {
            string short1=null;
            Array.Resize(ref Text2, Text1.Length + 1);
            
            for (int i=0;i<Text1.Length;i++)
            {
                Text1[i] = Text1[i].Replace("  "," ");
                Text1[i] = Text1[i].Replace("   ", " ");
                //Text1[i] = Text1[i].Replace(',', ' ');
                for (int j=0;j<Text1[i].Length;j++)
                {
                    morseTable.TryGetValue(Text1[i][j], out short1);
                    msg += short1 + " ";
                    short1 = null;
                }
                Text2[i] = msg ;
                msg = null;
            }
        }
        public void MorseConvertToText()                //Преобразуем текст в текст согласно тадлице
        {
            string short1 = null;
            Text2 = null;
            Array.Resize(ref Text2, Text1.Length);
            string[] msg;
            string word=null;
            char symb;
            for (int i = 0; i < Text1.Length; i++)
            {
                msg = Text1[i].Split(' ');
                for (int j = 0; j < msg.Length; j++)
                {
                    AlphabetTable.TryGetValue(msg[j], out symb);                    
                    word += symb;                    
                }
                Text2[i] = word ;
                msg = null;
                word = null;
            }
        }
        public void MorseBeep2()                        //Озвучивание кода морзе 
        {
            for (int i = 0; i < Text2.Length; i++)
            {
                for (int j = 0; j < Text2[i].Length; j++)
                {
                    if (Text2[i][j] == '*') Console.Beep(300,150);
                    if (Text2[i][j] == '-') Console.Beep(300,450);
                    if (Text2[i][j] == ' ') Thread.Sleep(300);
                }
                Thread.Sleep(500);
            }
        }
        public void WriteToFile()                       //Метод для чтения файла
        {
            File.WriteAllLines($"Morse.txt",Text2);
        }
        public void WriteToFile(string file)            //Метод для записи текста в файл
        {
            File.WriteAllLines($"{file}", Text2);
        }
        public void ShowText1()                         //Вывод на экран строк из файла до преобразования в МОРЗЕ
        {
            for (int i = 0; i < Text1.Length; i++)
            {
                Console.WriteLine(Text1[i]);
            }
        }
        public void ShowText2()                         //Вывод на экран строк после преобразования
        {
            for (int i = 0; i < Text2.Length; i++)
            {
                Console.WriteLine(Text2[i]);
            }
        }
        public void ConvertKeyMorse()
        {
            do
            {

                Console.ReadKey();
                if (ConsoleKey.Spacebar.IsDown)
                    PushTime = DateTime.Now;

            } while (Console.ReadKey().Key!=ConsoleKey.Escape);
        }
    }
}
