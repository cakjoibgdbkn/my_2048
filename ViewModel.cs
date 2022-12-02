using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_2048
{
    class ViewModel : PropertyChangedClass
    {

        readonly Model model;
        private bool _isEmptyCell;
        private int _countEmptyCell;
        private int _maxValue;
        private int _sumValue;

        public IEnumerable<Cell> Cells { get; }
        public bool IsEmptyCell { get => _isEmptyCell; private set { _isEmptyCell = value; OnPropertyChanged(); } }
        public int CountEmptyCell { get => _countEmptyCell; private set { _countEmptyCell = value; OnPropertyChanged(); } }
        public int MaxValue { get => _maxValue; private set { _maxValue = value; OnPropertyChanged(); } }
        public int SumValue { get => _sumValue; private set { _sumValue = value; OnPropertyChanged(); } }

        public void NextStep(DirectionEnum direction)
        {
            model.Step(direction);
            IsEmptyCell = !Cells.All(cell => cell.Value != CellValueEnum.None);
            MaxValue = Cells.Max(cell => (int)cell.Value);
            SumValue = Cells.Sum(cell => (int)cell.Value);
        }

        public ViewModel()
        {
            model = new Model();
            List<Cell> cells = new List<Cell>();
            foreach (ImmutableArray<Cell> rowCells in model.GetCells())
                foreach (Cell cell in rowCells)
                    cells.Add(cell);
            Cells = cells;
            NextStep(DirectionEnum.None);
        }
    }
}

