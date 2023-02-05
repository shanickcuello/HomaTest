using Sudoku.Utils;
using UnityEngine;
using UnityEngine.UI;
namespace Sudoku.Menu
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        private SceneTransitioner _sceneTransitioner;
        private void Start()
        {
            _sceneTransitioner = gameObject.AddComponent<SceneTransitioner>();
            playButton.onClick.AddListener(TransitionToGameScene);
        }
        private void TransitionToGameScene()
        {
            _sceneTransitioner.LoadAndTransitionToScene((int)EScenesToIndex.Game);
        }
    }
}