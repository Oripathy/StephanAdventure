using System;
using UnityEngine;

internal class StageSwitcher : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Stephan _stephan;
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private FoodSpawner _foodSpawner;

    public Stage CurrentStage { get; private set; }
    public SearchForStephanStage SearchForStephanStage { get; private set; }
    public SearchForFoodStage SearchForFoodStage { get; private set; }
    
    public event Action StephanHidden;

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
        };
        
        _player.StephanFound += () =>
        {
            CurrentStage.OnExit();
            CurrentStage = SearchForFoodStage;
            CurrentStage.OnEnter();
        };

        _resourceManager.FoodTaken += (value) =>
        {
            CurrentStage.OnExit();
            CurrentStage.OnEnter();
        };
    }

    private void InitializeStages()
    {
        SearchForFoodStage = new SearchForFoodStage(_foodSpawner);
        SearchForStephanStage = new SearchForStephanStage(_foodSpawner);
    }

    private void SetInitialStage(Stage initialStage)
    {
        CurrentStage = initialStage;
        CurrentStage.OnEnter();
    }
}