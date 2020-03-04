using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputeMethod
{
    class Unit3
    {

        #region 高斯消去法
        /// <summary>
        /// 高斯消去法
        /// </summary>
        /// <param name="mar"></param>
        public void 高斯消去法(double[,] mar)//mar矩阵 Nx矩阵列数 Ny矩阵行数
        {
            int Ny = mar.GetLength(0);
            int Nx = mar.GetLength(1);
            int k = 0;
            //循环
            while (0 <= k && k < Ny - 1)//处理行
            {
                for (int i = k + 1; i < Ny; i++){
                    double m = mar[i, k] / mar[k, k];
                    if (m > 0 || m < 0)
                    {
                        for (int j = k; j < Nx; j++)
                        {
                            mar[i, j] = mar[i, j] - mar[k, j] * m;
                        }
                    }
                }
                k++;
            }
            //计算未知数
            double[] res = new double[Ny];

            for (int i = Ny - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = Ny - 1; j > i; j--)
                {
                    sum += res[j] * mar[i, j];
                }
                res[i] = (mar[i, Nx - 1] - sum) / mar[i, i];
            }
            //输出结果
            foreach (int a in res)
            {
                Console.WriteLine(a);
            }

        }
        #endregion

        #region 列主消元法 将矩阵进行处理 column-pivoting elimination
        /// <summary>
        /// 列主消元法 将矩阵进行处理 column-pivoting elimination
        /// </summary>
        /// <param name="mar">问题矩阵</param>
        public void 高斯列主消去法(double[,] mar)
        {
            int Ny = mar.GetLength(0);
            int Nx = mar.GetLength(1);
            int k = 0;
            while (k < Ny)
            {

                //找寻绝对值最大元素行
                int maxLine = k;
                double max = Math.Abs(mar[k, k]);
                for (int i = k + 1; i < Ny; i++)
                {
                    if (max < Math.Abs(mar[i, k]))
                    {
                        maxLine = i;
                        max = Math.Abs(mar[i, k]);
                    }
                }


                if (maxLine != k)
                    for (int i = 0; i < Nx; i++)
                    {
                        double m = mar[maxLine, i];
                        mar[maxLine, i] = mar[k, i];
                        mar[k, i] = m;
                    }


                for (int i = k + 1; i < Ny; i++)
                {
                    //
                    double m = mar[i, k] / mar[k, k];
                    if (m > 0 || m < 0)
                    {
                        for (int j = k; j < Nx; j++)
                        {
                            mar[i, j] = mar[i, j] - mar[k, j] * m;
                        }
                    }
                }
                k++;
            }

            double[] res = new double[Ny];

            for (int i = Ny - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = Ny - 1; j > i; j--)
                {
                    sum += res[j] * mar[i, j];
                }
                double m = (mar[i, Nx - 1] - sum) / mar[i, i];
                res[i] = m;
            }

            foreach (double a in res)
            {
                Console.WriteLine(a);
            }
        }
        #endregion

        #region 高斯塞德尔
        /// <summary>
        /// 高斯塞德尔
        /// </summary>
        public void GaussSeidel()
        {
            //题目
            double[,] A = new double[3, 4]
            {
                {9,-2,1,6},
                {1,-8,1,-8},
                {2,-1,-8,9}
            };
            //结果矩阵
            double[] res = new double[3];
            //改写系数矩阵
            for (int i = 0; i < A.GetLength(0); i++)
            {
                double temp = A[i, i];
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    if (j != (A.GetLength(1) - 1))
                    {
                        A[i, j] = A[i, j] / temp * (-1.0);
                    }
                    else
                        A[i, j] = A[i, j] / temp;
                }
                A[i, i] = 0;
            }
            //迭代初始化
            bool flag = true;
            int k = 0;
            double[] maxk = new double[3];//记录中间结果
            //迭代
            while (flag)
            {
                //核心迭代
                double sum = 0;
                for (int j = 0; j < (A.GetLength(1) - 1); j++)
                {
                    sum += res[j] * A[k, j];
                }
                res[k] = sum + A[k, A.GetLength(1) - 1];
                k++;
                //每次所有未知数求解一遍后，判断是否满足题目输出要求
                if (k % 3 == 0)
                {
                    k = 0;
                    double max = 0;
                    //计算行范数
                    for (int i = 0; i < maxk.GetLength(0); i++)
                    {
                        maxk[i] = maxk[i] - res[i];
                        if (Math.Abs(maxk[i]) > max)
                            max = Math.Abs(maxk[i]);
                    }

                    if (max <= 0.0001)//满足
                        flag = false;
                    else//不满足
                        res.CopyTo(maxk, 0);
                }

            }
            //输出未知数
            foreach (double a in res)
            {
                Console.WriteLine(a);
            }

        }
        #endregion

        #region 矩阵分解法
        #region 三角分解法
        /// <summary>
        /// 三角分解法
        /// 解决方阵情况
        /// </summary>
        /// <param name="parameterMatrix">求解矩阵</param>
        /// <param name="resultMatrix">返回结果矩阵</param>
        public void TaiangleDecompose(double[,] parameterMatrix, out double[] resultMatrix)
        {
            double[,] L = new double[parameterMatrix.GetLength(0), parameterMatrix.GetLength(1) - 1];
            double[,] U = new double[parameterMatrix.GetLength(0), parameterMatrix.GetLength(1) - 1];
            for (int i = 0; i < parameterMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < parameterMatrix.GetLength(1) - 1; j++)
                {
                    if (i == j)
                        L[i, j] = 1;
                    U[0, j] = parameterMatrix[0, j];
                    L[i, 0] = parameterMatrix[i, 0] / U[0, 0];
                    double m = 0;

                    for (int k = 0; k <= i - 1; k++)
                    {
                        m += L[i, k] * U[k, j];
                    }
                    U[i, j] = parameterMatrix[i, j] - m;
                    m = 0;

                    L[i, j] = (parameterMatrix[i, j] - m) / U[j, j];


                }
            }

            for (int i = 0; i < parameterMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < parameterMatrix.GetLength(1) - 1; j++)
                {
                    Console.Write($"{L[i, j]}\t");
                }
                Console.Write("\n");
            }
            Console.WriteLine($"\n");
            for (int i = 0; i < parameterMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < parameterMatrix.GetLength(1) - 1; j++)
                {
                    Console.Write($"{U[i, j]}\t");
                }
                Console.Write("\n");
            }

            resultMatrix = new double[] { 0 };
        }
        #endregion

        #region 追赶法

        #endregion
        #endregion
    }
}
