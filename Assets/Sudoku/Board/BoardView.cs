using System;
using System.Collections.Generic;
using Sudoku.Difficulty;
using Sudoku.PopUp;
using Sudoku.Utils;
using UnityEngine;
using UnityEngine.UI;
namespace Sudoku.Board
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField] private GameObject cellNumberButton;
        [SerializeField] private GameObject chunkPrefab;
        [SerializeField] private Button hintButton;
        private IDifficultySettings difficultySettings;
        private List<Transform> chunks = new();
        private const int AmountOfChunks = 9;
        private int[,] _riddleGrid;
        private BoardViewModel _viewModel;
        private List<INumberField> fieldList = new();
        private event Action _gridPrepared;
        private event Action<bool> win;
        private SceneTransitioner _sceneTransitioner;
        private void Start()
        {
            _sceneTransitioner = gameObject.AddComponent<SceneTransitioner>();
        }
        public void Init()
        {
            hintButton.onClick.AddListener(OnHintbuttonClicked);
            CreateChunks();
            win += DisplayPopUp;
            _viewModel = new BoardViewModel(difficultySettings, _gridPrepared, win);
            _viewModel.Init();
            _riddleGrid = _viewModel.RiddleGrid;
            CreateButtons();
        }
        private void DisplayPopUp(bool win)
        {
            if (win)
            {
                PopUpFactory.Instance.Create(TransitionToMenu, "Nice! You Win!", "Go to menu");
            }
            else
            {
                PopUpFactory.Instance.Create((() => { }), "Oops something is wrong, keep trying!");
            }
        }
        private void TransitionToMenu()
        {
            _sceneTransitioner.LoadAndTransitionToScene((int)EScenesToIndex.Menu);
        }
        private void OnHintbuttonClicked()
        {
            _viewModel.ShowHint(fieldList);
        }
        private void CreateChunks()
        {
            for (int i = 0; i < AmountOfChunks; i++)
            {
                var temporalChunk = Instantiate(chunkPrefab, transform);
                chunks.Add(temporalChunk.transform);
            }
        }
        private void CreateButtons()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    GameObject newBtn = Instantiate(cellNumberButton);
                    NumberField numField = newBtn.GetComponent<NumberField>();
                    numField.SetValue(i, j, _riddleGrid[i, j], i + "," + j, _viewModel);
                    newBtn.name = i + "," + j;
                    if (_riddleGrid[i, j] == 0)
                        fieldList.Add(numField);
                    int chunkIndex = (i / 3) * 3 + j / 3;
                    newBtn.transform.SetParent(chunks[chunkIndex], false);
                }
            }
        }
        public void SetDifficulty(IDifficultySettings difficultySettings)
        {
            this.difficultySettings = difficultySettings;
        }
    }
}