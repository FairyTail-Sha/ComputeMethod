using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace ComputeMethod
{
    internal delegate bool FuncOfOneVar(double x, out double y);
    class Function
    {
        //函数主体
        //function是readonly的满足计算条件
        public readonly FuncOfOneVar function;
        //函数范围，取值区间
        private double valueMin;
        private bool minAllow;
        private double valueMax;
        private bool maxAllow;

        //构造函数
        public Function(FuncOfOneVar mainFunction)//构造函数1
        {
            valueMin = double.MinValue;
            valueMax = double.MaxValue;
            function = mainFunction;
        }
        public Function(FuncOfOneVar mainFunction, double min, double max)//构造函数2
        {
            function = mainFunction;
            valueMin = min;
            minAllow = false;
            valueMax = max;
            maxAllow = false;
        }
        public Function(FuncOfOneVar _Function, double _min, bool _minAllow, double _max, bool _maxAllow)//构造函数2
        {
            function = _Function;
            valueMin = _min;
            minAllow = _minAllow;
            valueMax = _max;
            maxAllow = _maxAllow;
        }
        //Calculating function value
        public void Func(double num, out double result)
        {
            if (IsPermitted(num))
                function(num, out result);
            else
            {
                result = 0;
                Console.WriteLine($"Error: Func -- the x isn't permitted!");
            }
        }
        //the x is or isn't permitted
        private bool IsPermitted(double x)
        {
            if (x == valueMax && maxAllow) return true;
            else if (x == valueMin && minAllow) return true;
            else if (valueMin < x && x < valueMax)
                return true;
            else
                return false;
        }

        //数值导数计算
        public void FuncDerF(double x, out double y, double ep)//一阶导数
        {
            if (IsPermitted(x))
            {
                y = 0;
                if (ep != 0)
                    ep = Math.Pow(10, -ep);
                else
                    ep = Math.Pow(10, -6);//默认精确度
                double r1 = -1, r2 = 0, h = 1;
                while (Math.Abs(r1 - r2) > ep)
                {
                    r2 = r1;
                    h = h / 2;
                    function(x + h, out double y1);
                    function(x, out double y2);
                    r1 = (y1 - y2) / h;
                    //Console.WriteLine($"r1:{r1}; r2:{r2}");
                }
                y = r1;
            }
            else
            {
                y = 0;
                Console.WriteLine("Error:FuncDerF -- the x isn't permitted!");
            }
        }
        public void FuncDerF(double x, out double y)//一阶导数
        {
            if (IsPermitted(x))
            {
                y = 0;
                double ep = Math.Pow(10, -6);//默认精确度
                double r1 = -1, r2 = 0, h = 1;
                while (Math.Abs(r1 - r2) > ep)
                {
                    r2 = r1;
                    h = h / 2;
                    function(x + h, out double y1);
                    function(x, out double y2);
                    r1 = (y1 - y2) / h;
                    //Console.WriteLine($"r1:{r1}; r2:{r2}");
                }
                y = r1;
            }
            else
            {
                y = 0;
                Console.WriteLine("Error:FuncDerF -- the x isn't permitted!");
            }
        }
        public void FuncDerS(double x, out double y, double ep)//二阶导数
        {
            if (!IsPermitted(x))
            {
                y = 0;
                Console.WriteLine("SError:FuncDer -- the x isn't permitted!");
            }
            else
            {
                y = 0;
                if (ep != 0)
                    ep = Math.Pow(10, -ep);
                else
                    ep = Math.Pow(10, -6);//默认精确度
                double r1 = -1, r2 = 0, h = 1;
                while (Math.Abs(r1 - r2) > ep)
                {
                    r2 = r1;
                    h = h / 2;
                    FuncDerF(x + h, out double y1, ep);
                    FuncDerF(x, out double y2, ep);
                    r1 = (y1 - y2) / h;
                    //Console.WriteLine($"h:{h}; r1:{r1}; r2:{r2}");
                }
                y = r1;
            }
        }
        public void FuncDerS(double x, out double y)//二阶导数
        {
            if (!IsPermitted(x))
            {
                y = 0;
                Console.WriteLine("SError:FuncDer -- the x isn't permitted!");
            }
            else
            {
                y = 0;
                double ep = Math.Pow(10, -6);//默认精确度
                double r1 = -1, r2 = 0, h = 1;
                while (Math.Abs(r1 - r2) > ep)
                {
                    r2 = r1;
                    h = h / 2;
                    FuncDerF(x + h, out double y1, ep);
                    FuncDerF(x, out double y2, ep);
                    r1 = (y1 - y2) / h;
                    //Console.WriteLine($"h:{h}; r1:{r1}; r2:{r2}");
                }
                y = r1;
            }
        }

        //数值积分
        private bool TiXing(double a, double b, int n, out double res)
        {
            double h = (b - a) / n;
            double sum = 0;
            function(a, out double fa);
            function(b, out double fb);
            for (int i = 1; i < n; i++)
            {
                if (function(a + i * h, out double s))
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
        ////龙贝格表格
        public void Romberg(double a, double b)
        {
            double[,] TCSR = new double[20, 6];

            for (int i = 0; i < 5; i++)
            {
                TCSR[i, 0] = Math.Pow(2, i);
                if (TiXing(a, b, (int)TCSR[i, 0], out double y))
                {
                    TCSR[i, 1] = y;
                }
                if (i > 0)
                {
                    TCSR[i, 2] = (4 * TCSR[i, 1] - TCSR[i - 1, 1]) / 3;
                    if (i > 1)
                    {
                        TCSR[i, 3] = (16 * TCSR[i, 2] - TCSR[i - 1, 2]) / 15;
                        if (i > 2)
                        {
                            TCSR[i, 4] = (64 * TCSR[i, 3] - TCSR[i - 1, 3]) / 63;
                        }
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"i: {i}\tn: {TCSR[i, 0]}\t" +
                    $"T: {TCSR[i, 1]}\tS: {TCSR[i, 2]}\t" +
                    $"C: {TCSR[i, 3]}\tR: {TCSR[i, 4]}");
            }

        }

        //函数图像
        public void DrawImage()
        {

        }
    }
}
