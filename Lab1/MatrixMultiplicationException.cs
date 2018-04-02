using System;

namespace GTLabs
{
    class MatrixMultiplicationException:Exception
    {
        public Matrix LeftMatrix { get; private set; }
        public Matrix RightMatrix { get; private set; }

        public MatrixMultiplicationException(string message, Matrix leftMatrix, Matrix rightMMatrix) : base(message)
        {
            LeftMatrix = leftMatrix;
            RightMatrix = rightMMatrix;
        }

        public static MatrixMultiplicationException Create(Matrix leftMatrix, Matrix rightMatrix)
        {
            string message = $"Incommpatible sizes of matrices {leftMatrix.Rows}x{leftMatrix.Columns} and {rightMatrix.Rows}x{rightMatrix.Columns} ";
            return new MatrixMultiplicationException(message, leftMatrix, rightMatrix);
        }
    }
}
