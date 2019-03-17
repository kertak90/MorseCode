using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Th = System.Threading;

namespace MorseEvent
{
    delegate void ButtonEvents(object o, KeyEventArgs KeyEvent);        //Общий делегат для нажатия клавиш
    public delegate void StringOut(string Letter);                      //Делегат для вывода букв
    class Morse
    {
        private string[] Text2;
        private string[] Text1;
        private static string word;
        private static int dTPoint = 150;                               //Длительность одной точки
        private static long DownTime = 0;                               //Время нажатия кнопки
        private static long UpTime = 0;                                 //Время отпускания кнопки

        private static long dT1 = 0;                                    //Длительность нажатого состояния кнопки
        private static long dT2 = 0;                                    //Длительность отпущенного состояния кнопки

        public static bool CloseProgramFlag = false;                    //Флаг закрытия основного цикла для расчета времени нажатия кнопки
        private static bool KeyDownFlag = false;                        //Флаг нажатия на кнопку
        private static bool KeyPressedFlag = false;                     //Флаг нажатой кнопки
        private static bool KeyUpFlag = false;                          //Флаг отпщенной кнопки
        private static bool FirstStart = true;

        public ButtonEvents KeyDownEvent = KeyDownFunction;             //Публичный делегат события нажатия кнопки
        public ButtonEvents KeyUpEvent = KeyUpFunction;                 //Публичный делегат события отпускания кнопки
        public static StringOut stringOut;                              //Для вывода букв привязали Консоль

        #region Dictionary AlphabetTable
        private static Dictionary<string, char> AlphabetTable = new Dictionary<string, char>
        {
            ["._"] = 'A',
            ["_..."] = 'B',
            ["_._."] = 'C',
            ["_.."] = 'D',
            ["."] = 'E',
            [".._."] = 'F',
            ["__."] = 'G',
            ["...."] = 'H',
            [".."] = 'I',
            [".___"] = 'J',
            ["_._"] = 'K',
            ["._.."] = 'L',
            ["__"] = 'M',
            ["_."] = 'N',
            ["___"] = 'O',
            [".__."] = 'P',
            ["__._"] = 'Q',
            ["._."] = 'R',
            ["..."] = 'S',
            ["_"] = 'T',
            [".._"] = 'U',
            ["..._"] = 'V',
            ["..__"] = 'W',
            ["_.._"] = 'X',
            ["_.__"] = 'Y',
            ["__.."] = 'Z',

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

        public static void KeyDownFunction(object o, KeyEventArgs args) //Метод выполняемый при нажатии кнопки
          {
            KeyDownFlag = true;            
            if (!KeyPressedFlag && !FirstStart)
            {
                KeyPressedFlag = true;
                DownTime = DateTime.Now.Ticks/10000;                    //Получили время в миллисекундах     
                dT1 = DownTime - UpTime;                                //Расчитали время не нажатого состояния кнопки
                decipherPause(dT1);
            }            
        }

        public static void KeyUpFunction(object o, KeyEventArgs args)   //Метод выполняемый при отпускании кнопки
        {
            KeyUpFlag = true;
            KeyDownFlag = false;
            KeyPressedFlag = false;
            FirstStart = false;
            UpTime = DateTime.Now.Ticks/10000;                          //Получили время в Миллисекундах
            dT2 = UpTime - DownTime;                                    //Расчитали время нажатого состояния кнопки            
            decipherLetter(dT2);                                        //отдадим время на насшифровку точки и тире
        }

        private static void decipherLetter(long dT)                     //Метод который расшифровывает точку и тире
        {
            if(dT > 0)                                                  //Разность времен должна быть положительная
            {
                if (dT >= 0 && dT < dTPoint * 1.1)                      //Если разность времени не превышает длятельность одной точки
                {                                                       //То это точка
                    stringOut(".");
                    word += ".";
                }                
                if (dT >= dTPoint * 3)                                  //Если разность времени превышает время длительности трех точек
                {                                                       //То это тире
                    stringOut("_");
                    word += "_";
                }
            }
        }

        private static void decipherPause(long dT)                      //Метод, который ставит между буквами и словами пробелы
         {
            if (dT < dTPoint * 1.1)                                     //Если длительность не нажатого состояния не превышает длительность точки
            {                                                           //То это промежуток между знаками
                //stringOut("");
            }
            if (dT > 3 * dTPoint * 0.9)       //Если длительность не нажатого состояния превышает длительность трех точек
            {                                                           //То это промежуток между буквами                
                MorseConvertToLetter(word);                             //Как только мы поняли что временная пауза является паузой между буквами, то 
                word = "";                                              //передаем последовательность точек и тире в метод для преобразования в букву и обнуляем строку для приема
            }
            //if (dT > 3 * dTPoint * 0.9 && dT < 7 * dTPoint * 0.9)       //Если длительность не нажатого состояния превышает длительность трех точек
            //{                                                           //То это промежуток между буквами                
            //    MorseConvertToLetter(word);                             //Как только мы поняли что временная пауза является паузой между буквами, то 
            //    word = "";                                              //передаем последовательность точек и тире в метод для преобразования в букву и обнуляем строку для приема
            //}
            //if (dT > 7 * dTPoint * 0.9)                               //Если длительность ненажатого состояния превышает длительности семи точек
            //{                                                         //То это промежуток между словами
            //    stringOut(" ");
            //    MorseConvertToLetter(word);
            //    word = "";
            //}
        }

        public static void MorseConvertToLetter(string str)             //Преобразуем текст в символ согласно таблице
        {
            char symb;
            //stringOut(str);                                             //Выведем нашу последовательность 
            if (AlphabetTable.ContainsKey(str))                         //Если список содержит данную последовательность, то
            {
                AlphabetTable.TryGetValue(str, out symb);               //В symb записывается символ из dictionary
                stringOut(symb.ToString() + "\n");                             //Вывод символа
            }            
        }

        public void MorseConvertToText()                                //Преобразуем текст в текст согласно таблице
        {
            string short1 = null;
            Text2 = null;
            Array.Resize(ref Text2, Text1.Length);
            string[] msg;
            string word = null;
            char symb;
            for (int i = 0; i < Text1.Length; i++)
            {
                msg = Text1[i].Split(' ');                              //В msg помещаем слова из i-ой строки текста Text1, разделив строку по пробелу " "
                for (int j = 0; j < msg.Length; j++)
                {
                    AlphabetTable.TryGetValue(msg[j], out symb);        //В symb записывается символ из dictionary
                    word += symb;
                }
                Text2[i] = word;
                msg = null;
                word = null;
            }
        }
    }
}
