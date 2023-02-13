using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Sudoku.PopUp
{
    public class ButtonWithAction : Button
    {
        private PopUpView _popUpView;
        public ButtonWithAction SetAction(Action buttonValue)
        {
            onClick.AddListener(() =>
            {
                DestroyPopUpView();
                buttonValue?.Invoke();
            });
            return this;
        }
        public ButtonWithAction SetText(string text)
        {
            GetComponentInChildren<TMP_Text>().text = text;
            return this;
        }
        public ButtonWithAction SetPopUpView(PopUpView popUpView)
        {
            _popUpView = popUpView;
            return this;
        }
        private void DestroyPopUpView()
        {
            Debug.LogWarning("Estoy destryendo este objetoooo");
            Destroy(_popUpView.gameObject);
        }
    }
}