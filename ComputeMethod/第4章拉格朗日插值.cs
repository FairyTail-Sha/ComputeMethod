using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputeMethod
{
    class Unit4
    {
        


        //string s = null;
        //for(int i = 0;i<=10;i++)
        //{
        //    s += Console.ReadLine() + ",";
        //}
        //Console.Write(s);
        //lagrange两点
        public void Language2Point(double test, double[,] txy)
        {
            //lagrange线性插值
            //两点插值函数
            double l0 = (test - txy[0, 1]) / (txy[0, 0] - txy[0, 1]);
            double l1 = (test - txy[0, 0]) / (txy[0, 1] - txy[0, 0]);

            double result = l0 * txy[1, 0] + l1 * txy[1, 1];

            Console.WriteLine("ln(11.75) = " + Math.Log(11.75));
            Console.WriteLine("lagrange线性插值 result = " + result);

        }
        //lagrange二次插值
        public void Language3Point(double test, double[,] txy)
        {
            //lagrange二次插值
            double l0 = ((test - txy[0, 1]) * (test - txy[0, 2])) / ((txy[0, 0] - txy[0, 1]) * (txy[0, 0] - txy[0, 2]));
            double l1 = ((test - txy[0, 0]) * (test - txy[0, 2])) / ((txy[0, 1] - txy[0, 0]) * (txy[0, 1] - txy[0, 2]));
            double l2 = ((test - txy[0, 0]) * (test - txy[0, 1])) / ((txy[0, 2] - txy[0, 0]) * (txy[0, 2] - txy[0, 1]));

            double result = l0 * txy[1, 0] + l1 * txy[1, 1] + l2 * txy[1, 2];

            Console.WriteLine("lagrange二次插值 result = " + result);
            Console.WriteLine("小结：二次插值之后的更加逼近");

        }           

        //拉格朗日插值
        public static void MethodLa(double[,] P, double x, out double y)
        {
            //y = f(x)
            y = 0;
            for (int i = 0; i < P.GetLength(1); i++)
            {
                double L = 1;
                for (int j = 0; j < P.GetLength(1); j++)
                {
                    if (i != j)
                        L *= (x - P[0, j]) / (P[0, i] - P[0, j]);
                }
                y += L * P[1, i];
            }
        }
        //牛顿插值
        public static void MethodNe(double[,] nE, double x, out double y)
        {
            //构造差商表 table         
            //前两列 x, f(x)
            double[,] table = new double[nE.GetLength(1), nE.GetLength(1) + 1];
            for (int i = 0; i < nE.GetLength(1); i++)
            {
                table[i, 0] = nE[0, i];
                table[i, 1] = nE[1, i];
            }
            //各阶差商
            for (int j = 2, K = 1; j < table.GetLength(1); j++, K++)
            {
                for (int i = j - 1; i < table.GetLength(0); i++)
                {
                    double f = (table[i, j - 1] - table[i - 1, j - 1]);
                    double x90 = (table[i, 0] - table[i - K, 0]);
                    table[i, j] = f / x90;
                }
            }
            //核心
            y = table[0, 1];
            for (int j = 1; j < table.GetLength(0); j++)
            {
                double sum = 1;
                for (int i = 0; i < j; i++)
                {
                    sum *= (x - table[i, 0]);
                }
                y += sum * table[j, j + 1];
            }
        }
        //
    }
}
