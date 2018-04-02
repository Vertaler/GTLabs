using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTLabs.MatrixLib
{
    public class MatrixSlice<T>:IEnumerable<T>
    {
        public enum SliceType { Row, Column};

        private Matrix<T> _matrix;
        private SliceType _type;
        private int _fixedIndex;

       

        private IEnumerator<T> _RowEnumerator()
        {
            for(int i=0; i< _matrix.Columns; i++)
            {
                yield return _matrix[_fixedIndex, i];
            }
        }

        private IEnumerator<T> _ColumnEnumerator()
        {
            for (int i = 0; i < _matrix.Rows; i++)
            {
                yield return _matrix[i, _fixedIndex];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if(_type == SliceType.Row)
            {
                return _RowEnumerator();
            }
            else
            {
                return _ColumnEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public MatrixSlice(Matrix<T> matrix, int fixedIndex, SliceType sliceType)
        {
            _matrix = matrix;
            _fixedIndex = fixedIndex;
            _type = sliceType;
        }
    }
}
