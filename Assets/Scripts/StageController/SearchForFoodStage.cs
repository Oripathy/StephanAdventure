using System.Collections;
using UnityEngine;

internal class SearchForFoodStage : Stage
{
    private Food _food;
    
    public SearchForFoodStage(FoodSpawner foodSpawner) : base(foodSpawner)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _food = _foodSpawner.SpawnFood();
    }

    public override void OnExit()
    {
        if (_food != null)
            GameObject.Destroy(_food.gameObject);
    }
}