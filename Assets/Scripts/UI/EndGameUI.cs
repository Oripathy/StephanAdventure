using System;
using StageController;
using UnityEngine;

namespace UI
{
    internal class EndGameUI : MonoBehaviour
    {
        [SerializeField] private StageSwitcher _stageSwitcher;
        private void Awake()
        {
            gameObject.SetActive(false);
            _stageSwitcher.GameEnded += OnGameEnded;
        }

        private void OnGameEnded() => gameObject.SetActive(true);
    }
}