using UnityEngine;
using UnityEngine.UI;
namespace Sudoku.PopUp
{
    public class PopUpFactory : MonoBehaviour
    {
        [SerializeField] private PopUpView _popUpViewPrefab;
        [SerializeField] private ButtonWithAction buttonWithActionPrefabPrefab;
        public static PopUpFactory Instance;
        public void Create(PopUpProperties popUpProperties)
        {
            var popUpView = Instantiate(_popUpViewPrefab, transform).SetTitle(popUpProperties.title);
            var layoutGroupTranform = popUpView.GetHorizontalLayoutGroup();
            foreach (var button in popUpProperties.buttons)
                Instantiate(buttonWithActionPrefabPrefab, layoutGroupTranform)
                    .SetAction(button.Value)
                    .SetText(button.Key)
                    .SetPopUpView(popUpView);
        }
        private void Awake()
        {
            Singleton();
        }
        private void Singleton()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
    }
}