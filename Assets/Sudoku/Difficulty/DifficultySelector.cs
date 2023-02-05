using System.Collections.Generic;
using Sudoku.Board;
using UnityEngine;
namespace Sudoku.Difficulty
{
   public class DifficultySelector : MonoBehaviour
   {
      [SerializeField] private List<DifficultySettings> difficultySettings;
      [SerializeField] private DifficultyButton difficultyButtonPrefab;
      [SerializeField] private BoardView boardView;
      private readonly Dictionary<DifficultyButton, IDifficultySettings> _difficultiesButtonsSettings = new Dictionary<DifficultyButton, IDifficultySettings>();

      private void Awake()
      {
         foreach (var difficultySetting in difficultySettings)
         {
            var difficultyButtonSelection = Instantiate(difficultyButtonPrefab, transform);
            difficultyButtonSelection.SetDisplayName(difficultySetting.Name);
            difficultyButtonSelection.SetOnClick(OnDifficultySelected);
            _difficultiesButtonsSettings.Add(difficultyButtonSelection, difficultySetting);
         }
      }
      private void OnDifficultySelected(DifficultyButton difficultyButton)
      {
         boardView.SetDifficulty(_difficultiesButtonsSettings[difficultyButton]);
         boardView.Init();
         gameObject.SetActive(false);
      }

   }
}
