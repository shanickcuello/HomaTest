using System;
using Sudoku.InputField;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Sudoku.Board
{
    public class NumberField : MonoBehaviour, INumberField
    {
        private BoardViewModel board;
        private int x1, y1;
        private int value;
        public TextMeshProUGUI number;
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(ButtonClick);
        }
        public void SetValue(int x1, int y1, int value, string identifier, BoardViewModel board)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.value = value;
            this.board = board;
            number.text = value != 0 ? value.ToString() : "";
            if (value != 0)
                GetComponentInParent<Button>().interactable = false;
            else
                number.color = Color.white;
        }
        public void ButtonClick()
        {
            NumbersInputFieldGrid.Instance.SetLastField(this);
        }
        public void ReceiveInput(int newValue)
        {
            value = newValue;
            number.text = value != 0 ? value.ToString() : "";
            number.color = Color.black; //Este es un naranjita que me gusto jeje. #F06A00
            board.SetInputInRiddleAndCheckCompleted(x1, y1, value);
        }
        public int GetX()
        {
            return x1;
        }
        public int GetY()
        {
            return y1;
        }
        public void SetHint(int value)
        {
            this.value = value;
            number.text = value.ToString();
            number.color = Color.green;
            GetComponentInParent<Button>().interactable = false;
        }
    }
}