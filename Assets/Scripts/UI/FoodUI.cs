using UnityEngine;
using UnityEngine.UI;
using Player;

namespace UI
{
    internal class FoodUI : MonoBehaviour
    {
        [SerializeField] private Text _foodText;
        [SerializeField] private ResourceManager _resourceManager;

        private float _foodAmount; 
    
        private void Awake()
        {
            _resourceManager = FindObjectOfType<ResourceManager>();
            _foodText.text = _foodAmount + " / 10";
            _resourceManager.FoodTaken += OnFoodTaken;
        }

        private void OnFoodTaken()
        {
            _foodAmount += 1f;
            _foodText.text = _foodAmount + " / 10";
        }
    }
}