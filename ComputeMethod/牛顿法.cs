using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    class Newton
    {
        private double wucha;

        public double Wucha
        {
            get { return wucha; }
            set { wucha = value; }
        }
        private int maxN;

        public int MaxN
        {
            get { return maxN; }
            set { maxN = value; }
        }

        public Newton(double p, int p_2)
        {
            // TODO: Complete member initialization
            this.wucha = p;
            this.maxN = p_2;
        }

        public void Nt()
        {
            if (this.IsPNull())
            {
                Console.Write("未初始化");
                return; 
            }
            //f'(x) = 3*x*x - 3 
            //f(x) = x*x*x - 3*x - 1
            //迭代次数上限
            int N = this.maxN;
            //ep误差
            double wucha = 0.001;
            //控制运行flag
            int IP = 1;
            //记录运行次数
            int IC = 0;
            //设置变量
            double x0, x1;
            x0 = 2;
            x1 = 0;
            //牛顿法循环
            while (Math.Abs(x0 - x1) > this.wucha && IP == 1)
            {
                //一阶导数
                double W = 3 * x0 * x0 - 3;
                //判断是否满足定理四 牛顿迭代格式收敛
                if (Math.Abs(W) <= wucha)//不满足
                {
                    IP = -1;
                }
                else
                {
                    //f（x0）
                    double M = x0 * x0 * x0 - 3 * x0 - 1;
                    //迭代 x1 = g(x0)
                    x1 = x0 - M / W;
                    //△x
                    double absX = Math.Abs(x1 - x0);
                    //计数加一
                    IC += 1;
                    //是否超出迭代次数上限
                    if (IC > N)//超出
                    {
                        IP = -2;
                    }
                    else
                    {
                        //x0 x1 交换 循环
                        double temp = x0;
                        x0 = x1;
                        x1 = temp;
                        //测试
                        //Console.Write(x0.ToString() + "\n");
                    }
                }
            }
            //输出保留四位
            Console.Write(x0.ToString("G4"));
        }
        //判断是否初始化
        private bool IsPNull()
        {
            if (wucha >=0 || maxN <= 0)
                return true;
            else return false;
        }
    }
}
