using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Sudoku.PopUp
{
    public class PopUpView : MonoBehaviour
    {
        [SerializeField] private Button confirmButton;
        [SerializeField] private TMP_Text TitleText;
        private event Action _onConfirmButtonClicked;
        private void Awake()
        {
            confirmButton.onClick.AddListener(OnConfirmClicked);
        }
        public PopUpView SetConfirmButtonCallback(Action onConfirmButtonClicked)
        {
            _onConfirmButtonClicked = onConfirmButtonClicked;
            return this;
        }
        public PopUpView SetTitle(string text)
        {
            TitleText.text = text;
            return this;
        }
        private void OnConfirmClicked()
        {
            _onConfirmButtonClicked?.Invoke();
            Destroy(gameObject);
        }
        /// <summary>
        /// The text displayed bust be short. For example "Ok", "Confirm". One word is better.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public PopUpView SetButtonText(string text)
        {
            confirmButton.GetComponentInChildren<TMP_Text>().text = text;
            return this;
        }
    }
}