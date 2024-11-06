using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void MD5()
        {
            byte[] text = Encoding.UTF8.GetBytes(tbtext.Text);
            List<byte> list;
            list = text.ToList();
            if(text.Length % 64 != 0)
            {
                list.Add(0x80);
                if(list.Count %64 != 0)
                {
                    do
                    {
                        list.Add(0);
                    } while (list.Count % 64 != 0);
                }
                text = list.ToArray();
            }
            
            uint A = 0x67452301;
            uint B = 0xefcdab89;
            uint C = 0x98badcfe;
            uint D = 0x10325476;
            for(int i = 0; i < text.Length; i += 64)
            {
                uint[] X = new uint[16];
                for(int j = 0; j < 16; j++)
                {
                    X[j] = BitConverter.ToUInt32(text, i * 64 + j * 4);
                }
                uint A1 = A, B1 = B, C1 = C, D1 = D;
                for (int j = 0; j < 64; j++)
                {
                    A = B + ((A + G(B, C, D, j) + X[g(j)] + T(j)) << S(i));
                    uint t = D;
                    D = C; ;
                    C = A;
                    B = t;
                }
                A = A + A1;
                B = B + B1;
                C = C + C1;
                D = D + D1;
            }
            byte[] a = BitConverter.GetBytes(A);
            byte[] b = BitConverter.GetBytes(B);
            byte[] c = BitConverter.GetBytes(C);
            byte[] d = BitConverter.GetBytes(D);
            List<byte[]> W = new List<byte[]>();
            W.Add(a);
            W.Add(b);
            W.Add(c);
            W.Add(d);
            tbres.Text = "";
            for(int i =0;i < 4; i++)
            {
                for (int j = 0; j < a.Length; j++)
                {
                    tbres.Text += W[i][j].ToString("x2");
                }
            }
            File.WriteAllText("E:\\Е\\Криптология\\lab4\\1.txt", tbres.Text);
        }
        uint T(int i)
        {
            uint t = (uint)(Math.Pow(2, 32) * Math.Abs(Math.Sin(i + 1)));
            return t;
        }
        int g(int i)
        {
            int g1 = 0;
            if (i < 16)
            {
                g1 = i;
            }
            else if (i > 15 && i < 32)
            {
                g1 = (5 * i + 1) % 16;
            }
            else if (i > 31 && i < 48)
            {
                g1 = (3 * i + 5) % 16;
            }
            else if (i > 47 && i < 64)
            {
                g1 = (7 * i) % 16;
            }
            return g1;
        }
        uint G(uint b,uint c,uint d, int i)
        {
            uint g = 0;
            if(i < 16)
            {
                g = (b & c) | (~b & d);
            }
            else if(i > 15 && i < 32)
            {
                g = (b & d) | (c & ~d);
            }
            else if (i > 31 && i < 48)
            {
                g = b ^ c ^ d;
            }
            else if (i > 47 && i < 64)
            {
                g = c ^ (b | ~d);
            }
            return g;
        }
        int S(int i)
        {
            int[] s = new int[] { 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22 };
            return s[i % 16];
        }
        private void btnMD5_Click(object sender, EventArgs e)
        {
            MD5();
        }
    }
}
