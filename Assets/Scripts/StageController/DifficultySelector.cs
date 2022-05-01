using System;
using System.Collections.Generic;
using Enemies;
using Enemies.BioBot;
using UnityEngine;
using Enemies.Kyle;
using Enemies.SpheroBot;

namespace StageController
{
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }
    
    [CreateAssetMenu(menuName = "ScriptableObjects/DifficultySelector")]
    internal class DifficultySelector : ScriptableObject
    {
        [SerializeField] private Difficulty _chosenDifficulty;
        
        private Dictionary<Difficulty, Type> _enemyTypeToDifficultyDictionary =
            new Dictionary<Difficulty, Type>
            {
                {Difficulty.Easy, typeof(KyleEntity)},
                {Difficulty.Normal, typeof(BioBotEntity)},
                {Difficulty.Hard, typeof(SpheroBotEntity)}
            };

        public Type GetDifficultySetting()
        {
            if (_enemyTypeToDifficultyDictionary.TryGetValue(_chosenDifficulty, out var enemyType))
                return enemyType;
            else
                throw new KeyNotFoundException();
        }

        public void SetDifficulty(Difficulty difficulty) => _chosenDifficulty = difficulty;
    }
}