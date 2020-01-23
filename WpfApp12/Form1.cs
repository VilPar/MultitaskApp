using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WpfApp12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            webBrowser1.Navigate("http://google.com");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://google.com");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.google.fi/search?source=hp&ei=_XfrWsyaA8j-sQGmwJuYAg&q="+ textBox1.Text);
        }
    }
}
