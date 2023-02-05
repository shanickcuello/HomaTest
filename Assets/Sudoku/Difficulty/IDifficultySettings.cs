namespace Sudoku.Difficulty
{
    public interface IDifficultySettings
    {
        int PiecesToErase { get; }
        int MaxHints { get; }
        string Name { get; set; }
    }
}