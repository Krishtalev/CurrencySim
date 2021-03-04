using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cur
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const double k = 0.05;
        public Wallet account = new Wallet();
        private void btStart_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            
            double rate = (double)edRate.Value;
            int days = (int)edDays.Value;
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(0, rate);

            for (int i = 1; i <= days; i++)
            {
                rate = rate * (1 + k * (random.NextDouble() - 0.5));
                edRate.Value = (decimal)rate;
                chart1.Series[0].Points.AddXY(i, rate);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double rate = (double)edRate.Value;
            double percent = (double)edPercent.Value;
            double val = (double)edValue.Value;
            account.buyDollars(val, rate, percent);
            labelDollars.Text = "Мои доллары: " + account.myDollars;
            labelRubles.Text = "Мои рубли: " + account.myRubles;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double rate = (double)edRate.Value;
            double percent = (double)edPercent.Value;
            double val = (double)edValue.Value;
            account.sellDollars(val, rate, percent);
            labelDollars.Text = "Мои доллары: " + account.myDollars;
            labelRubles.Text = "Мои рубли: " + account.myRubles; ;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double rate = (double)edRate.Value;
            double percent = (double)edPercent.Value;
            account.buyAllDollars(rate, percent);
            labelDollars.Text = "Мои доллары: " + account.myDollars;
            labelRubles.Text = "Мои рубли: " + account.myRubles;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double rate = (double)edRate.Value;
            double percent = (double)edPercent.Value;
            account.sellAllDollars(rate, percent);
            labelDollars.Text = "Мои доллары: " + account.myDollars;
            labelRubles.Text = "Мои рубли: " + account.myRubles;
        }
    }

    public class Wallet
    {
        public double myRubles = 1000;
        public double myDollars = 0;
        public void buyDollars(double val, double Rate, double percent)
        {
            double cost = val * Rate * (100+percent)/100;
            if (myRubles >= cost)
            {
                myRubles -= cost;
                myDollars += val;
            }
        }
        public void sellDollars(double val, double Rate, double percent)
        {
            if (myDollars >= val)
            {
                myDollars -= val;
                myRubles += val*Rate*(100-percent)/100;
            }
        }
        public void buyAllDollars(double Rate, double percent)
        {
            double val = myRubles / Rate;
            double cost = val * Rate * (100 + percent) / 100;
            while (cost>myRubles && val>0)
            {
                val -= 0.01;
                cost = val * Rate * (100 + percent) / 100;
            }

            if (val > 0) buyDollars(val, Rate, percent);
        }
        public void sellAllDollars(double Rate, double percent)
        {
            double val = myDollars;
            sellDollars(val, Rate, percent);
        }
    }
}
