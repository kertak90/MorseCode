using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MorseEvent
{
    public partial class Form1 : Form
    {
        Morse MorseController;
        
        public Form1()
        {
            InitializeComponent();
            MorseController = new Morse();            
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {           
            MorseController.KeyDownEvent(sender, e);
            Morse.stringOut = Console.WriteLine;
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {  

        }

        private void button1_KeyUp(object sender, KeyEventArgs e)
        {           
            MorseController.KeyUpEvent(sender, e);
        }
    }
}
