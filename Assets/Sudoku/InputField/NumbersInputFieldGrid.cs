using System.Collections.Generic;
using Sudoku.Board;
using UnityEngine;
using UnityEngine.UI;
namespace Sudoku.InputField
{
    public class NumbersInputFieldGrid : MonoBehaviour
    {
        public static NumbersInputFieldGrid Instance;
        [SerializeField] private NumberInputField numberInputFieldPrefab;
        [SerializeField] private Button backButton;
        private const int NumberOfInputValues = 9;
        NumberField _lastField;
        private Stack<NumberField> _historyNumberfieldSetted = new Stack<NumberField>();
        private void Awake()
        {
            Singleton();
            CreateInputsFieldNumbers();
            backButton.onClick.AddListener(EraseLastNumber);
        }
        public void SetLastField(NumberField field)
        {
            _lastField = field;
        }
        public void ClickedInput(int number)
        {
            if (_lastField == null) return;
            _lastField.ReceiveInput(number);
            _historyNumberfieldSetted.Push(_lastField);
        }
        private void EraseLastNumber()
        {
            if (_historyNumberfieldSetted.Count < 1) return;
            _historyNumberfieldSetted.Peek().ReceiveInput(0);
            _historyNumberfieldSetted.Pop();
        }
        private void Singleton()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
        private void CreateInputsFieldNumbers()
        {
            for (int i = 1; i <= NumberOfInputValues; i++)
            {
                Instantiate(numberInputFieldPrefab, transform)
                    .SetInputValue(i)
                    .SetInputFieldGrid(this);
            }
        }
        private void Start()
        {
            gameObject.SetActive(true);
        }
    }
}