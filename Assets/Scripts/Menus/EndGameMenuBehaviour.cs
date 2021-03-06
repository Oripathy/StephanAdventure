using UnityEngine;

namespace Menus
{
    internal class EndGameMenuBehaviour : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _mainCamera;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    if (hit.collider.TryGetComponent<Button>(out var button))
                        button.OnButtonPressed();
                }
            }
        }
    }
}