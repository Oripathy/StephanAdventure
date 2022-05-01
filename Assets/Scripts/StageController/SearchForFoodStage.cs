using UnityEngine;

namespace StageController
{
    internal class SearchForFoodStage : Stage
    {
        private Food _food;
    
        public SearchForFoodStage(FoodSpawner foodSpawner) : base(foodSpawner)
        {
        }

        public override void OnEnter()
        {
            if (Time.timeScale != 1f)
                Time.timeScale = 1f;
            
            base.OnEnter();
            _food = _foodSpawner.SpawnFood();
        }

        public override void OnExit()
        {
            if (_food != null)
                GameObject.Destroy(_food.gameObject);
        }
    }
}