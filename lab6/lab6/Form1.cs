using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int k = 4, n, p, x, y, kol, kp;

        private void button1_Click(object sender, EventArgs e)
        {
            
            Calc_n_p();
            gx = minus_pol(Calc_pol(kk), Calc_pol(fx));
            foreach (int i in gx) richTextBox1.AppendText(i.ToString());
            richTextBox1.AppendText("\n");
            /*foreach (int i in px) richTextBox1.AppendText(i.ToString());
            richTextBox1.AppendText("\n");*/
        }

        int[] px, gx, fx = {1, 0}, kk = {1, 0, 0, 1};
        int[,] px_tab = {
                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
                            {0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1},
                            {0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1},
                            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1},
                            {0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1},
                            {0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1},
                            {0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1},
                            {0, 0, 1, 0, 0, 0, 1, 1, 0, 1, 1},
                            {0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1},
                            {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1},
                            };
        void Calc_n_p()
        {
            n = k; //мб n = k
            while (Math.Pow(2, k) > (Math.Pow(2, n) / (n + 1)))
                n++;
            p = n - k;
        }

        int[] Calc_pol(int[] bin)
        {
            int count = 0;
            for (int i = 0; i < bin.Length; i++) if (bin[i] == 1) count++;
            int[] pol = new int[count];
            int g = 0;
            for (int i = bin.Length - 1; i >= 0; i--)
                if (bin[bin.Length - 1 - i] == 1)
                {
                    pol[g] = i;
                    g++;
                }
            return pol;
        }

        int[] Calc_bin(int[] pol)
        {
            int count = pol[0] + 1;
            int[] bin = new int[count];
            int g = 0;
            Array.Reverse(pol);
            for (int i = 0; i < bin.Length; i++)
                if (g < pol.Length)
                if (i == pol[g])
                {
                    bin[i] = 1;
                    g++;
                }
                else bin[i] = 0;
            Array.Reverse(bin);
            return bin;
        }
        int[] Calc_bin(int[] bin, int count)
        {
            int[] result = new int[count];
            int g = bin.Length - 1;
            for (int i = result.Length - 1; i >= 0; i--)
                if (g >= 0)
                {
                    result[i] = bin[g];
                    g--;
                }
            return result;
        }
        int[] mult_pol(int[] pol, int mult)
        {
            for (int i = 0; i < pol.Length; i++) pol[i] += mult;
            return pol;
        }

        int[] plus_pol(int[] pol1, int[] pol2)
        {
            int[] sum = new int[pol1.Length + pol2.Length];
            for (int i = 0; i < pol1.Length; i++) sum[i] = pol1[i];
            int g = 0;
            for (int i = pol1.Length; i < sum.Length; i++)
            {
                sum[i] = pol2[g];
                g++;
            }
            Array.Sort(sum);
            Array.Reverse(sum);
            return sum;
        }

        int[] minus_pol(int[] pol1, int[] pol2)
        {
            pol1 = Calc_bin(pol1);
            pol2 = Calc_bin(Calc_bin(pol2), pol1.Length);
            int[] res = new int[pol1.Length];
            for (int i = 0; i < res.Length; i++)
                    res[i] = Math.Abs(pol1[i] - pol2[i]);
            return res;
        }

     /*   int[] div_pol(int[] pol1, int pol2)
        {
            int[] result = new int[1];
            int[] residue = new int[1]; //остаток
            residue[0] = pol1[0];
            while(residue[0] > pol2[0])
        }*/
    }
}
