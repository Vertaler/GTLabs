using System;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace GTLabs
{
    class BrownRobinson
    {
        private Excel.Application _excelApp;
        private Excel._Worksheet _workSheet;
        private Excel._Workbook _workBook;
        private double _errorBound;
        private double _currentError;
        private double _lowerPrice;
        private double _upperPrice;
        private double _averagePrice;
        private double _minimalUpperPrice;
        private double _maximalLowerPrice;
        private double[] _firstPlayerGain;
        private double[] _secondPlayerLoss;
        private double[] _firstPlayerStrategies;
        private double[] _secondPlayerStrategies;
        private int _firstPlayerChoice;
        private int _secondPlayerChoice;
        private int _iterationsCount = 1;
        private Matrix _gameMatrix;
        private Random _rand = new Random();

        public double ErrorBound
        {
            get
            {
                return _errorBound;
            }
            set
            {
                if (value <= 0) throw new ArgumentException("Error bound must be positive number");
                _errorBound = value;
            }
        }

        private void _Initialize(Matrix gameMatrix)
        {
            _excelApp = new Excel.Application();
            _excelApp.Visible = false;
            _workBook = (Excel.Workbook)_excelApp.ActiveWorkbook;
            if (_workBook == null)
            {
                _workBook = _excelApp.Workbooks.Add();
            }
            _workSheet = (Excel.Worksheet)_excelApp.ActiveSheet;

            _gameMatrix = gameMatrix;
            _minimalUpperPrice = double.MaxValue;
            _maximalLowerPrice = double.MinValue;
            _firstPlayerGain = new double[gameMatrix.Rows];
            _secondPlayerLoss = new double[gameMatrix.Columns];
            _firstPlayerStrategies = new double[gameMatrix.Rows];//Число выборов той или иной стратегии 1-м игроком
            _secondPlayerStrategies = new double[gameMatrix.Columns];
            _firstPlayerChoice = _rand.Next(gameMatrix.Rows);
            _secondPlayerChoice = _rand.Next(gameMatrix.Columns);
            _firstPlayerStrategies[_firstPlayerChoice] += 1;
            _secondPlayerStrategies[_secondPlayerChoice] += 1;
            _iterationsCount = 1;
        }

        private void _UpdateGainAndLoss()
        {
            for (int i = 0; i < _firstPlayerGain.Length; i++)
            {
                _firstPlayerGain[i] += _gameMatrix[i, _secondPlayerChoice];
            }
            for (int j = 0; j < _secondPlayerLoss.Length; j++)
            {
                _secondPlayerLoss[j] += _gameMatrix[_firstPlayerChoice, j];
            }
        }

        private void _UpdatePrices()
        {
            _upperPrice = _firstPlayerGain.Max() / _iterationsCount;
            if (_upperPrice < _minimalUpperPrice)
            {
                _minimalUpperPrice = _upperPrice;
            }
            _lowerPrice = _secondPlayerLoss.Min() / _iterationsCount;
            if (_lowerPrice > _maximalLowerPrice)
            {
                _maximalLowerPrice = _lowerPrice;
            }
            _averagePrice = (_upperPrice + _lowerPrice) / 2;
        }

        private void _ExcelWrite()
        {
            _workSheet.Cells[_iterationsCount, 1] = _iterationsCount;
            _workSheet.Cells[_iterationsCount, 2] ="x" + (_firstPlayerChoice + 1);
            _workSheet.Cells[_iterationsCount, 3] ="y" + (_secondPlayerChoice + 1);
            for(int i=0; i<_firstPlayerGain.Length; i++)
            {
                _workSheet.Cells[_iterationsCount, 4 + i] = _firstPlayerGain[i];
            }
            for (int i = 0; i < _secondPlayerLoss.Length; i++)
            {
                _workSheet.Cells[_iterationsCount, 4 + i + _firstPlayerGain.Length] = _secondPlayerLoss[i];
            }
            var currentColumn = 4 + _firstPlayerGain.Length + _secondPlayerLoss.Length;
            _workSheet.Cells[_iterationsCount, currentColumn] = _upperPrice;
            _workSheet.Cells[_iterationsCount, currentColumn + 1] = _lowerPrice;
            _workSheet.Cells[_iterationsCount, currentColumn + 2] = _averagePrice;
            _workSheet.Cells[_iterationsCount, currentColumn + 3] = _currentError;

        }

        private void _ChooseStrategies()
        {
            var firstPlayerPossibleChoices = Enumerable.Range(0, _firstPlayerGain.Length).Where((i) => _firstPlayerGain[i] == _firstPlayerGain.Max());
            _firstPlayerChoice = firstPlayerPossibleChoices.ElementAt(_rand.Next(firstPlayerPossibleChoices.Count()));//Выбор случайного эл-та из максимальных
            var secondPlayerPossibleChoices = Enumerable.Range(0, _secondPlayerLoss.Length).Where((i) => _secondPlayerLoss[i] == _secondPlayerLoss.Min());
            _secondPlayerChoice = secondPlayerPossibleChoices.ElementAt(_rand.Next(secondPlayerPossibleChoices.Count()));//Выбор случайного эл-та из минимальных
            _firstPlayerStrategies[_firstPlayerChoice]++;
            _secondPlayerStrategies[_secondPlayerChoice]++;
        }

        private void _PrintCurrentState()
        {
            Console.Write($"{_iterationsCount,5}");
            Console.Write(" ");
            Console.Write($"{_firstPlayerChoice,2}");
            Console.Write(" ");
            Console.Write($"{_secondPlayerChoice,2}");
            Console.Write(" ");
            foreach (double gain in _firstPlayerGain)
            {
                Console.Write($"{gain.ToString("F3"),9}");
                Console.Write(" ");
            }
            foreach (double loss in _secondPlayerLoss)
            {
                Console.Write($"{loss.ToString("F3"),9}");
                Console.Write(" ");
            }
            Console.Write(_upperPrice.ToString("F3"));
            Console.Write(" ");
            Console.Write(_lowerPrice.ToString("F3"));
            Console.Write(" ");
            Console.Write(_averagePrice.ToString("F3"));
            Console.Write(" ");
            Console.WriteLine(_currentError.ToString("F3"));
        }

        public GameSolution Solve(Matrix gameMatrix)
        {
            var solution = new GameSolution();
            _Initialize(gameMatrix);
            do
            {
                _UpdateGainAndLoss();
                _UpdatePrices();
                _currentError = _minimalUpperPrice - _maximalLowerPrice;
                _PrintCurrentState();
                _ExcelWrite();
                _ChooseStrategies();
                _iterationsCount++;
            } while (Math.Abs(_currentError) >= ErrorBound);
            _workBook.SaveAs("result.xlsx");
            _workBook.Close();
            solution.FirstPlayerStrategy = _firstPlayerStrategies.Select((x) => x / (_iterationsCount-1)).ToArray();
            solution.SecondPlayerStrategy = _secondPlayerStrategies.Select((x) => x / (_iterationsCount-1)).ToArray();
            return solution;
        }

        public BrownRobinson(double errorBound)
        {
            ErrorBound = errorBound;
        }
    }
}
