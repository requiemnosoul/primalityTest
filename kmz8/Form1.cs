using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kmz8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox1.Text);
            textBox2.Text = Atkin(n);
        }
        string Atkin(int limit)
        {
            string res = "";
            bool[] is_prime = new bool[limit+1];
            int n;

            int sqr_lim = (int)Math.Sqrt(limit);
            for (int i = 0; i <= limit; i++) is_prime[i] = false;
            is_prime[2] = true;
            is_prime[3] = true;

            int x2 = 0;
            for (int i = 1; i <= sqr_lim; i++)
            {
                x2 += 2 * i - 1;
                int y2 = 0;
                for (int j = 1; j <= sqr_lim; j++)
                {
                    y2 += 2 * j - 1;

                    n = 4 * x2 + y2;
                    if ((n <= limit) && (n % 12 == 1 || n % 12 == 5))
                        is_prime[n] = !is_prime[n];
                    // n = 3 * x2 + y2; 
                    n -= x2;
                    if ((n <= limit) && (n % 12 == 7))
                        is_prime[n] = !is_prime[n];
                    // n = 3 * x2 - y2;
                    n -= 2 * y2;
                    if ((i > j) && (n <= limit) && (n % 12 == 11))
                        is_prime[n] = !is_prime[n];
                }
            }
            for (int i = 5; i <= sqr_lim; i++)
            {
                if (is_prime[i])
                {
                    n = i * i;
                    for (int j = n; j <= limit; j += n)
                    {
                        is_prime[j] = false;
                    }
                }
            }
            for (int i = 2; i <= limit; i++)
            {
                if ((is_prime[i]))
                {
                    res += i+ " ";
                }
            }

            return res;
        }
        bool MilRab(long n) // Тест Миллера-Рабина на простое число
        {
            Random rnd = new Random();
            long a, s = 0;
            long k = Convert.ToInt32(Math.Log(n, 2));
            long t = n - 1;
            while (t % 2 == 0)
            {
                t /= 2;
                s++;
            }
            for (int i = 0; i < k; i++)
            {
                a = rnd.Next(2, (int)n - 1);
                long x = (long)BigInteger.ModPow(a, t, n);
                if (x == 1 || x == n - 1)
                    continue;
                for (int j = 0; j < s - 1; j++)
                {
                    x = (long)BigInteger.ModPow(x, 2, n);
                    if (x == 1)
                        return false;
                    if (x == n - 1)
                        break;
                }
                if (x != n - 1)
                    return false;
            }
            return true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (MilRab(Convert.ToInt64(textBox1.Text)) == true)
                label1.Text = "Вероятностно простое";
            else
                label1.Text = "Составное";
        }
    }
}
