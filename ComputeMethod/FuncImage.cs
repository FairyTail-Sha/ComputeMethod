using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ComputeMethod
{
    public partial class FuncImage : Form
    {
        public FuncImage()
        {
            InitializeComponent();
        }

        private void FuncImage_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pen blackpen = new Pen(Color.Black, (float)0.5);
            Point point1;
            Point point2;
            Function Func = new Function(Function2_BS);
            int center = 200;
            int x = -100 * Int32.Parse(Math.Round(Math.PI / 2).ToString());
            double yk = 100;
            double xk = 0.01;

            Graphics g = this.CreateGraphics();
            g.DrawLine(blackpen, new Point(0, center), new Point(center * 2, center));
            g.DrawLine(blackpen, new Point(center, center * 2), new Point(center, 00));
            Func.Func((double)x * xk, out double y);
            point1 = new Point(center - x, center - Int32.Parse(Math.Round(y * yk).ToString()));
            do
            {
                point2 = point1;
                Func.Func((double)x * xk, out double y1);
                int m = Int32.Parse(Math.Round(y1 * yk).ToString());
                point1 = new Point(center - x, center - m);
                g.DrawLine(blackpen, point1, point2);
                x++;
            } while (x < 100 * Int32.Parse(Math.Round(Math.PI / 2).ToString()));
        }
        public static bool Function2_BS(double x, out double y)
        {
            y = Math.Cos(x);
            return true;
        }
    }
}
