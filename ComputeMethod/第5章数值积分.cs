using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputeMethod
{
    class Unit5
    {
        //第五章数值积分 a = new 第五章数值积分();
        //a.Tixing();
        //a.Simpson();
        static double[,] table = new double[2, 11]
        {
            {0,0.1,0.2,0.3,0.4,0.5,0.6,0.7,0.8,0.9,1},
            {1,1.004971,1.019536,1.042668,1.072707,1.107432,1.144157,1.179859,1.211307,1.235211,1.248375}
        };

        double h;
        int n;
        //梯形
        public void Tixing(double a,double b)
        {
            n = 10;
            h = (b-a)/10;
            double sum = 0;
            for (int i = 1; i<n; i++)
            {
                sum += f(0 + i*h);
            }
            double res = h/2 * (f(0) + f(1) + sum * 2);
            Console.WriteLine("梯形公式 " + res);
        }
        //Simpson
        public void Simpson()
        {
            h = 0.1;
            double s = 0;
            for (int i = 0; i<10; i++)
            {
                s += f(i*h) + 4 * f(i * h + h / 2) + f(i*h + h);
            }
            Console.WriteLine("Simpson " + s * (h / 6));
        }

        private double f(double x)
        {
            return Math.Cos(x) + Math.Sin(x) * Math.Sin(x);
        }

        //逐次分半加速
        public void QuBanTiXing(double a, double b,double ep,FuncOfOneVar F)
        {
            n = 1;
            double Tn,T2n;
            Tn = 0;
            while(true)
            {                
                this.TiXing(a, b, n,out T2n,F);
                Console.WriteLine("n: {" + n +"}\tT2n: {"+T2n+"+}\t|T2n-Tn|: {"+Math.Abs(T2n - Tn)/3+"}");
                if (Math.Abs((T2n-Tn)/3) > ep)
                {
                    Tn = T2n;
                    n = n * 2;
                }
                else
                {
                    break;
                }
            }
            

        } 
        private double Function(double x)
        {
            //Page130例题4
            //if (x == 0) return 1;
            //else
            //    return Math.Sin(x) / x;
            //习题五 9
            //return 1.0 / (2 * x);
            //习题五 11
            //return Math.Sqrt(x);
            //return 4.0 / (1.0 + x * x);
            if (x == 0) return -1;
            return 1 / x;
        }
        private bool TiXing(double a, double b, int n, out double res,FuncOfOneVar F)
        {
            h = (b - a) / n;
            double sum = 0;
            F(a, out double fa);
            F(b, out double fb);
            for (int i = 1; i < n; i++)
            {
                if (F(a + i * h, out double s))
                    sum += s;
                else
                {
                    Console.WriteLine($"错误：梯形公式计算错误");
                    res = 0;
                    return false;
                }
            }
            res = h / 2 * (fa + fb + sum * 2);
            return true;
        }
        //龙贝格表格
        public void Romberg(double a,double b,FuncOfOneVar F)
        {
            double[,] TCSR = new double[20,6];
            
            for(int i = 0;i<5;i++)
            {
                TCSR[i, 0] = Math.Pow(2, i);
                if(TiXing(a,b,(int)TCSR[i,0],out double y,F))
                {
                    TCSR[i, 1] = y;
                }
                if (i > 0)
                {
                    TCSR[i, 2] = (4 * TCSR[i, 1] - TCSR[i - 1, 1]) / 3;
                    if(i>1)
                    {
                        TCSR[i, 3] = (16 * TCSR[i, 2] - TCSR[i - 1, 2]) / 15;
                        if (i>2)
                        {
                            TCSR[i, 4] = (64 * TCSR[i, 3] - TCSR[i - 1, 3]) / 63;
                        }
                    }
                }                
            }
            for(int i = 0;i<5;i++)
            {
                Console.WriteLine($"i: {i}\tn: {TCSR[i, 0]}\t" +
                    $"T: {TCSR[i, 1]}\tS: {TCSR[i, 2]}\t" +
                    $"C: {TCSR[i, 3]}\tR: {TCSR[i, 4]}");
            }

        }
    }
}
