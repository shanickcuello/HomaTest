using System;
using UnityEngine;
namespace Sudoku.PopUp
{
    public class PopUpFactory : MonoBehaviour
    {
        [SerializeField] private PopUpView _popUpViewPrefab;
        public static PopUpFactory Instance;
        public void Create(Action onConfirmButtonClicked, string text, string buttonText = "Ok!")
        {
            Instantiate(_popUpViewPrefab, transform)
                .SetConfirmButtonCallback(onConfirmButtonClicked)
                .SetTitle(text)
                .SetButtonText(buttonText);
        }
        private void Awake()
        {
            Singleton();
        }
        private void Singleton()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
    }
}