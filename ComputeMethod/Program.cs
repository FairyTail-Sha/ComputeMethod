using System;


namespace ComputeMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] m = new double[3, 4]
            {
                { 1,2,3,14},
                { 2,5,2,18},
                { 3,1,5,20}
            };
            double[] res;
            Unit3 sd = new Unit3();
            sd.TaiangleDecompose(m, out res);

            #region 测试调用
            //double n = 0;
            //n = double.Parse(Console.ReadLine());
            //Function m = new Function(Function2_BS);
            //m.function(n, out double w);
            //m.FuncDerF(n, out double f,6);
            //m.FuncDerS(n, out double s,6);
            //Console.WriteLine($"函数求职f({n}) = {w};" +
            //    $"函数求导f'({n}) = {f};f\"({ n}) = {s}");
            //Console.WriteLine("hello world");
            //FuncImage funcImage = new FuncImage();
            //funcImage.ShowDialog();
            ////1-1二分法
            //Console.WriteLine("第二章，非线性方程求根");
            //Console.WriteLine("二分法");
            //Unit2 bs = new Unit2();
            //bs.Bs(2,3,0.001,new FuncOfOneVar(Function2_BS));
            ////1-2牛顿法
            //Console.WriteLine("牛顿法");
            //Newton nt = new Newton(0.00001, 100);
            //nt.Nt();

            //Console.WriteLine("\n第三章，线性方程组解法");
            ////2-0高斯消元法 列主消元法 约当消元法
            //Console.WriteLine("高斯列主消元法");
            //高斯 ga = new 高斯();
            //const int n = 3;
            //double[,] matrix = new double[n, n + 1]
            //     {
            //         {-3,2,6,4},
            //         {10,-7,0,7},
            //         {5,-1,5,6}
            //     };
            //ga.高斯列主消去法(matrix);

            ////3-0 追赶法 
            //Console.WriteLine("追赶法");
            //追赶 test = new 追赶();
            //test.追赶法();
            ////3 - 1高斯赛德尔
            //Console.WriteLine("Gauss-Seidel");
            //高斯 ga2 = new 高斯();
            //ga2.GaussSeidel();

            ////第四章插值与拟合题目，参数矩阵
            //Unit4 La = new Unit4();
            //double tla = 11.75;
            //double[,] txy = new double[2, 3]{
            //    {11,12,13 },
            //    {2.3979,2.4849,2.5649 }
            //    };

            ////两点
            //La.Language2Point(tla, txy);
            ////三点
            //La.Language3Point(tla, txy);
            ////上机作业
            ////牛顿差值与拉格朗日差值
            ////插值 chazhi = new 插值();
            ////chazhi.cha();

            //////第五章数值积分
            ////上机作业
            //Unit5 a = new Unit5();
            ////梯形
            //a.Tixing(0, 1);
            ////辛普生
            //a.Simpson();
            ////习题五 9
            ////a.QuBanTiXing(2, 8, 0.000005);
            //Console.WriteLine("ln2: {Math.Log(2)}");
            //a.QuBanTiXing(1, 3, 0.00001, new FuncOfOneVar(Function5_11));
            ////龙贝格
            //FuncOfOneVar F1 = new FuncOfOneVar(Function5_10);
            //a.Romberg(1, 3, F1);
            #endregion
        }
        #region 调用函数列表
        public static bool Function2_BS(double x, out double y)
        {
            y = x * x * x - 2 * x - 5;
            return true;
        }
        public static bool Function5_10(double x, out double y)
        {
            if (x != 0)
            {
                y = 1 / x;
                return true;
            }
            else
                y = x;
            return false;
        }
        public static bool Function5_11(double x, out double y)
        {
            if (x < 0)
            {
                y = -1;
                return false;
            }
            else
            {
                y = Math.Pow(x, 0.5);
                return true;
            }
        }
        #endregion
    }
}
