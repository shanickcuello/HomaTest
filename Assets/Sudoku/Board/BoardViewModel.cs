using System;
using System.Collections.Generic;
using Sudoku.Difficulty;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Sudoku.Board
{
    public class BoardViewModel
    {
        public event Action GridPrepared;
        public event Action<bool> OnFinished;
        private int[,] _solvedGrid = new int[9, 9];
        private int[,] _riddleGrid = new int[9, 9];
        private int _maxHint;
        private string _debugText;
        private int _piecesToErase;
        public int[,] RiddleGrid => _riddleGrid;
        public BoardViewModel(IDifficultySettings difficultySettings, Action gridPrepared, Action<bool> onFinished)
        {
            _piecesToErase = difficultySettings.PiecesToErase;
            _maxHint = difficultySettings.MaxHints;
            GridPrepared = gridPrepared;
            OnFinished = onFinished;
        }
        public void Init()
        {
            FillGridBase(ref _solvedGrid);
            SolveGrid(ref _solvedGrid);
            CreateRiddleGrid(ref _solvedGrid, ref _riddleGrid);
            GridPrepared?.Invoke();
        }
        public void SetInputInRiddleAndCheckCompleted(int x, int y, int value)
        {
            RiddleGrid[x, y] = value;
            CheckWin();
        }
        public bool ShowHint(List<INumberField> fieldList)
        {
            if (_maxHint <= 0) return false;
            if (fieldList.Count <= 0) return false;
            var randomIndex = Random.Range(0, fieldList.Count);
            _maxHint--;
            RiddleGrid[fieldList[randomIndex].GetX(), fieldList[randomIndex].GetY()] =
                _solvedGrid[fieldList[randomIndex].GetX(), fieldList[randomIndex].GetY()];
            fieldList[randomIndex]
                .SetHint(RiddleGrid[fieldList[randomIndex].GetX(), fieldList[randomIndex].GetY()]);
            fieldList.RemoveAt(randomIndex);
            CheckWin();
            return true;
        }
        private void DebugGrid(ref int[,] grid)
        {
#if UNITY_EDITOR
            _debugText = "";
            var separator = 0;
            for (var i = 0; i < 9; i++)
            {
                _debugText += "|";
                for (var j = 0; j < 9; j++)
                {
                    _debugText += grid[i, j].ToString();
                    separator = j % 3;
                    if (separator == 2)
                        _debugText += "|";
                }
                _debugText += "\n";
            }
            Debug.Log(_debugText);
#endif
        }
        private void ShuffleGrid(ref int[,] grid, int shuffleAmount)
        {
            for (var i = 0; i < shuffleAmount; i++)
            {
                var value1 = Random.Range(1, 10);
                var value2 = Random.Range(1, 10);
                //Aca tenfgo que mixear las seldas
                MixTwoGridCells(ref grid, value1, value2);
            }
            DebugGrid(ref grid);
        }
        private void MixTwoGridCells(ref int[,] grid, int value1, int value2)
        {
            var x1 = 0;
            var x2 = 0;
            var y1 = 0;
            var y2 = 0;
            for (var i = 0; i < 9; i += 3)
            for (var k = 0; k < 9; k += 3)
            {
                for (var j = 0; j < 3; j++)
                for (var l = 0; l < 3; l++)
                {
                    if (grid[i + j, k + l] == value1)
                    {
                        x1 = i + j;
                        y1 = k + l;
                    }
                    if (grid[i + j, k + l] == value2)
                    {
                        x2 = i + j;
                        y2 = k + l;
                    }
                }
                grid[x1, y1] = value2;
                grid[x2, y2] = value1;
            }
        }
        private void CreateRiddleGrid()
        {
            //Copiar la anterior solvegrid
            for (var i = 0; i < 9; i++)
            for (var j = 0; j < 9; j++)
                RiddleGrid[i, j] = _solvedGrid[i, j];

            //borrar los numeros que no queremos
            for (var i = 0; i < _piecesToErase; i++)
            {
                var x1 = Random.Range(0, 9);
                var y1 = Random.Range(0, 9);
                //Si hay un cero lo tiro de nuevo hasta encontrar uno sin 0
                while (RiddleGrid[x1, y1] == 0)
                {
                    x1 = Random.Range(0, 9);
                    y1 = Random.Range(0, 9);
                }
                //una vez que lo encontamos sin 0 lo sesteamos a 0
                RiddleGrid[x1, y1] = 0;
            }
            DebugGrid(ref _riddleGrid);
        }
        private void CheckWin()
        {
            if (!HasCompletedGrid()) return;
            OnFinished?.Invoke(HasWon());
        }
        private bool HasCompletedGrid()
        {
            for (var i = 0; i < 9; i++)
            for (var j = 0; j < 9; j++)
                if (RiddleGrid[i, j] == 0)
                    return false;
            return true;
        }
        private bool HasWon()
        {
            for (var i = 0; i < 9; i++)
            for (var j = 0; j < 9; j++)
                if (RiddleGrid[i, j] != _solvedGrid[i, j])
                    return false;
            return true;
        }
        private void CreateRiddleGrid(ref int[,] sGrid, ref int[,] rGrid)
        {
            Array.Copy(sGrid, rGrid, sGrid.Length);

            //borrar los numeros que no queremos
            for (var i = 0; i < _piecesToErase; i++)
            {
                var x1 = Random.Range(0, 9);
                var y1 = Random.Range(0, 9);
                //Si hay un cero lo tiro de nuevo hasta encontrar uno sin 0
                while (rGrid[x1, y1] == 0)
                {
                    x1 = Random.Range(0, 9);
                    y1 = Random.Range(0, 9);
                }
                //una vez que lo encontamos sin 0 lo sesteamos a 0
                rGrid[x1, y1] = 0;
            }
            DebugGrid(ref _riddleGrid);
        }
        #region BACKTRACKING
        private bool ColumnContainsNumber(int y, int value, ref int[,] grid)
        {
            for (var x = 0; x < 9; x++)
                if (grid[x, y] == value)
                    return true;
            return false;
        }
        private bool RowContainsNumber(int x, int value, ref int[,] grid)
        {
            for (var y = 0; y < 9; y++)
                if (grid[x, y] == value)
                    return true;
            return false;
        }
        private bool BlockContainsNumber(int x, int y, int value, ref int[,] grid)
        {
            for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                if (grid[x - x % 3 + i, y - y % 3 + j] == value)
                    return true;
            return false;
        }
        private bool CheckAll(int x, int y, int value, ref int[,] grid)
        {
            if (ColumnContainsNumber(y, value, ref grid)) return false;
            if (RowContainsNumber(x, value, ref grid)) return false;
            if (BlockContainsNumber(x, y, value, ref grid)) return false;
            return true;
        }
        private bool IsValidGrid(ref int[,] grid)
        {
            for (var i = 0; i < 9; i++)
            for (var j = 0; j < 9; j++)
                if (grid[i, j] == 0)
                    return false;
            return true;
        }
        private void FillGridBase(ref int[,] grid)
        {
            var rowValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var columnValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var value = rowValues[Random.Range(0, rowValues.Count)];
            grid[0, 0] = value;
            rowValues.Remove(value);
            columnValues.Remove(value);

            //Rows
            for (var i = 1; i < 9; i++)
            {
                value = rowValues[Random.Range(0, rowValues.Count)];
                grid[i, 0] = value;
                rowValues.Remove(value);
            }

            //Columns
            for (var i = 1; i < 9; i++)
            {
                value = columnValues[Random.Range(0, columnValues.Count)];
                if (i < 3)
                    while (BlockContainsNumber(0, 0, value, ref grid))
                        value = columnValues[Random.Range(0, columnValues.Count)];
                grid[0, i] = value;
                columnValues.Remove(value);
            }
        }
        private bool SolveGrid(ref int[,] grid)
        {
            DebugGrid(ref grid);
            if (IsValidGrid(ref grid))
                return true;
            var x = 0;
            var y = 0;
            for (var i = 0; i < 9; i++)
            for (var j = 0; j < 9; j++)
                if (grid[i, j] == 0)
                {
                    x = i;
                    y = j;
                    break;
                }
            var possibilities = new List<int>();
            possibilities = GetAllPossibilities(x, y, ref grid);
            for (var p = 0; p < possibilities.Count; p++)
            {
                grid[x, y] = possibilities[p]; //aca sucede la magic
                if (SolveGrid(ref grid)) return true;
                grid[x, y] = 0;
            }
            return false;
        }
        private List<int> GetAllPossibilities(int x, int y, ref int[,] grid)
        {
            var possibilities = new List<int>();
            for (var val = 1; val <= 9; val++)
                if (CheckAll(x, y, val, ref grid))
                    possibilities.Add(val);
            return possibilities;
        }
        #endregion
    }
}