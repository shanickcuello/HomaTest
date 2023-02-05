namespace Sudoku.Board
{
    public interface INumberField
    {
        void SetValue(int x1, int y1, int value, string identifier, BoardViewModel board);
        void ButtonClick();
        void ReceiveInput(int newValue);
        int GetX();
        int GetY();
        void SetHint(int value);
    }
}