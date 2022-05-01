namespace StageController
{
    internal abstract class Stage
    {
        private protected FoodSpawner _foodSpawner;
    
        public Stage(FoodSpawner foodSpawner)
        {
            _foodSpawner = foodSpawner;
        }
    
        public virtual void OnEnter()
        {
        
        }

        public virtual void OnExit()
        {
        
        }
    
    }
}