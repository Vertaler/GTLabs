using System;

namespace GTLabs
{
    class MatrixInvertException:Exception
    {
        public enum ExceptionCause
        {
            NotSquare,
            Singular
        }
        public Matrix Matrix { get; private set; }
        public ExceptionCause Cause { get; private set; }

        public MatrixInvertException(string message, Matrix matrix, ExceptionCause cause) : base(message)
        {
            Matrix = matrix;
            Cause = cause;
        }

        public static MatrixInvertException Create(Matrix matrix, ExceptionCause cause)
        {
            
            var message = "Matrix is not invertable, because it ";
            switch (cause)
            {
                case ExceptionCause.NotSquare:
                    message += "is not square";
                    break;
                case ExceptionCause.Singular:
                    message += "is singular";
                    break;
            }
            return new MatrixInvertException(message, matrix, cause);
        }
    }
}
