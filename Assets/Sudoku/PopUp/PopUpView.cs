using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Sudoku.PopUp
{
    public class PopUpView : MonoBehaviour
    {
        [SerializeField] private TMP_Text TitleText;
        [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;
        public Transform GetHorizontalLayoutGroup()
        {
            return horizontalLayoutGroup.gameObject.transform;
        }
        public PopUpView SetTitle(string text)
        {
            TitleText.text = text;
            return this;
        }
    }
}