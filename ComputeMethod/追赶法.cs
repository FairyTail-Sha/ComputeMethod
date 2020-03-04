using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputeMethod
{
    class 追赶
    {
        //Ax = B
        //B
        double[] r = new double[4] { 1, 2, -2, 0 };
        //采用三个数组代替矩阵表示
        double[] a = new double[3] { 1, 1, 2 };
        double[] b = new double[4] { 2, 3, 1, 1 };
        double[] c = new double[3] { 1, 1, 1 };

        public void 追赶法()
        {
        
            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    b[0] = b[0];
                    c[0] = c[0] / b[0];
                }
                else
                {
                    b[i] = b[i] - a[i - 1] * c[i - 1];
                    if(i<3)
                        c[i] = c[i] / b[i];
                }
            }

            double[,] m = new double[4, 5];
            double[,] n = new double[4, 5];
            for (int i = 0; i < 4; i++)
            {
                m[i, 4] = r[i];
                for (int j = 0; j < 4; j++)
                {

                    if (i == j)
                    {
                        m[i, j] = b[i];
                        n[i,j] = 1;
                    }
                    else if (i == j - 1)
                        n[i, j] = c[i];
                    else if (i - 1 == j)
                        m[i, j] = a[j];
                }
            }


            //计算未知数
            double[] res = new double[4];
            //计算y
            for (int i = 0; i <4; i++)
            {
                double sum = 0;
                for (int j = 0; j < i; j++)
                {
                    sum += n[j,4] * m[i, j];
                }
                n[i,4] = (m[i, 4] - sum) / m[i, i];
            }

            for (int i = 3; i >= 0; i--)
            {
                double sum = 0;
                for (int j = 3; j > i; j--)
                {
                    sum += res[j] * n[i, j];
                }
                res[i] = (n[i, 4] - sum) / n[i, i];
               // Console.Write(res[i] + "\n");
            }

            foreach (double a in res)
            {
                Console.WriteLine(a);
            }
            
        }
    }
}
