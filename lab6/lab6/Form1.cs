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
            Calc_px();
            gx = Calc_pol(kk);
            int[] vrem = new int[gx.Length];
            arr_copy(ref vrem, mult_pol(gx, p));
            arr_copy(ref fx, plus_pol(vrem, div_pol(vrem, Calc_pol(px))));
            foreach (int i in fx) richTextBox1.AppendText(i.ToString());
            richTextBox1.AppendText("\n");
            arr_copy(ref fx, Calc_bin(fx));
            foreach (int i in fx) richTextBox1.AppendText(i.ToString());
            richTextBox1.AppendText("\n");
            /*foreach (int i in px) richTextBox1.AppendText(i.ToString());
            richTextBox1.AppendText("\n");*/
        }

        int[] px, gx, fx, kk = {1, 0, 0, 1};
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
        void Calc_px()
        {
            px = new int[p + 1];
            int g = 10;
            for (int i = 0; i < px.Length; i++)
            {
                px[i] = px_tab[p - 1, g];
                g--;
            }
           // Array.Reverse(px);
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

        void arr_copy(ref int[] arr1, int[] arr2)
        {
            arr1 = new int[arr2.Length];
            for (int i = 0; i < arr2.Length; i++) arr1[i] = arr2[i];
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
            //res = Calc_pol(res);
            arr_copy(ref res, Calc_pol(res));
            return res;
        }

        int[] div_pol(int[] pol1, int[] pol2)
        {
            
            int[] residue = new int[pol1.Length]; //остаток
           // Array.Copy(pol1, residue, pol1.Length);
            arr_copy(ref residue, pol1);
            int[] vrem = new int[pol2.Length];
            int[] vrem2 = new int[pol2.Length];
            while (residue[0] >= pol2[0])
            {
                arr_copy(ref vrem2, pol2);
               // Array.Copy(mult_pol(pol2, residue[0] - pol2[0]), vrem, pol2.Length);
                arr_copy(ref vrem, mult_pol(vrem2, residue[0] - vrem2[0]));
                //residue = minus_pol(residue, vrem);
                arr_copy(ref residue, minus_pol(residue, vrem));
            }
            return residue;
        }
    }
}
