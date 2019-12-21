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
            gx = Calc_pol(kk);
            foreach (int i in gx) richTextBox1.AppendText(i.ToString());
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

        int[] mult_pol(int[] pol, int mult)
        {
            for (int i = 0; i < pol.Length; i++) pol[i] += mult;
            return pol;
        }
    }
}
