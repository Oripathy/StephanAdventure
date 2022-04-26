using UnityEngine;

namespace StageController
{
    internal class SearchForStephanStage : Stage
    {
        public SearchForStephanStage(FoodSpawner foodSpawner) : base(foodSpawner)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            if (Time.timeScale != 1f)
                Time.timeScale = 1f;
        }
    }
}