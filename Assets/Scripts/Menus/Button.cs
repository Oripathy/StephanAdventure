using StageController;
using UnityEngine;

namespace Menus
{
    internal abstract class Button : MonoBehaviour
    {
        [SerializeField] private protected DifficultySelector _difficultySelector;
        
        public abstract void OnButtonPressed();
    }
}