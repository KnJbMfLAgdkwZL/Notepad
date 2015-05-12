using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Open(object sender, EventArgs e)
        {
            OpenFileDialog tmp = new OpenFileDialog();
            tmp.Filter = "Все файлы |*.*|Текстовые документы (*.txt)|*.txt||";
            tmp.DefaultExt = "txt";
            tmp.CheckFileExists = true;
            tmp.CheckPathExists = true;
            if (tmp.ShowDialog() == DialogResult.OK)
            {
                StreamReader load = new StreamReader(tmp.OpenFile(), Encoding.Default);
                richTextBox1.Text = load.ReadToEnd();
                load.Close();
                this.Text = tmp.SafeFileName;
            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Lines.Length != richTextBox2.Lines.Length - 1)
            {
                richTextBox2.Text = "";
                for (int i = 0; i < richTextBox1.Lines.Length; i++)
                    richTextBox2.Text += i + 1 + Environment.NewLine;
            }
        }
        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            richTextBox2.SelectionStart = richTextBox2.GetFirstCharIndexFromLine(richTextBox1.GetLineFromCharIndex(richTextBox1.GetCharIndexFromPosition(new Point(1, 1))));
            richTextBox2.ScrollToCaret();
            System.Threading.Thread.Sleep(10);
        }
        private void FontChange(object sender, EventArgs e)
        {
            FontDialog tmp = new FontDialog();
            if (tmp.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = tmp.Font;
                richTextBox2.Font = tmp.Font;
            }
        }
        private void ColorChange(object sender, EventArgs e)
        {
            ColorDialog tmp = new ColorDialog();
            if (tmp.ShowDialog()==DialogResult.OK)
                richTextBox1.BackColor = tmp.Color;
        }
        private void Print(object sender, EventArgs e)
        {
            MessageBox.Show("Не готово");
        }
        private void SyntaxChange(object sender, EventArgs e)
        {
            StreamReader load = new StreamReader("./keywords/C++.txt", Encoding.Default);
            String str = load.ReadToEnd();
            load.Close();
            String[] arrstr = str.Split(' ');
            int a = 0, cur = richTextBox1.SelectionStart;

            //RichTextBox TMP = new RichTextBox();//БУФЕР
            //TMP.Text = richTextBox1.Text;
            for (int i = 0; i < arrstr.Length; i++)
            {
                
                do
                {
                    a = richTextBox1.Find(arrstr[i] + " ", (a + 1), RichTextBoxFinds.None);
                    if (a >= 0)
                    {
                        richTextBox1.Select(a, arrstr[i].Length);
                        richTextBox1.SelectionColor = Color.Blue;
                    }
                }
                while (a > 0);//Чтобы не крутилась коретка делать ето в буфере и потом заменять текст
            }
            richTextBox1.SelectionStart = cur;
            richTextBox1.SelectionLength = 0;
            //richTextBox1 = TMP;
        }
    }
}