namespace GTLabs
{
    class Matrix
    {
        private double[][] _matrix;

        public int Rows
        {
            get
            {
                return _matrix.Length;
            }
        }
        public int Columns
        {
            get
            {
                return _matrix[0].Length;
            }
        }

        public double this[int i, int j]
        {
            get
            {
                return _matrix[i][j];
            }
            set
            {
                _matrix[i][j] = value;
            }
        }


        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A.Columns != B.Rows) throw MatrixMultiplicationException.Create(A,B);
            var result = new Matrix(A.Rows, B.Columns);
            for (int i = 0; i < A.Rows; i++)
            {
                for (int j = 0; j < B.Columns; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < A.Columns; k++)
                    {
                        result[i, j] += A[i, k] * B[k, j];
                    }
                }
            }
            return result;
        }

        public static Matrix operator *(double lambda, Matrix A)
        {
            var result = new Matrix(A.Rows, A.Columns);
            for (int i = 0; i < A.Rows; i++)
            {
                for (int j = 0; j < A.Columns; j++)
                {
                    result[i, j] = lambda * A[i, j];
                }
            }
            return result;
        }

        public static Matrix UnitRow(int length)
        {
            return new Matrix(1, length, 1);
        }

        public static Matrix UnitColumn(int length)
        {
            return new Matrix(length, 1, 1);
        }

        public static Matrix Identity(int n)
        {
            if (n == 0) return null;
            Matrix result = new Matrix(n, n);
            for (int i = 0; i < n; i++)
            {
                result[i, i] = 1;
            }
            return result;
        }

        public void SwapRows(int i, int j)
        {
            var tmp = _matrix[i];
            _matrix[i] = _matrix[j];
            _matrix[j] = tmp;
        }

        public void SumRows(int i, int j)
        {
            for (int k = 0; k < Columns; k++)
            {
                this[i, k] += this[j, k];
            }
        }

        public void LinearCombRows(int i, int j, double lambda, double mu)
        {
            for (int k = 0; k < Columns; k++)
            {
                this[i, k] = lambda * this[i, k] + mu * this[j, k];
            }
        }

        public void MultiplyRow(int i, double lambda)
        {
            for (int k = 0; k < Columns; k++)
            {
                this[i, k] *= lambda;
            }
        }

        public Matrix Inverse()
        {
            if (Rows != Columns) throw MatrixInvertException.Create(this, MatrixInvertException.ExceptionCause.NotSquare);
            double lambda = 1;
            var copy = new Matrix(this);
            var result = Matrix.Identity(Rows);
            for (int i = 0; i < result.Rows; i++)
            {
                int count = i;
                while (copy[i, i].Equals6DigitPrecision(0) && count < Rows - 1)
                {
                    count++;
                    copy.SwapRows(i, count);
                    result.SwapRows(i, count);
                }
                if (copy[i, i].Equals6DigitPrecision(0))
                {
                    throw MatrixInvertException.Create(this, MatrixInvertException.ExceptionCause.Singular);
                }

                lambda = 1 / copy[i, i];
                copy.MultiplyRow(i, lambda);
                result.MultiplyRow(i, lambda);

                for (int j = i + 1; j < Columns; j++)
                {
                    if (copy[j, i].Equals6DigitPrecision(0)) continue;
                    lambda = -1 / copy[j, i];
                    copy.MultiplyRow(j, lambda);
                    result.MultiplyRow(j, lambda);
                    copy.SumRows(j, i);
                    result.SumRows(j, i);
                }
            }

            for (int j = Columns - 1; (int)j >= 0; j--)
            {

                for (int i = j - 1; (int)i >= 0; i--)
                {
                    if (copy[i, j].Equals6DigitPrecision(0)) continue;
                    lambda = -copy[i, j];
                    copy.LinearCombRows(i, j, 1, lambda);
                    result.LinearCombRows(i, j, 1, lambda);
                }
            }
            return result;
        }

        public double[] Row(int i)
        {
            if (i >= Rows) return null;
            return _matrix[i];
        }

        public double[] Column(int j)
        {
            if (j >= Columns) return null;
            var result = new double[Rows];
            for(int i=0; i<Rows; i++)
            {
                result[i] = _matrix[i][j];
            }
            return result;
        }

        public Matrix(int rows, int columns, double val = 0)
        {
            _matrix = new double[rows][];
            for(int i = 0; i < rows; i++)
            {
                _matrix[i] = new double[columns];
            }
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this[i,j] = val;
                }
            }
        }

        public Matrix(Matrix M)
        {
            _matrix = new double[M.Rows][];
            for (int i = 0; i < M.Rows; i++)
            {
                _matrix[i] = new double[M.Columns];
            }
            for (int i = 0; i < M.Rows; i++)
            {
                for (int j = 0; j < M.Columns; j++)
                {
                    this[i, j] = M[i,j];
                }
            }
        }
                
    }
}
