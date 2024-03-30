using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private char current_parity = '0';
        private char current_start = '0';
        public Form1()
        {
            InitializeComponent();
            show_data_frame('\0', '\0', "", '\0');
        }
        public string Reverse(string Input)
        {

            // Converting string to character array 
            char[] charArray = Input.ToCharArray();

            // Declaring an empty string
            string reversedString = String.Empty;

            // Iterating the each character from right to left 
            for (int i = charArray.Length - 1; i > -1; i--)
            {

                // Append each character to the reversedstring.
                reversedString += charArray[i];
            }

            // Return the reversed string.
            return reversedString;
        }
        public Tuple<char,char> get_DATA(String inp)
        {
            int res = 0;
            int C_LSB_to_MSB = 0;
            int C_MSB_to_LSB = 0;

            if(inp.Length == 10)
            {
                C_MSB_to_LSB += (inp[1] == '1') ? (1 << 7) : (0);//MSB
                C_MSB_to_LSB += (inp[2] == '1') ? (1 << 6) : (0);
                C_MSB_to_LSB += (inp[3] == '1') ? (1 << 5) : (0);
                C_MSB_to_LSB += (inp[4] == '1') ? (1 << 4) : (0);
                C_MSB_to_LSB += (inp[5] == '1') ? (1 << 3) : (0);
                C_MSB_to_LSB += (inp[6] == '1') ? (1 << 2) : (0);
                C_MSB_to_LSB += (inp[7] == '1') ? (1 << 1) : (0);
                C_MSB_to_LSB += (inp[8] == '1') ? (1 << 0) : (0);//LSB

                C_LSB_to_MSB += (inp[1] == '1') ? (1 << 0) : (0);//MSB
                C_LSB_to_MSB += (inp[2] == '1') ? (1 << 1) : (0);
                C_LSB_to_MSB += (inp[3] == '1') ? (1 << 2) : (0);
                C_LSB_to_MSB += (inp[4] == '1') ? (1 << 3) : (0);
                C_LSB_to_MSB += (inp[5] == '1') ? (1 << 4) : (0);
                C_LSB_to_MSB += (inp[6] == '1') ? (1 << 5) : (0);
                C_LSB_to_MSB += (inp[7] == '1') ? (1 << 6) : (0);
                C_LSB_to_MSB += (inp[8] == '1') ? (1 << 7) : (0);//LSB
            }
            else if(inp.Length == 8)
            {
                C_MSB_to_LSB += (inp[0] == '1') ? (1 << 7) : (0);//MSB
                C_MSB_to_LSB += (inp[1] == '1') ? (1 << 6) : (0);
                C_MSB_to_LSB += (inp[2] == '1') ? (1 << 5) : (0);
                C_MSB_to_LSB += (inp[3] == '1') ? (1 << 4) : (0);
                C_MSB_to_LSB += (inp[4] == '1') ? (1 << 3) : (0);
                C_MSB_to_LSB += (inp[5] == '1') ? (1 << 2) : (0);
                C_MSB_to_LSB += (inp[6] == '1') ? (1 << 1) : (0);
                C_MSB_to_LSB += (inp[7] == '1') ? (1 << 0) : (0);//LSB

                C_LSB_to_MSB += (inp[0] == '1') ? (1 << 0) : (0);//MSB
                C_LSB_to_MSB += (inp[1] == '1') ? (1 << 1) : (0);
                C_LSB_to_MSB += (inp[2] == '1') ? (1 << 2) : (0);
                C_LSB_to_MSB += (inp[3] == '1') ? (1 << 3) : (0);
                C_LSB_to_MSB += (inp[4] == '1') ? (1 << 4) : (0);
                C_LSB_to_MSB += (inp[5] == '1') ? (1 << 5) : (0);
                C_LSB_to_MSB += (inp[6] == '1') ? (1 << 6) : (0);
                C_LSB_to_MSB += (inp[7] == '1') ? (1 << 7) : (0);//LSB
            }
            return new Tuple<char,char>( (char) C_LSB_to_MSB, (char)C_MSB_to_LSB);
        }
        public char get_Parity(String inp)
        {
            int p = 0;
            for (int i = 0; i < inp.Length; i++)
                p += (bool)(inp[i] == '1')?(1):(0);
            return (char)('0' + p % 2);
        }
        //return MSB->LSB bit
        public string char_to_binary(char c)
        {
            string str = "";
            int num = (int)c;
            for (int i = 0; i < 8; i++)
            {
                str += (num % 2 == 1) ? ('1'.ToString()) : ('0'.ToString());
                num /= 2;
            }
            char[] tmp = str.ToCharArray();
            Array.Reverse(tmp);
            str = new string(tmp);
            return str;

        }
        public void show_data_frame(char flag, char parity, string data, char start)
        {
            if (flag != 's' && flag != 'S')//hidden
            {
                button3.Hide();
                button4.Hide();
                label15.Hide();
                label16.Hide(); 
                label17.Hide();
                label18.Hide();
                label19.Hide();
                label20.Hide();
                label21.Hide();
                groupBox1.Hide();
            }
            else
            {
                button3.Show();
                button4.Show();
                label15.Show();
                label16.Show();
                label17.Show();
                label18.Show();
                label19.Show();
                label20.Show();
                label21.Show();
                groupBox1.Show();
                label15.Text = "Data-frame:";
                //button3.Text = "Parity bit: even";
                //button4.Text = "Start bit: 0";
                label19.Text = parity.ToString(); label19.Refresh();
                label20.Text = data.Substring(0, 4) + " " + data.Substring(4,4);
                label21.Text = start.ToString(); label21.Refresh();

            }

        }
        public void convert_to_datagram_process(string inp, char parity, char start)
        {
            if (inp.Length == 1)
            {
                char sum_1bit = get_Parity(char_to_binary(inp[0]));
                if (parity == '0') parity = (sum_1bit == '0') ? ('0') : ('1');
                if (parity == '1') parity = (sum_1bit == '0') ? ('1') : ('0');
                show_data_frame('s', parity, char_to_binary(inp[0]), start);
            }
            else
            {
                show_data_frame('\0', '\0', "", '\0');
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inp = textBox1.Text.Replace(" ", "");
            convert_to_datagram_process(inp, current_parity, current_start);
            Tuple<char, char> DATA = get_DATA(inp);
            char P = get_Parity(inp);
            label5.Text = DATA.Item2.ToString();
            label6.Text = DATA.Item1.ToString();
            if (inp.Length == 10)
            {
                label7.Text = (P == '1') ? ("odd") : ("even");
                label13.Text = inp[0].ToString();
            }
            else
            {
                label7.Text = "nothing ...";
                label13.Text = "nothing ...";
            }

            if ('A' < DATA.Item2 && DATA.Item2 < 'Z' || 'a' < DATA.Item2 && DATA.Item2 < 'z')
                label12.Text = (inp.Length==10)?inp.Substring(1, 8):inp;
            else
                label12.ResetText();
            
            if ('A' < DATA.Item1 && DATA.Item1 < 'Z' || 'a' < DATA.Item1 && DATA.Item1 < 'z')
                label11.Text = Reverse((inp.Length == 10) ? inp.Substring(1, 8) : inp);
            else
                label11.ResetText();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void label10_Click(object sender, EventArgs e)
        {

        }
        
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string inp = textBox1.Text.Replace(" ", "");
            convert_to_datagram_process(inp, current_parity, current_start);
            if (inp.Length == 10 || inp.Length == 8)
            {
                Tuple<char, char> DATA = get_DATA(inp);
                char P = get_Parity(inp);
                label5.Text = DATA.Item2.ToString();
                label6.Text = DATA.Item1.ToString();
                if (inp.Length == 10)
                {
                    label7.Text = (P == '1') ? ("odd") : ("even");
                    label13.Text = inp[0].ToString();
                }else{
                    label7.Text = "nothing ...";
                    label13.Text = "nothing ...";
                }
                if ('A' < DATA.Item2 && DATA.Item2 < 'Z' || 'a' < DATA.Item2 && DATA.Item2 < 'z')
                    label12.Text = (inp.Length == 10) ? inp.Substring(1, 8) : inp;
                else
                    label11.ResetText();
                if ('A' < DATA.Item1 && DATA.Item1 < 'Z' || 'a' < DATA.Item1 && DATA.Item1 < 'z')
                    label11.Text = Reverse((inp.Length == 10) ? inp.Substring(1, 8) : inp);
                else
                    label11.ResetText();
            }
            else
            {
                label5.Text = "nothing ...";
                label6.Text = "nothing ...";
                label7.Text = "nothing ...";
                label7.Text = "nothing ...";
                label13.Text = "nothing ...";
                label12.ResetText();
                label11.ResetText();
            }

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label15_Click(object sender, EventArgs e)
        {

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void label17_Click(object sender, EventArgs e)
        {

        }
        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string inp = textBox1.Text.Replace(" ", "");
            current_parity = (current_parity == '0') ? ('1') : ('0');
            //button3.Text = current_parity.ToString();
            button3.ResetText();
            button3.Text = (current_parity == '0') ? ("Parity bit: even") : ("Parity bit: odd");
            convert_to_datagram_process(inp, current_parity, current_start);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string inp = textBox1.Text.Replace(" ", "");
            current_start = (current_start == '0') ? ('1') : ('0');
            button4.Text = (current_start == '0') ? ("Start bit: 0") : ("Parity bit: 1");
            convert_to_datagram_process(inp, current_parity, current_start);
        }
    }
}
