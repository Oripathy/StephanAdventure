using System;
using System.Collections.Generic;
using Enemies.BioBot;
using Enemies.Kyle;
using Enemies.SpheroBot;
using StageController;
using UnityEngine;

namespace Enemies
{
    internal class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private DifficultySelector _difficultySelector;
        [SerializeField] private GameObject _kylePrefab;
        [SerializeField] private GameObject _bioBotPrefab;
        [SerializeField] private GameObject _spheroBotPrefab;
        [SerializeField] private List<Transform> _spawnPositions;
        [SerializeField] private List<Transform> _movePoints1;
        [SerializeField] private List<Transform> _movePoints2;
        [SerializeField] private List<Transform> _movePoints3;
        [SerializeField] private List<Transform> _movePoints4;
        [SerializeField] private List<Transform> _movePoints5;
        [SerializeField] private List<Transform> _movePoints6;
        [SerializeField] private List<Transform> _movePoints7;
        [SerializeField] private List<Transform> _movePoints8;
        [SerializeField] private List<Transform> _movePoints9;

        private Dictionary<Type, GameObject> _enemiesToTypeDictionary;
        private Queue<List<Transform>> _movePointsQueue;

        private void Awake()
        {
            _enemiesToTypeDictionary = new Dictionary<Type, GameObject>
            {
                {typeof(KyleEntity), _kylePrefab},
                {typeof(BioBotEntity), _bioBotPrefab},
                {typeof(SpheroBotEntity), _spheroBotPrefab}
            };
            
            _movePointsQueue = new Queue<List<Transform>>();
            _movePointsQueue.Enqueue(_movePoints1);
            _movePointsQueue.Enqueue(_movePoints2);
            _movePointsQueue.Enqueue(_movePoints3);
            _movePointsQueue.Enqueue(_movePoints4);
            _movePointsQueue.Enqueue(_movePoints5);
            _movePointsQueue.Enqueue(_movePoints6);
            _movePointsQueue.Enqueue(_movePoints7);
            _movePointsQueue.Enqueue(_movePoints8);
            _movePointsQueue.Enqueue(_movePoints9);
            
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            var enemyType = _difficultySelector.GetDifficultySetting();

            if (_enemiesToTypeDictionary.TryGetValue(enemyType, out var enemyPrefab))
            {
                foreach (var point in _spawnPositions)
                {
                    GameObject enemy;
                    enemy = Instantiate(enemyPrefab, point.position, Quaternion.identity);
                    if (enemy.TryGetComponent<IMovePointsSetter>(out var movePointsSetter))
                        movePointsSetter.SetMovePoints(_movePointsQueue.Dequeue());
                    else
                        throw new MissingComponentException();
                }
            }
            else
                throw new KeyNotFoundException();
        }
    }
}