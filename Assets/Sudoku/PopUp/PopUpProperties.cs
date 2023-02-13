using System;
using System.Collections.Generic;
namespace Sudoku.PopUp
{
    public class PopUpProperties
    {
        public string title;
        public Dictionary<string, Action> buttons;
        public PopUpProperties(string title, Dictionary<string, Action> buttons)
        {
            this.title = title;
            this.buttons = buttons;
        }
    }
}