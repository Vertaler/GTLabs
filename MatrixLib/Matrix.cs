using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTLabs.MatrixLib
{
    public class Matrix<T>
    {

        private T[,] _matrix;

        public int Rows => _matrix.GetLength(0);
        public int Columns => _matrix.GetLength(1);

        public IEnumerable<MatrixSlice<T>> AllRows => Enumerable.Range(0, this.Rows).Select((i) => GetRow(i));
        public IEnumerable<MatrixSlice<T>> AllColumns => Enumerable.Range(0, this.Columns).Select((i) => GetColumn(i));
        public IEnumerable<(int, int)> ArgmaxColumns
        {
            get
            {
                for(int i=0; i< Columns; i++)
                {
                    foreach(var j in GetColumn(i).ArgMax())
                    {
                        yield return (j, i);
                    }
                }
            }
        }
        public IEnumerable<(int, int)> ArgmaxRows
        {
            get
            {
                for (int i = 0; i < Rows; i++)
                {
                    foreach (var j in GetRow(i).ArgMax())
                    {
                        yield return (i, j);
                    }
                }
            }
        }

        public MatrixSlice<T> GetColumn(int i)
        {
            return new MatrixSlice<T>(this, i, MatrixSlice<T>.SliceType.Column);
        }
        public MatrixSlice<T> GetRow(int i)
        {
            return new MatrixSlice<T>(this, i, MatrixSlice<T>.SliceType.Row);
        }

        public T this[int i, int j]
        {
            get
            {
                return _matrix[i,j];
            }
            set
            {
                _matrix[i,j] = value;
            }
        }

        public override string ToString()
        {
            var resultBuilder = new StringBuilder();
            foreach(var row in AllRows)
            {
                resultBuilder.Append(row);
                resultBuilder.Append("\n");
            }
            return resultBuilder.ToString();
        }

        public Matrix(T[,] matrix)
        {
            _matrix = matrix;
        }

        public Matrix(int rows, int columns)
        {
            _matrix = new T[rows, columns];
        }
    }
}
