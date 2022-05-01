using System;
using System.Collections.Generic;
using StageController;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    internal class StageUI : MonoBehaviour
    {
        [SerializeField] private Text _stageText;
        [SerializeField] private GameObject _stageTextUIPanel;
        [SerializeField] private StageSwitcher _stageSwitcher;

        private Dictionary<Type, string> _textDictionary;
    
        private void Awake()
        {
            _textDictionary = new Dictionary<Type, string>
            {
                {typeof(SearchForFoodStage), "Find food for Stephan!"},
                {typeof(SearchForStephanStage), "Stephan is Hidden! Find him!"}
            };

            _stageSwitcher.StageSwitched += OnStageSwitched;
            _stageSwitcher.GameEnded += SetStageTextUIPanelInActive;
        }

        private void OnStageSwitched(Type type)
        {
            if (_textDictionary.TryGetValue(type, out var value))
                _stageText.text = value;
            else
                throw new KeyNotFoundException();
        }

        private void SetStageTextUIPanelInActive() => _stageTextUIPanel.SetActive(false);
    }
}