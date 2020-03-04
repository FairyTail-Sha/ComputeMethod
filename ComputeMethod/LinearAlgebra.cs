namespace Code
{
    public class LinearAlgebra
    {
        int Row;
        int Column;
        ///行处理化为阶梯矩阵
        public void RowSoluting(double[,] varAlgebra,out double[,] resAlgebra)
        {
            this.Row = varAlgebra.GetLength(0);
            this.Column = varAlgebra.GetLength(1);
            //申请空间
            double[,] array = new double[this.Row,this.Column];
            //复制矩阵
            for(int i = 0; i<this.Row; i++)
            {
                for(int j = 0; j<this.Column; j++)
                {
                    array[i,j] = varAlgebra[i,j];
                }
            }   
            int current = 0;
            while(0 <= current && current <= this.Row)
            {
                //对k+1行到n行进行处理
                for(int r = current + 1; r<this.Row; r++ )
                {
                    double m = array[r,current]/array[current,current];
                    //对c行的current到n列处理
                    if(m!=0)
                    {
                        for(int c = current; c<this.Column; c++ )
                        {
                            array[r,c] -= array[current,c] * m;
                        }
                    }
                }
                current++;
            }
            resAlgebra = array;
        }

        //calculate unknown
        ///前提化为阶梯行列式
        public void C(double[,] varAlgebra,bool directionOfCalculate, out double[] Algebra)
        {
            Algebra = new double[varAlgebra.GetLength(0)];
            //判断上处理还是下处理
            if(directionOfCalculate)//directionOfCount为真，正常自下而上求
            {
                for(int i = varAlgebra.GetLength(0) - 1; i >= 0 ; i--)
                {
                    double sum = 0;
                    for(int j = varAlgebra.GetLength(0) - 1; j > i; j--)
                    {
                        sum += Algebra[j] * varAlgebra[i, j];
                    }
                    Algebra[i] = (varAlgebra[i, varAlgebra.GetLength(1) - 1] - sum) / varAlgebra[i, i]; 
                }
            }else{//自上而下
                for(int i = varAlgebra.GetLength(0) - 1; i >= 0 ; i--)
                {
                    double sum = 0;
                    for(int j = varAlgebra.GetLength(0) - 1; j > i; j--)
                    {
                        sum += Algebra[j] * varAlgebra[i, j];
                    }
                    Algebra[i] = (varAlgebra[i, varAlgebra.GetLength(1) - 1] - sum) / varAlgebra[i, i]; 
                }
            }
        }
    }
}