using Sudoku.Board;
namespace EditorTest.Mocks
{
    public class NumberFieldMock : INumberField
    {
        public void SetValue(int x1, int y1, int value, string identifier, BoardViewModel board)
        {
        }
        public void ButtonClick()
        {
        }
        public void ReceiveInput(int newValue)
        {
        }
        public int GetX()
        {
            return 1;
        }
        public int GetY()
        {
            return 1;
        }
        public void SetHint(int value)
        {
        }
    }
}