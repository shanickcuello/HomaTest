using UnityEngine;
namespace Sudoku.Difficulty
{
    [CreateAssetMenu(menuName = "Difficulty Settings", fileName = "DifficultySettings")]
    public class DifficultySettings : ScriptableObject, IDifficultySettings
    {
        [Range(0, 81)] [SerializeField] private int piecesToErase;
        [Range(0, 81)] [SerializeField] private int maxHints;
        [SerializeField] private string nameDisplay;
        public int MaxHints => maxHints;
        public string Name
        {
            get => nameDisplay;
            set => nameDisplay = value;
        }
        public int PiecesToErase => piecesToErase;
    }
}