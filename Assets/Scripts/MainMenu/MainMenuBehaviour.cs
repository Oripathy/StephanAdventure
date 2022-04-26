using UI;
using UnityEngine;

namespace MainMenu
{
    internal class MainMenuBehaviour : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _mainCamera;
        [SerializeField] private GameObject _secondUIPanel;

        private void Awake()
        {
            _secondUIPanel.SetActive(false);
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    Debug.Log(hit);
                    
                    if (hit.collider.TryGetComponent<Button>(out var button))
                        button.OnButtonPressed();
                }
            }
            
        }
    }
}