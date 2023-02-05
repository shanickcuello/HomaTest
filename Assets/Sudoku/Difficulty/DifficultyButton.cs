using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Sudoku.Difficulty
{
    public class DifficultyButton : MonoBehaviour
    {
        private event Action<DifficultyButton> onDifficultySelected; 
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(()=> onDifficultySelected?.Invoke(this));
        }
        public void SetDisplayName(string text)
        {
            GetComponentInChildren<TMP_Text>().text = text;
        }
        public void SetOnClick(Action<DifficultyButton> onDifficultySelected)
        {
            this.onDifficultySelected = onDifficultySelected;
        }
    }
}