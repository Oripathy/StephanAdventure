using System;
using Player;
using Stephan;
using UnityEngine;

namespace StageController
{
    internal class StageSwitcher : MonoBehaviour
    {
        [SerializeField] private PlayerEntity _player;
        [SerializeField] private StephanEntity _stephan;
        [SerializeField] private StephanData _data;
        [SerializeField] private ResourceManager _resourceManager;
        [SerializeField] private FoodSpawner _foodSpawner;

        private float _currentFoodAmount;

        public Stage CurrentStage { get; private set; }
        public SearchForStephanStage SearchForStephanStage { get; private set; }
        public SearchForFoodStage SearchForFoodStage { get; private set; }
        public EndGameStage EndGameStage { get; private set; }

        public event Action StephanHidden;
        public event Action<Type> StageSwitched;
        public event Action GameEnded;

        private void Awake()
        {
            InitializeStages();
            SetInitialStage(SearchForFoodStage);
            _resourceManager = _player.GetComponent<ResourceManager>();

            _stephan.StepanHidden += () =>
            {
                CurrentStage.OnExit();
                CurrentStage = SearchForStephanStage;
                CurrentStage.OnEnter();
                StephanHidden?.Invoke();
                StageSwitched?.Invoke(typeof(SearchForStephanStage));
            };
        
            _player.StephanFound += () =>
            {
                CurrentStage.OnExit();
                CurrentStage = SearchForFoodStage;
                CurrentStage.OnEnter();
                StageSwitched?.Invoke(typeof(SearchForStephanStage));
            };

            _stephan.MaxSizeAchieved += () =>
            {
                CurrentStage.OnExit();
                CurrentStage = EndGameStage;
                CurrentStage.OnEnter();
                GameEnded?.Invoke();
            };

            _resourceManager.FoodTaken += () =>
            {
                CurrentStage.OnExit();
                CurrentStage.OnEnter();
            };
        }

        private void InitializeStages()
        {
            SearchForFoodStage = new SearchForFoodStage(_foodSpawner);
            SearchForStephanStage = new SearchForStephanStage(_foodSpawner);
            EndGameStage = new EndGameStage(_foodSpawner);
        }

        private void SetInitialStage(Stage initialStage)
        {
            CurrentStage = initialStage;
            CurrentStage.OnEnter();
            StageSwitched?.Invoke(CurrentStage.GetType());
        }
    }
}