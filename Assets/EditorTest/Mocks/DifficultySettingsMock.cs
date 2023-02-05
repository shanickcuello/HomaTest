using Sudoku.Difficulty;
namespace EditorTest.Mocksx
{
    public class DifficultySettingsMock : IDifficultySettings
    {
        public int PiecesToErase { get; }
        public int MaxHints { get; }
        public string Name { get; set; }
        public DifficultySettingsMock(int piecesToErase, int maxHints)
        {
            PiecesToErase = piecesToErase;
            MaxHints = maxHints;
        }
    }
}