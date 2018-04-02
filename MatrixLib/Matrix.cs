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
        public IEnumerable<(int, int)> ArgmaxColumns => Enumerable.Range(0, Columns).Select((i) => (GetColumn(i).ArgMax(), i ));
        public IEnumerable<(int, int)> ArgmaxRows => Enumerable.Range(0, Rows).Select((i) => (i, GetRow(i).ArgMax()));

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
        public Matrix(T[,] matrix)
        {
            _matrix = matrix;
        }
    }
}
