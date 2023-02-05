using System;
using System.Collections.Generic;
using EditorTest.Mocks;
using EditorTest.Mocksx;
using NUnit.Framework;
using Sudoku.Board;
using Sudoku.Difficulty;
namespace EditorTest
{
    public class BoardViewModelShould
    {
        private IDifficultySettings difficultySettings;
        [SetUp]
        public void SetUp()
        {
            difficultySettings = new DifficultySettingsMock(50, 2);
        }
        [Test]
        public void CallGridPreparedAfterInitialization()
        {
            bool gridPreparedEventCalled = false;
            var viewModel = GivenBoardViewModel(() => gridPreparedEventCalled = true, _ => _ = true);
            viewModel.Init();
            Assert.AreEqual(true, gridPreparedEventCalled);
        }
        [Test]
        public void ReturnTrueWhenHasHints()
        {
            var viewModel = GivenBoardViewModel(() => { }, _ => _ = true);
            viewModel.Init();
            var numberFields = GivenAListOfNumberFields();
            var shouldShowHint = viewModel.ShowHint(numberFields);
            Assert.AreEqual(true, shouldShowHint);
        }
        [Test]
        public void ReturnFalseWhenHintsAreNotEnough()
        {
            var viewModel = GivenBoardViewModel(() => { }, _ => _ = true);
            viewModel.Init();
            var numberFields = GivenAListOfNumberFields();
            WhenCallHint(viewModel, numberFields, difficultySettings.MaxHints);
            var shouldShowHint = viewModel.ShowHint(numberFields);
            Assert.AreEqual(false, shouldShowHint);
        }
        private void WhenCallHint(BoardViewModel viewModel, List<INumberField> numberFields, int amountOfCalls)
        {
            for (int i = 0; i < amountOfCalls; i++)
            {
                viewModel.ShowHint(numberFields);
            }
        }
        private List<INumberField> GivenAListOfNumberFields()
        {
            var numberFields = new List<INumberField>();
            INumberField numberFieldMock = new NumberFieldMock();
            numberFields.Add(numberFieldMock);
            return numberFields;
        }
        private BoardViewModel GivenBoardViewModel(Action gridPrepared, Action<bool> action)
        {
            return new BoardViewModel(difficultySettings, gridPrepared, action);
        }
    }
}