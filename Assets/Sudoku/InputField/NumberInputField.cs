using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Sudoku.InputField
{
    public class NumberInputField : MonoBehaviour
    {
        private int _value;
        private NumbersInputFieldGrid _numbersInputFieldGrid;
        private TMP_Text _textValue;
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClicked);
            _textValue = GetComponentInChildren<TMP_Text>();
        }
        private void OnClicked()
        {
            _numbersInputFieldGrid.ClickedInput(_value);
        }
        public NumberInputField SetInputValue(int value)
        {
            _value = value;
            _textValue.text = _value.ToString();
            gameObject.name = value.ToString();
            return this;
        }
        public NumberInputField SetInputFieldGrid(NumbersInputFieldGrid numbersInputFieldGrid)
        {
            _numbersInputFieldGrid = numbersInputFieldGrid;
            return this;
        }
    }
}