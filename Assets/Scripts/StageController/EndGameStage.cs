using UnityEngine;

namespace StageController
{
    internal class EndGameStage : Stage
    {
        public EndGameStage(FoodSpawner foodSpawner) : base(foodSpawner)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Time.timeScale = 0f;
        }

        public override void OnExit()
        {
            base.OnExit();
            Time.timeScale = 1f;
        }
    }
}