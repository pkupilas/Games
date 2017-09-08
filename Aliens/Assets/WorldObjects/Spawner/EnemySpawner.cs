﻿using System.Collections;
using System.Collections.Generic;
using Characters.Enemies;
using UnityEngine;

namespace WorldObjects.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private PossibleEnemies _possibleEnemies;
        [SerializeField] private List<GameObject> _spawnPoints;
        [SerializeField] private float _spawnCooldown;
        [SerializeField] private int _enemiesPerWave = 1;

        private int _enemyCounter;
        private bool _isSpawning;
        private int _currentWave = 1;
        private bool _isBreak;
        private float _roundBreak = 10f;
        private float _roundBreakTimer;
        private float _roundBreakRemainingTime;
        private const int EnemiesGrowth = 10;
        
        public bool IsBreak => _isBreak;
        public int CurrentWave => _currentWave;
        public int RemainingEnemyCount => _enemiesPerWave - _enemyCounter;
        public int EnemiesPerWave => _enemiesPerWave;
        public float RoundBreakRemainingTime => _roundBreakRemainingTime;
        public int KilledInCurrentWave;

        void Update()
        {
            if (!_isBreak && !_isSpawning && _enemyCounter < _enemiesPerWave)
            {
                _isSpawning = true;
                StartCoroutine(SpawnEnemies());
            }

            if (_enemiesPerWave - KilledInCurrentWave == 0)
            {
                _isBreak = true;
            }

            if (_isBreak)
            {
                ManageRoundBreak();
            }
        }

        private void ManageRoundBreak()
        {
            _roundBreakTimer += Time.deltaTime;
            _roundBreakRemainingTime = _roundBreak - _roundBreakTimer;
            if (_roundBreakTimer >= _roundBreak)
            {
                _isBreak = false;
                _currentWave++;
                _enemyCounter = 0;
                _enemiesPerWave += EnemiesGrowth;
                KilledInCurrentWave = 0;
                _roundBreakTimer = 0;
            }
        }

        private IEnumerator SpawnEnemies()
        {
            yield return new WaitForSecondsRealtime(_spawnCooldown);

            var randomEnemy = GetRandomEnemy();
            var randomSpawnPoint = GetRandomSpawnPoint();
            Instantiate(randomEnemy, randomSpawnPoint.transform.position, Quaternion.identity);
            _isSpawning = false;
            _enemyCounter++;
        }

        private GameObject GetRandomEnemy()
        {
            int randomIndex = Random.Range(0, _possibleEnemies.EnemyList.Count);
            return _possibleEnemies.EnemyList[randomIndex].EnemyPrefab;
        }

        private GameObject GetRandomSpawnPoint()
        {
            int randomIndex = Random.Range(0, _spawnPoints.Count);
            return _spawnPoints[randomIndex];
        }
    }
}