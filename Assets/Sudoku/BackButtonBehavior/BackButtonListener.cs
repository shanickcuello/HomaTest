using System;
using System.Collections.Generic;
using Sudoku.PopUp;
using Sudoku.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Sudoku.BackButtonBehavior
{
    public class BackButtonListener : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                var buttonActions = new Dictionary<string, Action>
                {
                    { "Exit", TransitionToMenu },
                    { "Continue", null }
                };
                var popUpProperties = new PopUpProperties("You want to exit?", buttonActions);
                PopUpFactory.Instance.Create(popUpProperties);
            }
        }
        private void TransitionToMenu() // Todo: Call SceneTransitioner
        {
            SceneManager.LoadScene((int)EScenesToIndex.Menu);
        }
    }
}