using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputeMethod
{
    class Unit2
    {       
        //二分法计算
        public void Bs(double a, double b,double ep,FuncOfOneVar function)
        {
            //
            int num = 2;
            //二分循环
            while (b - a >= ep)
            {
                num *= 2;
                double x = (b + a) / 2.0;

                if (function(x,out double y1) && function(a, out double y2) && y1*y2 > 0)
                {
                    a = x;
                }
                else
                {
                    b = x;
                }
            }
            //生成计算误差 
            //生成计算结果
            //生成输出字符串
            Console.WriteLine($"\t计算误差： {(b - a) / num}\t计算结果: {(b + a) / 2.0}");
        }
        //牛顿法需要先对函数进行处理变形，我处理不了成普遍的通用程序，只得针对确定题目进行处理
    }
}