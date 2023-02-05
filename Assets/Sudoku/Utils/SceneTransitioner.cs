using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Sudoku.Utils
{
    public class SceneTransitioner : MonoBehaviour
    {
        private AsyncOperation _asyncLoad;
        public void LoadAndTransitionToScene(int index)
        {
            _asyncLoad = SceneManager.LoadSceneAsync(index);
            _asyncLoad.allowSceneActivation = false;
            StartCoroutine(WaitForSceneLoad());
        }
        private IEnumerator WaitForSceneLoad()
        {
            while (!_asyncLoad.isDone)
            {
                if (_asyncLoad.progress >= 0.9f)
                {
                    _asyncLoad.allowSceneActivation = true;
                    break;
                }
                yield return null;
            }
        }
    }
    public enum EScenesToIndex
    {
        Menu = 0,
        Game = 1
    }
}