using UnityEngine;

namespace Menus
{
    internal class BackButton : Button
    {
        [SerializeField] private GameObject _firstUIPanel;
        [SerializeField] private GameObject _secondUIPanel;
        
        public override void OnButtonPressed()
        {
            _secondUIPanel.SetActive(false);
            _firstUIPanel.SetActive(true);
        }
    }
}