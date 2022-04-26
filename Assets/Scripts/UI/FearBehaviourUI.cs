using Stephan;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    internal class FearBehaviourUI : MonoBehaviour
    {
        [SerializeField] private Text _fearBehaviourText;
        [SerializeField] private Slider _slider;
        [SerializeField] private FearBehaviour _fearBehaviour;

        private void Awake()
        {
            _fearBehaviour.FearAmountChanged += OnFearAmountChanged;
        }

        private void OnFearAmountChanged(float value)
        {
            _fearBehaviourText.text = value + " / 100";
            _slider.value = value;
        }
    }
}