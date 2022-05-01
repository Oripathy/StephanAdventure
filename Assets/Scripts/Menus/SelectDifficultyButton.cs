using UnityEngine;

namespace Menus
{
    internal class SelectDifficultyButton : Button
    {
        [SerializeField] private GameObject _firstUIPanel;
        [SerializeField] private GameObject _secondUIPanel;
        
        public override void OnButtonPressed()
        {
            _firstUIPanel.SetActive(false);
            _secondUIPanel.SetActive(true);
        }
    }
}