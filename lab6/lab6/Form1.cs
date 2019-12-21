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
        int k = 30, n, p, mis_pos_calc = -1;
        Random rand = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            
            Calc_n_p();
            Calc_px();
            Calc_fx();
            Calc_pos_tab();
            Calc_mis();
            Output();
        }

        int[] px_pol, px_bin, gx, rx, fx_pol, fx_bin, mis_simpt, mis_bin, mis_pol, kk;
        int[][] pos_tab_pol;
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
            px_bin = new int[p + 1];
            int g = 10;
            for (int i = 0; i < px_bin.Length; i++)
            {
                px_bin[i] = px_tab[p - 1, g];
                g--;
            }
            arr_copy(ref px_pol, Calc_pol(px_bin));
        }

        void Calc_fx()
        {
            bool equal = true;
            while (equal)
            {
                kk = new int[k];
                int g = 0;
                int g2 = 0;
                for (int i = 0; i < k; i++)
                {
                    kk[i] = rand.Next(2);
                    if (k == px_bin.Length) if (kk[i] == px_bin[i]) g++;
                    if (kk[i] == 1) g2++;
                }
                if (k != px_bin.Length && g2 != 0) break;
                if (g != k && g2 != 0) equal = false;
            }
            arr_copy(ref gx, Calc_pol(kk));
            int[] vrem = new int[gx.Length];
            arr_copy(ref vrem, mult_pol(gx, p));
            arr_copy(ref fx_pol, plus_pol(vrem, div_pol(vrem, px_pol)));
            arr_copy(ref fx_bin, Calc_bin(fx_pol));
            Array.Reverse(fx_pol);
        }

        void Calc_pos_tab()
        {
            int[] vrem = new int[fx_pol[0] + 1];
            int[] mis = new int[fx_bin.Length];
            pos_tab_pol = new int[fx_bin.Length][];
            for (int i = 0; i < fx_bin.Length; i++)
                pos_tab_pol[i] = new int[fx_pol[0] + 1]; 
            for (int i = 0; i < fx_bin.Length; i++)
            {
                arr_copy(ref mis, fx_bin);
                if (mis[i] == 0) mis[i] = 1;
                else mis[i] = 0;
                arr_copy(ref mis, Calc_pol(mis));
                arr_copy(ref vrem, div_pol(mis, px_pol));
                arr_copy(ref pos_tab_pol[i], vrem);
            }
        }

        void Calc_mis()
        {
            mis_bin = new int[fx_bin.Length];
            int mis_pos = rand.Next(fx_bin.Length);
            mis_pos_calc = -1;
            mis_simpt = new int[1];
            arr_copy(ref mis_bin, fx_bin);
            if (mis_bin[mis_pos] == 0) mis_bin[mis_pos] = 1;
            else mis_bin[mis_pos] = 0;
            arr_copy(ref mis_pol, Calc_pol(mis_bin));
            arr_copy(ref mis_simpt, div_pol(mis_pol, px_pol));
            for (int i = 0; i < fx_bin.Length; i++)
            {
                int g = 0;
                if (pos_tab_pol[i].Length == mis_simpt.Length)
                    for (int j = 0; j < mis_simpt.Length; j++)
                        if (pos_tab_pol[i][j] == mis_simpt[j]) g++;
                if (g == pos_tab_pol[i].Length) mis_pos_calc = i;
            }
        }

        void Output()
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
            foreach (int i in kk) richTextBox1.AppendText(i.ToString());
            richTextBox1.AppendText(" - кодовая комбинация\n");
            foreach (int i in px_pol) richTextBox1.AppendText(i.ToString() + " ");
            richTextBox1.AppendText(" - степени образующего полинома\n");
            richTextBox1.AppendText(n + " - n\n");
            richTextBox1.AppendText(p + " - p\n");
            foreach (int i in fx_pol) richTextBox1.AppendText(i.ToString() + " ");
            richTextBox1.AppendText(" - степени в полиноме циклического кода\n");
            foreach (int i in fx_bin) richTextBox1.AppendText(i.ToString());
            richTextBox1.AppendText("- циклический код\n\n");
            for (int i = 0; i < fx_bin.Length; i++)
            {
                richTextBox1.AppendText(i + 1 + " - ");
                for (int j = 0; j < pos_tab_pol[i].Length; j++)
                {
                    richTextBox1.AppendText(pos_tab_pol[i][j].ToString() + " ");
                }
                richTextBox1.AppendText("\n");
            }
            foreach (int i in mis_bin) richTextBox2.AppendText(i.ToString());
            richTextBox2.AppendText(" - код с ошибкой\n");
            foreach (int i in mis_simpt) richTextBox2.AppendText(i.ToString() + " ");
            richTextBox2.AppendText(" - симптом ошибки\n");
            richTextBox2.AppendText((mis_pos_calc + 1).ToString() + " - позиция ошибки");
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
            arr_copy(ref res, Calc_pol(res));
            return res;
        }

        int[] div_pol(int[] pol1, int[] pol2)
        {
            
            int[] residue = new int[pol1.Length]; //остаток
            arr_copy(ref residue, pol1);
            int[] vrem = new int[pol2.Length];
            int[] vrem2 = new int[pol2.Length];
            while (residue[0] >= pol2[0])
            {
                arr_copy(ref vrem2, pol2);
                arr_copy(ref vrem, mult_pol(vrem2, residue[0] - vrem2[0]));
                arr_copy(ref residue, minus_pol(residue, vrem));
            }
            return residue;
        }
    }
}
